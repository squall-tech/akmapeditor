using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AKMapEditor.OtMapEditor
{
    public partial class ConnectForm : Form
    {
        public String ip;
        public String password;
        public ConnectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ip = tbIp.Text;
            this.password = tbPassword.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
