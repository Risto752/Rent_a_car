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
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
            load();
        }
        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;


        public void load()
        {
            sql = "select * from customer where deleted=0";

            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            dr = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (dr.Read())
            {

                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);

            }

            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            menu form = new menu();

            this.Hide();

            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addCustomer form = new addCustomer();

            this.Hide();

            form.Show();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {


                sql = "update customer set deleted=1 where idcustomer=@id";

                if (dataGridView1.CurrentRow.Cells[0].Value == null)
                {
                    return;
                }


                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();


                cmd = new MySqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@id", id);

                connection.Open();

                cmd.ExecuteNonQuery();

                connection.Close();

                dataGridView1.Rows.Clear();

                load();

            }
            else if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                if (dataGridView1.CurrentRow.Cells[0].Value == null)
                {
                    return;
                }

                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string customerName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string address = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string mobile = dataGridView1.CurrentRow.Cells[3].Value.ToString();



                updateCustomer form = new updateCustomer(id,customerName,address, mobile);

                this.Hide();

                form.Show();


            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                load();
            }
            else
            {
                sql = "select * from customer where deleted=0 and customerName like @name";

                cmd = new MySqlCommand(sql, connection);

                connection.Open();

                cmd.Parameters.AddWithValue("@name", txtSearch.Text + "%");

                dr = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (dr.Read())
                {

                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);

                }

                connection.Close();

            }

        }
    }
}
