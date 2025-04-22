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
    public partial class Add : Form
    {
        OrderService orderService;
        public Add(OrderService orderService)
        {
            this.orderService = orderService;
            InitializeComponent();
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order();
            if (textBox1.Text.Length > 0) newOrder.Customer = new Customer(textBox1.Text);
            orderService.AddOrder(newOrder);
        }
    }
}
