using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple_Music_Player_NateDraft
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        private void cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void folderBrowserText_Changed(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
    }
}
