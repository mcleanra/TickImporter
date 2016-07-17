using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickImporter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            DialogResult r = dialog.ShowDialog();

            if( r.Equals( DialogResult.OK ))
            {
                Program.SetFolder(dialog.SelectedPath);
                lblFolder.Text = dialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.SpawnThread();
        }

        public void Log(string message)
        {
            listBox1.Items.Add(message);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
