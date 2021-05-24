using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Questionnaries.cs.users
{
    public partial class users_managment : UserControl
    {
        public bool is_load = false;
        public static int w;
        public users_managment()
        {
            InitializeComponent();
        }

        public void get_users()
        {
            
           
        }
      
        public void search_user_void(string txt,string col)
        {
           

            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.search_user(txt,col);
            dataGridView1.Rows.Clear();
          
            foreach (DataRow row in _dt.Rows)
            {


                Bitmap delete = mybitmap(@"D:\MyApp\Questionnaires\images\delete.png", 35, 35);
                Bitmap edit = mybitmap(@"D:\MyApp\Questionnaires\images\edit.png", 30, 30);

                CheckBox active = new CheckBox();
                var id = row["id"].ToString();
                var fullname = row[1].ToString();
                var UserJobTitle = row[2].ToString();
                var username = row[4].ToString();
                var password = row[5].ToString();
                var phone = row["phone"].ToString();
                var section = db.get_section_name_from_id_section(Convert.ToInt32(row[8].ToString()));

                dataGridView1.Rows.Add(id, fullname, UserJobTitle, section,username, password, phone, edit, delete, null);

            }
       
             
        }
        public void  fill_combobox_sections_names()
        {
            db db = new db();
            DataTable _dt = new DataTable();
            List<string> list = new List<string>();
            _dt = db.get_sections();
            
            list.Add("كل الاقسام");
            foreach (DataRow row3 in _dt.Rows)
            {
                list.Add(row3["name"].ToString());
            }
            this.comboBox1.DataSource = list;
            this.comboBox1.Text = "كل الاقسام";
            
            
            if(!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            is_load = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            
            add_users2 add_users2 = new add_users2(this);
            add_users2.new_user2 = true;
            add_users2.Show();
        }

        private void users_managment_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            backgroundWorker1.RunWorkerAsync();
            w = dataGridView1.Width;
            fill_combobox_sections_names();
            
        }
        public Bitmap mybitmap(string path , int h , int w)
        {
          
            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
               
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
      
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_3(object sender, EventArgs e)
        {
            filters_users filter = new filters_users(this);
            filter.ShowDialog();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;

            if (dataGridView1.CurrentCell == null ||
       dataGridView1.CurrentCell.Value == null ||
       e.RowIndex == -1) return;
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(9))
            {
                int index_row = dataGridView1.CurrentCell.RowIndex;

                int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
              //  DialogResult res = MessageBox.Show("Dare you want active / unactive this user ?", "Note !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               // if (res == DialogResult.Yes)
                //{

                    db db = new db();
                    
                    if (is_load == true)
                    {
                        db db2 = new db();
                        db.active_user(id);
                        backgroundWorker1.RunWorkerAsync();
                    }
               // }
            }
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(8))
            {
                int index_row = dataGridView1.CurrentCell.RowIndex;

                int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
                DialogResult res = MessageBox.Show("Dare you sure delete this user ?", "Note !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {

                    db db = new db();
                    db.delete_user(id);
                    if (is_load == true)
                    {
                        db db2 = new db();
                        if(comboBox1.Text == "كل الاقسام")
                        {
                            get_users();
                        }
                        else
                        {
                            string section_id = db2.get_id_section_from_section_name(comboBox1.Text).ToString();
                            search_user_void(section_id, "section_id");
                        }
                        
                    }
                }
            }
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex.Equals(7))
            {
                int user_id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
                add_users2.user_id = user_id;
                add_users2.new_user2 = false;
                add_users2 edit = new add_users2(this);
                
                edit.Show();

            }
            }catch(Exception e2){

            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (is_load == true)
            {
                db db = new db();
                string section_id = db.get_id_section_from_section_name(comboBox1.Text).ToString();
                if (comboBox1.Text == "كل الاقسام")
                {
                    get_users();
                    
                }
                else
                {
                    search_user_void(section_id, "section_id");
                }
                
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[index].Selected = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.get_all_users();
            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Clear();
            });
            

            foreach (DataRow row in _dt.Rows)
            {


                Bitmap delete = mybitmap(@"D:\MyApp\Questionnaires\images\delete.png", 35, 35);
                Bitmap edit = mybitmap(@"D:\MyApp\Questionnaires\images\edit.png", 30, 30);
                Bitmap save = mybitmap(@"D:\MyApp\Questionnaires\images\save.png", 26, 30);
                Bitmap user_active = mybitmap(@"D:\MyApp\Questionnaires\images\user_active.png", 26, 26);
                if (row[9].ToString() == "0")
                {
                    user_active = mybitmap(@"D:\MyApp\Questionnaires\images\user_unactive1.png", 26, 26);
                }
                var id = row["id"].ToString();
                var fullname = row["Name"].ToString();
                var UserJobTitle = row[2].ToString();
                var username = row[4].ToString();
                var password = row[5].ToString();
                var phone = row["phone"].ToString();
                // MessageBox.Show(row[8].ToString());
                var section = db.get_section_name_from_id_section(Convert.ToInt32(row[8].ToString()));
                CheckBox active = new CheckBox();
                dataGridView1.Invoke((MethodInvoker)delegate {
                    dataGridView1.Rows.Add(id, fullname, UserJobTitle, section, username, password, phone, edit, delete, user_active);
                });
                

            }
        }
    }
}
