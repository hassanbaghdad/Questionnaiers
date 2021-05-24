using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs
{
    public partial class Form1 : Form
    {
        public static int w, h;
        
        public Form1()
        {
            InitializeComponent();
            panel2.Height = 900;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // sections.forms.add_section_form add_section_form = new sections.forms.add_section_form();
            //add_section_form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //resize();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Button5_Click(object sender, EventArgs e)
        {
         
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            w = panel2.Width;
            h = panel2.Height;
            sections.components.sections_place place = new sections.components.sections_place(this);
            place.Width = w;
            place.Height = h;
            panel2.Controls.Clear();
            panel2.Controls.Add(place);
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            users.users_managment users = new users.users_managment();
            users.Width = panel2.Width;
            users.Height = panel2.Height;
            panel2.Controls.Clear();
            panel2.Controls.Add(users);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            charts.place_charts place_charts = new charts.place_charts();
            panel2.Controls.Clear();
            panel2.Controls.Add(place_charts);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            resize();
           
        }

        public void resize()
        {
            w = this.Width - 180;
            h = panel2.Height;
            panel7.Width = Panel4.Width - 184;
            

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            resize();
        }
    }
}
