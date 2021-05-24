using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaires.users
{
    public partial class users_ctrl : UserControl
    {
        public users_ctrl()
        {
            InitializeComponent();
        }

        private void users_ctrl_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable _dt = new DataTable();
            db db = new db();
           _dt= db.get_all_users();
           dataGridView1.DataSource = _dt;
            
        }
    }
}
