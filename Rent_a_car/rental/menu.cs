using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rental
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            car form = new car();
            this.Hide();

            form.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer form = new customer();
            this.Hide();

            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rental form = new rental();
            this.Hide();

            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login form = new login();

            this.Hide();

            form.Show();


        }
    }
}
