using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_ADO.NET
{
    public partial class Form1 : Form
    {
        private NorthwindDB _northwindDB;
        public Form1()
        {
            InitializeComponent();

            try
            {
                _northwindDB = new NorthwindDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }


        }

        private void employeesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.northwindDataSet.Employees);
            // TODO: This line of code loads data into the 'northwindDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.northwindDataSet.Employees);

            cdCustomer.DataSource = _northwindDB.DataSet.Tables["Customers"];
            cdCustomer.DisplayMember = "CompanyName";
            cdCustomer.ValueMember = "CustomerID";

            dgvOrders.DataSource = _northwindDB.DataSet.Tables["Orders"];

            tbNumberOrder.DataBindings.Add
                ("Text", _northwindDB.DataSet.Tables["OrdersID"],
                "OrderID",true);
            dtpOrderDate.DataBindings.Add("Value", _northwindDB.DataSet.
                Tables["Orders"],"OrderDate", true);
            dtpOrderDate.Format = DateTimePickerFormat.Custom;
            dtpOrderDate.CustomFormat = "MM/dd/yyyy";

            tbEmployee.DataBindings.Add("Text", _northwindDB.DataSet.
                Tables["Employees"],"FullName",true);
            RefreshOrders();
        }

        private void RefreshOrders()
        {
           if(cdCustomer.SelectedIndex!=-1)
            {
                _northwindDB.DataSet.Tables["Orders"].Clear();
                //////////////////////////
                //здесь в команду AdapterOrders = 
                //new SqlDataAdapter("SELECT OrderID, OrderDate,
                //RequiredDate, ShippedDate FROM Orders WHERE 
                //CustomerID = @ID ORDER BY OrderDate;", 
                //ConnectString);
                //@ID это и будет как раз наш ID из cdCustomer.SelectedValue.ToString();

                _northwindDB.AdapterOrders.SelectCommand.Parameters[0].Value =
                    cdCustomer.SelectedValue.ToString();

                _northwindDB.AdapterOrders.Fill(_northwindDB.DataSet, "Orders");

            }
        }

        private void employeesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void cdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        private void dgvOrders_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = Convert.ToInt32(_northwindDB.DataSet.Tables["Orders"].
                    Rows[dgvOrders.CurrentCell.RowIndex]["OrderID"]);

                _northwindDB.DataSet.Tables["OrdersID"].Clear();
                _northwindDB.AdapterOrdersId.SelectCommand.Parameters[0].Value = orderId;
                _northwindDB.AdapterOrdersId.Fill(_northwindDB.DataSet, "OrdersID");

                _northwindDB.DataSet.Tables["Employees"].Clear();
                _northwindDB.AdapterEmployees.SelectCommand.Parameters[0].Value =
                    _northwindDB.DataSet.Tables["OrdersID"].Rows[0]["EmployeeID"];
                _northwindDB.AdapterEmployees.Fill(_northwindDB.DataSet, "Employees");

                Form2 form2 = new Form2(_northwindDB, orderId);
                form2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataView dataView = new DataView
            {
                Table = _northwindDB.DataSet.Tables["Customers"],
                //важно что бы фильтр, выглядел как в SQL
                RowFilter = "CustomerID='ANATR'"
            };
            dataGridView1.DataSource = dataView;
        }
    }
}
