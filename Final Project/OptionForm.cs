// Final Project - OptionsForm.cs
// Created by: Austin Baker & Chip Denton
// Created on: Feb. 2013 - April 2013
// (c) Copyright 2013, P.I.R.A.T.E.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Final_Project
{
    public partial class frmOptionForm : Form
    {
        public frmOptionForm()
        {
            InitializeComponent();
        }

        private void pbColorGray_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Gray); // Gray
        }

        private void pbColorDarkRed_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.DarkRed); // Dark Red
        }

        private void pbColorRed_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Red); // Red
        }

        private void pbColorOrange_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Orange); // Orange
        }

        private void pbColorYellow_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Yellow); // Yellow
        }

        private void pbColorGreen_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Green); // Green
        }

        private void pbColorTurquiose_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Turquoise); // Turquoise
        }

        private void pbColorIndigo_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.MediumBlue); // Blue
        }

        private void pbColorPurple_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.Indigo); // purple
        }

        private void ChangeColor(Color pcolor)
        {
            // Method ChangeColor - change the chat box color
            GlobalVars.cPlayerColor = pcolor;
            txtColorTest.ForeColor = pcolor;

        }



        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOptionForm_Load(object sender, EventArgs e)
        {
            // set variables on load
            timForceDraw.Enabled = true;
            txtStats.Focus();
            txtColorTest.ForeColor = GlobalVars.cPlayerColor;
            txtColorTest.Font = GlobalVars.fntFont;
            nudFontSize.Value = GlobalVars.iFontSize;

            // Stats
            FileStream fsIn = new FileStream(@"database.cdata", FileMode.Open, FileAccess.Read);
            StreamReader srIn = new StreamReader(fsIn);
            string[] asDataIn;
            string sData = " ";
            sData = srIn.ReadLine();
            while (sData != null) // loop until no more
            {
                
                asDataIn = sData.Split('|'); // split into array
                if (asDataIn[0] == GlobalVars.sName)
                                {
                    // stat box
                    txtStats.Text = "Player Name: " + asDataIn[0] + "\nTotal Wins: " + asDataIn[1] +
                        "\nTotal Loses: " + asDataIn[2] + "\nTotal Draws: " + asDataIn[3] + "\nTotal Forfeits: " + asDataIn[4] + "\nTotal Jumps: " + asDataIn[5] + "\nHighest Win Streak: " + asDataIn[6] + "\nCurrent Win Streak: " + asDataIn[7] + "\nHighest Lose Streak: " + asDataIn[8] + "\nCurrent Lose Streak: " + asDataIn[9];
                }

                sData = srIn.ReadLine();
            } // end loop through file


            srIn.Close();
            fsIn.Close();
        }

        private void DrawRectangles()
        {

            // Color Selection Box
            System.Drawing.Pen myColorSelectionBox;
            myColorSelectionBox = new Pen(Color.White, 24);

            // Color Selection Outline
            System.Drawing.Pen myColorSelectionOutline;
            myColorSelectionOutline = new Pen(System.Drawing.Color.DarkGreen, 4);


            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.DrawLine(myColorSelectionBox, 202, 49, 460, 49); // Color Selection Box
            formGraphics.DrawRectangle(myColorSelectionOutline, new Rectangle(200, 36, 262, 26)); // Color Selection

            // Dispose of Everything
            myColorSelectionOutline.Dispose();
            myColorSelectionBox.Dispose();
            formGraphics.Dispose();
        }

        private void timForceDraw_Tick(object sender, EventArgs e)
        {
            DrawRectangles();
            timForceDraw.Enabled = false;
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.iFontSize = (int)nudFontSize.Value;
            GlobalVars.fntFont = new Font("Microsoft Sans Serif", (float)nudFontSize.Value);
            txtColorTest.Font = GlobalVars.fntFont;
        }


        private void pbColorWhite_Click(object sender, EventArgs e)
        {
            ChangeColor(Color.White); // white
        }

        private void frmOptionForm_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles();
        }




        

    }
}
