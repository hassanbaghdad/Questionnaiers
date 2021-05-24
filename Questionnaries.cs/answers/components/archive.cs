using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaries.cs.answers.components
{
    
    public partial class archive : UserControl
    {
        view_control view_control;
        public static int section_id;
        public static int month;
        public static string year;
        public int id_history;

        public archive()
        {
            InitializeComponent();
          
            label_month.Text = month.ToString();
            label_year.Text = year;
            db db = new db();
            label_section.Text = db.get_section_name_from_id_section(section_id);
            panel6.Width = Form1.w;
            panel7.Width = Form1.w;
           // MessageBox.Show(month.ToString());
        }

        private void bg_archive_get_DoWork(object sender, DoWorkEventArgs e)
        {
            
          
                dataGridView1.Invoke((MethodInvoker)delegate
                {

                    DataTable _dt = new DataTable();
                    db db = new db();
                    _dt = db.get_archives(section_id, year, month);
                    
                    dataGridView1.Columns[0].Width = 42;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    int x = 0;
                    foreach(DataRow row in _dt.Rows)
                    {
                        x +=1;
                        string date_archive = row["date_archive"].ToString();
                        string user_answer = row["user_answer"].ToString();
                        id_history = Convert.ToInt32(row["id"].ToString());
                        Bitmap edit = mybitmap(@"D:\MyApp\Questionnaires\images\view.png", 90, 50);
                        dataGridView1.Rows.Add(x.ToString(),row["id"].ToString(), date_archive,user_answer, edit);
                    }

                });
            
            
             
            
        }

        private void archive_Load(object sender, EventArgs e)
        {
            bg_archive_get.RunWorkerAsync();
        }

        public Bitmap mybitmap(string path, int h, int w)
        {

            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(4))
            {
                bg_load_questions.RunWorkerAsync();

            }
        }

        private void bg_load_questions_DoWork(object sender, DoWorkEventArgs e)
        {
            panel_content.Invoke((MethodInvoker)delegate
            {
                var index = dataGridView1.CurrentCell.RowIndex;
                int _id_history = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value.ToString());
                panel_content.Hide();
                panel_content.Controls.Clear();
                panel_content.AutoScroll = true;
                panel_content.Font = new Font("Arial", 7, FontStyle.Bold);

                DataTable _dt = new DataTable();
                db db = new db();
                _dt = db.get_questions_from_archive(section_id, year, month,_id_history);
                foreach (DataRow row in _dt.Rows)
                {
                    ctl_view_question q_ctrl = new ctl_view_question(view_control);
                    string _answer = row["answer"].ToString();


                    q_ctrl.Width = 500;

                    q_ctrl.panel_btns_actions.Hide();

                    if (_answer == "1")
                    {
                        q_ctrl.q_yes.Checked = true;
                    }
                    else
                    {
                        q_ctrl.q_no_r.Checked = false;
                    }

                    q_ctrl.Width = Form1.w;
                    q_ctrl.groupBox2.Width = Form1.w;
                    q_ctrl.q_no.Text = row["id_q"].ToString();
                    q_ctrl.q_text.Text = row["q_text"].ToString();
                    q_ctrl.txt_event_type.Text = row["event_type"].ToString();
                    q_ctrl.txt_event_details.Text = row["event_details"].ToString();
                    q_ctrl.txt_event_impact.Text = row["event_impact"].ToString();
                    q_ctrl.txt_event_source.Text = row["event_source"].ToString();
                    q_ctrl.txt_notic.Text = row["event_notic"].ToString();
                    //q_ctrl.label_q_type2.Text = row["q_type"].ToString();
                    q_ctrl.richTextBox1.Text = row["q_type"].ToString();
                    string _updated_at = row["updated_at"].ToString();
                    if (_updated_at != "")
                    {
                        q_ctrl.panel_update_at.Show();
                        q_ctrl.label_update_at.Text = _updated_at;
                    }

                    int _assetment = Convert.ToInt32(row["event_assetment"].ToString());
                    if (_assetment == 1)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_1.Name;
                        q_ctrl.radio_1.Checked = true;

                    }
                    if (_assetment == 2)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_2.Name;
                        q_ctrl.radio_2.Checked = true;

                    }
                    if (_assetment == 3)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_3.Name;
                        q_ctrl.radio_3.Checked = true;

                    }
                    if (_assetment == 4)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_4.Name;
                        q_ctrl.radio_4.Checked = true;

                    }
                    if (_assetment == 5)
                    {
                        q_ctrl.checkbox_value.Text = q_ctrl.radio_5.Name;
                        q_ctrl.radio_5.Checked = true;

                    }

                    panel_content.Controls.Add(q_ctrl);

                    TopPanelHistory.date_send = row["date_send"].ToString();
                    TopPanelHistory.date_answer = row["date_answer"].ToString();


                }
                TopPanelHistory TopPanel = new TopPanelHistory();
                BlackPanel black = new BlackPanel();
                panel_cover_top_panel.Controls.Add(black);
                panel_cover_top_panel.Controls.Add(TopPanel);
                panel_content.Show();
            });
        }
    }
}
