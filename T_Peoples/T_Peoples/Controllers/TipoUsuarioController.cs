using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T_Peoples.Domain;
using T_Peoples.Interfaces;
using T_Peoples.Repositories;

namespace T_Peoples.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ItipoUsuario _TipoUsuarioRepository { get; set; }

        public TipoUsuarioController()
        {
            _TipoUsuarioRepository = new TipoUsuarioRepository();
        }
        //LISTAR TODOS OS FUNCIONARIOS 
        [HttpGet]
        public IEnumerable<TipoUsuarioDomain> Get()
        {
            return _TipoUsuarioRepository.Listar();
        }

        public IActionResult GetById(int id)
        {
            TipoUsuarioDomain tipo = _TipoUsuarioRepository.BuscarId(id);

            if (tipo == null)
            {
                return NotFound("Nenhum funcionario encontrado");
            }

            return Ok(tipo);
        }

        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain tipo)
        {
            if (tipo.Titulo == "")
            {
                return NotFound("E necessario um nome para criar um TipoUsuario");
            }
            else 
            {
            _TipoUsuarioRepository.AdicionaDados(tipo);
                return StatusCode(201, new { mensagem = "Funcionario Criado" });
            }
            
        }

        [HttpPut]
        public IActionResult PutIdCorpo(TipoUsuarioDomain tipo)
        {
            TipoUsuarioDomain UsuarioBuscado = _TipoUsuarioRepository.BuscarId(tipo.IdTipoUsuario);

            if (UsuarioBuscado != null)
            {
                try
                {
                    _TipoUsuarioRepository.AtuaulizarIdCorpo(tipo);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }

            }


            return NotFound
                (new{mensagem = "Usuario não encontrado",erro = true});
        }

        //DELETAR UM FUNCIONARIO PELO ID 

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _TipoUsuarioRepository.Deletar(Id);

            if (Id == null)
            {
                return NotFound(new { mensagem = "Usuario não encontrado" });

            }

            try
            {
                _TipoUsuarioRepository.Deletar(Id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}