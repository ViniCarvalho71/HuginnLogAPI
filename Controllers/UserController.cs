using HuginnLogAPI.Models;
using HuginnLogAPI.Repository;
using HuginnLogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HuginnLogAPI.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService service;

    public UserController(UserService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get(string? search)
    {
        return string.IsNullOrEmpty(search) ? 
            Ok(service.Consultar()) :
            Ok(service.Consultar(search));
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        if (service.Cadastrar(user, out List<MensagemErro> erros))
        {
            return Ok(user);
        }
        return UnprocessableEntity(erros);
    }

    [HttpPut]
    public IActionResult Put([FromBody] User user)
    {
        var resultado = service.Editar(user);
        if (resultado == null)
        {
            return NotFound();
        }
        return Ok(resultado);
    }

    [HttpDelete]

    public IActionResult Delete(long id)
    {
        var resultado = service.Deletar(id);
        if (resultado == null)
        {
            return NotFound();
        }
        return Ok(resultado);
    }
}