using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.questions.forms
{
    
    public partial class add_question_form : Form
    {
        public static int section_id;
        questions.components.questions_place place;
        public add_question_form(questions.components.questions_place place1)
        {
            InitializeComponent();
            place = place1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            db db = new db();
            db.create_question_for_section(q_text.Text, q_notic.Text, comboBox1.Text, section_id);
            place.bg_w_get_questions.RunWorkerAsync();
        }
    }
}
