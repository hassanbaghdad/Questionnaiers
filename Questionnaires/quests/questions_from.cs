using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.quests
{
    public partial class questions_from : Form
    {
        public static int id_depart_to_questions = departs.components.screen_work.x;
        public static int id_q_to_delete ;
        public static int id_depart;
        
        public int h, w;
        public questions_from(int id)
        {
            InitializeComponent();
            id_depart = id;
           // MessageBox.Show(id_depart.ToString());
            
        }

        private void questions_from_Load(object sender, EventArgs e)
        {
            show_all_question();
            w = panel1.Width;
        }

        public void show_all_question()
        {
            panel1.Controls.Clear();
            DataTable _dt = new DataTable();
            db db = new db();
            _dt = db.get_questions_depart(id_depart);
            foreach (DataRow row in _dt.Rows)
            {
                quests.components.question_ctrl.q_id = Convert.ToInt32(row["id"].ToString());
                //departs.components.depart_item_ctrl.count_q= db.get_count_questions_depart(id_depart);

                components.question_ctrl qu = new components.question_ctrl(this);
               // qu.label1.Text = row["q_text"].ToString();
               // qu.label_date.Text = row["updated_at"].ToString();
                qu.Width = panel1.Width;
                
                

                if (row["answer1"].ToString() == "1")
                {
                  //  qu.radioButton1.Checked = true;
                 //   qu.notic_text.Text = row["notic"].ToString();
                  //  qu.notic_text.Show();
                }
                else
                {
                  //  qu.radioButton2.Checked = true;
                   // qu.notic_text.Hide();
                }

                panel1.Controls.Add(qu);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            quests.components.add_question add = new components.add_question(this,id_depart);
          //  quests.components.add_question.id_depart = id_depart_to_questions;
            add.Show();
        }
    }
}
