﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T_Peoples.Domain
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public int IdTipoUsuario { get; set; }
    }
}
