using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.sections.forms
{
    public partial class date_form : Form
    {
        sections.components.sections_place place;
        public static string year;
        public static int month;

        public date_form(sections.components.sections_place place1)
        {
            InitializeComponent();
            place = place1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(place.send_questions_for_checked_sections(year_combo.Text,Convert.ToInt32(month_combo.Text))==true)
            {
                try
                {
                    MessageBox.Show("تم ارسال استبيان سنة " + year_combo.Text + " لشهر  " + month_combo.Text + " بنجاح", "تم ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    place.backgroundWorker1.RunWorkerAsync();
                   
                    this.Hide();
                }catch(Exception ee)
                {
                    MessageBox.Show("error in send questions for web is : " + ee.Message);
                }
                
            }
            
        }
    }
}
