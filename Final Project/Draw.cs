// Final Project - Draw.cs
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

namespace Final_Project
{
    public partial class Draw : Form
    {
        public Draw()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            GlobalVars.sResult = "Yes"; // The Game is a draw
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e) // the opponent doesn't want to offer a draw
        {
            GlobalVars.sResult = "No";
            this.Close();
        }
    }
}
