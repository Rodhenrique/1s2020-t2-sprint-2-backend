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
    }
}