using homework5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6
{
    public partial class Change : Form
    {
        OrderService orderService;
        Order order;
        public Change(OrderService Service, Order order)
        {   InitializeComponent();
            this.orderService = Service;
            this.order = order;

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order();
            if(textBox1.Text.Length >0) newOrder.Customer = new Customer(textBox1 .Text );
            newOrder .OrderID = order.OrderID;
            orderService.ChangeOrder (newOrder);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CustomerName_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
