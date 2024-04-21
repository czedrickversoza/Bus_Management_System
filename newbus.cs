using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FloridaBusReservationSystem
{
    public partial class newbus : Form
    {
        public static double distance = 0;
        public newbus()
        {
            InitializeComponent();
        }

        private void newbus_FormClosed(object sender, FormClosedEventArgs e)
        {
            new dashboard().Show();
        }

        private void newbus_Load(object sender, EventArgs e)
        {
            time.Format = DateTimePickerFormat.Time;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            bus_config.bus_name = bus_name.Text;
            bus_config.bus_type = "Sleeper";
            bus_config.max_seat = 32;
            bus_config.fare = 2.30;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bus_config.bus_name = bus_name.Text;
            bus_config.bus_type = "Super Deluxe";
            bus_config.max_seat = 29;
            bus_config.fare = 1.70;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            bus_config.bus_name = bus_name.Text;
            bus_config.bus_type = "Deluxe";
            bus_config.max_seat = 29;
            bus_config.fare = 1.30;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fare.Text = (distance*bus_config.fare).ToString();

        }
        private void clickedDt(string rbtn)
        {
            MySqlConnection conn = new MySqlConnection($"server=localhost;port=3306;database=floridabusreservationsystem;uid={global_var.uid};password={global_var.upasw};");

            string insert_to_route = "";

            string insert_to_bus = "";

            try
            {
                conn.Open();
                
                if (conn.State == ConnectionState.Open)
                {
                    try
                    {

                        insert_to_route = $"INSERT INTO `tbl_route`(`origin`, `destination`, `depart_date`, `depart_time`, `fare`) VALUES ('{origin.Text}','{desination.Text}','{date.Text}','{time.Text}','{fare.Text}')";

                        MySqlCommand ins = new MySqlCommand(insert_to_route, conn);

                        ins.ExecuteNonQuery();
                        conn.Close();

                        insert_to_bus = $"INSERT INTO `tbl_bus`(`bus_name`, `bus_type`, `bus_max_seat`, `available_seat`, `route`) VALUES ('{bus_config.bus_name}','{bus_config.bus_type}','{bus_config.max_seat}','{bus_config.max_seat}','{getRouteID()}')";
                        conn.Open();

                        ins = new MySqlCommand(insert_to_bus, conn);

                        ins.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error!");
            }
            conn.Close();

        }

        private static int getRouteID()
        {
            int routeId = -1;
            using (MySqlConnection conn = new MySqlConnection($"server=localhost;port=3306;database=floridabusreservationsystem;uid={global_var.uid};password={global_var.upasw};"))
            {
                MySqlCommand com = new MySqlCommand("SELECT route_id FROM tbl_route ORDER BY route_id DESC LIMIT 1;", conn);

                try
                {
                    // Open the connection
                    conn.Open();

                    // Execute the query and retrieve the route_id
                    object result = com.ExecuteScalar();

                    // Check if result is not null and convert it to an integer
                    if (result != null)
                    {
                        routeId = Convert.ToInt32(result);
                    }
                    
                }
                catch (Exception ex)
                {
                    // Handle any exceptions here
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

             return routeId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res;
            string rbtn = bus_config.bus_type;

            if (rbtn == "Regular")
            {
                res = MessageBox.Show("Air-conditioned Bus Class 32-Seater Bus with 2x1 seating configuration.", "Confirm New Bus!", MessageBoxButtons.YesNo);
            }
            else if (rbtn == "Deluxe")
            {
                res = MessageBox.Show("Airconditioned 29-seater bus with 2×1 seating configuration, and on-board restroom and television.", "Confirm New Bus!", MessageBoxButtons.YesNo);
            }
            else if (rbtn == "Super Deluxe")
            {
                res = MessageBox.Show("An airconditioned 29-seater bus with 2×1 seating configuration and spacious legroom, and on-board restroom and television.", "Confirm New Bus!", MessageBoxButtons.YesNo);
            }
            else if (rbtn == "Sleeper")
            {
                res = MessageBox.Show("An airconditioned 32-seater bus with 1x1x1 seating configuration and double-decked beds, and on-board restroom and television.", "Confirm New Bus!", MessageBoxButtons.YesNo);
            }

            clickedDt(rbtn);

        }

        private void desination_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(desination.Text == "Tuguegarao")
            {
                distance = 105;
            }else if(desination.Text == "Manila")
            {
                distance = 546;
            }else if(desination.Text == "Isabela")
            {
                distance = 182;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new viewbus().Show();
        }
    }
    public class bus_config
    {
        public static string bus_type { get; set; }
        public static string bus_name { get; set; }
        public static int max_seat { get; set; }
        public static double fare { get; set; }
    }
}
