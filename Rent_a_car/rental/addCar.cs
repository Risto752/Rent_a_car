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
    public partial class addCar : Form
    {
        public addCar()
        {
            InitializeComponent();
        }
        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;

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



            sql = "insert into vozilo(regNum, model, available, make, deleted) VALUES(@regNum, @model, @available, @make, 0)";

            connection.Open();

             cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@regNum", regNum);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@available", aval);
            cmd.Parameters.AddWithValue("@make", make);

            cmd.ExecuteNonQuery();

            

            connection.Close();

            car form = new car();

            this.Hide();

            form.Show();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            car form = new car();

            this.Hide();

            form.Show();
        }
    }
}
