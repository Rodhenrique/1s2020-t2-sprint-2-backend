using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_Peoples.Domain;

namespace T_Peoples.Interfaces
{
    interface Iusuario
    {
            List<UsuarioDomain> Listar();

            void AdicionaDados(UsuarioDomain usuario);

            UsuarioDomain BuscarId(int Id);

            void AtuaulizarIdCorpo(UsuarioDomain usuario);

            void Deletar(int Id);

            List<UsuarioDomain> BuscarNome(string Nome);

            List<UsuarioDomain> ListarNomeCompleto();

            List<UsuarioDomain> ListarPorAsc(string ordem);

            UsuarioDomain LogarEmailSenha(string email,string senha);
    }
}