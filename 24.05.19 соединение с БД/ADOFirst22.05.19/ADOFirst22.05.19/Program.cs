using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOFirst22._05._19
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////////////////////////
            //////для работы здесь нужно удалить из файла App.config лишнее
            /* string connectionString = "Data Source=localhost;Initial " +
                 "Catalog=Students;Integrated Security=true;" +
                 " Connection Timeout=30";


      try
      {
          using (SqlConnection connection = new SqlConnection
                  (connectionString))
          {
              //создаем команду для БД
              SqlCommand sqlCommand = new SqlCommand
              {
                  CommandText = "SELECT * FROM Persons;",
                  Connection = connection
              };
              //это мы открыли соединение
              connection.Open();
              //строки уже поменялись
              //команда из строки 25 будет возвращаться в табличку вот эту 32 строки
              SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

              //вот здесь мы будем считывать каждую строку sqlDataReader.Read() из таблици
              while (sqlDataReader.Read())
              {
                  //sqlDataReader[] здесь обращаемся к каждому столбцу таблици
                  Console.WriteLine($"Last.Name: { sqlDataReader["LastName"]} " +
                      $"First name: {sqlDataReader[2]}");
              }
              sqlDataReader.Close();
          }
      }
      catch (Exception ex )
      {

          Console.WriteLine(ex.Message);
      }
      */

            /////////////////////////////////////////////////////////////////////////////////////
            /*
                        string connectionString = "Data Source=localhost;Initial " +
                            "Catalog=Students;Integrated Security=true;" +
                            " Connection Timeout=30";


                        //здесь указывает провайдер, (нужная база данных) и здесь мы пишем все команды
                        //это очень универсальный класс, если я потом поменяю sql на magenty я просто поменяю строку System.Data.SqlClient
                        DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");


                         try
                        {
                            using (DbConnection connection=dbProviderFactory.CreateConnection())
                            {
                                connection.ConnectionString = connectionString;
                                connection.Open();

                                DbCommand command = dbProviderFactory.CreateCommand();
                                command.CommandText = "SELECT * FROM Persons;";
                                command.Connection = connection;
                                using (DbDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                         Console.WriteLine($"Last.Name: { reader["LastName"]} \t" +
                                            $"First name: {reader[2]} \t GroupID: {reader["GroupId"]}");
                                    }
                                }        

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }          
                 */



            ///////////////////////////////////////////////////////////////////////////////////////

            //второй вариант, с App.config

            //сделали тоже самое, что и в верху, но тут мы написали все в App.config
            // < appSettings >
            //< add key = "provider" value = "System.Data.SqlClient" />

            // < add key = "connectStr" value = "Data Source=localhost;Initial Catalog=Students;
            // Integrated Security = true; Connection Timeout = 30; "/>
            // </ appSettings >
            //это все из App.config

            /*
            string connectionString = ConfigurationManager.AppSettings["connectStr"];
            string provider = ConfigurationManager.AppSettings["provider"];

            DbProviderFactory dbProviderFactory = 
                DbProviderFactories.GetFactory(provider);


            try
            {
                using (DbConnection connection=dbProviderFactory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    DbCommand command = dbProviderFactory.CreateCommand();
                    command.CommandText = "SELECT * FROM Persons;";
                    command.Connection = connection;
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             Console.WriteLine($"Last.Name: { reader["LastName"]} \t" +
                                $"First name: {reader[2]} \t GroupID: {reader["GroupId"]}");
                        }
                    }        

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
     */
            //////////////////////////////////////////////////////////////////////////
            //снова дописали в App.config
            /*
            string connectionString = ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString;
            string provider = ConfigurationManager.AppSettings["provider"];

            DbProviderFactory dbProviderFactory =
                DbProviderFactories.GetFactory(provider);


            try
            {
                using (DbConnection connection = dbProviderFactory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    DbCommand command = dbProviderFactory.CreateCommand();
                    command.CommandText = "SELECT * FROM Persons;";
                    command.Connection = connection;
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Last.Name: { reader["LastName"]} \t" +
                               $"First name: {reader[2]} \t GroupID: {reader["GroupId"]}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            */
            ////////////////////////////////////////////////////////////////////
            //здесь не используем конфигурационный файл
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "Students",
                IntegratedSecurity = true,
                ConnectTimeout=40,
            };

            string connectionString = builder.ConnectionString;
            string provider = ConfigurationManager.AppSettings["provider"];

            DbProviderFactory dbProviderFactory =
                DbProviderFactories.GetFactory(provider);


            try
            {
                using (DbConnection connection = dbProviderFactory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    DbCommand command = dbProviderFactory.CreateCommand();
                    command.CommandText = "SELECT * FROM Persons;";
                    command.Connection = connection;
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Last.Name: { reader["LastName"]} \t" +
                               $"First name: {reader[2]} \t GroupID: {reader["GroupId"]}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
