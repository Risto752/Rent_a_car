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
    public partial class updateEmployee : Form
    {

        string employeeId;

        public updateEmployee(string id, string username, string password)
        {
            InitializeComponent();

            employeeId = id;

            txtUsername.Text = username;
            txtPassword.Text = password;


        }

        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;

        private void button2_Click(object sender, EventArgs e)
        {
            employee form = new employee();

            this.Hide();

            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Text;
           

            sql = "update persons set username=@username, passwd=@password where personID=@id";

            connection.Open();

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            
            cmd.Parameters.AddWithValue("@id", employeeId);


            cmd.ExecuteNonQuery();



            connection.Close();

            employee form = new employee();

            this.Hide();

            form.Show();
        }
    }
}
