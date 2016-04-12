namespace WinAudit
{
    partial class frmUserInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTechName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtTechName = new System.Windows.Forms.TextBox();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.lblWorkstation = new System.Windows.Forms.Label();
            this.lblOS = new System.Windows.Forms.Label();
            this.lblProcessor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTechName
            // 
            this.lblTechName.AutoSize = true;
            this.lblTechName.Location = new System.Drawing.Point(14, 30);
            this.lblTechName.Name = "lblTechName";
            this.lblTechName.Size = new System.Drawing.Size(97, 13);
            this.lblTechName.TabIndex = 0;
            this.lblTechName.Text = "Technician Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Client :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Site :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Workstation :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Operating System :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Continue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(162, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "C&ancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtTechName
            // 
            this.txtTechName.Location = new System.Drawing.Point(112, 27);
            this.txtTechName.Name = "txtTechName";
            this.txtTechName.Size = new System.Drawing.Size(140, 20);
            this.txtTechName.TabIndex = 7;
            this.txtTechName.Tag = "Technician Name";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(112, 55);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(140, 20);
            this.txtClient.TabIndex = 8;
            this.txtClient.Tag = "Client";
            // 
            // txtSite
            // 
            this.txtSite.Location = new System.Drawing.Point(112, 82);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(140, 20);
            this.txtSite.TabIndex = 9;
            this.txtSite.Tag = "Site";
            // 
            // lblWorkstation
            // 
            this.lblWorkstation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkstation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWorkstation.Location = new System.Drawing.Point(112, 136);
            this.lblWorkstation.Name = "lblWorkstation";
            this.lblWorkstation.Size = new System.Drawing.Size(140, 20);
            this.lblWorkstation.TabIndex = 10;
            // 
            // lblOS
            // 
            this.lblOS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOS.Location = new System.Drawing.Point(112, 162);
            this.lblOS.Name = "lblOS";
            this.lblOS.Size = new System.Drawing.Size(140, 20);
            this.lblOS.TabIndex = 11;
            // 
            // lblProcessor
            // 
            this.lblProcessor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProcessor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProcessor.Location = new System.Drawing.Point(112, 110);
            this.lblProcessor.Name = "lblProcessor";
            this.lblProcessor.Size = new System.Drawing.Size(140, 20);
            this.lblProcessor.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Processor:";
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.Controls.Add(this.lblProcessor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOS);
            this.Controls.Add(this.lblWorkstation);
            this.Controls.Add(this.txtSite);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.txtTechName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTechName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Information";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTechName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtTechName;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.Label lblWorkstation;
        private System.Windows.Forms.Label lblOS;
        private System.Windows.Forms.Label lblProcessor;
        private System.Windows.Forms.Label label6;
    }
}