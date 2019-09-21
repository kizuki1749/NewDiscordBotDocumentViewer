using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NewDiscordBotDocumentViewer
{
    public partial class Form1 : Form
    {
        const string Version = "1.1";

        public Form1()
        {
            InitializeComponent();
        }

        XMLTypeEmbedDocuments LoadedDocument = null;
        int NowPage = 0;

        private void 右端で折り返すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WordWrap = !Properties.Settings.Default.WordWrap;
            Properties.Settings.Default.Save();
            richTextBox1.WordWrap = Properties.Settings.Default.WordWrap;
            右端で折り返すToolStripMenuItem.Checked = Properties.Settings.Default.WordWrap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = Properties.Settings.Default.WordWrap;
            右端で折り返すToolStripMenuItem.Checked = Properties.Settings.Default.WordWrap;
            Text = "NewDiscordBotDocumentViewer " + Version;
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            if (LoadedDocument != null && NowPage != 1)
            {
                JumpPageDocument(NowPage - 1);
            }
        }

        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XMLTypeEmbedDocuments));
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName, new System.Text.UTF8Encoding(false));
                    LoadedDocument = (XMLTypeEmbedDocuments)serializer.Deserialize(sr);
                    sr.Close();
                    ReloadDocument();
                }
                catch
                {
                    MessageBox.Show("XMLデータの読み込みに失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void ReloadDocument()
        {
            NowPage = 1;
            toolStripLabel1.Text = NowPage.ToString().PadRight(LoadedDocument.Pages.Count.ToString().Length) + "ページ / " + LoadedDocument.Pages.Count + "ページ";
            toolStripLabel2.Text = LoadedDocument.Title;
            JumpPageDocument(NowPage);
        }

        public void JumpPageDocument(int PageCount)
        {
            NowPage = PageCount;
            richTextBox1.Text = "";
            foreach (var a in LoadedDocument.Pages[PageCount - 1].ConvertToDictionary())
            {
                if (richTextBox1.Text == "")
                {
                    AddRichTextBoxText(a.Key + "\n", true);
                }
                else
                {
                    AddRichTextBoxText("\n\n" + a.Key + "\n", true);
                }
                AddRichTextBoxText(a.Value);
            }
            toolStripLabel1.Text = NowPage.ToString().PadRight(LoadedDocument.Pages.Count.ToString().Length) + "ページ / " + LoadedDocument.Pages.Count + "ページ";
        }

        public void AddRichTextBoxText(string Text, bool Bold = false)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            if (Bold)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            }
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectedText = Text;
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            if (LoadedDocument != null && LoadedDocument.Pages.Count != NowPage)
            {
                JumpPageDocument(NowPage + 1);
            }
        }

        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            if (LoadedDocument != null)
            {
                GoToPageDialog goToPageDialog = new GoToPageDialog(NowPage, LoadedDocument.Pages.Count);
                if (goToPageDialog.ShowDialog() == DialogResult.OK)
                {
                    JumpPageDocument(goToPageDialog.SelectedPage);
                }
            }
        }
    }
}
