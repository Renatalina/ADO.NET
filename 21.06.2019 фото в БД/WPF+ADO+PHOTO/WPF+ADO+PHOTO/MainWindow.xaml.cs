using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//вот здесь мы подключаем библиотеку для виндовс формс 
//а это нужно для того что бы залить нашу картинку
using SWF = System.Windows.Forms;

namespace WPF_ADO_PHOTO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //сюда все выгружается из базы данных 
        private DataSet _dataSet;

        private SqlDataAdapter _sqlDataAdapter;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _dataSet = new DataSet();
                //вот это команды для БД
                _sqlDataAdapter = new SqlDataAdapter

                    //для ConfigurationManager нужно зайти в рефенерсес, добавить Систем Конфигуратион, и тогда загорится лампочка
                    //и можно будет подключить библиотеку для ConfigurationManager
                    ("SELECT Id, Name, Photo FROM Photos;",ConfigurationManager.ConnectionStrings["Img"].ConnectionString);
                SqlCommandBuilder builder = new SqlCommandBuilder(_sqlDataAdapter);
                //там создали адаптер, что бы из базы данных все вылелось в наш ДатаСэт 
                //и в _dataSet создастся такая же таблица как в нашей базе данных 
                //и мы ее создали так же как и в нашей БД "Photos"
                _sqlDataAdapter.Fill(_dataSet, "Photos");



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                //здесь приложение закроется если что то не так пошло в приложении
                Application.Current.Shutdown();
            }

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //вот здесь открываем диалоговое окно, что бы можно было 
                //загружать фотографиии с флешки
                SWF.OpenFileDialog fileDialog =
                     new SWF.OpenFileDialog();
                if (fileDialog.ShowDialog() == SWF.DialogResult.OK)
                {

                    //записываем фотографию в байтах
                    byte[] imageBytes = null;

                    //вытаскиваем картинку из файла 
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    //считываем инфомарцию из потока, что бы потом записать
                    //информация это наша картинка
                    using (FileStream stream = fileInfo.Open(FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            imageBytes = reader.ReadBytes((int)fileInfo.Length);
                        }
                    }

                    //это конструкция что бы красиво прятать код
                    #region MyRegion
                    //вот здесь пишется код и потом все красиво 
                    //свернется 
                    #endregion

                    DataRow dataRow = _dataSet.Tables["Photos"].NewRow();

                    //здесь должен записаться массив со всеми именами картинок 
                    string str = System.IO.Path.GetFileNameWithoutExtension
                           (fileInfo.Name);
                    dataRow["Name"] = str;
                    dataRow["Photo"] = imageBytes;
                    _dataSet.Tables["Photos"].Rows.Add(dataRow);

                    _sqlDataAdapter.Update(_dataSet, "Photos");
                    MessageBox.Show("Create Ok!");
                }
                else
                {
                    throw new Exception("Error!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               DataRow dataRow=_dataSet.Tables["Photos"].Rows.Cast<DataRow>().
                    FirstOrDefault(r => r.Field<string>("Name") == "Penguins");
                if (dataRow != null)
                {
                    //вытаскиваем картинку здесь из строки 0 
                    //byte[] bytes = (byte[])_dataSet.Tables["Photos"].Rows[0]["Photo"];

                    byte[] bytes = dataRow.Field<byte[]>("Photo");

                    using (MemoryStream memoryStream = new MemoryStream(bytes))
                    {
                        var bitmapImage = new BitmapImage();
                        //начинаем собирать картинку по байтикам
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        //прекратили здесь собирать картинку по байтикам
                        bitmapImage.EndInit();
                        //и вот теперь она должна отобразится на форме
                        image.Source = bitmapImage;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

       
    }
}
