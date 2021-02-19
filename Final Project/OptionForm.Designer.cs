namespace Final_Project
{
    partial class frmOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptionForm));
            this.cdOptionsColor = new System.Windows.Forms.ColorDialog();
            this.fdOptionsFont = new System.Windows.Forms.FontDialog();
            this.btnReturn = new System.Windows.Forms.Button();
            this.timForceDraw = new System.Windows.Forms.Timer(this.components);
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.pbColorYellow = new System.Windows.Forms.PictureBox();
            this.pbColorGreen = new System.Windows.Forms.PictureBox();
            this.pbColorTurquiose = new System.Windows.Forms.PictureBox();
            this.pbColorIndigo = new System.Windows.Forms.PictureBox();
            this.pbColorPurple = new System.Windows.Forms.PictureBox();
            this.pbColorGray = new System.Windows.Forms.PictureBox();
            this.pbColorWhite = new System.Windows.Forms.PictureBox();
            this.pbColorOrange = new System.Windows.Forms.PictureBox();
            this.pbColorRed = new System.Windows.Forms.PictureBox();
            this.pbColorDarkRed = new System.Windows.Forms.PictureBox();
            this.txtColorTest = new System.Windows.Forms.TextBox();
            this.txtStats = new System.Windows.Forms.RichTextBox();
            this.lblStats = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorTurquiose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorIndigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorPurple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorOrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorDarkRed)).BeginInit();
            this.SuspendLayout();
            // 
            // fdOptionsFont
            // 
            this.fdOptionsFont.AllowScriptChange = false;
            this.fdOptionsFont.AllowSimulations = false;
            this.fdOptionsFont.AllowVectorFonts = false;
            this.fdOptionsFont.AllowVerticalFonts = false;
            this.fdOptionsFont.FontMustExist = true;
            this.fdOptionsFont.MaxSize = 20;
            this.fdOptionsFont.MinSize = 4;
            this.fdOptionsFont.ShowEffects = false;
            this.fdOptionsFont.ShowHelp = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(382, 286);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 14;
            this.btnReturn.Text = "&Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // timForceDraw
            // 
            this.timForceDraw.Tick += new System.EventHandler(this.timForceDraw_Tick);
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new System.Drawing.Point(143, 39);
            this.nudFontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(45, 20);
            this.nudFontSize.TabIndex = 15;
            this.nudFontSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // pbColorYellow
            // 
            this.pbColorYellow.Image = global::Final_Project.Properties.Resources.YellowColor;
            this.pbColorYellow.Location = new System.Drawing.Point(333, 39);
            this.pbColorYellow.Name = "pbColorYellow";
            this.pbColorYellow.Size = new System.Drawing.Size(20, 20);
            this.pbColorYellow.TabIndex = 12;
            this.pbColorYellow.TabStop = false;
            this.pbColorYellow.Click += new System.EventHandler(this.pbColorYellow_Click);
            // 
            // pbColorGreen
            // 
            this.pbColorGreen.Image = global::Final_Project.Properties.Resources.DarkGreenColor;
            this.pbColorGreen.Location = new System.Drawing.Point(359, 39);
            this.pbColorGreen.Name = "pbColorGreen";
            this.pbColorGreen.Size = new System.Drawing.Size(20, 20);
            this.pbColorGreen.TabIndex = 11;
            this.pbColorGreen.TabStop = false;
            this.pbColorGreen.Click += new System.EventHandler(this.pbColorGreen_Click);
            // 
            // pbColorTurquiose
            // 
            this.pbColorTurquiose.Image = global::Final_Project.Properties.Resources.TurquoiseColor;
            this.pbColorTurquiose.Location = new System.Drawing.Point(385, 39);
            this.pbColorTurquiose.Name = "pbColorTurquiose";
            this.pbColorTurquiose.Size = new System.Drawing.Size(20, 20);
            this.pbColorTurquiose.TabIndex = 10;
            this.pbColorTurquiose.TabStop = false;
            this.pbColorTurquiose.Click += new System.EventHandler(this.pbColorTurquiose_Click);
            // 
            // pbColorIndigo
            // 
            this.pbColorIndigo.Image = global::Final_Project.Properties.Resources.IndigoColor;
            this.pbColorIndigo.Location = new System.Drawing.Point(411, 39);
            this.pbColorIndigo.Name = "pbColorIndigo";
            this.pbColorIndigo.Size = new System.Drawing.Size(20, 20);
            this.pbColorIndigo.TabIndex = 9;
            this.pbColorIndigo.TabStop = false;
            this.pbColorIndigo.Click += new System.EventHandler(this.pbColorIndigo_Click);
            // 
            // pbColorPurple
            // 
            this.pbColorPurple.Image = global::Final_Project.Properties.Resources.PurpleColor;
            this.pbColorPurple.Location = new System.Drawing.Point(437, 39);
            this.pbColorPurple.Name = "pbColorPurple";
            this.pbColorPurple.Size = new System.Drawing.Size(20, 20);
            this.pbColorPurple.TabIndex = 8;
            this.pbColorPurple.TabStop = false;
            this.pbColorPurple.Click += new System.EventHandler(this.pbColorPurple_Click);
            // 
            // pbColorGray
            // 
            this.pbColorGray.Image = global::Final_Project.Properties.Resources.GrayColor;
            this.pbColorGray.Location = new System.Drawing.Point(231, 39);
            this.pbColorGray.Name = "pbColorGray";
            this.pbColorGray.Size = new System.Drawing.Size(20, 20);
            this.pbColorGray.TabIndex = 7;
            this.pbColorGray.TabStop = false;
            this.pbColorGray.Click += new System.EventHandler(this.pbColorGray_Click);
            // 
            // pbColorWhite
            // 
            this.pbColorWhite.Image = global::Final_Project.Properties.Resources.WhiteColor;
            this.pbColorWhite.Location = new System.Drawing.Point(205, 39);
            this.pbColorWhite.Name = "pbColorWhite";
            this.pbColorWhite.Size = new System.Drawing.Size(20, 20);
            this.pbColorWhite.TabIndex = 6;
            this.pbColorWhite.TabStop = false;
            this.pbColorWhite.Click += new System.EventHandler(this.pbColorWhite_Click);
            // 
            // pbColorOrange
            // 
            this.pbColorOrange.Image = global::Final_Project.Properties.Resources.OrangeColor;
            this.pbColorOrange.Location = new System.Drawing.Point(307, 39);
            this.pbColorOrange.Name = "pbColorOrange";
            this.pbColorOrange.Size = new System.Drawing.Size(20, 20);
            this.pbColorOrange.TabIndex = 5;
            this.pbColorOrange.TabStop = false;
            this.pbColorOrange.Click += new System.EventHandler(this.pbColorOrange_Click);
            // 
            // pbColorRed
            // 
            this.pbColorRed.Image = global::Final_Project.Properties.Resources.RedColor;
            this.pbColorRed.Location = new System.Drawing.Point(281, 39);
            this.pbColorRed.Name = "pbColorRed";
            this.pbColorRed.Size = new System.Drawing.Size(20, 20);
            this.pbColorRed.TabIndex = 4;
            this.pbColorRed.TabStop = false;
            this.pbColorRed.Click += new System.EventHandler(this.pbColorRed_Click);
            // 
            // pbColorDarkRed
            // 
            this.pbColorDarkRed.Image = global::Final_Project.Properties.Resources.DarkRedColor;
            this.pbColorDarkRed.Location = new System.Drawing.Point(257, 39);
            this.pbColorDarkRed.Name = "pbColorDarkRed";
            this.pbColorDarkRed.Size = new System.Drawing.Size(20, 20);
            this.pbColorDarkRed.TabIndex = 3;
            this.pbColorDarkRed.TabStop = false;
            this.pbColorDarkRed.Click += new System.EventHandler(this.pbColorDarkRed_Click);
            // 
            // txtColorTest
            // 
            this.txtColorTest.BackColor = System.Drawing.Color.Black;
            this.txtColorTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColorTest.Location = new System.Drawing.Point(25, 39);
            this.txtColorTest.Name = "txtColorTest";
            this.txtColorTest.ReadOnly = true;
            this.txtColorTest.Size = new System.Drawing.Size(100, 21);
            this.txtColorTest.TabIndex = 17;
            this.txtColorTest.Text = "Example";
            // 
            // txtStats
            // 
            this.txtStats.Location = new System.Drawing.Point(25, 123);
            this.txtStats.Name = "txtStats";
            this.txtStats.ReadOnly = true;
            this.txtStats.Size = new System.Drawing.Size(432, 147);
            this.txtStats.TabIndex = 16;
            this.txtStats.Text = "";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStats.Location = new System.Drawing.Point(21, 87);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(54, 24);
            this.lblStats.TabIndex = 18;
            this.lblStats.Text = "Stats:";
            // 
            // frmOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(483, 327);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.txtStats);
            this.Controls.Add(this.nudFontSize);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.pbColorYellow);
            this.Controls.Add(this.pbColorGreen);
            this.Controls.Add(this.pbColorTurquiose);
            this.Controls.Add(this.pbColorIndigo);
            this.Controls.Add(this.pbColorPurple);
            this.Controls.Add(this.pbColorGray);
            this.Controls.Add(this.pbColorWhite);
            this.Controls.Add(this.pbColorOrange);
            this.Controls.Add(this.pbColorRed);
            this.Controls.Add(this.pbColorDarkRed);
            this.Controls.Add(this.txtColorTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptionForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOptionForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorTurquiose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorIndigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorPurple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorOrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorDarkRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog cdOptionsColor;
        private System.Windows.Forms.FontDialog fdOptionsFont;
        private System.Windows.Forms.PictureBox pbColorDarkRed;
        private System.Windows.Forms.PictureBox pbColorRed;
        private System.Windows.Forms.PictureBox pbColorOrange;
        private System.Windows.Forms.PictureBox pbColorWhite;
        private System.Windows.Forms.PictureBox pbColorGray;
        private System.Windows.Forms.PictureBox pbColorPurple;
        private System.Windows.Forms.PictureBox pbColorIndigo;
        private System.Windows.Forms.PictureBox pbColorTurquiose;
        private System.Windows.Forms.PictureBox pbColorGreen;
        private System.Windows.Forms.PictureBox pbColorYellow;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Timer timForceDraw;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.TextBox txtColorTest;
        private System.Windows.Forms.RichTextBox txtStats;
        private System.Windows.Forms.Label lblStats;
    }
}