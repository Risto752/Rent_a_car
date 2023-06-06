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
    public partial class addCustomer : Form
    {
        public addCustomer()
        {
            InitializeComponent();
        }
        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;


        private void button2_Click(object sender, EventArgs e)
        {

            customer form = new customer();

            this.Hide();

            form.Show();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customerName =   txtCustomerName.Text;
            string address = txtAddress.Text;
            string mobile = txtMobile.Text;


            sql = "insert into customer(customerName, Address, Mobile, deleted) VALUES(@customerName, @address, @mobile, 0)";

            connection.Open();

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@mobile", mobile);
           

            cmd.ExecuteNonQuery();


            connection.Close();

            customer form = new customer();

            this.Hide();

            form.Show();






        }
    }
}
