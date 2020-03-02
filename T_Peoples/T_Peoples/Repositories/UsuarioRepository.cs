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
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Usuario(Nome,Sobrenome,DataNascimento,Email,Senha,IdTipoUsuario) VALUES(@Nome,@SobreNome,@Data,@email,@senha,@tipousuario)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);

                    cmd.Parameters.AddWithValue("@SobreNome", usuario.Sobrenome);

                    cmd.Parameters.AddWithValue("@Data", usuario.DataNascimento);

                    cmd.Parameters.AddWithValue("@email", usuario.Email);

                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);

                    cmd.Parameters.AddWithValue("@tipousuario", usuario.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void AtuaulizarIdCorpo(UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAtuaulizarIdCorpo = "UPDATE Usuario SET Nome = @Nome,Sobrenome = @Sobrenome,DataNascimento = @Data,Email = @email,Senha = @senha Where IdUsuario = @ID; ";


                using (SqlCommand cmd = new SqlCommand(queryAtuaulizarIdCorpo, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                    cmd.Parameters.AddWithValue("@Data", usuario.DataNascimento);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@ID", usuario.IdUsuario);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public UsuarioDomain BuscarId(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryGetById = "SELECT * FROM Usuario WHERE IdFuncionario = @Id";


                using (SqlCommand cmd = new SqlCommand(queryGetById, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", Id);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
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
                        return usuario;
                    }
                    return null;

                }
            }
        }

        public List<UsuarioDomain> BuscarNome(string Nome)
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectAll = "SELECT IdUsuario, CONCAT (Nome, ' ', Sobrenome), DataNascimento,Email,Senha,IdTipoUsuario FROM Usuario";
       
                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
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
            }
            return usuarios;
        }

        public void Deletar(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDeletar = "DELETE FROM Usuario WHERE IdUsuario = @ID";


                using (SqlCommand cmd = new SqlCommand(queryDeletar, con))
                {

                    cmd.Parameters.AddWithValue("@ID", Id);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
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
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Usuario";

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
                            Nome = rdr["Nome"].ToString() +' '+ rdr["Sobrenome"].ToString(),
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

        public List<UsuarioDomain> ListarPorAsc(string ordem)
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Usuario ORDER BY Nome @ordem";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    cmd.Parameters.AddWithValue("@ordem", ordem);


                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString() + ' ' + rdr["Sobrenome"].ToString(),
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

        public UsuarioDomain LogarEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Define a query a ser executada no banco
                string querySelect = "SELECT IdUsuario, Email, Senha, TipoUsuario.IdTipoUsuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.IdTipoUsuario = TipoUsuario.IdUsuario WHERE Email = @Email AND Senha = @Senha";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    // Define o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Abre a conexão com o banco
                    con.Open();

                    // Executa o comando e armazena os dados no objeto rdr
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Caso dados tenham sido obtidos
                    if (rdr.HasRows)
                    {
                        // Cria um objeto usuario
                        UsuarioDomain usuario = new UsuarioDomain();

                        // Enquanto estiver percorrendo as linhas
                        while (rdr.Read())
                        {
                            // Atribui à propriedade IdUsuario o valor da coluna IdUsuario
                            usuario.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);

                            // Atribui à propriedade Email o valor da coluna Email
                            usuario.Email = rdr["Email"].ToString();

                            // Atribui à propriedade Senha o valor da coluna Senha
                            usuario.Senha = rdr["Senha"].ToString();

                            // Atribui à propriedade Permissao o valor da coluna Permissao
                            usuario.IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]);
                        }

                        // Retorna o objeto usuario
                        return usuario;
                    }
                }

                // Caso não encontre um email e senha correspondente, retorna null
                return null;
            }
        }
    }
}
