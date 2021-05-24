using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.answers
{
    public partial class answers_form : Form
    {
        public static int depart_id;
        public answers_form( int id)
        {
            InitializeComponent();
            depart_id = id;
        }

        private void answers_form_Load(object sender, EventArgs e)
        {
            
            db db = new db();
            SqlDataReader _reader = db.get_answers_department(depart_id);
            
            while(_reader.Read())
            {
                
                //dataGridView1.Rows.Add();
            }
        }
    }
}
