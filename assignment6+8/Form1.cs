using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using homework5;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace homework6
{
    public partial class Form1 : Form
    {
        static OrderService orderService = new OrderService();

        public Form1()
        {
            InitializeComponent();
            bdsOrder.DataSource = orderService.orders;
            
        }
       


        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedValue.ToString();
            if (selectedValue.Equals("OrderID"))
                bdsOrder.DataSource = orderService.Search(o => o.OrderID.ToString() == textBox1.Text || textBox1.Text == "");

            else if (selectedValue.Equals("CustomerName"))
                bdsOrder.DataSource = orderService.Search(o => o.Customer.Name == textBox1.Text || textBox1.Text == "");

            else if (selectedValue.Equals("Amount(more than)"))
                bdsOrder.DataSource = orderService.Search(o => o.TotalSum >= int.Parse(textBox1.Text) || textBox1.Text == "");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddAndChange(orderService).ShowDialog();
        }

        private void Orderview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}
