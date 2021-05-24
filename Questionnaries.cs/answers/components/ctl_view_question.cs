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
    public partial class ctl_view_question : UserControl
    {
        bool edit = true;
       public  bool has_edited = false;
        view_control view_place;
        public ctl_view_question(view_control view_place1)
        {
            InitializeComponent();
            view_place = view_place1;
            this.Dock = DockStyle.Top;
            this.Width = Form1.w;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label_assetment.ForeColor = Color.Gray;
            panel_assetment_after_edit.Visible = true;
            change_colors_assetment(panel_edit);
            panel_assetment_after_edit.Enabled = true;
            editing_radio = true;
            pic_clear_edits.Visible = true;
            pic_save.Visible = true;
            pic_reset.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(edit ==true)
            {
                if(r_edit_1.Checked == false && r_edit_2.Checked == false && r_edit_3.Checked == false && r_edit_4.Checked == false && r_edit_5.Checked == false )
                {
                    MessageBox.Show("يرجى اختيار احدى التقييمات للتعديل !", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int degree = 0;
                    if (r_edit_1.Checked)
                    {
                        degree = 1;
                    }
                    if (r_edit_2.Checked)
                    {
                        degree = 2;
                    }
                    if (r_edit_3.Checked)
                    {
                        degree = 3;
                    }
                    if (r_edit_4.Checked)
                    {
                        degree = 4;
                    }
                    if (r_edit_5.Checked)
                    {
                        degree = 5;
                    }

                    db db = new db();
                   // MessageBox.Show("id :" + view_control.section_id + " month: " + view_control.month + " q_no no :" + Convert.ToInt32(q_no.Text) + " degree: " + degree);
                    db.edit_answer(view_control.section_id, view_control.month, Convert.ToInt32(q_no.Text), degree);
                   
                    editing_radio = false;
                    pic_clear_edits.Visible = false;
                    view_place.bg_get_view_questions.RunWorkerAsync();
                    pic_reset.Hide();
                    pic_save.Hide();
                    pic_clear_edits.Hide();
                }
                
            }
            
        }
        public void change_colors_assetment(Control panel)
        {
            foreach (Control _panel in panel.Controls)
            {
                if (_panel is Panel)
                {
                    _panel.BackColor = Color.FromArgb(224, 224, 224);
                    _panel.ForeColor = Color.Gray;
                }
                foreach (Control radio in _panel.Controls)
                {
                    if (radio is RadioButton)
                    {
                        radio.BackColor = Color.FromArgb(224, 224, 224);
                        radio.ForeColor = Color.Gray;
                    }

                }

            }
            
           

        }

        private void radio_1_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(radio_1, panel_edit);
        }
        public bool editing_radio = false;
        public void anti_check(RadioButton radio , Control panel)
        {
            if(panel.Name == panel_assetment_after_edit.Name)
            {
                if(editing_radio == false)
                {
                    var checkbox_name = radio_value.Text;
                    foreach (Control r in panel.Controls)
                    {
                        if (r is RadioButton && r.Name != checkbox_name)
                        {
                            ((RadioButton)r).Checked = false;
                        }
                    }
                    foreach (Control r in panel.Controls)
                    {
                        if (r is RadioButton && r.Name == checkbox_name)
                        {
                            ((RadioButton)r).Checked = true;
                        }
                    }
                }
            }
            else
            {
                var checkbox_name = checkbox_value.Text;
                foreach (Control r in panel.Controls)
                {
                    if (r is RadioButton && r.Name != checkbox_name)
                    {
                        ((RadioButton)r).Checked = false;
                    }
                }
                foreach (Control r in panel.Controls)
                {
                    if (r is RadioButton && r.Name == checkbox_name)
                    {
                        ((RadioButton)r).Checked = true;
                    }
                }
            }
            

        }

        private void radio_2_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(radio_2, panel_edit);
        }

        private void radio_3_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(radio_3, panel_edit);
        }

        private void radio_4_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(radio_4, panel_edit);
        }

        private void radio_5_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(radio_5, panel_edit);
        }

        private void r_edit_1_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(r_edit_1, panel_assetment_after_edit);
        }

        private void r_edit_2_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(r_edit_2, panel_assetment_after_edit);
        }

        private void r_edit_3_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(r_edit_3, panel_assetment_after_edit);
        }

        private void r_edit_4_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(r_edit_4, panel_assetment_after_edit);
        }

        private void r_edit_5_CheckedChanged(object sender, EventArgs e)
        {
            anti_check(r_edit_5, panel_assetment_after_edit);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل انت متأكد من اللغاء التغييرات ؟ ", "انتباه !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (has_edited == false)
                {
                    edit = true;
                    panel_assetment_after_edit.Visible = false;
                    clear_check();
                }
                label_assetment.ForeColor = Color.Black;
                pic_clear_edits.Hide();
                pic_save.Hide();
                pic_reset.Hide();
                view_place.bg_get_view_questions.RunWorkerAsync();
            }
           
        }
        public void clear_check()
        {
            
              
                    var checkbox_name = radio_value.Text;
                    //MessageBox.Show(checkbox_name);
                    foreach (Control r in panel_assetment_after_edit.Controls)
                    {
                        if (r is RadioButton && r.Name != checkbox_name)
                        {
                            ((RadioButton)r).Checked = false;
                           // MessageBox.Show("");
                        }
                    }
                    foreach (Control r in panel_assetment_after_edit.Controls)
                    {
                        if (r is RadioButton && r.Name == checkbox_name)
                        {
                            ((RadioButton)r).Checked = true;
                        }
                    }
                }

        private void label_q_id_Click(object sender, EventArgs e)
        {

        }

        private void pic_reset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل انت متاكد من ارجاع التقييم الى الحالة الاصلية ؟ ", "انتباه !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                db db = new db();
                db.reset_edits(answers_ctrl.section_id, view_control.month, Convert.ToInt32(q_no.Text));
                view_place.bg_get_view_questions.RunWorkerAsync();
            }
           
        }

        private void label_q_type2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ctl_view_question_Load(object sender, EventArgs e)
        {
            disable_editing();
            txt_event_details.Enabled = false;
            txt_event_impact.Enabled = false;
            txt_event_source.Enabled = false;
            txt_event_type.Enabled = false;
        }

        public void disable_editing()
        {
            db db = new db();
            string _year = view_control.come_from_year;
            int _month = view_control.come_from_month;
            int _section_id = view_control.come_from_section_id;

            bool approve_state = db.check_approve_month(_year, _month, _section_id);
            //MessageBox.Show(_year + " " + _month.ToString() + "   " + _section_id.ToString());
           // MessageBox.Show(approve_state.ToString());
            if (q_no_r.Checked == true || approve_state == true)
            {
                panel_btns_actions.Enabled = false;
                pictureBox1.ImageLocation = @"D:\MyApp\Questionnaires\images\edit-disable.png";
                panel3.Enabled = false;
            }

        }
            
        
    }
}
