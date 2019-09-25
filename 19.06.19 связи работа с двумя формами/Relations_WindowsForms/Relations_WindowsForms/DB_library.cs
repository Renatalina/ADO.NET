using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Relations_WindowsForms
{
    class DB_library
    {
        string strConnect;
        SqlConnection connection;
        public DataSet DS_library { get; set; }
        public SqlDataAdapter AdapterAuthors { get; private set; }
        public SqlDataAdapter AdapterAuthorsId { get; private set; }
        public SqlDataAdapter AdapterBooks { get; private set; }
        public SqlDataAdapter AdapterCategories { get; private set; }
        public SqlDataAdapter AdapterPress { get; private set; }

        public DB_library()
        {
            // получение строки подключения
            strConnect = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            DS_library = new DataSet();

            connection = new SqlConnection(strConnect);

            SqlCommand command = new SqlCommand("SELECT * FROM " +
                "Authors WHERE Id=@Id;", connection);
            command.Parameters.Add("@Id", SqlDbType.Int);

            // создание адаптеров
            AdapterAuthorsId = new SqlDataAdapter(command);

            AdapterAuthors = new SqlDataAdapter("SELECT * FROM Authors;", strConnect);
            AdapterBooks = new SqlDataAdapter("SELECT * FROM Books;", strConnect);
            AdapterCategories = new SqlDataAdapter("SELECT * FROM Categories;", strConnect);
            AdapterPress = new SqlDataAdapter("SELECT * FROM Press;", strConnect);
            
            // генерация команд и добавление команд в DataSet
            SqlCommandBuilder sqlCB = new SqlCommandBuilder(AdapterAuthors);

            AdapterAuthors.Fill(DS_library, "Authors");
            sqlCB = new SqlCommandBuilder(AdapterBooks);
            AdapterBooks.Fill(DS_library, "Books");
            sqlCB = new SqlCommandBuilder(AdapterCategories);
            AdapterCategories.Fill(DS_library, "Categories");

            // создание отношений между таблицами
            BuildTableRelationship();

            TablesRelations();
        }

        private void BuildTableRelationship()
        {
            // создание объекта отношения между данными CategoriesBooks
            DataRelation dr = new DataRelation("CategoriesBooks", DS_library.Tables["Categories"].Columns["Id"], DS_library.Tables["Books"].Columns["Id_Category"]);
            DS_library.Relations.Add(dr);

            // создание объекта отношения между данными AuthorsBooks
            dr = new DataRelation("AuthorsBooks", DS_library.Tables["Authors"].Columns["Id"], DS_library.Tables["Books"].Columns["Id_Author"]);
            DS_library.Relations.Add(dr);
        }

        // Вывод связанных таблиц. Вывод двух таблиц в один элемент DataGrid
        private void TablesRelations()
        {
            //В объекте DataSet здесь будут храниться две таблицы - главная и связанная с ней дочерняя. Поэтому воспользуемся свойством TableMappings объекта DataAdapter для занесения в него первой таблицы "Books":

            AdapterPress.TableMappings.Add("Table", "Press");
            AdapterPress.Fill(DS_library);

            AdapterBooks.TableMappings.Add("Table", "Books");
            AdapterBooks.Fill(DS_library);

            // создание объекта отношения между данными PressBooks
            DataRelation dr = new DataRelation("PressBooks", DS_library.Tables["Press"].Columns["Id"], DS_library.Tables["Books"].Columns["Id_Press"]);
            DS_library.Relations.Add(dr); 
        }
    }
}