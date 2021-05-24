using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.sections.forms
{
    public partial class add_section_form : Form
    {
        sections.components.sections_place place;
        sections.forms.edit_section_form edit_section;
        public add_section_form(sections.components.sections_place place1)
        {
            InitializeComponent();
            place = place1;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_phone.Text == "" || txt_location.Text == "" || txt_manager.Text == "" || txt_section_name.Text =="")
            {
                MessageBox.Show("يرجى ملئ جميع الحقول", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string type = "department";
                if(radioButton1.Checked ==true)
                {
                    type = "department";
                }
                if(radioButton2.Checked ==true)
                {
                    type = "branch";
                }

                
                    db db = new db();
                    db.create_section(txt_section_name.Text, txt_manager.Text, txt_phone.Text, type, 1, txt_location.Text, "admin", true);
                    place.backgroundWorker1.RunWorkerAsync();
                    groupBox3.Enabled = true;
                
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            users.add_users add_user = new users.add_users(this,edit_section);
            add_user.ShowDialog();
        }

        public void add_users_to_dgv_section()
        {
            
            DataTable _dt = new DataTable();
            db db = new db();
            _dt=db.get_users_for_section(db.get_last_id("tbl_sections") - 1);
            dataGridView1.DataSource = _dt;
        }

        private void add_section_form_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
    }
}
