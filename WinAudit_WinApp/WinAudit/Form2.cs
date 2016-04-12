using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAudit
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (c.Text == string.Empty)
                    {
                        c.Focus();
                        MessageBox.Show(c.Tag + " is empty");
                        return;
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            var processor = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get().OfType<ManagementObject>()
                                 select x.GetPropertyValue("Name")).FirstOrDefault();
            lblProcessor.Text = processor != null ? processor.ToString() : "Unknown";
            lblWorkstation.Text = Environment.MachineName;
            lblOS.Text = name != null ? name.ToString() : "Unknown";
        }


    }
}
