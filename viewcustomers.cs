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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FloridaBusReservationSystem
{
    public partial class viewcustomers : Form
    {
        public viewcustomers()
        {
            InitializeComponent();
        }

        public int c_id;
        private void viewcustomers_Load(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult r = MessageBox.Show("Cancel Reservation Ticket? Are you sure?", "Confirmation", MessageBoxButtons.YesNo);

            if(r == DialogResult.Yes && !string.IsNullOrWhiteSpace(bus_ticket_no.Text))
            {
                string conStr = $"server=localhost;port=3306;database=floridabusreservationsystem;uid='root';password='';";

                MySqlConnection conn = new MySqlConnection(conStr);
                conn.Open();
                try
                {
                    string b = $"UPDATE tbl_bus SET available_seat = available_seat + 1 WHERE route IN (SELECT route_id FROM tbl_reserves WHERE ticket_id = {Convert.ToInt32(bus_ticket_no.Text)});";
                    string a = $"DELETE tbl_reserves, tbl_passenger FROM tbl_reserves INNER JOIN tbl_passenger ON tbl_reserves.passenger_id = tbl_passenger.passenger_id WHERE tbl_reserves.ticket_id = {Convert.ToInt32(bus_ticket_no.Text)};";
    
                    MySqlCommand cmd = new MySqlCommand(b,conn);

                    cmd.ExecuteNonQuery();
                    conn.Close();


                    conn.Open();
                    cmd = new MySqlCommand(a, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Reservation Ticket Succesfully Canceled!", "Success");
                    this.Close();

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }


                conn.Close();
            }
            
        }
    }
}
