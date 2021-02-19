// Final Project - Program.cs
// Created by: Austin Baker & Chip Denton
// Created on: Feb. 2013 - April 2013
// (c) Copyright 2013, P.I.R.A.T.E.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (GlobalVars.iControlYou != -1)
            {
                switch (GlobalVars.iControlYou)
                {
                    case 1:  //main form
                        Application.Run(new MainForm());
                        break;
                    case 2:  //Game Form
                        Application.Run(new frmGameForm());
                        break;
                    case 3:  // IP Form
                        Application.Run(new IP());
                        break;
                    case 4:  //Unused
                        break;
                    //default:
                    //    break;
                }
            }
            //Application.Run(new MainForm());
            
        }
    }
}
