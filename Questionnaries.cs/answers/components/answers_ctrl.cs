using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Questionnaries.cs.answers.components
{
    public partial class answers_ctrl : UserControl
    {
        public static int section_id;
        public int month;
        public static string year;
        public static int panel_width ;
        public answers_ctrl()
        {
            InitializeComponent();
            panel_width = panel1.Width;
           // MessageBox.Show(section_id.ToString());
        }

        private void answers_ctrl_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        public Bitmap mybitmap(string path, int h, int w)
        {

            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
          
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            //Delete Button
            var index = dataGridView1.CurrentCell.RowIndex;
            
                    
                    dataGridView1.ClearSelection();

                    if (dataGridView1.CurrentCell == null ||
                           dataGridView1.CurrentCell.Value == null ||
                           e.RowIndex == -1) return;
                    if (dataGridView1.CurrentCell.ColumnIndex.Equals(12))
                    {
                        string AcceptSate = dataGridView1.Rows[index].Cells[9].Value.ToString();
                        if (AcceptSate == "تم الموافقة")
                        {
                            MessageBox.Show("عفوا لايمكنك حذف الاستبيان بعد الموافقة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                        {
                            DialogResult result2 = MessageBox.Show("هل انت متأكد من حذف الاستبيان لهذا الشهر ؟", "انتباه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result2 == DialogResult.Yes)
                            {
                                int index_row = dataGridView1.CurrentCell.RowIndex;
                                string _year = dataGridView1.Rows[index].Cells[2].Value.ToString();
                                int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
                                int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
                                db db = new db();
                                if (!db.check_approve_month(_year, _month, section_id))
                                {
                                    db.delete_exam_from_tbl_months(id, section_id, _year, _month);
                                    backgroundWorker1.RunWorkerAsync();
                                }
                                else
                                {
                                    MessageBox.Show("عفوا لايمكنك حذف الاستبيان بعد الموافقة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                        }
                   
           
                }

            //View Button

            if (dataGridView1.CurrentCell == null ||
       dataGridView1.CurrentCell.Value == null ||
       e.RowIndex == -1) return;
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(11))
            {
                int index_row = dataGridView1.CurrentCell.RowIndex;
                string _year = dataGridView1.Rows[index].Cells[2].Value.ToString();
                int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
                view_control.come_from_ui = "questionnaiers";
                view_control.come_from_section_id = section_id;
                view_control.come_from_year = _year;
                view_control.come_from_month = _month;


                get(section_id,year,month);
            }
            //Renable Button
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(13))
            {
                string _answer_state = dataGridView1.Rows[index].Cells[5].Value.ToString();
                if (_answer_state == "تمت الاجابة")
                {
                    DialogResult result = MessageBox.Show("هل انت متأكد من رغبتك بأعادة تفعيل الاستبيان لهذا الشهر ؟", "انتباه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if(result == DialogResult.Yes)
                    {
                        int sequnce = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
                        string _year = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
                        db db = new db();
                        if (!db.check_approve_month(_year, _month, section_id))
                        {
                            db.renable(section_id, _year, _month);
                            backgroundWorker1.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("عفوا لايمكنك اعادة تفعيل الاستبيان بعد الموافقة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    

                    
                }
                else
                {
                    MessageBox.Show("لم تتم الاجابة بعد ", "انتباه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            //Archive Button
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(14))
            {
                string _year = dataGridView1.Rows[index].Cells[2].Value.ToString();
                int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
               // MessageBox.Show(_month.ToString());
                panel1.Controls.Clear();
                archive.section_id = section_id;
                archive.year = _year;
                archive.month = _month;
                archive archive_control = new archive();
                archive_control.Width = Form1.w;
                archive_control.Height = Form1.h;
                panel1.Controls.Add(archive_control);
            }
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataTable _dt = new DataTable();

            db db = new db();
            _dt = db.get_answers_section(section_id);

            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.DataSource = _dt;
                dataGridView1.Columns[0].Width = 80;
                dataGridView1.Columns[1].Width = 80;
                //dataGridView1.Columns[4].Width = 80;
                // dataGridView1.Columns[7].Width = 80;

                ///////////////////////

                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    int month = Convert.ToInt32(dataGridView1.Rows[x].Cells[1].Value.ToString());
                    string year = dataGridView1.Rows[x].Cells[2].Value.ToString();
                    dataGridView1[3, x].Value = db.get_column_count_questions(section_id,year,month);

                }
            });
            

        }
        public void get(int this_section_id, string this_year, int this_month)
        {
           // MessageBox.Show(this_section_id.ToString());
            if (this_section_id == 0)
            {
                this_section_id = section_id;
                
            }
            if (this_year == null)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                string _year = dataGridView1.Rows[index].Cells[2].Value.ToString();
                this_year = _year;
            }
            if (this_month == 0)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                int _month = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
                this_month = _month;
            }

           ////////////////// int index_row = dataGridView1.CurrentCell.RowIndex;
            //var index = dataGridView1.CurrentCell.RowIndex;
            db db = new db();
           // month = Convert.ToInt32(dataGridView1.Rows[index_row].Cells[1].Value.ToString());
           // year = dataGridView1.Rows[index_row].Cells[2].Value.ToString();
            panel1.Controls.Clear();
            view_control.month = this_month;
            view_control.year = this_year;
            view_control place_control = new view_control(this);
            DataTable _dt = new DataTable();
            _dt= db.get_approve_state(this_section_id, this_month);
            string _approve = "";
            string _date_approve = "";

            foreach(DataRow row in _dt.Rows)
            {
                _approve = row["approve"].ToString();
                _date_approve = row["date_approve"].ToString();
            }
            if (_approve == "تم الموافقة")
            {
                
                place_control.btn_approve.Text = "اللغاء الموافقة";
                place_control.btn_approve.BackColor = Color.Red;
                place_control.panel4.Visible = true;
                place_control.label_date_approve.Text = _date_approve;
               
            }

            db db2 = new db();
         
            place_control.label_section_name.Text = db2.get_section_name_from_id_section(this_section_id);
            place_control.Width = panel1.Width;
            panel1.Controls.Add(place_control);
            // MessageBox.Show("place : "+place_control.Width.ToString());
        }
    }
}
