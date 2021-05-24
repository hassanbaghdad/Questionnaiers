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
    public partial class add_user_from_create_section : Form
    {
        public add_user_from_create_section()
        {
            InitializeComponent();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            ui ui = new ui();
            ui.placeholder(textBox1);
        }

        private void add_user_from_create_section_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
