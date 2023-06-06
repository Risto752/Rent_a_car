using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rental
{
    public partial class login : Form
    {
        public login()
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
           string username = txtUsername.Text;
           string password = txtPassword.Text;

            sql = "select * from persons where username=@username and passwd=@password and deleted=0;";

            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            dr = cmd.ExecuteReader();


            if (dr.Read())
            {

                string userType = dr[3].ToString();

                if (userType.Equals("employee"))
                {

                   menu main = new menu();
                    this.Hide();

                    main.Show();

                }else if (userType.Equals("admin"))
                {
                    
                    employee main = new employee();
                    this.Hide();

                    main.Show();


                }
            }
            else
            {
                MessageBox.Show("Username or password are incorrect");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();

            }

            connection.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
