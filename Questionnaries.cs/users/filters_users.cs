using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.users
{
    public partial class filters_users : Form
    {
        public bool is_load = false;
        users_managment users;
        public filters_users(users_managment users1)
        {
            InitializeComponent();
            users = users1;
        }

        private void filters_users_Load(object sender, EventArgs e)
        {
            
            this.Width = users_managment.w;
            this.Height = users.groupBox1.Height;
            this.Location = new Point(190, 80);
           
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_fullname.Text, "UserFullName");
        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_job_title.Text, "UserJobTitle");
        }

        private void txt_username_TextChanged_1(object sender, EventArgs e)
        {
            users.search_user_void(txt_username.Text, "UserUsername");
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_phone.Text, "UserPassword");
        }

        private void txt_phone_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_phone.Text, "phone");
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            users.search_user_void(txt_password.Text, "UserPassword");
        }

        public void placeholder(TextBox txt)
        {
            txt.Text = "";
            txt.ForeColor = Color.Black;
        }

        private void txt_fullname_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_job_title_Click(object sender, EventArgs e)
        {
           
        }

        private void txt_username_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_password_Click(object sender, EventArgs e)
        {
           
        }

        private void txt_phone_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_phone_TextChanged_1(object sender, EventArgs e)
        {
            users.search_user_void(txt_phone.Text, "phone");
        }

        private void txt_fullname_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_fullname.Text, "UserFullName");
        }

        private void txt_job_title_TextChanged(object sender, EventArgs e)
        {
            users.search_user_void(txt_job_title.Text, "UserJobTitle");
        }

        private void txt_username_TextChanged_2(object sender, EventArgs e)
        {
            users.search_user_void(txt_username.Text, "UserUsername");
        }

        private void txt_password_TextChanged_1(object sender, EventArgs e)
        {
            users.search_user_void(txt_password.Text, "UserPassword");
        }
    }
}
