// Final Project - MainForm.cs
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
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timForceDraw.Enabled = true;
        }
        private void DrawRectangles()
        {
            // Picture Box 
            System.Drawing.Pen myPicBoxOutline;
            myPicBoxOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen,4);

            // Create Game Button
            System.Drawing.Pen myCreateGameOutline;
            myCreateGameOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 4);

            // Join Game Button
            System.Drawing.Pen myJoinGameOutline;
            myJoinGameOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 4);

            // Top Line
            System.Drawing.Pen myTopLine;
            myTopLine = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Bottom Line 1
            System.Drawing.Pen myBottomLine1;
            myBottomLine1 = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Bottom Line 2
            System.Drawing.Pen myBottomLine2;
            myBottomLine2 = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Name Outline
            System.Drawing.Pen myNameOutline;
            myNameOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 4);

            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myPicBoxOutline, new Rectangle(20, 30, 200, 200)); // picture box
            formGraphics.DrawRectangle(myCreateGameOutline, new Rectangle(275, 40, 200, 75)); // create game
            formGraphics.DrawRectangle(myJoinGameOutline, new Rectangle(275, 150, 200, 75)); // join game
            formGraphics.DrawRectangle(myNameOutline, new Rectangle(138, 250,110, 30)); // name outline
            formGraphics.DrawLine(myTopLine,0,2,540,2); // top line
            formGraphics.DrawLine(myBottomLine1, 0, 264, 140, 264); // bottom line 1
            formGraphics.DrawLine(myBottomLine2, 246, 264, 540, 264); // bottom line 2


            // Dispose Other boxes
            myPicBoxOutline.Dispose();
            formGraphics.Dispose();
        }


        private void timForceDraw_Tick(object sender, EventArgs e)
        {
            DrawRectangles();
            timForceDraw.Enabled = false; // stop the timer
        }

        private void pbCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please input a name.");
            }
            else
            {

                // Look up to see if the player exists.
                LookUpPlayer(txtName.Text);


                //GlobalVars.sHostIP = txtIP.Text;
                GlobalVars.sName = txtName.Text;
                GlobalVars.bHost = true;
                GlobalVars.bYourTurn = true;
                GlobalVars.bPlayer2 = false;
                GlobalVars.bSpectator = false;

                //Calls the Server_Test Application
                GlobalVars.process1 = new System.Diagnostics.Process();
                GlobalVars.process1.StartInfo.FileName = (@"server3.exe");
                GlobalVars.process1.Start();
                GlobalVars.iControlYou = 2;

                this.Close();
            }
        }

        private void LookUpPlayer(string psName)
        {
            //bool bExists = false; // does the player exist

            // look to see if player exists
            FileStream fsIn = new FileStream(@"database.cdata", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader srIn = new StreamReader(fsIn);
            string[] asDataIn;
            string sData = " ";
            sData = srIn.ReadLine();
            while (sData != null) // loop until no more
            {
                asDataIn = sData.Split('|'); // split into two arrays
                if (psName == asDataIn[0])
                {
                    GlobalVars.bExists = true;
                    //frmGameForm frmGameForm = new frmGameForm();
                    GlobalVars.sWelcomeBack = "Welcome back, " + psName;
                    //MessageBox.Show("Welcome back, " + psName);
                }

                sData = srIn.ReadLine();
            } // end loop through file


            srIn.Close();
            fsIn.Close();


            if (GlobalVars.bExists == false) // if player does not exist, then add the player to the database
            {
                FileStream fsStr = new FileStream(@"database.cdata", FileMode.Append, FileAccess.Write);
                StreamWriter swOut = new StreamWriter(fsStr);

                swOut.WriteLine(psName + "|0|0|0|0|0|0|0|0|0"); // create new entry in database

                swOut.Close();
                fsStr.Close();
            }
        }

        private void pbJoin_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please input a name.");
            }
            else
            {

                // Look up to see if the player exists.
                LookUpPlayer(txtName.Text);

                GlobalVars.sName = txtName.Text;
                GlobalVars.iControlYou = 3;
                //GlobalVars.iControlYou = 2;
                this.Close();
            }

        }


        private void MainForm_Move(object sender, EventArgs e)
        {
            timForceDraw.Enabled = true; // start the timer
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close the form
            if (GlobalVars.iControlYou == 1)
            {
                GlobalVars.iControlYou = -1;
                this.Close();
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles(); // redraw screen
        }


    }
}
