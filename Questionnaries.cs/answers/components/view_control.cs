using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.answers.components
{
    public partial class view_control : UserControl
    {
        
        int _section_id;
        public static int section_id;
        public static int month;
        public static string year;
        public static string user_answer;
        answers.components.answers_ctrl main_answer;
        ///Redirection
        public static string come_from_ui;
        public static int come_from_section_id;
        public static string come_from_year;
        public static int come_from_month;

        public view_control(answers.components.answers_ctrl main_answer1)
        {
            InitializeComponent();

           // MessageBox.Show(come_from_ui + "/" + come_from_section_id + "/" + come_from_year + "/" + come_from_month);
           
            main_answer = main_answer1;
            if (answers.components.answers_ctrl.section_id > 0)
            {

                _section_id = answers.components.answers_ctrl.section_id;
                
            }
            else
            {
                _section_id = section_id;
            }
           // MessageBox.Show("section_id : " + section_id.ToString() + " year: " + year + " month:" + month.ToString());
            this.Height = Form1.h;
           // this.Width = Form1.w;
           label_date.Text = year + "/" + month.ToString();
        }

        private void bg_get_view_questions_DoWork(object sender, DoWorkEventArgs e)
        {
            panel1.Invoke((MethodInvoker)delegate {
                panel1.Hide();
                panel1.Controls.Clear();
                db db = new db();
                DataTable _dt = new DataTable();
                _dt = db.get_answers_log_for_section(_section_id,month);
              
                foreach(DataRow row in _dt.Rows)
                {
                    string _answer = row["answer"].ToString();
                    
                    string _answer_edit = "";
                    _answer_edit = row["edit_answer"].ToString();
                    ctl_view_question q_ctrl = new ctl_view_question(this);
                    q_ctrl.Width = 500;
                    if (row["user_answer"].ToString() == "")
                    {
                        q_ctrl.panel_btns_actions.Hide();
                        panel_date.Hide();
                    }
                    if(_answer=="1")
                    {
                        q_ctrl.q_yes.Checked = true;
                    }
                    else if(_answer=="0")
                    {
                        q_ctrl.q_no_r.Checked = true;
                    }
                    if (_answer_edit == "تم التعديل")
                    {
                        q_ctrl.label_assetment.ForeColor = Color.Gray;
                        q_ctrl.has_edited = true;
                        q_ctrl.change_colors_assetment(q_ctrl.panel_edit);
                        q_ctrl.panel_assetment_after_edit.Visible = true;
                        q_ctrl.editing_radio = true;
                        int _assetment_after_edit = Convert.ToInt32(row["event_assetment_after_edit"].ToString());
                        if (_assetment_after_edit == 1)
                        {
                            q_ctrl.radio_value.Text = q_ctrl.r_edit_1.Name;
                            q_ctrl.r_edit_1.Checked = true;
                        }
                        if (_assetment_after_edit == 2)
                        {
                            q_ctrl.radio_value.Text = q_ctrl.r_edit_2.Name;
                            q_ctrl.r_edit_2.Checked = true;
                        }
                        if (_assetment_after_edit == 3)
                        {
                            q_ctrl.radio_value.Text = q_ctrl.r_edit_3.Name;
                            q_ctrl.r_edit_3.Checked = true;
                        }
                        if (_assetment_after_edit == 4)
                        {
                            q_ctrl.radio_value.Text = q_ctrl.r_edit_4.Name;
                            q_ctrl.r_edit_4.Checked = true;
                        }
                        if (_assetment_after_edit == 5)
                        {
                            q_ctrl.radio_value.Text = q_ctrl.r_edit_5.Name;
                            q_ctrl.r_edit_5.Checked = true;
                        }
                        q_ctrl.editing_radio = false;
                    }
                    else
                    {
                        q_ctrl.label_assetment.Enabled = true;
                    }
                    q_ctrl.Width = Form1.w;
                    q_ctrl.groupBox2.Width = Form1.w;
                    q_ctrl.q_no.Text = row["id_q"].ToString();
                    q_ctrl.q_text.Text = row["q_text"].ToString();
                    q_ctrl.txt_event_type.Text = row["event_type"].ToString();
                    q_ctrl.txt_event_details.Text = row["event_details"].ToString();
                    q_ctrl.txt_event_impact.Text = row["event_impact"].ToString();
                    q_ctrl.txt_event_source.Text = row["event_source"].ToString();
                    q_ctrl.txt_notic.Text = row["event_notic"].ToString();
                    //q_ctrl.label_q_type2.Text = row["q_type"].ToString();
                    q_ctrl.richTextBox1.Text = row["q_type"].ToString();
                    string _updated_at = row["updated_at"].ToString();
                    if(_updated_at != "")
                    {
                        q_ctrl.panel_update_at.Show();
                        q_ctrl.label_update_at.Text = _updated_at;
                    }
                    
                    int _assetment = Convert.ToInt32(row["event_assetment"].ToString());
                    if (_assetment == 1)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_1.Name;
                        q_ctrl.radio_1.Checked = true;
                        
                    }
                    if (_assetment == 2)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_2.Name;
                        q_ctrl.radio_2.Checked = true;
                        
                    }
                    if (_assetment == 3)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_3.Name;
                        q_ctrl.radio_3.Checked = true;
                        
                    }
                    if (_assetment == 4)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_4.Name;
                        q_ctrl.radio_4.Checked = true;
                        
                    }
                    if (_assetment == 5)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_5.Name;
                        q_ctrl.radio_5.Checked = true;
                        
                    }
                    user_answer = row["user_answer"].ToString();
                    panel1.Controls.Add(q_ctrl);

                    if(user_answer !="")
                    {
                        btn_renable_in_view_palce.Show();
                    }
                    
                }
                panel1.Show();
            });
            
        }

        private void view_control_Load(object sender, EventArgs e)
        {
            questionnaires_states_Analytics_approval approval_class = new questionnaires_states_Analytics_approval();
            if(approval_class.checkApproval(come_from_year , come_from_month)==true)
            {
                btn_approve.Enabled = false;
                btn_approve.BackColor = Color.LightGray;
                btn_approve.FlatStyle = FlatStyle.Standard;


                btn_renable_in_view_palce.Enabled = false;
                btn_renable_in_view_palce.BackColor = Color.LightGray;
                btn_renable_in_view_palce.FlatStyle = FlatStyle.Standard;
            }
            bg_get_view_questions.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db db = new db();
            db.approve_answer(_section_id, year,month,btn_approve.Text);
            //try
            //{
            
            //a.get(come_from_section_id, come_from_year, come_from_month);
                //main_answer.get(come_from_section_id, come_from_year, come_from_month);
            //main_answer.Width = 44;
            //MessageBox.Show(come_from_section_id.ToString()+" "+ come_from_year+ " " + come_from_month.ToString());
            get2(come_from_section_id, come_from_year, come_from_month);
            //}catch{

            //}
            
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        answers_ctrl test_answer;
        private void button1_Click(object sender, EventArgs e)
        {
            //Renable Button

            
            if (user_answer !="")
            {
                DialogResult result = MessageBox.Show("هل انت متأكد من رغبتك بأعادة تفعيل الاستبيان لهذا الشهر ؟", "انتباه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {


                    db db = new db();
                    if (!db.check_approve_month(year, month, _section_id))
                    {
                        db.renable(_section_id, year, month);
                        // backgroundWorker1.RunWorkerAsync();

                        this.Controls.Clear();
                        
                        
                        
                        if (come_from_ui == "questionnaiers")
                        {
                            answers.components.answers_ctrl.section_id = come_from_section_id;
                            answers.components.answers_ctrl.year = come_from_year;
                            answers.components.answers_ctrl answers_control = new answers.components.answers_ctrl();
                            answers_control.Width = Form1.w;
                            answers_control.Height = Form1.h;
                            this.Controls.Add(answers_control);

                        }
                        if (come_from_ui == "sub_analytics")
                        {
                            sections.components.sub_analytics_details.month = come_from_month;
                            sections.components.sub_analytics_details.year = come_from_year;
                            sections.components.sub_analytics_details sub = new sections.components.sub_analytics_details(test_answer);
                            this.Controls.Add(sub);
                        }

                        
                        
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

        answers_ctrl answer;
        public void get2(int section_id,string year , int month)
        {

            db db = new db();
            // month = Convert.ToInt32(dataGridView1.Rows[index_row].Cells[1].Value.ToString());
            // year = dataGridView1.Rows[index_row].Cells[2].Value.ToString();
            this.Controls.Clear();
            view_control.month = month;
            view_control.year = year;
            view_control place_control = new view_control(answer);
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

            db db2 = new db();

            place_control.label_section_name.Text = db2.get_section_name_from_id_section(section_id);
            place_control.Width = panel1.Width;
            this.Controls.Add(place_control);
        }

        private void btn_approve_TextChanged(object sender, EventArgs e)
        {
            
        }



        
    }
}
