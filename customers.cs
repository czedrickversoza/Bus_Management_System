using MySql.Data.MySqlClient;
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
    public partial class customers : Form
    {
        public customers()
        {
            InitializeComponent();
        }

        private void customers_Load(object sender, EventArgs e)
        {
            getdata("SELECT * FROM tbl_passenger");
        }

        private void getdata(string args)
        {
            string conStr = $"server=localhost;port=3306;database=floridabusreservationsystem;uid='root';password='';";

            MySqlConnection conn = new MySqlConnection(conStr);
            conn.Open();
            DataTable dt = new DataTable();

            MySqlCommand com = new MySqlCommand(args, conn);

            try
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(com))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
