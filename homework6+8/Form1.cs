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
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //  bdsOrder.DataSource = orderService.orders.Where(s=>
            // s.Customer.Name == textBox1.Text || textBox1.Text=="");
            bdsOrder.DataSource = orderService.Search(o => o.Customer.Name == textBox1.Text || textBox1.Text == "");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddAndChange(orderService).ShowDialog();
        }

        private void Orderview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}
