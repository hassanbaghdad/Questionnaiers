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
    public partial class edit_section_form : Form
    {
        public static int id_section ;
        sections.components.sections_place place;
        sections.forms.add_section_form add_section;
        
        public edit_section_form(sections.components.sections_place place1)
        {
            InitializeComponent();
            place = place1;
            
            
        }

        private void edit_section_form_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            ////////////////////////////////////////
            DataTable _dt = new DataTable();
            db db = new db();
            _dt=db.get_section_to_edit(id_section);
            foreach(DataRow row in _dt.Rows)
            {
                txt_section_name.Text = row["name"].ToString();
                txt_manager.Text = row["manager"].ToString();
                txt_location.Text = row["address"].ToString();
                txt_phone.Text = row["phone"].ToString();
            }
            add_users_to_dgv_section_from_edit_section();
       
        }
        public void add_users_to_dgv_section_from_edit_section()
        {
            
            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.get_users_for_section(id_section);
            dataGridView1.DataSource = _dt;
            dataGridView1.Columns["ID"].Width = 33;
            dataGridView1.Columns["حذف"].Width = 55;
            dataGridView1.Columns.Remove("section_id");
        }
        private void button1_Click(object sender, EventArgs e)
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
            db.edit_section(id_section, txt_section_name.Text, txt_manager.Text, txt_phone.Text, type, 1, txt_location.Text, "admin", true);
            place.backgroundWorker1.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            users.add_users add_user = new users.add_users(add_section,this);
            add_user.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox4.Focus();
            for (int x = 0; x < dataGridView1.Rows.Count; x++)
            {

                // MessageBox.Show(dataGridView1.Rows[x].Cells[0].Value.ToString());

                db db = new db();
                int user_id = Convert.ToInt32(dataGridView1.Rows[x].Cells[0].Value.ToString());
                string fullname = dataGridView1.Rows[x].Cells[1].Value.ToString();
                string job_title = dataGridView1.Rows[x].Cells[2].Value.ToString();
                string phone = dataGridView1.Rows[x].Cells[3].Value.ToString();
                string username = dataGridView1.Rows[x].Cells[4].Value.ToString();
                string password = dataGridView1.Rows[x].Cells[5].Value.ToString();



                db.edit_user(user_id, fullname, job_title, username, password, phone, id_section, false, true);

            }
            MessageBox.Show("All edit success");
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(6))
            {
                int index_row = dataGridView1.CurrentCell.RowIndex;

                int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
                DialogResult res = MessageBox.Show("Dare you sure delete this user ?", "Note !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {

                    db db = new db();
                    db.delete_user(id);
                   
                        DataTable users_dt = new DataTable();
                        db db2 = new db();
                      users_dt=  db.get_users_for_section(id_section);
                      dataGridView1.Columns.Clear();
                      dataGridView1.DataSource = users_dt;
                      dataGridView1.Columns["ID"].Width = 33;
                      dataGridView1.Columns["حذف"].Width = 55;
                      dataGridView1.Columns.Remove("section_id");
                }
            }
        }

       
    }
}
