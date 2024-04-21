                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FloridaBusReservationSystem
{
    public partial class newcustomer : Form
    {
        public newcustomer()
        {
            InitializeComponent();
        }
        private int seatNo;
        private string bus_t = "";
        private int bus_a;
        private int bus_id = 0;
        private int route_id = 0;
        private int seat;
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            arrCom("Sleeper", comboBox2.Text);
            bus_t = "Sleeper";
            bus_a = 32;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            arrCom("Deluxe", comboBox2.Text);
            bus_t = "Deluxe";
            bus_a = 29;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            arrCom("Super Deluxe", comboBox2.Text);
            bus_t = "Super Deluxe";
            bus_a = 29;
        }

        private void newcustomer_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(global_var.uid))
            {
                button3.Visible = false;
                textBox1.Enabled = false;
            }

            string q = "Select * From tbl_bus";

            getdata(q);

        }
        private void arrCom(string a, string b)
        {
            string temp;
            if (!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
            {
                temp = $"SELECT * FROM tbl_bus WHERE `bus_type`='{a}' AND route IN (SELECT route_id FROM tbl_route WHERE destination='{b}')";
            }
            else if (!string.IsNullOrEmpty(b))
            {
                temp = $"SELECT * FROM tbl_bus WHERE route IN (SELECT route_id FROM tbl_route WHERE destination='{b}')";
            }
            else
            {
                temp = $"SELECT * FROM tbl_bus WHERE `bus_type`='{a}'";
            }

            getdata(temp);
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
            }catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrCom(bus_t, comboBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getdata("Select * From tbl_bus");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conStr = $"server=localhost;port=3306;database=floridabusreservationsystem;uid='root';password='';";

            MySqlConnection conn = new MySqlConnection(conStr);

            conn.Open();
            seat = getNextSeat(bus_a);

            bool isValid = true;

            bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.BackColor = Color.Red;
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(textBox7.Text) || !IsValidEmail(textBox7.Text))
            {
                textBox7.BackColor = Color.Red;
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Please enter valid data in all fields!", "Validation Error");
            }
            else
            {
                string in_to_par;
                if (checkBox1.Checked && global_var.seat_no>1)
                {
                    in_to_par = $"INSERT INTO `tbl_passenger`(`passenger_name`, `mobile_number`, `email`, `address`, `seat_no`) " +
                    $"VALUES ('{textBox2.Text}','{textBox6.Text}','{textBox7.Text}','{textBox5.Text}', {global_var.seat_no})";
                }
                else
                {
                    in_to_par = $"INSERT INTO `tbl_passenger`(`passenger_name`, `mobile_number`, `email`, `address`, `seat_no`) " +
                    $"VALUES ('{textBox2.Text}','{textBox6.Text}','{textBox7.Text}','{textBox5.Text}', {getNextSeat(bus_a)})";
                }
                
                int passengerId=0;

                try
                {

                    MySqlCommand c = new MySqlCommand(in_to_par, conn);

                    int rowsAffected = c.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        c.CommandText = "SELECT LAST_INSERT_ID()";
                         passengerId = Convert.ToInt32(c.ExecuteScalar());
                    }
                }
                catch(Exception s)
                {
                    MessageBox.Show(s.ToString(), "error");
                }
                conn.Close();

                string in_to_res = $"INSERT INTO `tbl_reserves`(`passenger_id`, `route_id`, `bus_id`) " +
                    $"VALUES ('{passengerId}','{route_id}', '{bus_id}')";

                conn.Open();
                try
                {

                    MySqlCommand c = new MySqlCommand(in_to_res, conn);

                    int rowsAffected = c.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string msg = "You have a reservation!";
                        MessageBox.Show(msg, "Successful!");
                        string up = $"UPDATE `tbl_bus` SET `available_seat`= available_seat - 1 WHERE `bus_id`={bus_id}";
                        c = new MySqlCommand(up, conn);
                        c.ExecuteNonQuery();

                        this.Close();
                    }
                    else
                    {
                        string errorMsg = "Reservation failed!";
                        MessageBox.Show(errorMsg, "Error");
                    }

                    
                }
                catch (Exception s)
                {
                    MessageBox.Show(s.ToString(), "error");
                }
                conn.Close();
             }


        }
    
        private int getNextSeat(int bus_seats=0)
        {
            string conStr = $"server=localhost;port=3306;database=floridabusreservationsystem;uid='root';password='';";

            MySqlConnection conn = new MySqlConnection(conStr);
            conn.Open();
            MySqlCommand com = new MySqlCommand("Select * FROM tbl_passenger ORDER BY `seat_no` ASC", conn);

            int nextAvailableSeat = 1;
            int ind = 1;
            try
            {
                MySqlDataReader adapter = com.ExecuteReader();
                {
                    while (adapter.Read())
                    {
                        int seat = adapter.GetInt32("seat_no"); // Assuming the column name is "seat_no"

                        if (seat == ind)
                        {
                            ind++;
                        }
                        else
                        {
                            nextAvailableSeat = ind+1;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "das");
                nextAvailableSeat = 1;
            }
            conn.Close();

            return nextAvailableSeat;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            new viewcustomers().Show();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            string temp;
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                temp = $"SELECT * FROM tbl_bus WHERE route IN (SELECT route_id FROM tbl_route WHERE destination='{comboBox2.Text}')";
            }
            else
            {
                temp = "SELECT * FROM tbl_bus";
            }

            getdata(temp);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                new custom_seat().Show();
            }
            else
            {
                seatNo = getNextSeat(bus_a);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    bus_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    route_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    global_var.bus_id = bus_id;
                }
            }
            catch (Exception b)
            {
                MessageBox.Show(b.ToString(), "error");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              