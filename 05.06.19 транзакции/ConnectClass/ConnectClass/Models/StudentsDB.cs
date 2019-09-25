using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private SqlConnection _connection;
        public string ConnectionString { get; }
        public StudentsDB()
        {
          ConnectionString= ConfigurationManager.ConnectionStrings["connectStr"].
                ConnectionString;
            _connection = new SqlConnection(ConnectionString);
        }
        public void SelectDB(string text)
        {            
                _connection.Open();
                SqlCommand command = new SqlCommand(text, _connection);
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
                _connection.Close();            
        }

        public void PersonInsert(Person person)
        {
            // string text = $"INSERT INTO Persons VALUES('{person.LastName}','{person.FirstName}'," +
            //    $"'{person.Birthday.ToString("yyyy-MM-dd")}',{person.GroupId});";
            //NonQueryDB(text);

            
        string text = $"INSERT INTO Persons VALUES" +
                $"(@LastName,@FirstName,@Birthday, @GroupId);";
            using (SqlCommand command = new SqlCommand(text, _connection))
            {
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size=50,
                    Value = person.LastName
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = person.FirstName
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@Birthday",
                    SqlDbType = SqlDbType.Date,                 
                    Value = person.Birthday
                };
                command.Parameters.Add(parameter);
                parameter = new SqlParameter
                {
                    ParameterName = "@GroupId",
                    SqlDbType = SqlDbType.Int,
                    Value = person.GroupId
                };
                command.Parameters.Add(parameter);

                IfMethod(command);
            }            
            
        }


        //без этого метода не запустится команда для БД, это как перегрузить + 
        private void NonQueryDB(string text)
        {
            
                
                SqlCommand command = new SqlCommand(text, _connection);
            //именно этот метод запускает команду ExecuteNonQuery();
            IfMethod(command);
            
            
        }


        public int RecordCount(string table)
        {
            string text = $"SELECT COUNT(*) FROM {table};";
            
                _connection.Open();
                SqlCommand command = new SqlCommand(text, _connection);
            _connection.Close();
            return Convert.ToInt32(command.ExecuteScalar());
            
            
        }

        public void PersonDelete(int id)
        {
            string text = $"DELETE FROM Persons WHERE Id={id};";

            NonQueryDB(text); 
        }
        public void PersonUpdate(int id, string name)
        {

            
            //просто по новому написали команду
            //"NameUpdate" это имя хранимой процедуры
            SqlCommand command = new SqlCommand
            {
                CommandText = "NameUpdate",
                Connection = _connection,
                CommandType = CommandType.StoredProcedure
            };
            //взяли данные из хранимой процедуры
            SqlParameter parameter = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Value = id
            };
            //добавляемя в команду нашей процедуры 
            command.Parameters.Add(parameter);
            parameter = new SqlParameter
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.NVarChar,
                Value = name
            };
            command.Parameters.Add(parameter);

            //здесь мы запустили команду
            IfMethod(command);

           

        }

        private void IfMethod(SqlCommand command)
        {
            
            _connection.Open();
            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("Command completed successfuly");
            }
            else
            {
                Console.WriteLine("Error");
            }
            _connection.Close();
        }

        public void RisksTransaction(int id)
        {
            Person person = new Person();
            SqlCommand command = new SqlCommand($"SELECT * FROM Persons WHERE Id={id};",
                _connection);
            _connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    //вычитывает нашего необходимого человека в буфера, точнее наш класс
                    person.Id = Convert.ToInt32(reader["Id"]);
                    person.LastName = (reader["LastName"]).ToString();
                    person.FirstName = (reader["FirstName"]).ToString();
                    person.Birthday = Convert.ToDateTime(reader["Birthday"]);
                    person.GroupId = Convert.ToInt32(reader["GroupId"]);
                }
                else
                {
                   throw new Exception ("Non row!");
                }
            }
            //этой командой вы удаляем одного человека из таблици ...
            SqlCommand delete = new SqlCommand($"DELETE FROM Persons WHERE Id={id};",
                _connection);
            //..а здесь мы этого человека добавляем в новую таблицу Рисковых
            SqlCommand insert = new SqlCommand($"INSERT INTO Risks " +
                $"VALUES ({person.Id}," +
                $"'{person.LastName}','{person.FirstName}'," +
                $"'{person.Birthday.ToString("yyyy-MM-dd")}'," +
                $"{person.GroupId});", _connection);
            SqlTransaction transaction = null;
            try
            {
                //если транзакция успешна , то она выполняется
                transaction = _connection.BeginTransaction("RisksTran");
                delete.Transaction = transaction;
                insert.Transaction = transaction;
                delete.ExecuteNonQuery();
                insert.ExecuteNonQuery();
                transaction.Commit();

                Console.WriteLine("Transaction OK!");
            }
            catch (Exception ex)
            {
                //если что то пошло не так, то мы откатываем транзакцию
                transaction.Rollback("RisksTran");

                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
