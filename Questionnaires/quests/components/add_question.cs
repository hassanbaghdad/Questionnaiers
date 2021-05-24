using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.quests.components
{
    public partial class add_question : Form
    {
        public static int id_depart;
        quests.questions_from q_work;
        public add_question(quests.questions_from q_work1,int id_de)
        {
            InitializeComponent();
            q_work = q_work1;
            id_depart = id_de;
           // MessageBox.Show(id_depart.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(id_depart2.ToString());
            db db = new db();
            db.create_question_for_department(q_text.Text ,q_notic.Text,id_depart);
            q_work.show_all_question();

        }

        private void add_question_Load(object sender, EventArgs e)
        {
            //combo_to.DataSource
            //db db = new db();
            //DataTable _dt = new DataTable();
            //_dt = db.get_departs_name();
            //List<string> departments = new List<string>();
            //foreach (DataRow row in _dt.Rows)
            //{
            //    departments.Add(row["department_name"].ToString());
            //}

            //combo_to.DataSource = departments;
        }

        private void q_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void q_text_Click(object sender, EventArgs e)
        {
            q_text.ForeColor = Color.Black;
            q_text.Clear();
        }
    }
}
