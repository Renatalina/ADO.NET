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
    public partial class Form2 : Form
    {
        private NorthwindDB _db;
        private int _orderId;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2 (NorthwindDB dB, int id):this()
        {
            _db = dB;
            _orderId = id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tbNumberOrder.DataBindings.Add("Text", _db.DataSet.Tables["OrdersID"], "OrderID", true);

            dtpOrderDate.DataBindings.Add("Value", _db.DataSet.Tables["OrdersID"], "OrderDate", true);
            dtpOrderDate.Format = DateTimePickerFormat.Custom;
            dtpOrderDate.CustomFormat = "MM/dd/yyyy";

            tbEmployee.DataBindings.Add("Text", _db.DataSet.Tables["Employees"], "FullName", true);

            dgvOrderDetails.DataSource = _db.DataSet.Tables["OrderDetails"];

            _db.DataSet.Tables["OrderDetails"].Clear();
            _db.AdapterOrderDetails.SelectCommand.Parameters[0].Value = _orderId;
            _db.AdapterOrderDetails.Fill(_db.DataSet, "OrderDetails");
        }
    }
}
