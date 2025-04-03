using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using homework5;
namespace homework6
{
    public partial class Form1 : Form
    {
        static OrderService orderService = new OrderService();
        public Form1()
        {
            InitializeComponent();
            
            Customer c1 = new Customer("雷军", "001");
            List<OrderDetails> Details = new List<OrderDetails> { new OrderDetails(new Product("xiaomiSU7","001",200000),1)};
            orderService.AddOrder("001", c1, Details);
            Service.DataSource = orderService.orders;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Service.DataSource = orderService.orders.Where(s=>
            s.Customer.Name == textBox1.Text || textBox1.Text=="");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddAndChange(new Order(),ref orderService).ShowDialog();
        }
    }


}
