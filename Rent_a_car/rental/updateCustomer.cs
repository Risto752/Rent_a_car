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
    public partial class updateCustomer : Form
    {

        string customerId;
        public updateCustomer(string id, string customerName, string address, string mobile)
        {
            InitializeComponent();

            customerId = id;
            txtCustName.Text = customerName;
            txtAddress.Text = address;
            txtMobile.Text = mobile;

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

            string customerName = txtCustName.Text;
            string address = txtAddress.Text;
            string mobile = txtMobile.Text;

            sql = "update customer set customerName=@customerName, address=@address,mobile=@mobile where idcustomer=@id";

            connection.Open();

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            cmd.Parameters.AddWithValue("@id", customerId);


            cmd.ExecuteNonQuery();

            

            connection.Close();

            customer form = new customer();

            this.Hide();

            form.Show();
        }
    }
}
