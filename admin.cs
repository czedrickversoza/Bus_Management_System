using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FloridaBusReservationSystem
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection();
        private void button1_Click(object sender, EventArgs e)
        {
              
            try
            {
                conn = new MySqlConnection($"server=localhost;port=3306;database=floridabusreservationsystem;uid={textBox1.Text};password={textBox2.Text};");
                conn.Open();

                if (conn.State == ConnectionState.Open )
                {
                    new dashboard().Show();
                    MessageBox.Show("Welcome Back! " + textBox1.Text, "Logged in successfully!");
                    global_var.uid = textBox1.Text;
                    global_var.upasw = textBox2.Text;
                    global_var.loggedIn = true;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error!");
            }
            conn.Close();
        }

        private void admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            new Form1().Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked is true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void admin_Load(object sender, EventArgs e)
        {
            
        }
    }
    public static class global_var
    {
        public static string uid { get; set; }
        public static string upasw { get; set; }
        public static Boolean loggedIn { get; set; }

        public static int bus_id { get; set; }
        public static int seat_no { get; set; }
    }
}
