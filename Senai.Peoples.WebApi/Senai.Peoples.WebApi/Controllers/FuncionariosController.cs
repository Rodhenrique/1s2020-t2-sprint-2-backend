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

        //LISTAR TODOS OS FUNCIONARIOS 
        [HttpGet]
        public IEnumerable<FuncionariosDomain> Get()
        {
            return _FuncionariosRepository.Listar();
        }

        //CADASTRAR UM NOVO FUNCIONARIO NO SISTEMA 
        [HttpPost]
        public IActionResult Post(FuncionariosDomain funcionarios)
        {

            if (funcionarios.Nome == "")
            {
                return NotFound("E necessario um nome para criar um usuario nenhum Funcionario Criado");
            }
            else if(funcionarios.Sobrenome == "")
            {
                return NotFound("E necessario um sobrenome para criar um usuario nenhum Funcionario Criado");
            }
            else if(funcionarios.DataNascimento == null)
            {
                return NotFound("E necessario um data de nascimento para criar um usuario nenhum Funcionario Criado");
            }
            else
            {
            _FuncionariosRepository.AdicionaDados(funcionarios);

            return StatusCode(201,new {mensagem = "Funcionario Criado"});
            }
        }

        //LISTAR TODOS OS FUNCIONARIOS  PELA ORDEM ALFABETICA DO A ATÉ O Z
        [HttpGet("Asc")]
        public IEnumerable<FuncionariosDomain> ListarPorAsc()
        {
            return _FuncionariosRepository.ListarPorAsc();
        }

        //BUSCAR FUNCIONARIO POR ID

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

        //BUSCAR FUNCIONARIO PELO NOME

        [HttpGet("BuscarNome/{Nome}")]
        public IActionResult BuscarPorNome(string Nome)
        {
            FuncionariosDomain funcionarios = _FuncionariosRepository.BuscarNome(Nome);

            if (funcionarios == null)
            {
                return NotFound("Nenhum funcionario encontrado");
            }

            return Ok(funcionarios);
        }

        //LISTAR PELO NOME E SOBRENOME
        [HttpGet("BuscarNomeCompleto")]
        public IEnumerable<FuncionariosDomain> BuscarNomeCompleto()
        {
            return _FuncionariosRepository.ListarNomeCompleto();
        }

        //ATUALIZAR AS INFORMAÇÕES DOS FUNCIONARIOS

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

        //DELETAR UM FUNCIONARIO PELO ID 

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