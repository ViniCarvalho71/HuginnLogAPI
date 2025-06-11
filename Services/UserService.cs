using System.ComponentModel.DataAnnotations;
using HuginnLogAPI.Models;
using HuginnLogAPI.Repository;

namespace HuginnLogAPI.Services
{
    public class UserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }
        
        public bool Cadastrar(User user, out List<MensagemErro> mensagens)
        {
            var valido = Validar(user, out mensagens);
            if (valido)
            {
                try
                {
                    using var transacao = repository.InitTransaction();
                    repository.Add(user);
                    repository.Commit();
                    return true;
                }
                catch (Exception)
                {
                    repository.Rollback();
                    return false;
                }
            }
            return false;
        }

        public static bool Validar(User user, out List<MensagemErro> mensagens)
        {
            var validationContext = new ValidationContext(user);
            var erros = new List<ValidationResult>();
            var validation = Validator.TryValidateObject(user,
                validationContext,
                erros,
                true);

            mensagens = new List<MensagemErro>();
            foreach (var erro in erros)
            {
                var mensagem = new MensagemErro(
                    erro.MemberNames.First(),
                    erro.ErrorMessage);

                mensagens.Add(mensagem);
                Console.WriteLine("{0}: {1}",
                    erro.MemberNames.First(),
                    erro.ErrorMessage);
            }
            

            //throw new Exception("dados invalidos!!!!");
            return validation;
        }

        public List<User> Consultar()
        {
            return repository.GetAll<User>().ToList();
        }

        public List<User> Consultar(string pesquisa)
        {
            bool FiltraLista(User item)
            {
                return item.Name.Contains(pesquisa);
            }
            // lambda expression - LINQ
            var resultado2 = repository
                .GetAll<User>()
                .Where(item => item.Name.Contains(pesquisa))
                .OrderBy(item => item.Name)
                .Take(10)
                .ToList();
            return resultado2;
        }

        public User ConsultarPorCodigo(long codigo)
        {
            return repository.GetById<User>(codigo);
        }

        public User Editar(User user)
        {
            var existente = ConsultarPorCodigo(user.Id);

            if (existente == null)
            {
                return null;
            }
            existente.Name = user.Name;
            existente.Username = user.Username;
            existente.Email = user.Email;
            

            try
            {
                using var transacao = repository.InitTransaction();
                repository.Save(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }

        public User Deletar(long codigo)
        {
            var existente = ConsultarPorCodigo(codigo);
            try
            {
                using var transacao = repository.InitTransaction();
                repository.Delete(existente);
                repository.Commit();
                return existente;
            }
            catch (Exception)
            {
                repository.Rollback();
                return null;
            }
        }
    }
}