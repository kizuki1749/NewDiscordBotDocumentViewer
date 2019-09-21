using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewDiscordBotDocumentViewer
{
    public partial class GoToPageDialog : Form
    {
        public GoToPageDialog(int Page, int Max)
        {
            InitializeComponent();
            numericUpDown1.Value = Page;
            numericUpDown1.Maximum = Max;
            Text = "指定ページへ移動 (1～" + Max + ")";
        }

        public int SelectedPage { get; set; } = 0;

        private void Button1_Click(object sender, EventArgs e)
        {
            SelectedPage = (int)numericUpDown1.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
