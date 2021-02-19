namespace Final_Project
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timForceDraw = new System.Windows.Forms.Timer(this.components);
            this.pbJoin = new System.Windows.Forms.PictureBox();
            this.pbCreate = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbJoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // timForceDraw
            // 
            this.timForceDraw.Tick += new System.EventHandler(this.timForceDraw_Tick);
            // 
            // pbJoin
            // 
            this.pbJoin.Image = global::Final_Project.Properties.Resources.joingame;
            this.pbJoin.Location = new System.Drawing.Point(280, 155);
            this.pbJoin.Name = "pbJoin";
            this.pbJoin.Size = new System.Drawing.Size(190, 65);
            this.pbJoin.TabIndex = 2;
            this.pbJoin.TabStop = false;
            this.pbJoin.Click += new System.EventHandler(this.pbJoin_Click);
            // 
            // pbCreate
            // 
            this.pbCreate.Image = global::Final_Project.Properties.Resources.creategame;
            this.pbCreate.Location = new System.Drawing.Point(280, 45);
            this.pbCreate.Name = "pbCreate";
            this.pbCreate.Size = new System.Drawing.Size(190, 65);
            this.pbCreate.TabIndex = 1;
            this.pbCreate.TabStop = false;
            this.pbCreate.Click += new System.EventHandler(this.pbCreate_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(25, 35);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(190, 190);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(22, 258);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(101, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "What is your &name?";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(143, 255);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 4;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(526, 291);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbJoin);
            this.Controls.Add(this.pbCreate);
            this.Controls.Add(this.pbLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LAN Checkers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Move += new System.EventHandler(this.MainForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.pbJoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timForceDraw;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pbCreate;
        private System.Windows.Forms.PictureBox pbJoin;
        private System.Windows.Forms.Label lblName;
        public System.Windows.Forms.TextBox txtName;
    }
}

