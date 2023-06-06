using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rental
{
    public partial class updateCar : Form
    {
        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;
        string carId;


        public updateCar(string id)
        {
            InitializeComponent();

            setForm(id);


        }

        private void setForm(string id)
        {
            carId = id;

            sql = "select * from vozilo where idvozilo=@id";

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            connection.Open();

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtRegNum.Text = dr[1].ToString();
                txtModel.Text = dr[2].ToString();
                txtMake.Text = dr[4].ToString();

                string status = dr[3].ToString();

                if (status.Equals("Yes"))
                {

                    txtAvailable.SelectedIndex = 0;

                }
                else
                {
                    txtAvailable.SelectedIndex = 1;
                }

            }

            connection.Close();




        }

        private void button2_Click(object sender, EventArgs e)
        {

            car form = new car();

            this.Hide();

            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string regNum = txtRegNum.Text;
            string make = txtMake.Text;
            string model = txtModel.Text;


            if (txtAvailable.SelectedItem == null)
            {

                MessageBox.Show("Incomplete data entered!");
                return;

            }
            string aval = txtAvailable.SelectedItem.ToString();



            sql = "update vozilo set regNum=@regNum, model=@model,make=@make,available=@available where idvozilo=@id";

            connection.Open();

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@regNum", regNum);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@available", aval);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@id", carId);


            cmd.ExecuteNonQuery();

            connection.Close();

            car form = new car();

            this.Hide();

            form.Show();


        }
    }
}
