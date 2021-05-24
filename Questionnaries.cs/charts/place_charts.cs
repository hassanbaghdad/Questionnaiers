using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.charts
{
    public partial class place_charts : UserControl
    {
        db db = new db();
        public place_charts()
        {
            
            InitializeComponent();
            this.Width = Form1.w;
            dateTimePicker3.Hide();
            dateTimePicker4.Hide();

            ///////////////////
            line_period.Hide();
            label_peroid.Hide();
            peroid_Box.Hide();
            ///////////////////

            label_month.Show();
            monthBox.Show();
            yearBox.Show();
            label_year.Show();
            line_monthly.Show();
            label_month.Show();
            btn_execute.Show();
            //////////////////
            section_combo.DataSource = get_sections_names();
            categories_combo.DataSource = get_categories_names();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            //Button6.BackColor = Color.FromArgb(224, 224, 224);
            
          //  Button7.BackColor = Color.WhiteSmoke;
        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
         
        }

        private void panel_peroid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button7_Click_1(object sender, EventArgs e)
        {
            dateTimePicker3.Hide();
            dateTimePicker4.Hide();

            ///////////////////
            line_period.Hide();
            label_peroid.Hide();
            peroid_Box.Hide();
            ///////////////////

            label_month.Show();
            monthBox.Show();
            yearBox.Show();
            label_year.Show();
            line_monthly.Show();
            label_month.Show();
            btn_execute.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dateTimePicker3.Hide();
            dateTimePicker4.Hide();
            ///////////////////////
            label_month.Hide();
            monthBox.Hide();
            yearBox.Hide();
            line_monthly.Hide();
            label_year.Hide();
            label_month.Hide();
            btn_execute.Hide();
            ////////////////////////
            line_period.Show();
            label_peroid.Show();
            peroid_Box.Show();
            

        }

        private void peroid_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(peroid_Box.Text =="Custumize..")
            {
                dateTimePicker3.Show();
                dateTimePicker4.Show();

            }
        }


        public void test()
        {
            int      section_id   =  db.get_id_section_from_section_name(section_combo.Text);
            string   year         =  yearBox.Text;
            int      month        =  Convert.ToInt32(monthBox.Text);
            int      category_id  =  db.get_category_id(categories_combo.Text);

            if(categories_combo.Text =="All")
            {
                category_id = 0;
            }

            DataTable _dt = new DataTable();
            ui ui = new cs.ui();
            //_dt = ui.Avarge(section_id,year,month,category_id);
            chart1.DataSource = _dt;
          
            //chart1.Series["answers"].YValueMembers = "month";
            //chart1.Series["answers"].XValueMember = "event_assetment";
            //chart1.Series["answers"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

        }

        public List<String> get_sections_names()
        {
            DataTable _dt_departments = db.get_departments();
            DataTable _dt_branchs = db.get_branchs();

            List<String> list = new List<string>();
            list.Add("All");
            foreach (DataRow row in _dt_departments.Rows)
            {
                list.Add(row["name"].ToString());
            }
            foreach (DataRow row in _dt_branchs.Rows)
            {
                list.Add(row["name"].ToString());
            }

            return list;
        }
        public List<String> get_categories_names()
        {
            DataTable _dt_categories = db.get_all_cat();
            
            List<String> list = new List<string>();
            list.Add("All");
            foreach (DataRow row in _dt_categories.Rows)
            {
                list.Add(row["category_name"].ToString());
            }
          

            return list;
        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            ui ui = new ui();
            //ui.Avarge();
            
        }
    }
}
