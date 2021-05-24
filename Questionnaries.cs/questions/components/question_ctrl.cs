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
    public partial class question_ctrl : UserControl
    {
        public static int id_q;
        public static int id;
        public static int section_id;
        questions_place place;
        public question_ctrl(questions_place place1)
        {
            InitializeComponent();
            refresh_list_categories();
            place = place1;
            this.Dock = DockStyle.Top;
            label_q_id.Text = id_q.ToString();
            label_id.Text = id.ToString();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            db db = new db();
            db.create_category(q_type.Text);
            refresh_list_categories();
        }

        private void question_ctrl_Load(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            btn_plus.ForeColor = Color.White;
            btn_minse.ForeColor = Color.White;
            groupBox3.Enabled = false;

        }

        public void refresh_list_categories()
        {
            db db = new db();
            List<string> list = new List<string>();
            DataTable _dt = new DataTable();
            _dt = db.get_all_cat();
            foreach (DataRow row in _dt.Rows)
            {
                list.Add(row[0].ToString());
            }

            q_type.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db db = new db();
            db.delete_category(q_type.Text);
            refresh_list_categories();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            DialogResult res = MessageBox.Show("Are you sure delete this question ?","Warining",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(res == DialogResult.Yes)
            {
                db db = new db();
                
                db.delete_question(Convert.ToInt32(q_no.Text),section_id);
                place.bg_w_get_questions.RunWorkerAsync();
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            db db = new db();
            db.edit_question(q_text.Text, txt_notic.Text,q_type.Text, questions_place.section_id,Convert.ToInt32(label_q_id.Text));
            place.get_questions();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
