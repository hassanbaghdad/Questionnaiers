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
    
    public partial class TopPanelHistory : UserControl
    {
        public static string date_send;
        public static string date_answer;

        public TopPanelHistory()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            this.Width = Form1.w;
            this.Height = 30;
        }

        private void TopPanelHistory_Load(object sender, EventArgs e)
        {
            label_date_answer.Text = date_answer;
            label_date_send.Text = date_send;
        }
    }
}
