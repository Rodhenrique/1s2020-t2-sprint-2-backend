using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using T_Peoples.Domain;
using T_Peoples.Interfaces;

namespace T_Peoples.Repositories
{
    public class TipoUsuarioRepository : ItipoUsuario
    {
        private string StringConexao = "Data Source=DEV2\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public void AdicionaDados(TipoUsuarioDomain tipo)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO TipoUsuario(Titulo) VALUES(@titulo)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@titulo", tipo.Titulo);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void AtuaulizarIdCorpo(TipoUsuarioDomain tipo)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAtuaulizarIdCorpo = "UPDATE TipoUsuario SET Titulo = @titulo Where IdTipoUsuario = @ID; ";


                using (SqlCommand cmd = new SqlCommand(queryAtuaulizarIdCorpo, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@ID", tipo.IdTipoUsuario);

                    cmd.Parameters.AddWithValue("@titulo", tipo.Titulo);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public TipoUsuarioDomain BuscarId(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryGetById = "SELECT * FROM TipoUsuario WHERE IdTipoUsuario = @Id";


                using (SqlCommand cmd = new SqlCommand(queryGetById, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", Id);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TipoUsuarioDomain tipo = new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[0]),
                            Titulo = rdr["Titulo"].ToString()
                        };
                        
                        return tipo;
                    }
                    return null;

                }
            }
        }

        public void Deletar(int Id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDeletar = "DELETE FROM TipoUsuario WHERE IdTipoUsuario = @ID";


                using (SqlCommand cmd = new SqlCommand(queryDeletar, con))
                {

                    cmd.Parameters.AddWithValue("@ID", Id);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public List<TipoUsuarioDomain> Listar()
        {
            List<TipoUsuarioDomain> tipos = new List<TipoUsuarioDomain>();

            //CONECTAR NO SQL 
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdTipoUsuario,Titulo FROM TipoUsuario";

                con.Open();

                SqlDataReader rdr;

                //EXECUTAR O COMANDOS E QUERY 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        TipoUsuarioDomain tipo = new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr[0]),
                            Titulo = rdr["Titulo"].ToString()
                        };

                        tipos.Add(tipo);
                    }
                }
                return tipos;
            }
        }
    }
}
