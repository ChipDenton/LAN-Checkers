// Final Project - IP.cs
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
    public partial class IP : Form
    {
        public bool bInConnect = false;

        public IP()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // look to see if IP exists
            FileStream fsIn = new FileStream(@"IP.cdata", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader srIn = new StreamReader(fsIn);
            StreamWriter swOut = new StreamWriter(fsIn);

            bool bExists = false; // if ip address is already in database
            string sData = " ";
            sData = srIn.ReadLine();
            while (sData != null) // loop until no more
            {

                if (cmbIP.Text == sData)
                {
                    bExists = true;
                }

                sData = srIn.ReadLine();
            } // end loop through file

            if (bExists == false)
            {
                swOut.WriteLine(cmbIP.Text); // save to database
            }

            swOut.Close();
            srIn.Close();
            fsIn.Close();


            // set up if your a spectator or player
            if (chkSpectator.Checked == true)
            {
                GlobalVars.bSpectator = true;
                GlobalVars.bHost = false;
                GlobalVars.bPlayer2 = false;
            }
            else if (chkSpectator.Checked == false) // not a spectator
            {
                GlobalVars.bSpectator = false;
                GlobalVars.bHost = false;
                GlobalVars.bPlayer2 = true;
            }

            bInConnect = true;
            GlobalVars.iControlYou = 2;
            this.Close();
        }

        private void cmbIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.sHostIP = cmbIP.Text; // set the Host IP to what is in the combo box
        }

        private void IP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bInConnect == false)
            {
                GlobalVars.iControlYou = 1;
                //this.Close();
            }
        }

        private void IP_Load(object sender, EventArgs e)
        {
            // look up ip list
            FileStream fsIn = new FileStream(@"IP.cdata", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader srIn = new StreamReader(fsIn);

            string sData = "";
            sData = srIn.ReadLine();
            while (sData != null) // loop until no more
            {
                cmbIP.Items.Add(sData);

                sData = srIn.ReadLine();
            } // end loop through file

            srIn.Close();
            fsIn.Close();

            //cmbIP.Items.RemoveAt(0);
        }

        private void chkSpectator_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpectator.Checked == true)
            {
                GlobalVars.bSpectator = true;
                GlobalVars.bHost = false;
                GlobalVars.bPlayer2 = false;
            }
            else if (chkSpectator.Checked == false)
            {
                GlobalVars.bSpectator = false;
                GlobalVars.bHost = true;
                GlobalVars.bPlayer2 = false;
            }

        }

        private void cmbIP_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.sHostIP = cmbIP.Text;
        }

        private void DrawRectangles()
        {
            // Draws items to the screen if we decide to do so
 
        }

        private void IP_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            // look to see if IP exists
            FileStream fsIn = new FileStream(@"IP.cdata", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader srIn = new StreamReader(fsIn);
            StreamWriter swOut = new StreamWriter(fsIn);

            string sData = " ";
            string sIPDatabase = "";
            sData = srIn.ReadLine();
            while (sData != null) // loop until no more
            {
                
                if (cmbIP.Text == sData)
                { // do nothing
                }
                else
                {
                    sIPDatabase +=  sData + "\n"; // add to new database
                }

                sData = srIn.ReadLine();
            } // end loop through file

            swOut.Close();
            srIn.Close();
            fsIn.Close();



            // recreate IP database without the deleted IP
            FileStream fsIn2 = new FileStream(@"IP.cdata", FileMode.Create, FileAccess.Write);
            StreamWriter swOut2 = new StreamWriter(fsIn2);
            string[] asDataIn;
            sIPDatabase.Replace("\n", "~");
            asDataIn = sIPDatabase.Split('~');
            swOut2.WriteLine(sIPDatabase);

            swOut2.Close();
            fsIn2.Close();

            if (cmbIP.SelectedIndex >= 0)
            {
                cmbIP.Items.RemoveAt(cmbIP.SelectedIndex); // remove from combobox
            }




        }

    }
}
