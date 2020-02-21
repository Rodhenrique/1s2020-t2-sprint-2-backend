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
        //STRING PARA ACESSAR O SQL SERVER 
        private string StringConexao = "Data Source=DEV2\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        //LISTAR OS FUNCIONARIOS

        public List<FuncionariosDomain> Listar()
        {
            List<FuncionariosDomain> Funcionario = new List<FuncionariosDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        FuncionariosDomain funcionariosDomain = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3])
                        };

                        Funcionario.Add(funcionariosDomain);
                    }
                }
                return Funcionario;
            }
        }

        //ADICIONA UM NOVO FUNCIONARIO

        public void AdicionaDados(FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios(Nome,Sobrenome,DataNascimento) VALUES(@Nome,@SobreNome,@Data)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);

                    cmd.Parameters.AddWithValue("@SobreNome", funcionarios.Sobrenome);

                    cmd.Parameters.AddWithValue("@Data", funcionarios.DataNascimento);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //BUSCAR PELO ID

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
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3])
                        };
                        return funcionarios;
                    }
                    return null;

                }
            }
        }

        //ATUALIZAR PELO CORPO DO REQUEST

        public void AtuaulizarIdCorpo(FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAtuaulizarIdCorpo = "UPDATE Funcionarios SET Nome = @Nome,Sobrenome = @Sobrenome,DataNascimento = @Data Where IdFuncionario = @ID; ";


                using (SqlCommand cmd = new SqlCommand(queryAtuaulizarIdCorpo, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarios.Sobrenome);
                    cmd.Parameters.AddWithValue("@Data", funcionarios.DataNascimento);
                    cmd.Parameters.AddWithValue("@ID", funcionarios.IdFuncionario);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //DELETAR PELO ID

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

        //BUSCAR PELO NOME

        public FuncionariosDomain BuscarNome(string Nome)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryGetById = "SELECT * FROM Funcionarios WHERE Nome = @NomeFuncionario";


                using (SqlCommand cmd = new SqlCommand(queryGetById, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@NomeFuncionario", Nome);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionariosDomain funcionarios = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3])
                        };
                        return funcionarios;
                    }
                    return null;

                }
            }
        }

        //LISTAR NOME COMPLETO

        public List<FuncionariosDomain> ListarNomeCompleto()
        {
            List<FuncionariosDomain> Funcionario = new List<FuncionariosDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        FuncionariosDomain funcionariosDomain = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString() +' '+ rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3])
                        };

                        Funcionario.Add(funcionariosDomain);
                    }
                }
                return Funcionario;
            }
        }

        //LISTAR PELA ORDEM ALFABETICA

        public List<FuncionariosDomain> ListarPorAsc()
        {
            List<FuncionariosDomain> Funcionario = new List<FuncionariosDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "select * from Funcionarios ORDER BY Nome ASC";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        FuncionariosDomain funcionariosDomain = new FuncionariosDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr[3])
                        };

                        Funcionario.Add(funcionariosDomain);
                    }
                }
                return Funcionario;
            }
        }
    }
}
