using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.quests.components
{
    public partial class question_ctrl : UserControl
    {
        public static int q_id;
        quests.questions_from ques;
        public question_ctrl(quests.questions_from ques1)
        {
            InitializeComponent();
            ques = ques1;
            Dock = DockStyle.Top;
            
        }

        private void question_ctrl_Load(object sender, EventArgs e)
        {
            //if (radioButton1.Checked == true)
            //{
            //    notic_text.Show();
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            db db = new db();
            DialogResult dialogResult = MessageBox.Show("Are you sure for delete the question ?", "Note !", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                db.delete_question(q_id);
                ques.show_all_question();
            }
            
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
