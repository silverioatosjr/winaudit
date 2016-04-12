using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAudit
{
    public partial class Form1 : Form
    {
        private frmUserInfo form2;
        private bool CheckConnection;
        public bool clicked;
        public Form1()
        {
            InitializeComponent();
            CheckConnection = true;
            form2 = new frmUserInfo();
            timer1.Interval = 1000;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clicked = false;
          
            HtmlDocument doc = webBrowser1.Document;
            var t = doc.GetElementById("noinfo");
           
                if (t.InnerText == "Getting Information. Please wait..." || t.InnerText == "Saving Information. Please wait...")
                {
                    MessageBox.Show("The system is busy. Please wait...");
                }
                else
                {
                    
                    
                    
                    if (form2.ShowDialog()==DialogResult.OK)
                    {
                    t.InnerText = "Getting Information. Please wait...";
                    Thread get = new Thread(() => GetInfo(doc, ((TextBox)form2.Controls["txtTechName"]).Text, ((TextBox)form2.Controls["txtClient"]).Text,
                        ((TextBox)form2.Controls["txtSite"]).Text, ((Label)form2.Controls["lblProcessor"]).Text, ((Label)form2.Controls["lblWorkstation"]).Text, ((Label)form2.Controls["lblOS"]).Text));
                    get.Start();
                    toolStripButton2.Enabled = true;

                    }
                  
                }
            }

        private void GetInfo(HtmlDocument doc, string techName, string client, string site, string processor, string pc, string os)
        {
            
            var getInfo = new GetInformation();
            var p = doc.GetElementById("only");
            p.InnerHtml = getInfo.ShowInfo( techName, client, site, processor, pc, os);
            doc.GetElementById("noinfo").InnerText = "Saving Information. Please wait...";
            p.AppendChild(doc.CreateElement("<input id='Save' type='submit' value='Save' />"));
            var t = doc.GetElementById("Save");
            t.Style = "display:none";
            t.InvokeMember("click");
            //menuId.Style = "visibility:visible";
            //search.Style = "visibility:visible";
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            HtmlDocument doc = webBrowser1.Document;
            var t = doc.GetElementById("noinfo");
            var s = doc.GetElementById("Save");
            if (t.InnerText == "Saving Information. Please wait..." || t.InnerText == "Getting Information. Please wait...")
            {
                MessageBox.Show("Please wait...");
             }
            else if(s!= null)
            {
                MessageBox.Show("System Information has been recorded successfully");
                toolStripButton1.Enabled = true;
                toolStripButton3.Enabled = true;
                toolStripButton2.Enabled = false;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.webBrowser1.ReadyState != WebBrowserReadyState.Complete){
                 toolStripButton1.Enabled = false;
                 toolStripButton2.Enabled = false;
                 toolStripButton3.Enabled = true;
                 CheckConnection = false;
                 return;
             }
             else {
                 HtmlDocument doc = webBrowser1.Document;
                 var t = doc.GetElementById("noinfo");
                 if (t != null)
                 {
                     toolStripButton1.Enabled = true;
                     toolStripButton2.Enabled = false;
                     toolStripButton3.Enabled = true;
                     CheckConnection = true;
                     timer1.Enabled = true;
                     //var download = doc.GetElementById("download");
                     //var searchButton = doc.GetElementById("searButton");
                     //searchButton.Style = "display:none";
                     //var log = doc.GetElementById("log");
                     //var menuId = doc.GetElementById("mainnav");
                     //menuId.Style = "visibility:hidden";
                     //var search = doc.GetElementById("search");
                     //search.Style = "visibility:hidden";
                     //download.Style = "display:none";
                   
                     //log.Style = "display:none";
                     //var machine = doc.GetElementById("machine");
                     //machine.Style = "display:none";
                 }
                 else
                 {
                     MessageBox.Show("Website not loaded. Please check your internet connection");
                     CheckConnection = false;
                     toolStripButton3.Enabled = true;
                     toolStripButton2.Enabled = false;
                     toolStripButton1.Enabled = false;
                 }
             }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            if (!CheckConnection)
            {
                webBrowser1.Url = new System.Uri("http://techhelper.azurewebsites.net/Home/Show", System.UriKind.Absolute);
                toolStripButton2.Enabled = true;
                toolStripButton1.Enabled = true;
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            if (doc.GetElementById("noinfo").InnerText == null)
            {
                timer1.Enabled = false;
                MessageBox.Show("Information has been recorded successfully" + Environment.NewLine + "You may now close the application","Save",MessageBoxButtons.OK);
                
            }
          
        }

    }
}
