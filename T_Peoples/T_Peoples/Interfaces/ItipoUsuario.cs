using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_Peoples.Domain;

namespace T_Peoples.Interfaces
{
    interface ItipoUsuario
    {
        List<TipoUsuarioDomain> Listar();

        void AdicionaDados(TipoUsuarioDomain tipo);

        void AtuaulizarIdCorpo(TipoUsuarioDomain tipo);

        void Deletar(int Id);

        TipoUsuarioDomain BuscarId(int Id);

    }
}
