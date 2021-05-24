using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
namespace Questionnaries.cs.sections
{
    class reload_after_approve
    {
        db db = new db();
        answers.components.view_control myview_control;
        answers.components.answers_ctrl answer_control;
        public void get(int this_section_id, string this_year, int this_month)
        {

            
           

            myview_control.Controls.Clear();

            answers.components.view_control.month = this_month;
            answers.components.view_control.year = this_year;
            answers.components.view_control place_control = new answers.components.view_control(answer_control);
            DataTable _dt = new DataTable();
            _dt = db.get_approve_state(this_section_id, this_month);
            string _approve = "";
            string _date_approve = "";

            foreach (DataRow row in _dt.Rows)
            {
                _approve = row["approve"].ToString();
                _date_approve = row["date_approve"].ToString();
            }
            if (_approve == "تم الموافقة")
            {

                place_control.btn_approve.Text = "اللغاء الموافقة";
                place_control.btn_approve.BackColor = Color.Red;
                place_control.panel4.Visible = true;
                place_control.label_date_approve.Text = _date_approve;

            }

            db db2 = new db();
            //  MessageBox.Show(section_id.ToString());
            place_control.label_section_name.Text = db2.get_section_name_from_id_section(this_section_id);
            place_control.Width = Form1.w;
            myview_control.Controls.Add(place_control);
            // MessageBox.Show("place : "+place_control.Width.ToString());
        }
    }
}
