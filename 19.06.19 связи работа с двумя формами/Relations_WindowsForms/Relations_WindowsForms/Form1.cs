using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Relations_WindowsForms
{
    public partial class Form1 : Form
    {
        DB_library db;

        public Form1()
        {
            InitializeComponent();
            /*
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandText = "Select * From Press";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = myCommand;
            conn.Open();

            DataSet ds = new DataSet();
            dataAdapter.TableMappings.Add("Table", "Press");
            dataAdapter.Fill(ds);

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.Connection = conn;
            myCommand2.CommandText = "Select * From Books";
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter();
            dataAdapter2.SelectCommand = myCommand2;
            dataAdapter2.TableMappings.Add("Table", "Books");
            dataAdapter2.Fill(ds);

            DataColumn dcTouristsID = ds.Tables["Press"].Columns["Id"];
            DataColumn dcInfoTouristsID = ds.Tables["Books"].Columns["Id_Press"];
            DataRelation dataRelation =
             new DataRelation("Дополнительная информация", dcTouristsID, dcInfoTouristsID);
            ds.Relations.Add(dataRelation);
            DataViewManager dsview = ds.DefaultViewManager;
            dataGridViewBooks.DataSource = dsview;
            dataGridViewBooks.DataMember = "Press";
            conn.Close();
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            db = new DB_library();

            // привязка к графическим элементам
            dataGridViewAuthors.DataSource = db.DS_library.Tables["Authors"];
            dataGridViewBooks.DataSource = db.DS_library.Tables["Books"].DefaultView;
            dataGridViewCategories.DataSource = db.DS_library.Tables["Categories"];

            comboBox1.DataSource = db.DS_library.Tables["Authors"];
            comboBox1.DisplayMember = "FirstName";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = 0;

            //db.AdapterAuthorsId.SelectCommand.Parameters[0].Value = comboBox1.SelectedValue;
            //db.AdapterAuthorsId.Fill(db.DS_library, "AuthorsId");
            //dataGridViewAuthors.DataSource = db.DS_library.Tables["AuthorsId"];

            /*
            //Создаем объект DataViewManager, отвечающий за отображение DataSet в объекте DataGrid:
            DataViewManager dsview = db.DS_library.DefaultViewManager;
            //Присваиваем свойству DataSource объекта DataGrid созданный объект DataViewManager:
            dataGridViewBooks.DataSource = dsview;
            //Последнее, что нам осталось сделать, - сообщить объекту DataGrid, какую таблицу считать главной (родительской) и, соответственно, отображать на форме:
            dataGridViewBooks.DataMember = "Press";
            */
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            string strAuthorInfo = string.Empty; // информация об авторе
            DataRow[] drAuth = null; // авторы
            DataRow[] drBook = null; // книги автора
            try
            {
                // Получение идентификатора клиента из текстового поля. 
                int authorID = Convert.ToInt32(tbID.Text);
                
                // Поиск строки в таблице Authors для введенного идентификатора 
                drAuth = db.DS_library.Tables["Authors"].Select(string.Format("ID = {0}", authorID));
                if (drAuth.Length > 0) // автор не найден
                {
                    strAuthorInfo += string.Format("Автор : {0} {1}\n",
                        //drAuth[0]["ID"].ToString(),{0}
                    drAuth[0]["FirstName"].ToString(),
                    drAuth[0]["LastName"].ToString());
                    // Переход из таблицы Authors к таблице Books
                    drBook = drAuth[0].GetChildRows(db.DS_library.Relations["AuthorsBooks"]);

                    DataRow[] drsCat;
                    // Перебор всех книг данного автора
                    foreach (DataRow dr in drBook)
                    {
                        // Получение названия книги
                        strAuthorInfo += string.Format("Название книги: {0}\n", dr["Name"]);

                        //А теперь переход из таблицы Books к таблице Categories 
                        // Выборка категории, соответствующей полученной книге 
                        drsCat = dr.GetParentRows("CategoriesBooks"); // название отношения

                        // Получение информации для этой (ОДНОЙ) категории 
                        strAuthorInfo += string.Format("Категория: {0}\n", drsCat[0]["Name"]); // название
                    }

                    MessageBox.Show(strAuthorInfo, "Информация о книгах"); // Информация о книгах автора
                }
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Автор не найден", "Ошибка"); // Информация о книгах автора		
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                db.AdapterAuthors.Update(db.DS_library, "Authors");
                db.AdapterBooks.Update(db.DS_library, "Books");
                db.AdapterCategories.Update(db.DS_library, "Categories");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}