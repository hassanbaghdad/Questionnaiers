using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires
{
    public partial class Form1 : Form
    {
        public static int h, w;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        Point lastPoint;
        private void Panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y;
            }
        }

        private void Panel4_MouseDown(object sender, MouseEventArgs e)
        {

            lastPoint = new Point(e.X , e.Y);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
