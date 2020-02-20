using Senai.Peoples.WebApi.Domain;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncioariosRepository : IFuncionarios
    {
        private string StringConexao = "Data Source=DEV2\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public List<FuncionariosDomain> Listar()
        {
            List<FuncionariosDomain> Funcionario = new List<FuncionariosDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        FuncionariosDomain funcionariosDomain = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        Funcionario.Add(funcionariosDomain);
                    }
                }
                return Funcionario;
            }
        }

        public void AdicionaDados(FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios(Nome, Sobrenome) VALUES(@Nome, @SobreNome)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);

                    cmd.Parameters.AddWithValue("@SobreNome", funcionarios.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public FuncionariosDomain BuscarId(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryGetById = "SELECT * FROM Funcionarios WHERE IdFuncionario = @Id";


                using (SqlCommand cmd = new SqlCommand(queryGetById, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", Id);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionariosDomain funcionarios = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };
                        return funcionarios;
                    }
                    return null;

                }
            }
        }


        public void AtuaulizarIdCorpo(FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAtuaulizarIdCorpo = "UPDATE Funcionarios SET Nome = @Nome,Sobrenome = @Sobrenome Where IdFuncionario = @ID; ";


                using (SqlCommand cmd = new SqlCommand(queryAtuaulizarIdCorpo, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarios.Sobrenome);
                    cmd.Parameters.AddWithValue("@ID", funcionarios.IdFuncionario);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Deletar(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDeletar = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";


                using (SqlCommand cmd = new SqlCommand(queryDeletar, con))
                {

                    cmd.Parameters.AddWithValue("@ID", Id);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }    

    }
}
