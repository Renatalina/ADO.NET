using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                _sqlDataAdapter = new SqlDataAdapter(
                    "SELECT Id, Name, Photo FROM Photos;",
                    ConfigurationManager.ConnectionStrings["Img"].ConnectionString
                    );
                SqlCommandBuilder builder = new SqlCommandBuilder(_sqlDataAdapter);
                _sqlDataAdapter.Fill(_dataSet, "Photos");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void download_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
