using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.departs
{
    public partial class edit_depart_form : Form
    {
        public static int id;
        string user_before_change;
        bool same_user;
        departs.components.screen_work scr;
        public edit_depart_form(departs.components.screen_work scr1 , int id2)
        {
            id = id2;
            InitializeComponent();
            scr = scr1;
          //  MessageBox.Show(id.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            

        }

        private void Button14_Click(object sender, EventArgs e)
        {
            
        }

        private void edit_depart_form_Load(object sender, EventArgs e)
        {
         //   user_before_change = txt_username.Text
          //  MessageBox.Show(id.ToString());
            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.get_deprt_to_edit(id);
           
            foreach(DataRow row in _dt.Rows)
            {
                
                textBox1.Text = row[1].ToString();
                if(row["enable"].ToString() == "0")
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }
            DataTable _dt2 = new DataTable();
            _dt2 = db.get_user_to_edit(id);
            foreach (DataRow row2 in _dt2.Rows)
            {
                txt_fullname.Text = row2["user_fullname"].ToString();
                txt_username.Text = row2["user_username"].ToString();
                txt_password.Text = row2["user_password"].ToString();
                string rank = row2["user_rank"].ToString();
                if (rank == "0")
                {
                    combo_rank.Text = "User";
                }
                if(rank == "1")
                {
                    combo_rank.Text = "Admin";
                }
                

            }
            user_before_change = txt_username.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int enable;
            int _rank = 0;
            if (combo_rank.Text == "User")
            {
                _rank = 0;
            }
            if (combo_rank.Text == "Admin")
            {
                _rank = 1;
            }

            if (checkBox1.Checked == true)
            {
                enable = 1;
            }
            else
            {
                enable = 0;
            }
            db db = new db();
            if(user_before_change != txt_username.Text)
            {
              //  MessageBox.Show(user_before_change.ToString());
            //    MessageBox.Show(" new user " + same_user.ToString());
                if (db.validate_user(txt_username.Text) == true)
                {
                    same_user = false;
                    
                    db.edit_department(id, textBox1.Text, enable);
                    db.edit_user(txt_fullname.Text, txt_username.Text, txt_password.Text, _rank, id, false,same_user);
                    scr.get_departs();
                }
                else
                {
                    MessageBox.Show("Sorry The Username Is Taken Already , Please Change Another Username !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                    same_user = true;
                   // MessageBox.Show("not new user " + same_user.ToString());
                    db.edit_department(id, textBox1.Text, enable);
                    db.edit_user(txt_fullname.Text, txt_username.Text, txt_password.Text, _rank, id, false,same_user);
                    scr.get_departs();
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
