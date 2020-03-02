using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using T_Peoples.Domain;
using T_Peoples.Interfaces;
using T_Peoples.Repositories;

namespace T_Peoples.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private Iusuario _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        //LISTAR TODOS OS FUNCIONARIOS 
        [HttpGet]
        public IEnumerable<UsuarioDomain> Get()
        {
            return _usuarioRepository.Listar();
        }
        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {

            UsuarioDomain usuarioBuscado = _usuarioRepository.LogarEmailSenha(usuario.Email, usuario.Senha);

            // Caso não encontre nenhum usuário com o e-mail e senha informados
            if (usuarioBuscado == null)
            {
                // Retorna NotFound com uma mensagem de erro
                return NotFound("E-mail ou senha inválidos");
            }

            // Caso o usuário seja encontrado, prossegue para a criação do token

            // Define os dados que serão fornecidos no token - Payload
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                new Claim("Claim personalizada", "Valor teste")
            };

            // Define a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Peoples-chave-autenticacao"));

            // Define as credenciais do token - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token
            var token = new JwtSecurityToken(
                issuer: "Peoples.WebApi",                // emissor do token
                audience: "Peoples.WebApi",              // destinatário do token
                claims: claims,                         // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),   // tempo de expiração
                signingCredentials: creds               // credenciais do token
            );

            // Retorna Ok com o token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        //LISTAR TODOS OS FUNCIONARIOS  PELA ORDEM ALFABETICA DO A ATÉ O Z
        [HttpGet("ordenar{ordem}")]
        public IEnumerable<UsuarioDomain> ListarPorAsc(string ordem)
        {
            return _usuarioRepository.ListarPorAsc(ordem);
        }

        //BUSCAR FUNCIONARIO POR ID

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuarioDomain funcionarios = _usuarioRepository.BuscarId(id);

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
           List<UsuarioDomain> usuarios = _usuarioRepository.BuscarNome(Nome);


            if (usuarios == null)
            {
                return NotFound("Nenhum funcionario encontrado");
            }

            return Ok(usuarios);
        }

        //LISTAR PELO NOME E SOBRENOME
        [HttpGet("BuscarNomeCompleto")]
        public IEnumerable<UsuarioDomain> BuscarNomeCompleto()
        {
            return _usuarioRepository.ListarNomeCompleto();
        }

        //ATUALIZAR AS INFORMAÇÕES DOS FUNCIONARIOS

        [HttpPut]
        public IActionResult PutIdCorpo(UsuarioDomain usuario)
        {
            UsuarioDomain filmeBuscado = _usuarioRepository.BuscarId(usuario.IdTipoUsuario);

            if (filmeBuscado != null)
            {
                try
                {
                    _usuarioRepository.AtuaulizarIdCorpo(usuario);

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
                        mensagem = "Usuario não encontrado",
                        erro = true
                    }
                );
        }

        //DELETAR UM FUNCIONARIO PELO ID 

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _usuarioRepository.Deletar(Id);

            if (Id == null)
            {
                return NotFound(new { mensagem = "Funcionario não encontrado" });

            }

            try
            {
                _usuarioRepository.Deletar(Id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}