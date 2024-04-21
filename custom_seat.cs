using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    public partial class custom_seat : Form
    {
        public custom_seat()
        {
            InitializeComponent();
        }

        private void custom_seat_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    radioButton.Click += cl;
                }
            }

            getdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Selected {global_var.seat_no} Seat, Are you sure?", "Confirmation");
            this.Close();
        }

        private void cl (object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                string text = radioButton.Text;
                string seatNumberString = text.Substring(4);
                global_var.seat_no = Convert.ToInt32(seatNumberString);
            }
        }

        private void getdata()
        {
            string conStr = $"server=localhost;port=3306;database=floridabusreservationsystem;uid='root';password='';";

            MySqlConnection conn = new MySqlConnection(conStr);
            conn.Open();

            //MySqlCommand com = new MySqlCommand($"SELECT seat_no FROM tbl_passenger WHERE `bus_id`={global_var.bus_id}", conn);

            //MySqlCommand com = new MySqlCommand($"SELECT * FROM tbl_reserves JOIN tbl_passenger ON tbl_reserves.passenger_id = tbl_passenger.passenger_id JOIN tbl_bus ON tbl_reserves.bus_id = tbl_bus.bus_id WHERE tbl_reserves.bus_id={global_var.bus_id}", conn);

            string query = @"SELECT * FROM tbl_reserves JOIN tbl_passenger ON tbl_reserves.passenger_id = tbl_passenger.passenger_id JOIN tbl_bus ON tbl_reserves.bus_id = tbl_bus.bus_id WHERE tbl_reserves.bus_id = @busId";

            // Create MySqlCommand object
            MySqlCommand com = new MySqlCommand(query, conn);

            // Add parameter for bus_id
            com.Parameters.AddWithValue("@busId", global_var.bus_id);

            try
            {
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control is RadioButton radioBtn)
                        {
                            int a = 0;
                            try
                            {
                               a  = Convert.ToInt32(radioBtn.Text.Substring(4));
                            }catch(Exception b)
                            {
                                
                            }

                            if (a == reader.GetInt32("seat_no") || radioBtn.Text.Contains("30") || radioBtn.Text.Contains("31") || radioBtn.Text.Contains("32"))
                            {
                                Console.WriteLine($"{a} : {reader.GetInt32("seat_no")}");
                                radioBtn.Enabled = false;
                                radioBtn.ForeColor = Color.Crimson;
                                radioBtn.Text = "N/A";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            conn.Close();
        }

    }
}
