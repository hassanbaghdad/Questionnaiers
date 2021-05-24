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
    public partial class add_users : Form
    {
        public static int section_id;
        sections.forms.edit_section_form edit_section;
        sections.forms.add_section_form add_section;
        public add_users(sections.forms.add_section_form add_section1,sections.forms.edit_section_form edit_section1)
        {
            InitializeComponent();
            add_section = add_section1;
            edit_section = edit_section1;
            //MessageBox.Show(section_id.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(txt_fullname.Text =="" || txt_job_title.Text=="" || txt_username.Text=="" || txt_password.Text=="" || txt_phone.Text =="" || txt_repassword.Text=="")
            {
                MessageBox.Show("يرجى ملئ جميع الحقول ", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_password.Text == txt_repassword.Text)
                {
                    db db = new db();
                    int id;
                    if(section_id !=0)
                    {
                        id = section_id;
                    }
                    else
                    {
                        id = db.get_last_id("tbl_sections") - 1;
                    }
                    db.create_user(txt_fullname.Text, txt_job_title.Text, txt_phone.Text, txt_username.Text, txt_password.Text,id );
                    if (section_id == 0)
                    {
                        add_section.add_users_to_dgv_section();
                    }
                    else
                    {
                        edit_section.add_users_to_dgv_section_from_edit_section();
                    }
                }
                else
                {
                    MessageBox.Show("كلمة المرور غير متطابقة ", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

        }
    }
}
