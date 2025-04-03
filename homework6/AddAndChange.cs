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
    public partial class AddAndChange : Form
    {
        OrderService orderService;
        public AddAndChange(Order order,ref OrderService orderService0)
        {
            InitializeComponent();
            orderService = orderService0;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string ID=textBoxID .Text;
            Customer customer = new Customer(textBoxCustomerName.Text,textBoxCustomerID.Text);
            orderService.AddOrder(ID,customer,new List<OrderDetails> { });
            this.Hide();

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
