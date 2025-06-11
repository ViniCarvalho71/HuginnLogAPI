using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;

namespace HuginnLogAPI.Repository.Implementations;

public class RepositoryNHibernate : IRepository
{
    private readonly ISessionFactory sessionFactory;
    private readonly ISession session;
    
    public RepositoryNHibernate(ISessionFactory sessionFactory)
    {
        this.sessionFactory = sessionFactory;
        // conecta no banco de dados
        this.session = sessionFactory.OpenSession();
    }

    public void Add(object model)
    {
        session.Save(model);
    }

    public void Save(object model)
    {
        session.Merge(model);
    }

    public void Delete(object model)
    {
        session.Delete(model);
    }

    public T GetById<T>(object id)
    {
        return session.Get<T>(id);
    }

    public IQueryable<T> GetAll<T>()
    {
        return session.Query<T>();
    }

    public IDisposable InitTransaction()
    {
        var transaction = session.BeginTransaction();
        return transaction;
    }

    public void Commit()
    {
        session.GetCurrentTransaction().Commit();
    }

    public void Rollback()
    {
        session.GetCurrentTransaction()?.Rollback();
    }
}