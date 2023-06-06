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
    public partial class rental : Form
    {
        public rental()
        {
            InitializeComponent();
            loadCars();
            loadCustomers();
            loadReservations();
        }
        static string conn_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(conn_string);
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;


        public void loadCars()
        {
            sql = "select * from vozilo where deleted=0 and available='Yes'";

            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               

                int idVozilo = Int32.Parse(dr["idvozilo"].ToString());
                txtCarId.Items.Add( new ComboItem(idVozilo, dr["model"].ToString()));


            }

            connection.Close();

        }


        public void loadCustomers()
        {
            sql = "select * from customer where deleted=0";

            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                int idCustomer = Int32.Parse(dr["idcustomer"].ToString());
                txtCustomerID.Items.Add(new ComboItem(idCustomer, dr["customerName"].ToString()));
              

            }

            connection.Close();

        }

        public void loadReservations()
        {

            sql = "select idrental, model, customerName,rentalFee,rental.Date,DueDate,rental.deleted from rental inner join vozilo on idvozilo=vozilo_idvozilo inner join customer on idcustomer=customer_idcustomer where rental.deleted=0 and DueDate >= CAST(NOW() AS Date); ";

            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                string date = dr[4].ToString();
                string dueDate = dr[5].ToString();

                string[] parts1 = date.Split(" ");
                string[] parts2 = dueDate.Split(" ");

                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], parts1[0], parts2[0]);


            }

            connection.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            int indexCar = txtCarId.SelectedIndex;

            if(indexCar == -1)
            {

                MessageBox.Show("Incomplete data entered!");
                return;
            }

           ComboItem car = (ComboItem)txtCarId.Items[indexCar];

            int carId = car.Key;


            int indexCustomer = txtCustomerID.SelectedIndex;

            if (indexCustomer == -1)
            {

                MessageBox.Show("Incomplete data entered!");
                return;
            }

            ComboItem customer = (ComboItem)txtCustomerID.Items[indexCustomer];

            int customerId = customer.Key;

            string rentalFee = txtFee.Text;
            string date = txtDate.Value.Date.ToString("yyyy-MM-dd");
            string dueDate = txtDueDate.Value.Date.ToString("yyyy-MM-dd");


            
            int check = 0;

            bool result = int.TryParse(rentalFee, out check);

            if (!result)
            {

                MessageBox.Show("Incorrect data provided!");
                return;
            }

            sql = "insert into rental(vozilo_idvozilo, customer_idcustomer, rentalFee, Date, DueDate, deleted) values(@voziloId, @customerId,@rentalFee, @date, @dueDate, 0)";


            cmd = new MySqlCommand(sql, connection);

            connection.Open();

            cmd.Parameters.AddWithValue("@voziloId", carId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@rentalFee", rentalFee);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@dueDate", dueDate);

            cmd.ExecuteNonQuery();

            connection.Close();

            dataGridView1.Rows.Clear();

            loadReservations();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu form = new menu();

            this.Hide();

            form.Show();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {


                sql = "update rental set deleted=1 where idrental=@id";

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

                loadReservations();

            }


        }
    }
}
