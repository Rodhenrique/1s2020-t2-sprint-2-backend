using Senai.Peoples.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarios
    {
       
        List<FuncionariosDomain> Listar();

        void AdicionaDados(FuncionariosDomain funcionarios);

        FuncionariosDomain BuscarId(int Id);
       
        void AtuaulizarIdCorpo(FuncionariosDomain funcionarios);
      
        void Deletar(int Id);

        FuncionariosDomain BuscarNome(string Nome);

        List<FuncionariosDomain> ListarNomeCompleto();

        List<FuncionariosDomain> ListarPorAsc();

    }
}
