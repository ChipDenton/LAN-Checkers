// Final Project - GlobalVars.cs
// Created by: Austin Baker & Chip Denton
// Created on: Feb. 2013 - April 2013
// (c) Copyright 2013, P.I.R.A.T.E.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Final_Project
{
    class GlobalVars
    {
        public static string sName = " "; // your name
        public static string sWelcomeBack = ""; // our welcome back message
        public static string sHostIP; // the IP address of the host/server
        public static Color cPlayerColor = Color.DarkGreen; // your chat box color
        public static Color cBackGroundColor = Color.Black; // your chat box background
        public static Font fntFont = new Font("Microsoft Sans Serif", 9); // your chat font
        public static System.Diagnostics.Process process1; // a new process
        public static int iControlYou = 1; // our variable to decide which form to open
        public static int iFontSize = 9; // your chat font size
        public static int iMovePieceX; // the x-value of the piece you're going to move
        public static int iMovePieceY; // the y-value of the piece you're going to move
        public static int iJumpPieceX; // x-value of piece being jumped
        public static int iJumpPieceY; //  y"                          "
        public static int iDifferenceX; // used to determine if you jumped
        public static int iDifferenceY; // used to determine if you jumped
        public static int iPlaceToMove = 0; 
        public static bool bExitGame = false;
        public static bool bHost = false; // are you the host
        public static bool bExists = false;
        public static bool bPlayer2 = false; // are you player 2
        public static bool bSpectator = false; // are you a spectator
        public static bool bOnHighlightMode = false; // did you click on a piece
        public static bool bYourTurn = false; // is it your turn
        public static bool bKingMeRed = false;
        public static bool bKingMeOrange = false;
        public static bool bDoubleJump = false; // can you double jump
        public static bool bSwitchTurns = true; // determines whether the game should switch turns or not
        public static bool bClearScreen = true;
        public static bool bGameOver = false;
        public static bool bInDrawMode = false; // check to see if someone hit the draw button
        public static string sResult = "";
        
    }
}
