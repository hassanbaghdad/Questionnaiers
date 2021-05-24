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
    public partial class sub_analytics_details : UserControl
    {
        public static int month;
        public static string year;
        db db = new db();
        public string ApprovalOrUnapproval = "اعتماد";

        answers.components.answers_ctrl answer;
        public sub_analytics_details(answers.components.answers_ctrl answer1)
        {
            InitializeComponent();
            answer=answer1;
            this.Width = Form1.w;
            this.Height = Form1.h;
            label_month.Text = month.ToString();
            label_year.Text = year;
        }

        private void sub_analytics_details_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            backgroundWorker1.RunWorkerAsync();
            questionnaires_states_Analytics_approval clas = new questionnaires_states_Analytics_approval();
            if(clas.checkApproval(year,month) ==true)
            {
                ApprovalSuccess("اللغاء الاعتماد", Color.Red, Color.White);
                ApprovalOrUnapproval = "اللغاء الاعتماد";
                label_date_approval.Text = clas.getDateApproval(year, month);
                panel_date.Show();
            }
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
           // MessageBox.Show(year + " " + month);
            dataGridView1.Invoke((MethodInvoker)delegate {
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.Rows.Clear();
                    questionnaires_states_Analytics_approval analaytics_db = new questionnaires_states_Analytics_approval();
                    DataTable _dt = new DataTable();
                    _dt = analaytics_db.get_analytics_with_months(year,month);
                    int s = 0;
                    foreach (DataRow row in _dt.Rows)
                    {
                        s++;
                        var section_id = row["section_id"].ToString();
                        var section_type = row["section_type"].ToString();
                        var section_name = row["section_name"].ToString();
                        var send_state = row["send_state"].ToString();
                        var answer_state = row["answer_state"].ToString();
                        var approve_state = row["approve_state"].ToString();
                        var send = btn_send(send_state, 60, 50);
                        var view = view_btn(send_state, 80, 50);
                        var delete = mybitmap(@"D:\MyApp\Questionnaires\images\delete.png", 60, 60);

                         dataGridView1.Rows.Add(s.ToString(),section_id, section_type, section_name, send_state, answer_state, approve_state, send, view,delete);
                        
                    }
                  

                });
            });
        }

       
        public Bitmap btn_send(string send_state, int h, int w)
        {
            string path = @"D:\MyApp\Questionnaires\images\send5.png";
            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            if (send_state == "تم الارسال")
            {
                path = @"D:\MyApp\Questionnaires\images\user_active.png";
                 img = Bitmap.FromFile(path);
                 resized = new Bitmap(img, new Size(50, 50));
            }
            else {
                 img = Bitmap.FromFile(path);
                 resized = new Bitmap(img, new Size(h, w));
                resized.Save("DSC_0000.jpg");
                
                }
            return resized;
            
            
        }
        public Bitmap view_btn(string send_state, int h, int w)
        {
            string path = @"D:\MyApp\Questionnaires\images\view.png";
            if(send_state =="لم يتم الارسال")
            {
                path = @"D:\MyApp\Questionnaires\images\view-disable.png";
            }
            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }
        answers.components.answers_ctrl answer_ctrl;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            var send_state = dataGridView1.Rows[index].Cells[4].Value.ToString();
            if (dataGridView1.CurrentCell == null ||
               dataGridView1.CurrentCell.Value == null ||
               e.RowIndex == -1) return;
            //answer.get(1,"2021",1);
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(8))
            {

                if (send_state != "لم يتم الارسال")
                {
                    int index_row = dataGridView1.CurrentRow.Index;
                    //string _section_name = dataGridView1.Rows[index_row].Cells[2].Value.ToString();
                    int section_id = Convert.ToInt32(dataGridView1.Rows[index_row].Cells[1].Value.ToString());

                    panel_container.Controls.Clear();


                    answers.components.view_control.come_from_ui = "sub_analytics";
                    answers.components.view_control.come_from_section_id = section_id;
                    answers.components.view_control.come_from_year = label_year.Text;
                    answers.components.view_control.come_from_month = Convert.ToInt32(label_month.Text);
                    answers.components.view_control.section_id = section_id;
                    answers.components.view_control.month = month;
                    answers.components.view_control.year = year;
                    answers.components.view_control place_control = new answers.components.view_control(answer_ctrl);
                    DataTable _dt = new DataTable();
                    _dt = db.get_approve_state(section_id, month);
                    string _approve = "";
                    string _date_approve = "";

                    foreach (DataRow row in _dt.Rows)
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
                    place_control.label_section_name.Text = db.get_section_name_from_id_section(section_id);
                    place_control.Width = panel_container.Width;
                    panel_container.Controls.Add(place_control);



                }

                 


            }

            //Delete
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(9))
            {

                if (btnApproval.Text == "تم الاعتماد")
                {
                    MessageBox.Show("عفوا لايمكن الحذف بعد الاعتماد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int index_row = dataGridView1.CurrentRow.Index;
                    //string _section_name = dataGridView1.Rows[index_row].Cells[2].Value.ToString();
                    int section_id = Convert.ToInt32(dataGridView1.Rows[index_row].Cells[1].Value.ToString());

                    questionnaires_states_Analytics_approval ApprovalClass = new questionnaires_states_Analytics_approval();
                    DialogResult deleteResult = MessageBox.Show("هل انت متأكد من الحذف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (deleteResult == DialogResult.Yes)
                    {
                        if (ApprovalClass.deleteSectionApproval(year, month, section_id))
                        {
                            backgroundWorker1.RunWorkerAsync();
                            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }

            }

            //Send Button
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(7))
            {
                //MessageBox.Show("");
                int index_row = dataGridView1.CurrentRow.Index;
                //string _section_name = dataGridView1.Rows[index_row].Cells[2].Value.ToString();
                int section_id = Convert.ToInt32(dataGridView1.Rows[index_row].Cells[1].Value.ToString());
                string send_state2 = dataGridView1.Rows[index_row].Cells[4].Value.ToString();
                
                if(send_state2 =="لم يتم الارسال")
                {
                   
                    if (db.check_has_send_to_web(section_id, year, month) == false)
                    {
                        db.send_questions_for_web(section_id, year, month);
                        //////////-Start insert to sections history-////////
                        db.insert_all_sections_to_tbl_statistics_section_history(year, month);

                        /////////-End insert to sections history-//////////
                        db.insert_to_tbl_statistics_months_history(month, year);
                        MessageBox.Show("تم الارسال بنجاح","تم",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        dataGridView1.Rows.Clear();
                        backgroundWorker1.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("عفوا لقد تم ارسال استبيان سابقا لهذ الشهر", "انتباه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
               
            }

           

                 
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            questionnaires_states_Analytics_approval clas = new questionnaires_states_Analytics_approval();

            if(ApprovalOrUnapproval =="اعتماد")
            {
                if (clas.Approval(year, month))
                {
                    ApprovalSuccess("اللغاء الاعتماد", Color.Red, Color.White);
                    ui ui = new ui();
                    ui.Avarge(label_year.Text, Convert.ToInt32(label_month.Text));
                    ApprovalOrUnapproval = "اللغاء الاعتماد";
                    panel_date.Show();
                   
                    ui.clear_table("tbl_statistics_section_total", label_year.Text, Convert.ToInt32(label_month.Text));
                    ui.clear_table("tbl_statistics_questions_total",label_year.Text, Convert.ToInt32(label_month.Text));
                    ui.clear_table("tbl_statistics_category_total", label_year.Text, Convert.ToInt32(label_month.Text));

                }
            }
            else if (ApprovalOrUnapproval == "اللغاء الاعتماد")
            {
                if (clas.unApproval(year, month))
                {
                    ApprovalSuccess("اعتماد", Color.FromArgb(192, 192, 0), Color.White);
                    ApprovalOrUnapproval = "اعتماد";
                    panel_date.Hide();

                }
            }
        
            
            
            
        }

        public void ApprovalSuccess(string text , Color backColor , Color foreColor)
        {
            btnApproval.Text = text;
            btnApproval.BackColor =backColor ;
            btnApproval.ForeColor = foreColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] months = { 1, 4, 7 };

            MessageBox.Show(months[0].ToString());
        }
    }
}
