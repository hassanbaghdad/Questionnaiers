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
    public partial class add_question : UserControl
    {
        public static int id_section;
        public add_question()
        {
            InitializeComponent();
            label_id.Text = id_section.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(label_id.Text);
            db db = new db();
            db.create_question_for_section(q_text.Text,q_notic.Text,comboBox1.Text,Convert.ToInt32(label_id.Text));
        }

        private void add_question_Load(object sender, EventArgs e)
        {
            db db = new db();
            List<string> list = new List<string>();
            DataTable _dt = new DataTable();
            _dt = db.get_all_cat();
            foreach(DataRow row in _dt.Rows)
            {
                list.Add(row[0].ToString());
            }

            comboBox1.DataSource = list;

        }
    }
}
