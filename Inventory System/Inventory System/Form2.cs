using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_System
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Height > 56)
            {
                panel1.Height -= 20;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel1.Height < 175)
            {
                panel1.Height += 20;
            }
            else
            {
                timer2.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (panel1.Height > 56)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer2.Enabled = true;
            }
        }

        private void appliancesbtn_Click(object sender, EventArgs e)
        {
            var newForm = new appliances();
            newForm.Show();
            this.Hide();
        }

        private void kitchenwarebtn_Click(object sender, EventArgs e)
        {
            var newForm = new kitchenware();
            newForm.Show();
            this.Hide();
        }

        private void furniturebtn_Click(object sender, EventArgs e)
        {
            var newForm = new furniture();
            newForm.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
