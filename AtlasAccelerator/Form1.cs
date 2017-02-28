using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPManagement;

namespace AtlasAccelerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(SPManagement.SPManagement.getDocCount(txtSiteUrl.Text, txtLibraryName.Text).ToString());
            //MessageBox.Show(SPManagement.SPManagement.getVersionCount(txtSiteUrl.Text, txtLibraryName.Text).ToString());
            MessageBox.Show(SPManagement.SPManagement.getMaxSizeDocument(txtSiteUrl.Text, txtLibraryName.Text).ToString());
        }

    }
}
