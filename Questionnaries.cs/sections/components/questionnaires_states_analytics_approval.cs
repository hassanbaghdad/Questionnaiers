using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.sections.components
{
    public partial class questionnaires_states_analytics_approval : UserControl
    {
        public questionnaires_states_analytics_approval()
        {
            InitializeComponent();
            this.Width = Form1.w;
            this.Height = Form1.h;
            dataGridView1.Rows.Clear();
            

        }
        public void get()
        {
            dataGridView1.Rows.Clear();
            
            questionnaires_states_Analytics_approval analaytics_db = new questionnaires_states_Analytics_approval();
            DataTable _dt = new DataTable();

            _dt = analaytics_db.get_analytics_for_sections(combo_year.Text, combo_month.Text);
            int s = 0;
           // var index = dataGridView1.CurrentCell.RowIndex;
          
            foreach (DataRow row in _dt.Rows)
            {
                
                s++;
                
                var year = row["year"].ToString();
                var month = row["month"].ToString();
                var noSections = row["noSections"].ToString();
                var noBranchs = row["noBranchs"].ToString();
                var noDepartments = row["noDepartments"].ToString();
                var complated = row["complated"].ToString();
                var view_btn = mybitmap(@"D:\MyApp\Questionnaires\images\view.png", 80, 50);
                var delete_btn = mybitmap(@"D:\MyApp\Questionnaires\images\delete.png", 60, 50);
                if (complated == "تم الاعتماد")
                {
                     delete_btn = mybitmap(@"D:\MyApp\Questionnaires\images\delete-disable.png", 60, 50);

                }

                
                dataGridView1.Rows.Add(s.ToString(), year, month, noSections, noBranchs, noDepartments, complated, view_btn, delete_btn);
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Clear();
                questionnaires_states_Analytics_approval analaytics_db = new questionnaires_states_Analytics_approval();
                DataTable _dt = new DataTable();

                _dt = analaytics_db.get_analytics_for_sections(combo_year.Text, combo_year.Text);
                var index = dataGridView1.CurrentCell.RowIndex;
                
                int s = 0;
                foreach (DataRow row in _dt.Rows)
                {
                    string approval_state = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    s++;
                    var year = row["year"].ToString();
                    var month = row["month"].ToString();
                    var noSections = row["noSections"].ToString();
                    var noBranchs = row["noBranchs"].ToString();
                    var noDepartments = row["noDepartments"].ToString();
                    var complated = row["complated"].ToString();
                    var view_btn = mybitmap(@"D:\MyApp\Questionnaires\images\view.png", 80, 50);
                    //var delete_btn = mybitmap(@"D:\MyApp\Questionnaires\images\view.png", 80, 50);

                    dataGridView1.Rows.Add(s.ToString(), year, month, noSections, noBranchs, noDepartments, complated, view_btn);
                }
            });
            
            
           
        }

        public Bitmap mybitmap(string path, int h, int w)
        {

            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }
        answers.components.answers_ctrl answer;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            
            if (dataGridView1.CurrentCell == null ||
               dataGridView1.CurrentCell.Value == null ||
               e.RowIndex == -1) return;

            //View
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(7))
            {
                string _year = dataGridView1.Rows[index].Cells[1].Value.ToString();
                int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[2].Value.ToString());
                //MessageBox.Show(_year + " " + _month.ToString());
                panel_container.Controls.Clear();
                sub_analytics_details.month = _month;
                sub_analytics_details.year = _year;
                sub_analytics_details sub = new sub_analytics_details(answer);
                panel_container.Controls.Add(sub);
            }

            //Delete
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(8))
            {
                string ApprovalState = dataGridView1.Rows[index].Cells[6].Value.ToString();
                if(ApprovalState=="تم الاعتماد")
                {
                    MessageBox.Show("عفوا لايمكن الحذف بعد الاعتماد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string _year = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[2].Value.ToString());
                    questionnaires_states_Analytics_approval ApprovalClass = new questionnaires_states_Analytics_approval();
                    DialogResult deleteResult = MessageBox.Show("هل انت متأكد من الحذف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deleteResult == DialogResult.Yes)
                    {
                        if (ApprovalClass.deleteMonthApproval(_year, _month))
                        {
                            get();
                            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                
                
                
            }


        }

        private void questionnaires_states_analytics_approval_Load(object sender, EventArgs e)
        {
            get_dropdowns();
            
            timer1.Enabled = true;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_dgv_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_black_bar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_head_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
         
          
        }

        private void combo_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drodowns_has_loaded == true)
            {
               
                get();
                
            }
        }

        public void get_dropdowns()
        {
            questionnaires_states_Analytics_approval analaytics_db = new questionnaires_states_Analytics_approval();
            var years = analaytics_db.dropdown_year_and_month_for_analytics("year");
            var months = analaytics_db.dropdown_year_and_month_for_analytics("month");

            combo_year.DataSource = years;
            combo_month.DataSource = months;
            get();
        }

        public bool drodowns_has_loaded = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if(combo_year.Text =="All" && combo_month.Text == "All")
            {
                
                timer1.Enabled = false;
            }
            drodowns_has_loaded = true;
        }

        private void combo_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (drodowns_has_loaded == true)
            {
                get();
            }
            
        }

        
    }
}
