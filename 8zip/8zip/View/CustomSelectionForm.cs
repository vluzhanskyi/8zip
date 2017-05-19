using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8zip.View
{
    public partial class CustomSelectionForm : Form
    {
        public List<string> CheckedPackages;
        public CustomSelectionForm(List<string> packages)
        {
            InitializeComponent();
            foreach (var package in packages)
            {
                PackagesListBox.Items.Add(package, false);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            CheckedPackages = PackagesListBox.CheckedItems.Cast<string>().ToList();
        }
    }
}
