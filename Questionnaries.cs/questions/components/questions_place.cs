using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.questions.components
{
    public partial class questions_place : UserControl
    {
        public static int section_id;
        sections.components.sections_place place;
        public questions_place(sections.components.sections_place place1)
        {
            InitializeComponent();
            place = place1;
            labedl_id.Text = section_id.ToString();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //add_question add_question = new add_question();
            //add_question.Width = panel1.Width;
            //panel1.Controls.Clear();
            //panel1.Controls.Add(add_question);
            forms.add_question_form.section_id = section_id;
            forms.add_question_form add_form = new forms.add_question_form(this);
            
            add_form.Show();
        }

        private void questions_place_Load(object sender, EventArgs e)
        {

            bg_w_get_questions.RunWorkerAsync();
        }

        public void get_questions()
        {
          
            
        }

        private void panel1_Validated(object sender, EventArgs e)
        {
         
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            panel1.Invoke((MethodInvoker)delegate
            {
                panel1.Hide();
                panel1.Controls.Clear();
                db db = new db();
                DataTable _dt = new DataTable();
                _dt = db.get_questions_section(Convert.ToInt32(labedl_id.Text));

              

                foreach (DataRow row in _dt.Rows)
                {
                    // MessageBox.Show(row["q_type"].ToString());
                    question_ctrl.section_id = section_id;
                    question_ctrl.id_q = Convert.ToInt32(row["id"].ToString());
                    question_ctrl q_item = new question_ctrl(this);
                    q_item.q_no.Text = row["section_q_secunce"].ToString();
                    q_item.q_type.Text = row["q_type"].ToString();
                    q_item.q_text.Text = row["q_text"].ToString();
                    q_item.txt_event_type.Text = row["event_type"].ToString();
                    q_item.txt_event_details.Text = row["event_details"].ToString();
                    q_item.txt_event_impact.Text = row["event_impact"].ToString();
                    q_item.txt_event_source.Text = row["event_source"].ToString();
                    q_item.txt_notic.Text = row["notic"].ToString();
                    switch (row["event_assetment"].ToString())
                    {
                        case "1":
                            q_item.radio_1.Checked = true;
                            break;
                        case "2":
                            q_item.radio_2.Checked = true;
                            break;
                        case "3":
                            q_item.radio_3.Checked = true;
                            break;
                        case "4":
                            q_item.radio_4.Checked = true;
                            break;
                        case "5":
                            q_item.radio_5.Checked = true;
                            break;
                    }

                    if (row["answer"].ToString() == "1")
                    {
                        q_item.q_yes.Checked = true;
                    }
                    else
                    {
                        q_item.q_no_r.Checked = true;
                    }
                    q_item.Width = panel1.Width;
                    q_item.groupBox2.Width = panel1.Width - 60;
                    q_item.groupBox1.Width = q_item.groupBox2.Width - 200;
                    q_item.groupBox3.Width = q_item.groupBox2.Width - 200;

                    panel1.Controls.Add(q_item);
                    panel1.Show();
                }
            });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}
