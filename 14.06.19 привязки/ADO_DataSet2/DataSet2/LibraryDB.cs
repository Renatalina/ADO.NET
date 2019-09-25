using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet2
{
    class LibraryDB
    {
        public string ConnectionString { get; }

        public DataSet DataSet { get; set; }

        //формирует таблици для Дата Сэт
        private SqlDataAdapter _adapterAuthors;

        private SqlDataAdapter _adapterBooks;
        public LibraryDB()
        {
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["connectStr"].ConnectionString;

                DataSet = new DataSet();

                _adapterAuthors = new SqlDataAdapter("SELECT Id, FirstName, LastName FROM Authors;", ConnectionString);

                _adapterBooks = new SqlDataAdapter("SELECT * FROM Books;",
                    ConnectionString);

                //создавать _adapterAuthors адаптер под каждую таблиуц БД
                SqlCommandBuilder builder = new SqlCommandBuilder(_adapterAuthors);
                                

                //это мы создали таблицу и назвали ее "Authors"
                _adapterAuthors.Fill(DataSet, "Authors");
                builder = new SqlCommandBuilder(_adapterBooks);
                _adapterBooks.Fill(DataSet, "Books");

                //это отношение, и название его должно быть уникальное! не повторять!
                DataRelation dataRelation = new DataRelation
                    ("AuthBook", DataSet.Tables["Authors"].Columns["Id"],
                    DataSet.Tables["Books"].Columns["Id_Author"]
                    );
                //здесь мы добавили это отношение в таблицу отношений
                DataSet.Relations.Add(dataRelation);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintRelation (string parent, string child, string relName)
        {
            foreach (DataRow row in DataSet.Tables[parent].Rows)
            {
                Console.WriteLine($"\t\t{row["LastName"]}");
                foreach(DataRow item in row.GetChildRows(relName))
                {
                    for(int i=0; i<DataSet.Tables[child].Columns.Count;i++)
                    {
                        Console.WriteLine($"{item[i]}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public void PrintDataSet()
        {
            foreach (DataTable table in DataSet.Tables)
            {
                Console.WriteLine($"\tTable name: {table.TableName}");

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Console.Write($"{table.Columns[i].Caption,-10}\t");
                }
                Console.WriteLine("\n-------------------------------------------\n");
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item,-10}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public void AuthorAdd(string fName, string lName)
        {
            try
            {
                int lastId = (int)DataSet.Tables["Authors"].Rows.Cast<DataRow>().Max(r => r["Id"]);

                //создаем строку для таблице
                DataRow dataRow = DataSet.Tables["Authors"].NewRow();
                dataRow["Id"] = lastId + 1;
                dataRow["FirstName"] = fName;
                dataRow["LastName"] = lName;

                //добавляем эту строку в таблицу
                DataSet.Tables["Authors"].Rows.Add(dataRow);

                //и теперь обновляем таблицу с новой строкой
                _adapterAuthors.Update(new DataRow[] { dataRow });

                Console.WriteLine("Insert OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AuthorDelete(string fName, string lName)
        {
            try
            {
                DataRow row = DataSet.Tables["Authors"].Rows.Cast<DataRow>().FirstOrDefault(r =>
                r.Field<string>("FirstName") == fName && r.Field<string>("LastName") == lName);

                if (row != null)
                {
                    row.Delete();

                    _adapterAuthors.Update(DataSet.Tables["Authors"]);

                    Console.WriteLine("Delete OK!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AuthorUpdate(string oldName, string newName)
        {
            try
            {
                DataRow row = DataSet.Tables["Authors"].Rows.Cast<DataRow>().FirstOrDefault(r => r.Field<string>("LastName") == oldName);

                if (row != null)
                {
                    row["LastName"] = newName;

                    _adapterAuthors.Update(DataSet.Tables["Authors"]);

                    Console.WriteLine("Update OK!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

