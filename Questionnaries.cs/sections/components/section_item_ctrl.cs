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
    public partial class section_item_ctrl : UserControl
    {
        public static int section_id;
        public static string section_name;
        sections_place place;
        
        Form1 form;

        public section_item_ctrl(sections_place place1, Form1 form1)
        {
            InitializeComponent();
            place = place1;
            
            form = form1;
            this.Dock = DockStyle.Top;
            label1.Text = section_name;
            label_id.Text = section_id.ToString();
            checkBox1.Name = label_id.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            place.uncheck_all_checkboxes();
            checkBox1.Checked = true;
            place.btn_send.PerformClick();
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            forms.edit_section_form.id_section = Convert.ToInt32(label_id.Text);
            users.add_users.section_id = Convert.ToInt32(label_id.Text);
            forms.edit_section_form edit_section_form = new forms.edit_section_form(place);
            edit_section_form.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            questions.components.questions_place.section_id = Convert.ToInt32(label_id.Text);
          //  questions.components.question_ctrl.id = section_id;
            questions.components.add_question.id_section = Convert.ToInt32(label_id.Text);
            questions.components.questions_place question_place = new questions.components.questions_place(place);
            question_place.Width = form.panel2.Width;
            question_place.Height = form.panel2.Height;
            
            form.panel2.Controls.Clear();
            form.panel2.Controls.Add(question_place);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure delete all questions for this section ?", "Delete !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                db db = new db();
                db.delete_all_question(Convert.ToInt32(label_id.Text));
                place.backgroundWorker1.RunWorkerAsync();
            }
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            db db = new db();
            answers.components.answers_ctrl.section_id = Convert.ToInt32(label_id.Text); ;
            answers.components.answers_ctrl item = new answers.components.answers_ctrl();
            item.label_section_name.Text = db.get_section_name_from_id_section(Convert.ToInt32(label_id.Text));
            item.Width = form.panel2.Width;
            item.Height = form.panel2.Height;
            form.panel2.Controls.Clear();
            form.panel2.Controls.Add(item);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure delete this section ?", "Delete !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                db db = new db();
                db.delete_section(Convert.ToInt32(label_id.Text));
                place.backgroundWorker1.RunWorkerAsync();
            }
           
        }
    }
}
