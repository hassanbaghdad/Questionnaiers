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
    public partial class add_users2 : Form
    {
        public static bool new_user2;
        public static int user_id = 0;
        public static int section_id=0;
        public string username;
        public bool SameUser;
        users_managment users;
        public add_users2(users_managment users1)
        {
            InitializeComponent();
            users = users1;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(new_user2 == true)
            {
                new_user();
            }
            else
            {
                edit_user();
            }
        }
        public void new_user()
        {
            if (txt_fullname.Text == "" || txt_job_title.Text == "" || txt_password.Text == "" || txt_repassword.Text == "" || txt_username.Text == "" || txt_phone.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Sorry all fields are required", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_password.Text == txt_repassword.Text)
                {
                    int section_id;
                    db db = new db();
                    section_id = db.get_id_section_from_section_name(comboBox1.Text);
                    db.create_user(txt_username.Text, txt_job_title.Text, txt_phone.Text, txt_username.Text, txt_password.Text, section_id);
                    users.backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Sorry passwords not match ", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        public void edit_user()
        {
            if (txt_fullname.Text == "" || txt_job_title.Text == "" || txt_password.Text == "" || txt_repassword.Text == "" || txt_username.Text == "" || txt_phone.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Sorry all fields are required", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_password.Text == txt_repassword.Text)
                {
                    int section_id;
                    db db = new db();
                    if(username == txt_username.Text)
                    {
                        SameUser = true;
                    }else{
                        SameUser = false;
                    }
                    section_id = db.get_id_section_from_section_name(comboBox1.Text);
                    db.edit_user(user_id,txt_fullname.Text, txt_job_title.Text, txt_username.Text, txt_password.Text, txt_phone.Text, section_id, true, SameUser);
                    users.get_users();
                }
                else
                {
                    MessageBox.Show("Sorry passwords not match ", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void get_user_info_to_edit()
        {
            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.get_info_user_to_edit(user_id);
            foreach(DataRow row in _dt.Rows)
            {
                txt_fullname.Text = row["UserFullName"].ToString();
                txt_job_title.Text = row["UserJobTitle"].ToString();
                txt_username.Text = row["UserUsername"].ToString();
                txt_password.Text = row["UserPassword"].ToString();
                txt_repassword.Text = row["UserPassword"].ToString();
                txt_phone.Text = row["phone"].ToString();
                comboBox1.Text = db.get_section_name_from_id_section(Convert.ToInt32(row["section_id"].ToString()));
               
            }
            username = txt_username.Text;

        }
        private void add_users2_Load(object sender, EventArgs e)
        {
            
           
            //////////////////////////////////

            db db = new db();
            DataTable _dt = new DataTable();
            List<string> list = new List<string>();
            _dt = db.get_sections();
            foreach (DataRow row3 in _dt.Rows)
            {
                list.Add(row3["name"].ToString());
            }
            this.comboBox1.DataSource = list;
            if (new_user2 == false)
            {
                get_user_info_to_edit();
            }
        }

        private void txt_fullname_Enter(object sender, EventArgs e)
        {
            
        }

        public void placeholder(TextBox txt)
        {
            txt.Text = "";
            txt.ForeColor = Color.Black;
        }

        private void txt_fullname_Click(object sender, EventArgs e)
        {
           // placeholder(txt_fullname);
        }

        private void txt_job_title_Click(object sender, EventArgs e)
        {
            //placeholder(txt_job_title);
        }

        private void txt_phone_Click(object sender, EventArgs e)
        {
           // placeholder(txt_phone);
        }

        private void txt_username_Click(object sender, EventArgs e)
        {
           // placeholder(txt_username);
        }

        private void txt_password_Click(object sender, EventArgs e)
        {
            //placeholder(txt_password);
        }

        private void txt_repassword_Click(object sender, EventArgs e)
        {
           // placeholder(txt_repassword);
        }
    }
}
