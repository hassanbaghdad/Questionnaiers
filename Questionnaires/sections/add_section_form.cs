using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.departs.components
{
    
    public partial class add_depart_form : Form
    {
       
      public add_depart_form(departs.components.screen_work screen1)
        {
            InitializeComponent();
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            add_user_from_create_section adduser = new add_user_from_create_section();
            adduser.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
   
}
