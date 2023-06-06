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
    public partial class addEmployee : Form
    {
        public addEmployee()
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
            employee form = new employee();

            this.Hide();

            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
         

            sql = "insert into persons(username, passwd, typePerson, deleted) VALUES(@username, @password, 'employee',0)";

            connection.Open();

            cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
          
            cmd.ExecuteNonQuery();

            connection.Close();

            employee form = new employee();

            this.Hide();

            form.Show();


        }
    }
}
