using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.WinForms.Gui.Dialogs
{
    public partial class RenameFleetDialog : Form
    {
        public RenameFleetDialog()
        {
            InitializeComponent();
        }

        public string FleetName
        {
            get 
            { 
                return txtNewName.Text.Trim(); 
            }

            set 
            { 
                lblFleetName.Text = value;
                txtNewName.Text = value;
            } 
        }

        private void RenameFleetDialog_Load(object sender, EventArgs e)
        {
            txtNewName.SelectAll();
            txtNewName.Focus();
        }

        private void TextNewName_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = txtNewName.Text.Trim().Length > 0;
        }
    }
}
