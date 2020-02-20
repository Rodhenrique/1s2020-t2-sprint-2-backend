using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domain;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarios _FuncionariosRepository { get; set; }

        public FuncionariosController()
        {
             _FuncionariosRepository = new FuncioariosRepository();
        }


        [HttpGet]
        public IEnumerable<FuncionariosDomain> Get()
        {
            return _FuncionariosRepository.Listar();
        }

        [HttpPost]
        public IActionResult Post(FuncionariosDomain funcionarios)
        {
            _FuncionariosRepository.AdicionaDados(funcionarios);

            if (funcionarios == null)
            {
                return NotFound("Nenhum funcionario Criado");
            }

            return StatusCode(201,new {mensagem = "Funcionario Criado"});

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FuncionariosDomain funcionarios = _FuncionariosRepository.BuscarId(id);

            if (funcionarios == null)
            {
                return NotFound("Nenhum funcionario encontrado");
            }

            return Ok(funcionarios);
        }


        [HttpPut]
        public IActionResult PutIdCorpo(FuncionariosDomain funcionarios)
        {
            FuncionariosDomain filmeBuscado = _FuncionariosRepository.BuscarId(funcionarios.IdFuncionario);

            if (filmeBuscado != null)
            {
                try
                {
                    _FuncionariosRepository.AtuaulizarIdCorpo(funcionarios);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }

            }


            return NotFound
                (
                    new
                    {
                        mensagem = "Funcionario não encontrado",
                        erro = true
                    }
                );
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _FuncionariosRepository.Deletar(Id);

            if (Id == null)
            {
                return NotFound(new {mensagem = "Funcionario não encontrado"});
        
            }

            try
            {
                _FuncionariosRepository.Deletar(Id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}