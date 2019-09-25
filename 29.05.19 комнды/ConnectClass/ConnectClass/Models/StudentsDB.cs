using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectClass.Models
{
    class StudentsDB
    {
        /*
         //это ленивая загрузка
       private string _connectionString;
        public string ConnectionString
        {
            get
            {
                _connectionString= _connectionString ?? ConfigurationManager.
                    ConnectionStrings["connectStr"].ConnectionString;
                return _connectionString;


               //if(_connectionString==null)
               // {
               //     _connectionString= ConfigurationManager.
               //       ConnectionStrings["connectStr"].ConnectionString;
               // }
               // return _connectionString;
            }
        }
        */
        public string ConnectionString { get; }
        public StudentsDB()
        {
          ConnectionString= ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString;
        }
        public void SelectDB(string text)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(text, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            for(int i=0;i<reader.FieldCount; i++)
                            {
                                Console.WriteLine($"{reader.GetName(i)}\t" +
                                    $"{reader.GetValue(i)}\t");
                            }
                        }
                    } while (reader.NextResult());
                }

            }
        }

        public void PersonInsert(Person person)
        {
            string text = $"INSERT INTO Persons VALUES('{person.LastName}','{person.FirstName}'," +
                $"'{person.Birthday.ToString("yyyy-MM-dd")}',{person.GroupId});";

            NonQueryDB(text);
        }


        //без этого метода не запустится команда для БД, это как перегрузить + 
        private void NonQueryDB(string text)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(text, connection);
                //именно этот метод запускает команду ExecuteNonQuery();
                if (command.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Command completed successfuly");
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }


        public int RecordCount(string table)
        {
            string text = $"SELECT COUNT(*) FROM {table};";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(text, connection);
                 return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void PersonDelete(int id)
        {
            string text = $"DELETE FROM Persons WHERE Id={id};";

            NonQueryDB(text);
        }

    }
}
