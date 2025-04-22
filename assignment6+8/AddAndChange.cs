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
            new Add(orderService).ShowDialog();
        }



        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Order o1 = (Order)bdsOrder.Current;
            int OrderID = o1.OrderID;
            orderService.DeleteOrder(OrderID);
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

        //ChangeOrder 按钮改不了名称我注释一下
        private void button2_Click(object sender, EventArgs e)
        {
            new Change(orderService,(Order)bdsOrder.Current).ShowDialog();
        }

        private void buttonAddDetails_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bdsOrder_CurrentChanged(object sender, EventArgs e)
        {

        }


    }
}
