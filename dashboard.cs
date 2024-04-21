using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloridaBusReservationSystem
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new admin().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new newbus().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new newcustomer().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new viewcustomers().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new customers().Show();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
