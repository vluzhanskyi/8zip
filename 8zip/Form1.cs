using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace _8zip
{
    public partial class EightZip : Form
    {
        public EightZip()
        {
            InitializeComponent();
        }

        private void EightZip_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label1.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                this.textBox1.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                this.label1.Text = file;
                this.textBox1.Text = file;
            }
        }

        
    }
}
