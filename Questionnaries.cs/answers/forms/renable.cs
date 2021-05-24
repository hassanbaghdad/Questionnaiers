using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.answers.forms
{
    
    public partial class renable : Form
    {
        public static string year;
        public static int month;
        public static int section_id;
        public static int sequnce;

        public renable()
        {
            InitializeComponent();
            year_combo.Text = year;
            month_combo.Text = month.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
