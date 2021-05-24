using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.departs.components
{
    
    public partial class screen_work : UserControl
    {
        public static int h, w;
        public static int x;
        public static int id_depart;
        public static int is_enabled;
        
       

        public screen_work()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void delete(String target)
        {
            
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void screen_work_Load(object sender, EventArgs e)
        {
            h = panel1.Height;
            w = panel1.Width;
            get_departs();
           
        }

        public void get_departs()
        {
            //panel1.Controls.Clear();
            //db db = new db();
            //DataTable _dt = db.get_departments();

           
            //foreach (DataRow row in _dt.Rows)
            //{
                
            //    id_depart = Convert.ToInt32(row["id"].ToString());
            //    depart_item_ctrl.id_depart = Convert.ToInt32(row["id"].ToString());
            //    depart_item_ctrl.count_q = db.get_count_questions_depart(id_depart);
            //    is_enabled = Convert.ToInt32(row["enable"].ToString());

            //    departs.components.depart_item_ctrl _item = new depart_item_ctrl(this);
                
            //    _item.label1.Text = row[1].ToString();
            //    _item.Width = w;
                
            //    panel1.Controls.Add(_item);
            //}
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            add_depart_form add_form = new add_depart_form(this);
            add_form.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            
            

        }
        public void push_unpush()
        {
            DialogResult dialogResult = MessageBox.Show("Are you want push all questionnaires for departments ?", "Push !", MessageBoxButtons.YesNo);


            if (dialogResult == DialogResult.Yes)
            {
                db db = new db();
                db.push_all_questionnaires();
                get_departs();
            }
            if (dialogResult == DialogResult.No)
            {
                db db = new db();
                db.unpush_all_questionnarires();
                get_departs();
                checkBox1.Checked = false;
            }

             
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            push_unpush();
        }
    }
}
