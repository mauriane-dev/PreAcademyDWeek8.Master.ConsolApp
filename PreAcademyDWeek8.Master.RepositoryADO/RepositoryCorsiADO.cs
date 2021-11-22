using PreAcademyDWeek8.Master.Core.Entities;
using PreAcademyDWeek8.Master.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreAcademyDWeek8.Master.RepositoryADO
{
    public class RepositoryCorsiADO : IRepositoryCorsi
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CorsiMaster;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Corso Add(Corso item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "insert into Corso values(@codice, @nome, @descrizione)";
                    command.Parameters.AddWithValue("@nome", item.Nome);
                    command.Parameters.AddWithValue("@descrizione", item.Descrizione);
                    command.Parameters.AddWithValue("@codice", item.CorsoCodice);


                    int numRighe = command.ExecuteNonQuery();
                    if (numRighe == 1)
                    {
                        connection.Close();
                        return item;
                    }
                    connection.Close();
                    return item;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Delete(Corso item)
        {
            throw new NotImplementedException();
        }

        public List<Corso> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from Corso";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Corso> corsi = new List<Corso>();

                    while (reader.Read())
                    {
                        var codice = (string)reader["CorsoCodice"];
                        var nome = (string)reader["Nome"];
                        var descrizione = (string)reader["Descrizione"];
                        Corso c = new Corso();
                        c.CorsoCodice = codice;
                        c.Nome = nome;
                        c.Descrizione = descrizione;
                        corsi.Add(c);
                    }
                    connection.Close();

                    return corsi;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Corso>();
            }
        }

        public Corso GetByCode(string codice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from Corso where CorsoCodice=@Codice";
                    command.Parameters.AddWithValue("@Codice", codice);
                    SqlDataReader reader = command.ExecuteReader();
                    Corso c = new Corso();
                    while (reader.Read())
                    {
                        //var codice = (string)reader["CorsoCodice"];
                        var nome = (string)reader["Nome"];
                        var descrizione = (string)reader["Descrizione"];

                        c.CorsoCodice = codice;
                        c.Nome = nome;
                        c.Descrizione = descrizione;
                    }
                    connection.Close();
                    return c;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Corso();
            }
        }

        public Corso Update(Corso item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update Corso set Nome=@nome, Descrizione=@descr where CorsoCodice=@codice";
                    command.Parameters.AddWithValue("@codice", item.CorsoCodice);
                    command.Parameters.AddWithValue("@nome", item.Nome);
                    command.Parameters.AddWithValue("@descr", item.Descrizione);

                    int rigaInserita = command.ExecuteNonQuery();
                    if (rigaInserita == 1)
                    {
                        connection.Close();
                        return item;
                        
                    }
                    else
                    {
                        connection.Close();
                        return null;
                        
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Corso();
            }
        }
    }
}
