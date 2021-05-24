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
    public partial class sections_place : UserControl
    {
        Form1 form;
        
        public sections_place(Form1 form1)
        {
            InitializeComponent();
            form = form1;
           
        }

        private void sections_place_Load(object sender, EventArgs e)
        {
           // get_sections();

            backgroundWorker1.RunWorkerAsync();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            forms.add_section_form add_section = new forms.add_section_form(this);
            add_section.Show();
        }

        public void get_sections()
        {
           


        }
        
        
        private void button7_Click(object sender, EventArgs e)
        {
            if(checkboxes() == true)
            {
                forms.date_form date = new forms.date_form(this);
                date.Show();
            }
            else
            {
                MessageBox.Show("You don't select any section !");
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            check_all_checkboxes();
            if(checkBox1.Checked ==true)
            {
                check_all_checkboxes();
            }
            else
            {
                uncheck_all_checkboxes();
            }
        }

        public bool checkboxes()
        {
            bool _checked = false;

            var sections = panel2.Controls;
            foreach (Control section in sections)
            {
                var groups = section.Controls;

                foreach (Control tbl_layout in groups)
                {

                    var groups_in_tbl_layout = tbl_layout.Controls;

                    foreach (Control tbl in groups_in_tbl_layout)
                    {
                        if (tbl is CheckBox)
                        {
                            MessageBox.Show(tbl.Name + " 1");
                        }
                        var groups2 = tbl.Controls;
                        foreach (Control group2 in groups2)
                        {
                            if (group2 is CheckBox)
                            {
                                MessageBox.Show(group2.Name + " 2");
                            }
                            foreach (Control c1 in group2.Controls)
                            {
                                if (c1 is CheckBox && ((CheckBox)c1).Checked)
                                {
                                    _checked = true;
                                }


                            }

                        }

                    }
                }
            }
          
                               

   
            return _checked;
        }
        public void check_all_checkboxes()
        {

            var sections = panel2.Controls;
            foreach (Control section in sections)
            {
                var groups = section.Controls;

                foreach (Control tbl_layout in groups)
                {

                    var groups_in_tbl_layout = tbl_layout.Controls;

                    foreach (Control tbl in groups_in_tbl_layout)
                    {
                        if (tbl is CheckBox)
                        {
                            MessageBox.Show(tbl.Name + " 1");
                        }
                        var groups2 = tbl.Controls;
                        foreach (Control group2 in groups2)
                        {
                            if (group2 is CheckBox)
                            {
                                MessageBox.Show(group2.Name + " 2");
                            }
                            foreach (Control c1 in group2.Controls)
                            {
                                if (c1 is CheckBox )
                                {
                                    ((CheckBox)c1).Checked = true;
                                }


                            }

                        }

                    }
                }

            }
        }
        public void uncheck_all_checkboxes()
        {

            var sections = panel2.Controls;
            foreach (Control section in sections)
            {
                var groups = section.Controls;

                foreach (Control tbl_layout in groups)
                {

                    var groups_in_tbl_layout = tbl_layout.Controls;

                    foreach (Control tbl in groups_in_tbl_layout)
                    {
                        if (tbl is CheckBox)
                        {
                            MessageBox.Show(tbl.Name + " 1");
                        }
                        var groups2 = tbl.Controls;
                        foreach (Control group2 in groups2)
                        {
                            if (group2 is CheckBox)
                            {
                                MessageBox.Show(group2.Name + " 2");
                            }
                            foreach (Control c1 in group2.Controls)
                            {
                                if (c1 is CheckBox )
                                {
                                    ((CheckBox)c1).Checked = false;
                                }


                            }

                        }

                    }
                }
            }
        }
        public bool send_questions_for_checked_sections(string year , int month)
        {
            bool has_send = false;
             var sections = panel2.Controls;
             foreach (Control section in sections)
             {
                 var groups = section.Controls;

                 foreach (Control tbl_layout in groups)
                 {

                     var groups_in_tbl_layout = tbl_layout.Controls;

                     foreach (Control tbl in groups_in_tbl_layout)
                     {
                         if (tbl is CheckBox)
                         {
                             MessageBox.Show(tbl.Name + " 1");
                         }
                         var groups2 = tbl.Controls;
                         foreach (Control group2 in groups2)
                         {
                             if (group2 is CheckBox)
                             {
                                 MessageBox.Show(group2.Name + " 2");
                             }
                             foreach (Control c1 in group2.Controls)
                             {
                                 if (c1 is CheckBox && ((CheckBox)c1).Checked)
                                 {
                                     db db = new db();
                                     if(db.check_has_send_to_web(Convert.ToInt32(c1.Name), year, month)==false)
                                     {
                                         questionnaires_states_Analytics_approval approval_class = new questionnaires_states_Analytics_approval();
                                         if (!approval_class.checkApproval(year,month))
                                         {
                                             if(approval_class.check_last_month(year,month,"insert"))
                                             {
                                                     //MessageBox.Show(Convert.ToInt32(c1.Name)+" " + year + " " + month);
                                                 db.send_questions_for_web(Convert.ToInt32(c1.Name), year, month);
                                                 //////////-Start insert to sections history-////////
                                                 db.insert_all_sections_to_tbl_statistics_section_history(year, month);

                                                 /////////-End insert to sections history-//////////
                                                 db.insert_to_tbl_statistics_months_history(month, year);
                                                 has_send = true;
                                             }
                                             
                                         }
                                         else
                                         {
                                             MessageBox.Show("عفوا لايمكن الارسال لهذا الشهر كونه تم اعتماده ", "انتباه", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                         }
                                         
                                     }
                                     else
                                     {
                                         MessageBox.Show("عفوا لقد تم ارسال استبيان سابقا لهذ الشهر", "انتباه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                     }
                                     

                                 }


                             }

                         }

                     }
                 }
             }
             return has_send;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            panel2.Invoke((MethodInvoker)delegate {
                panel2.Hide();
            });
            
            db db = new db();
            TopPanelDepartments top_departments = new TopPanelDepartments();
            panel2.Invoke((MethodInvoker)delegate
            {
                panel2.Controls.Clear();
            });
            
            this.Width = Form1.w;
            //panel1.Height = Form1.h;
            int w = panel1.Width;
            //
            //
            DataTable _dt_branchs = new DataTable();

            _dt_branchs = db.get_branchs();




            if (_dt_branchs.Rows.Count > 0)
            {
                // MessageBox.Show("");

            }
            foreach (DataRow row in _dt_branchs.Rows)
            {



                section_item_ctrl.section_id = Convert.ToInt32(row["id"].ToString());
                section_item_ctrl.section_name = row["name"].ToString();
                string login_state = row["login_state"].ToString();
                
                section_item_ctrl item = new section_item_ctrl(this, form);
                item.label_last_seen.Text = row["last_seen_date"].ToString();
                if (login_state == "0")
                {
                    item.label_login_state.Text = "Offline";
                    item.pic_login_state.ImageLocation = @"../../images/offline2.png";
                }
                else
                {
                    item.label_login_state.Text = "Online";
                    item.pic_login_state.ImageLocation = @"../../images/online9.png";
                    item.panel_last_seen.Hide();
                }
                item.label_address.Text = row["address"].ToString();
                item.label_manager.Text = row["manager"].ToString();
                item.label_phone.Text = row["phone"].ToString();
                var count_questions = db.get_count_questions_section(Convert.ToInt32(item.label_id.Text));
                var count_answers = db.get_count_answers_section(Convert.ToInt32(item.label_id.Text));
                
                if (count_questions > 0)
                {
                    item.pic_box_questions.ImageLocation = @"../../images/document-icon.png";
                    item.pic_box_delete_question.ImageLocation = @"../../images/document-delete3.png";
                    item.pic_box_send.ImageLocation = @"../../images/send4.png";
                    item.pic_box_send.Enabled = true;
                    item.pic_box_delete_question.Enabled = true;
                    
                }
                if (count_answers > 0)
                {
                    item.pic_box_ansers.ImageLocation = @"../../images/list-512.png";
                    item.pic_box_ansers.Enabled = true;
                    
                }
                item.Width = w;
                //if (row["section_type"].ToString() == "department")
                //{
                //    item.groupBox1.BackColor = Color.FromArgb(226, 226, 226);
                //}
                panel2.Invoke((MethodInvoker)delegate
                {
                    panel2.Controls.Add(item);
                });
               
                
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////
            TopPanelBranchs top_panel_branchs = new TopPanelBranchs();
            top_panel_branchs.Width = w;
            panel2.Invoke((MethodInvoker)delegate {
                panel2.Controls.Add(top_panel_branchs);
            });

            
            //
            DataTable _dt_departments = new DataTable();

            _dt_departments = db.get_departments();



            top_departments.Width = w;
            foreach (DataRow row in _dt_departments.Rows)
            {
                section_item_ctrl.section_id = Convert.ToInt32(row["id"].ToString());
                section_item_ctrl.section_name = row["name"].ToString();
                section_item_ctrl item = new section_item_ctrl(this, form);
                item.label_address.Text = row["address"].ToString();
                item.label_manager.Text = row["manager"].ToString();
                item.label_phone.Text = row["phone"].ToString();
                item.Width = w;
                string login_state = row["login_state"].ToString();
                item.label_last_seen.Text = row["last_seen_date"].ToString();
                if (login_state == "0")
                {
                    item.label_login_state.Text = "Offline";
                    item.pic_login_state.ImageLocation = @"../../images/offline2.png";

                }
                else
                {
                    item.label_login_state.Text = "Online";
                    item.pic_login_state.ImageLocation = @"../../images/online9.png";
                    item.panel_last_seen.Hide();

                }
                var count_questions = db.get_count_questions_section(Convert.ToInt32(item.label_id.Text));
                var count_answers = db.get_count_answers_section(Convert.ToInt32(item.label_id.Text));
                
                if (count_questions > 0)
                {
                    item.pic_box_questions.ImageLocation = @"../../images/document-icon.png";
                    item.pic_box_delete_question.ImageLocation = @"../../images/document-delete3.png";
                    item.pic_box_send.ImageLocation = @"../../images/send4.png";
                    item.pic_box_send.Enabled = true;
                    item.pic_box_delete_question.Enabled = true;

                }
                if (count_answers > 0)
                {
                    item.pic_box_ansers.ImageLocation = @"../../images/list-512.png";
                    item.pic_box_ansers.Enabled = true;

                }
                //if (row["section_type"].ToString() == "department")
                //{
                //    item.groupBox1.BackColor = Color.FromArgb(226, 226, 226);
                //}
                panel2.Invoke((MethodInvoker)delegate {
                    panel2.Controls.Add(item);
                });
                
            }

            if (_dt_departments.Rows.Count > 0)
            {
                panel2.Invoke((MethodInvoker)delegate {
                    panel2.Controls.Add(top_departments);
                });

                

            }

            panel2.Invoke((MethodInvoker)delegate
            {
                panel2.Show();
            });

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            questionnaires_states_analytics_approval analytics = new questionnaires_states_analytics_approval();
            panel1.Controls.Add(analytics);
        }
    }
}
