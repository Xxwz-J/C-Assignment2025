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
    public partial class AddDetails : Form
    {
        OrderService orderService;
        Order order;
        public AddDetails(OrderService Service, Order order)
        {
            InitializeComponent();
            this.orderService = Service;
            this.order = order;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new OrderDetails = new OrderDetails(new Product(textBox1 .Text),int.Parse(textBox2 .Text));
            order.Details.Add(OrderDetails);
        }
    }
}
