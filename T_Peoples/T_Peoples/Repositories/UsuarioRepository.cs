using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using T_Peoples.Domain;
using T_Peoples.Interfaces;

namespace T_Peoples.Repositories
{
    public class UsuarioRepository : Iusuario
    {
        //STRING PARA ACESSAR O SQL SERVER 
        private string StringConexao = "Data Source=DEV2\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public void AdicionaDados(UsuarioDomain usuario)
        {
            throw new NotImplementedException();
        }

        public void AtuaulizarIdCorpo(UsuarioDomain usuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarId(int Id)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarNome(string Nome)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int Id)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioDomain> Listar()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdUsuario,Nome,Sobrenome,DataNascimento,Email,Senha,TipoUsuario.IdTipoUsuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3]),
                            Email = rdr["Email"].ToString(),
                            Senha = rdr["Senha"].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[6])
                        };

                        usuarios.Add(usuario);
                    }
                }
                return usuarios;
            }
        }

        public List<UsuarioDomain> ListarNomeCompleto()
        {
            throw new NotImplementedException();
        }

        public List<UsuarioDomain> ListarPorAsc()
        {
            throw new NotImplementedException();
        }
    }
}
