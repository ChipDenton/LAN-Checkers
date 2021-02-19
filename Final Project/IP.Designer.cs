namespace Final_Project
{
    partial class IP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IP));
            this.btnConnect = new System.Windows.Forms.Button();
            this.chkSpectator = new System.Windows.Forms.CheckBox();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(208, 75);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // chkSpectator
            // 
            this.chkSpectator.AutoSize = true;
            this.chkSpectator.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkSpectator.Location = new System.Drawing.Point(12, 53);
            this.chkSpectator.Name = "chkSpectator";
            this.chkSpectator.Size = new System.Drawing.Size(108, 17);
            this.chkSpectator.TabIndex = 4;
            this.chkSpectator.Text = "Join as &Spectator";
            this.chkSpectator.UseVisualStyleBackColor = true;
            this.chkSpectator.CheckedChanged += new System.EventHandler(this.chkSpectator_CheckedChanged);
            // 
            // cmbIP
            // 
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Location = new System.Drawing.Point(77, 12);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(125, 21);
            this.cmbIP.TabIndex = 1;
            this.cmbIP.SelectedIndexChanged += new System.EventHandler(this.cmbIP_SelectedIndexChanged);
            this.cmbIP.TextChanged += new System.EventHandler(this.cmbIP_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&IP Address:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(208, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(289, 111);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbIP);
            this.Controls.Add(this.chkSpectator);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IP Address";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IP_FormClosing);
            this.Load += new System.EventHandler(this.IP_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.IP_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkSpectator;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
    }
}