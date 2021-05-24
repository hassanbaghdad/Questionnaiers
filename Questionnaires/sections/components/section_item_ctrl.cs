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
    public partial class depart_item_ctrl : UserControl
    {
        
        String btn_index = screen_work.x.ToString();
        public static int id_depart;
        public static String Depart_name;
        public static int count_q;
        departs.components.screen_work scr;
        public depart_item_ctrl(departs.components.screen_work scr1)
        {
         
          
            
           
           
        }

        public depart_item_ctrl()
        {
            // TODO: Complete member initialization
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void depart_item_ctrl_Load(object sender, EventArgs e)
        {
            if(count_q < 1 )
            {
                pictureBox1.ImageLocation = "../../images/document_0.png";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
         

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            
           
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
