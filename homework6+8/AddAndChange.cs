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
        public AddAndChange(OrderService orderService0)
        {
            InitializeComponent();
            orderService = orderService0;
            bdsOrder.DataSource = orderService.orders;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void bdsOrder_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Order o1 = (Order)bdsOrder.Current;
            int OrderID = o1.OrderID;
            orderService.DeleteOrder(OrderID);
        }
    }
}
