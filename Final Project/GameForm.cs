// Final Project - GameForm.cs
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
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Imaging;


namespace Final_Project
{
    public partial class frmGameForm : Form
    {
        // TCP Socket
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        // game arrays
        PictureBox[,] apbArray = new PictureBox[8, 8];
        char[,] acPiece = new char[8, 8];
        // L = Red King
        // P = Orange King
        // O = orange
        // R = red
        // B = blank
        bool[,] abHighlight = new bool[8, 8];
        bool[,] abKing = new bool[8, 8]; // not sure if used


        public frmGameForm()
        {
            InitializeComponent();
        }

        private void timForceDraw_Tick(object sender, EventArgs e)
        {
            // Redraw the screen
            DrawRectangles();
            timForceDraw.Enabled = false;
        }

        private void DrawRectangles()
        {

            // Checkers Board Outline
            System.Drawing.Pen myGameOutline;
            myGameOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Chat box line 1
            System.Drawing.Pen myChatLine1;
            myChatLine1 = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Chat box line 2
            System.Drawing.Pen myChatLine2;
            myChatLine2 = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 6);

            // Player Turn Outline
            System.Drawing.Pen myPlayerTurnOutline;
            myPlayerTurnOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 4);

            // Player Turn Textbox Outline
            System.Drawing.Pen myPlayerTurnTextBoxOutline;
            myPlayerTurnTextBoxOutline = new System.Drawing.Pen(System.Drawing.Color.DarkGreen, 4);


            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myGameOutline, new Rectangle(259, 3, 520, 520)); // picture box
            formGraphics.DrawLine(myChatLine1, 0, 565, 1020, 565);
            formGraphics.DrawLine(myChatLine2, 0, 785, 1020, 785);
            formGraphics.DrawRectangle(myPlayerTurnOutline, new Rectangle(86, 40, 64, 64)); // player turn outline
            formGraphics.DrawRectangle(myPlayerTurnTextBoxOutline, new Rectangle(67, 14, 100, 26)); // player turn textbox outline

            myChatLine2.Dispose();
            myChatLine1.Dispose();
            myGameOutline.Dispose();
            formGraphics.Dispose();

            // change color and font
            txtChat.ForeColor = GlobalVars.cPlayerColor;
            txtChat.Font = GlobalVars.fntFont;
            txtChat.BackColor = GlobalVars.cBackGroundColor;
            //txtChat.Font.Size = GlobalVars.iFontSize;

            if (GlobalVars.bYourTurn == true)
            {
                txtPlayerTurnName.Text = "Your Turn"; // your turn
            }
            else if (GlobalVars.bYourTurn == false)
            {
                txtPlayerTurnName.Text = "Opponent's Turn"; // opponent's turn
            }

            // Redraw Pieces because sometimes it doesn't work right
            if (GlobalVars.bClearScreen == true)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        switch (acPiece[x, y])
                        {
                            case 'B':
                                apbArray[x, y].Image = Final_Project.Properties.Resources.blacksquare;
                                break;
                            case 'O':
                                apbArray[x, y].Image = Final_Project.Properties.Resources.orangepiece;
                                break;
                            case 'R':
                                apbArray[x, y].Image = Final_Project.Properties.Resources.redpiece;
                                break;
                            case 'L':
                                apbArray[x, y].Image = Final_Project.Properties.Resources.redpieceking;
                                break;
                            case 'P':
                                apbArray[x, y].Image = Final_Project.Properties.Resources.orangepieceking;
                                break;
                        }

                    }
                } // end for
            } // end if bClearScreen
            GlobalVars.bClearScreen = true;


            

        }

        private void GameOver(bool pbRed, bool pbOrange)
        {
            // Method GameOver
            // The game is over. duh.
            DialogResult dialog;
            if (pbRed == false)
            {
                if (GlobalVars.bHost == true)
                {
                    dialog = MessageBox.Show("You Win"); // win & host
                    if (dialog == DialogResult.OK)
                    {
                        GlobalVars.process1.Kill();
                    }
                }
                else if (GlobalVars.bPlayer2 == true)
                {
                    MessageBox.Show("You Are A Complete Loser!!!"); // lose and 2nd player
                }
            } // end if
            else if (pbOrange == false)
            {
                if (GlobalVars.bPlayer2 == true)
                {
                    MessageBox.Show("You Win"); // win and 2nd player
                }
                else if (GlobalVars.bHost == true)
                {
                    dialog = MessageBox.Show("You Are A Complete Loser!!!"); // lose and host
                    if (dialog == DialogResult.OK)
                    {
                        GlobalVars.process1.Kill(); // kill server
                    }
                }
            } // end if


            // let spectators know who won
            if (GlobalVars.bSpectator == true)
            {
                if (pbRed == false)
                {
                    MessageBox.Show("Red Wins!");
                }
                else if (pbOrange == false)
                {
                    MessageBox.Show("Orange Wins!");
                }
            }

            //return;
        } // end method

        private void frmGameForm_Load(object sender, EventArgs e)
        {


            if (GlobalVars.bSpectator == true) // if you are a spectator
            {
                btnQuit.Text = "Leave";
                btnDraw.Enabled = false;
                DisablePictureBoxes();
            }

            SetPictureBoxArray();
            DisplaceTurnImage();

            GlobalVars.bExitGame = false;
            timForceDraw.Enabled = true;

            MainForm frmMainForm = new MainForm();

            // code to check IP
            if (GlobalVars.bHost == true)
            {
                txtChat.Text += "Make sure everyone connects before moving your first piece."; // warning message

                IPHostEntry host;

                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        GlobalVars.sHostIP = ip.ToString(); // find the host's IP address
                    }
                }
            }
            //else
            try
            {
                clientSocket.Connect(GlobalVars.sHostIP, 4456);
                serverStream = clientSocket.GetStream();
            }
            catch (Exception eStupidSeverShutDown)
            {
                //MessageBox.Show("Server is disconnected");
                

                GlobalVars.iControlYou = 1;
                this.Close();
            }

            // code for limit game connection
            if (GlobalVars.bPlayer2 == true)
            {
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("P" + " >> " + GlobalVars.sName + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            else
            {
                // send name to server
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(" >> " + GlobalVars.sName + "$"); // " >> " ?
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }


            // start new thread
            Thread ctThread = new Thread(getMessage);

            ctThread.Start();


            timLimbo.Enabled = true;
        }

        private void DisablePictureBoxes()
        {
            // Method DisablePictureBoxes
            // makes it so that spectators cannot click on pieces and mess up the game
            pb00.Enabled = false;
            pb02.Enabled = false;
            pb04.Enabled = false;
            pb06.Enabled = false;
            pb11.Enabled = false;
            pb13.Enabled = false;
            pb15.Enabled = false;
            pb17.Enabled = false;
            pb20.Enabled = false;
            pb22.Enabled = false;
            pb24.Enabled = false;
            pb26.Enabled = false;
            pb31.Enabled = false;
            pb33.Enabled = false;
            pb35.Enabled = false;
            pb37.Enabled = false;
            pb40.Enabled = false;
            pb42.Enabled = false;
            pb44.Enabled = false;
            pb46.Enabled = false;
            pb51.Enabled = false;
            pb53.Enabled = false;
            pb55.Enabled = false;
            pb57.Enabled = false;
            pb60.Enabled = false;
            pb62.Enabled = false;
            pb64.Enabled = false;
            pb66.Enabled = false;
            pb71.Enabled = false;
            pb73.Enabled = false;
            pb75.Enabled = false;
            pb77.Enabled = false;
        }



        private void getMessage(object obj)
        {
            // Method getMessage
            // Listens for communications with the server

            try
            {
                

                while (!GlobalVars.bExitGame)
                {
                    serverStream = clientSocket.GetStream();

                    int buffSize = 0;
                    byte[] inStream = new byte[250]; 
                    readData = null;



                    try
                    {
                        clientSocket.ReceiveBufferSize = 250; // byte size
                        buffSize = clientSocket.ReceiveBufferSize; 
                        //MessageBox.Show(Convert.ToString(buffSize));

                        serverStream.Read(inStream, 0, buffSize);
                        string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                        returndata = returndata.Replace("\0", string.Empty); // decrease size of the message
                        readData = "" + returndata;
                        
                        

                        if (returndata == "Host Quit") // host quit
                        {
                            GlobalVars.iControlYou = 1;
                            this.Close();
                        }
                            // Player textbox
                        else if (returndata.Substring(0, 1) == "^")
                        {
                            //txtPlayers.Text = "Players:";
                            txtPlayers.Text += "\n" + returndata.Substring(5,returndata.Length - 5);
                        }
                        else if (returndata.Substring(0,1) == "!")
                        {
                            txtPlayers.Text = "Players:";
                        }
                        else if (returndata.Substring(0, 1) == "&" && GlobalVars.bHost == true && returndata.Substring(1,1) == "P")
                        {
                            Draw draw = new Draw(); // Player 2 wants to offer a draw
                            draw.ShowDialog();
                            DrawChoice(GlobalVars.sResult);
                        }
                        else if (returndata.Substring(0, 1) == "&" && GlobalVars.bPlayer2 == true && returndata.Substring(1, 1) == "H")
                        {
                            Draw draw = new Draw(); // The Host wants to offer a draw
                            draw.ShowDialog();
                            DrawChoice(GlobalVars.sResult);
                        }
                        else if (returndata.Substring(0, 1) == "&" && GlobalVars.bPlayer2 == true && returndata.Substring(1, 1) == "P")
                        {
                        }
                        else if (returndata.Substring(0, 1) == "&" && GlobalVars.bHost == true && returndata.Substring(1, 1) == "H")
                        {
                        }
                        else if (returndata.Substring(0, 1) == "Q")
                        {
                            GlobalVars.iControlYou = 1; // the game is a draw
                            this.Close();
                        }
                        else if (returndata.Substring(0, 1) == "%")
                        {
                            this.Close();
                        }
                        else if (returndata.Substring(0, 1) == "Z" && GlobalVars.bSpectator == false)
                        {
                            btnDraw.Enabled = true;
                        }

                        // if a piece was moved
                        else if (returndata.Substring(0, 1) == "*")
                        {
                            if (returndata.Length == 5)
                            {
                                MovePieces(returndata.Substring(1, 1), returndata.Substring(2, 1), returndata.Substring(3, 1), returndata.Substring(4, 1));
                            }
                            if (returndata.Length == 7)
                            {
                                MovePieces(returndata.Substring(1, 1), returndata.Substring(2, 1), returndata.Substring(3, 1), returndata.Substring(4, 1), returndata.Substring(5, 1), returndata.Substring(6, 1));
                            }
                            //UnHighLight();
                            //GlobalVars.bOnHighlightMode = false; ?
                            if (GlobalVars.bSwitchTurns == true)
                            {
                                //GlobalVars.bYourTurn = !GlobalVars.bYourTurn;
                                switch (GlobalVars.bYourTurn)
                                {
                                    case true:
                                        GlobalVars.bYourTurn = false;
                                        break;
                                    case false:
                                        GlobalVars.bYourTurn = true;
                                        break;
                                    default:
                                        MessageBox.Show("Your game crashed. Please Restart."); // Error
                                        break;
                                }

                                DisplaceTurnImage(); // change the image of whose turn it is
                                DrawRectangles();
                            }

                            GlobalVars.bSwitchTurns = true;
                            //DrawRectangles();
                            //return;
                        }
                        else
                        {
                            msg();
                        }
                    }
                    catch (Exception e)
                    {
                        // Error
                        //MessageBox.Show("Server is disconnected.");
                        this.Close();
                    }

                } // end while
            } // end try
            catch (Exception eIhopethisworks)
            {
                // close the current project and reopen it to prevent Error that reopens and closes the main form
                MessageBox.Show("Server is disconnected.");
                GlobalVars.process1 = new System.Diagnostics.Process();
                GlobalVars.process1.StartInfo.FileName = (@"Final Project.exe");
                GlobalVars.process1.Start(); 
                Environment.Exit(0);

            }
        }

        private void DrawChoice(string DialogResult)
        {
            // Send whether the game is a draw or not depending on what the other player says
            if (DialogResult == "Yes")
            {
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Q" + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            else if (DialogResult == "No")
            {
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Z" + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }


        private void DisplaceTurnImage()
        {
            // displays whose turn it is on the picture box in the top left
            if (GlobalVars.bYourTurn == true && GlobalVars.bHost == true)
            {
                pbDisplay.Image = Final_Project.Properties.Resources.redpiece;
            }
            else if (GlobalVars.bYourTurn == false && GlobalVars.bHost == true)
            {
                pbDisplay.Image = Final_Project.Properties.Resources.orangepiece;
            }
            else if (GlobalVars.bYourTurn == true && GlobalVars.bPlayer2 == true)
            {
                pbDisplay.Image = Final_Project.Properties.Resources.orangepiece;
            }
            else if (GlobalVars.bYourTurn == false && GlobalVars.bPlayer2 == true)
            {
                pbDisplay.Image = Final_Project.Properties.Resources.redpiece;
            }
        }


        private void UnHighLight()
        {
            // Unhighlight the pieces
            for (int iCnt = 0; iCnt < 8; iCnt++)
            {
                for (int icnt2 = 0; icnt2 < 8; icnt2++)
                {
                    if (acPiece[iCnt, icnt2] == 'B')
                    {
                        apbArray[iCnt, icnt2].Image = Final_Project.Properties.Resources.blacksquare;
                    }
                    abHighlight[iCnt, icnt2] = false;
                }
            }
            GlobalVars.bOnHighlightMode = false;
        }

        private void MovePieces(string pXMoving, string pYMoving, string pXMoveTo, string pYMoveTo, string pxJumpedPiece = "NONE", string pyJumpedPiece = "NONE")
        {

            // if red
            if (acPiece[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)] == 'R')
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.redpiece;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'R';
            }
            // if orange
            if (acPiece[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)] == 'O')
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.orangepiece;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'O';
            }
            // if red king
            if (acPiece[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)] == 'L')
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.redpieceking;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'L';
            }
            // if orange king
            if (acPiece[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)] == 'P')
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.orangepieceking;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'P';
            }



            //red king
            if (acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] == 'R' && pYMoveTo == "0")
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.redpieceking;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'L';
            }

            //orange king
            if (acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] == 'O' && pYMoveTo == "7")
            {
                apbArray[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)].Image = Final_Project.Properties.Resources.orangepieceking;
                acPiece[Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo)] = 'P';
            }
            
            
            apbArray[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)].Image = Final_Project.Properties.Resources.blacksquare;
            acPiece[Convert.ToInt32(pXMoving), Convert.ToInt32(pYMoving)] = 'B';

            //jump
            if (pxJumpedPiece != "NONE" && pyJumpedPiece != "NONE")
            {
                acPiece[Convert.ToInt32(pxJumpedPiece), Convert.ToInt32(pyJumpedPiece)] = 'B';
                apbArray[Convert.ToInt32(pxJumpedPiece), Convert.ToInt32(pyJumpedPiece)].Image = Final_Project.Properties.Resources.blacksquare;
                //double jump
                DrawRectangles();
                DoubleJump(Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo));

            }

            if (GlobalVars.bDoubleJump == false)
            {
                UnHighLight();
                GlobalVars.bOnHighlightMode = false; 
            }
            else if (GlobalVars.bDoubleJump == true)
            {
                //HighlightPieces(Convert.ToInt32(pXMoveTo), Convert.ToInt32(pYMoveTo));
                GlobalVars.bSwitchTurns = false;
                GlobalVars.bDoubleJump = false;
            }
            //GlobalVars.iMovePieceX = 8;
            //GlobalVars.iMovePieceY = 8;

            // check to see if pieces are still there
            if (GlobalVars.bGameOver == false)
            {
                bool bRed = false;
                bool bOrange = false;

                // Check to see if red has any pieces left
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        switch (acPiece[x, y])
                        {
                            case 'R':
                                bRed = true;
                                break;
                            case 'L':
                                bRed = true;
                                break;
                            default:
                                break;
                        }
                    } // end for y
                } // end for x

                // Check to see if orange has any pieces left
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        switch (acPiece[x, y])
                        {
                            case 'O':
                                bOrange = true;
                                break;
                            case 'P':
                                bOrange = true;
                                break;
                            default:
                                break;
                        }
                    } // end for y
                } // end for x

                if (bRed == false) // red loses
                {
                    GameOver(true, false);
                    GlobalVars.bGameOver = true;
                    //timGameOver.Enabled = false;
                    //Thread.Sleep(1000);
                }
                if (bOrange == false) // orange loses
                {
                    GameOver(false, true);
                    GlobalVars.bGameOver = true;
                    //timGameOver.Enabled = false;
                    //Thread.Sleep(1000);
                }
            }
        }

        private void DoubleJump(int px, int py)
        {
            // Method DoubleJump - determines whether you can double jump

            // if your red
            if (acPiece[px, py] == 'R')
            {
                if (GlobalVars.bYourTurn == true)
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.Highlighted_Red;
                    abHighlight[px, py] = true;
                }
                GlobalVars.iMovePieceX = px;
                GlobalVars.iMovePieceY = py;
                if (px - 2 >= 0 && py - 2 >= 0)
                {
                    if ((acPiece[px - 1, py - 1] == 'O' || acPiece[px - 1, py - 1] == 'P') && acPiece[px - 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px + 2 <= 7 && py - 2 >= 0)
                {
                    if ((acPiece[px + 1, py - 1] == 'O' || acPiece[px + 1, py - 1] == 'P') && acPiece[px + 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                GlobalVars.bOnHighlightMode = true;
            } // end if host and red piece

            //if your red king
            if (acPiece[px, py] == 'L')
            {
                if (GlobalVars.bYourTurn == true)
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.highredpieceking; // highlighted king
                    abHighlight[px, py] = true;
                }
                GlobalVars.iMovePieceX = px;
                GlobalVars.iMovePieceY = py;
                if (px + 2 <= 7 && py + 2 <= 7)
                {
                    if ((acPiece[px + 1, py + 1] == 'O' || acPiece[px + 1, py + 1] == 'P') && acPiece[px + 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px - 2 >= 0 && py + 2 <= 7)
                {
                    if ((acPiece[px - 1, py + 1] == 'O' || acPiece[px - 1, py + 1] == 'P') && acPiece[px - 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px - 2 >= 0 && py - 2 >= 0)
                {
                    if ((acPiece[px - 1, py - 1] == 'O' || acPiece[px - 1, py - 1] == 'P') && acPiece[px - 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px + 2 <= 7 && py - 2 >= 0)
                {
                    if ((acPiece[px + 1, py - 1] == 'O' || acPiece[px + 1, py - 1] == 'P') && acPiece[px + 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                GlobalVars.bOnHighlightMode = true;
            } // end if red king

            // if your orange
            if (acPiece[px, py] == 'O')
            {
                if (GlobalVars.bYourTurn == true)
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.Highlighted_Orange;
                    abHighlight[px, py] = true;
                }
                GlobalVars.iMovePieceX = px;
                GlobalVars.iMovePieceY = py;
                if (px + 2 <= 7 && py + 2 <= 7)
                {
                    if ((acPiece[px + 1, py + 1] == 'R' || acPiece[px + 1, py + 1] == 'L') && acPiece[px + 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px - 2 >= 0 && py + 2 <= 7)
                {
                    if ((acPiece[px - 1, py + 1] == 'R' || acPiece[px - 1, py + 1] == 'L') && acPiece[px - 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                GlobalVars.bOnHighlightMode = true;
            } // end if player 2 and orange piece

            //if your orange king
            if (acPiece[px, py] == 'P')
            {
                if (GlobalVars.bYourTurn == true)
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.highorangepieceking; // add highlighted orange
                    abHighlight[px, py] = true;
                }
                GlobalVars.iMovePieceX = px;
                GlobalVars.iMovePieceY = py;
                if (px - 2 >= 0 && py - 2 >= 0)
                {
                    if ((acPiece[px - 1, py - 1] == 'R' || acPiece[px - 1, py - 1] == 'L') && acPiece[px - 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px + 2 <= 7 && py - 2 >= 0)
                {
                    if ((acPiece[px + 1, py - 1] == 'R' || acPiece[px + 1, py - 1] == 'L') && acPiece[px + 2, py - 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px + 2 <= 7 && py + 2 <= 7)
                {
                    if ((acPiece[px + 1, py + 1] == 'R' || acPiece[px + 1, py + 1] == 'L') && acPiece[px + 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                if (px - 2 >= 0 && py + 2 <= 7)
                {
                    if ((acPiece[px - 1, py + 1] == 'R' || acPiece[px - 1, py + 1] == 'L') && acPiece[px - 2, py + 2] == 'B')
                    {
                        if (GlobalVars.bYourTurn == true)
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                        GlobalVars.bDoubleJump = true;
                        GlobalVars.bOnHighlightMode = true;
                    }
                }
                GlobalVars.bOnHighlightMode = true;
            } // end if orange king

        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
            {
                // display message to chat box
                txtChat.Text += Environment.NewLine + readData;

                // fix spamming error
                GlobalVars.bClearScreen = false;

                
                timForceDraw.Enabled = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // send message to server to send back to everyone

            if (txtMessageBox.Text.Length > 0)
            {
                byte[] outStreamMsg = System.Text.Encoding.ASCII.GetBytes(" " + txtMessageBox.Text + "$");
                serverStream.Write(outStreamMsg, 0, outStreamMsg.Length);
                serverStream.Flush();
            }

            txtMessageBox.Text = null;
            Thread.Sleep(100);

        }

        private void txtMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // allows you to hit the send button with the enter key
            {
                btnSend_Click(sender, e);
            }
        }


        private void frmGameForm_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles(); // redraw the screen

        }

        private void btnOptions_Click(object sender, EventArgs e)
        {

            frmOptionForm frmOptionForm = new frmOptionForm();
            frmOptionForm.ShowDialog(); // open up the options form


        }

        private void txtChat_TextChanged(object sender, EventArgs e)
        {
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret(); // scroll to end
        }

        private void frmGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {


            if (GlobalVars.bHost == true)
            {
                byte[] outStream2 = System.Text.Encoding.ASCII.GetBytes("Host Quit" + "$"); // Host quit
                serverStream.Write(outStream2, 0, outStream2.Length);
                serverStream.Flush();
            }
            else
            {
                try
                { // someone else left the game
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes(" >> " + GlobalVars.sName + " quit the game." + "$");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
                catch (Exception eError)
                {
                    MessageBox.Show("Server is disconnected.");
                    GlobalVars.iControlYou = 1;
                }
            }

            try
            {
                serverStream.Dispose();
                serverStream.Close();
            }
            catch (Exception eStuff)
            { }
            GlobalVars.bExitGame = true;
            //GlobalVars.iControlYou = 1;
            if (GlobalVars.bHost == true)
            {
                //Thread.Sleep(5000);
                GlobalVars.process1.Kill(); // kill server
            }
            GlobalVars.iControlYou = 1;
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close(); // close game

        }

        private void SetPictureBoxArray()
        {
            // initialize picture box array
            apbArray[0, 0] = pb00;
            acPiece[0, 0] = 'O';
            apbArray[0, 2] = pb02;
            acPiece[0, 2] = 'O';
            apbArray[0, 4] = pb04;
            acPiece[0, 4] = 'B';
            apbArray[0, 6] = pb06;
            acPiece[0, 6] = 'R';
            apbArray[1, 1] = pb11;
            acPiece[1, 1] = 'O';
            apbArray[1, 3] = pb13;
            acPiece[1, 3] = 'B';
            apbArray[1, 5] = pb15;
            acPiece[1, 5] = 'R';
            apbArray[1, 7] = pb17;
            acPiece[1, 7] = 'R';
            apbArray[2, 0] = pb20;
            acPiece[2, 0] = 'O';
            apbArray[2, 2] = pb22;
            acPiece[2, 2] = 'O';
            apbArray[2, 4] = pb24;
            acPiece[2, 4] = 'B';
            apbArray[2, 6] = pb26;
            acPiece[2, 6] = 'R';
            apbArray[3, 1] = pb31;
            acPiece[3, 1] = 'O';
            apbArray[3, 3] = pb33;
            acPiece[3, 3] = 'B';
            apbArray[3, 5] = pb35;
            acPiece[3, 5] = 'R';
            apbArray[3, 7] = pb37;
            acPiece[3, 7] = 'R';
            apbArray[4, 0] = pb40;
            acPiece[4, 0] = 'O';
            apbArray[4, 2] = pb42;
            acPiece[4, 2] = 'O';
            apbArray[4, 4] = pb44;
            acPiece[4, 4] = 'B';
            apbArray[4, 6] = pb46;
            acPiece[4, 6] = 'R';
            apbArray[5, 1] = pb51;
            acPiece[5, 1] = 'O';
            apbArray[5, 3] = pb53;
            acPiece[5, 3] = 'B';
            apbArray[5, 5] = pb55;
            acPiece[5, 5] = 'R';
            apbArray[5, 7] = pb57;
            acPiece[5, 7] = 'R';
            apbArray[6, 0] = pb60;
            acPiece[6, 0] = 'O';
            apbArray[6, 2] = pb62;
            acPiece[6, 2] = 'O';
            apbArray[6, 4] = pb64;
            acPiece[6, 4] = 'B';
            apbArray[6, 6] = pb66;
            acPiece[6, 6] = 'R';
            apbArray[7, 1] = pb71;
            acPiece[7, 1] = 'O';
            apbArray[7, 3] = pb73;
            acPiece[7, 3] = 'B';
            apbArray[7, 5] = pb75;
            acPiece[7, 5] = 'R';
            apbArray[7, 7] = pb77;
            acPiece[7, 7] = 'R';

            // set bool array to false
            for (int iCntX = 0; iCntX < 8; iCntX++)
            {
                for (int iCntY = 0; iCntY < 8; iCntY++)
                {
                    abHighlight[iCntX, iCntY] = false;
                } // end for y
            } // end for x
        }


        private void HighlightPieces(int px, int py)
        {
            if (GlobalVars.bYourTurn == true)
            {


                // if your red
                if (GlobalVars.bHost == true && acPiece[px, py] == 'R')
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.Highlighted_Red;
                    abHighlight[px, py] = true;
                    GlobalVars.iMovePieceX = px;
                    GlobalVars.iMovePieceY = py;
                    if (px - 1 >= 0 && py - 1 >= 0)
                    {
                        if (acPiece[px - 1, py - 1] == 'B')// up left
                        {
                            apbArray[px - 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py - 1] = true;
                            GlobalVars.bOnHighlightMode = true;
                        }
                    }
                    if (px + 1 <= 7 && py - 1 >= 0)
                    {
                        if (acPiece[px + 1, py - 1] == 'B') // up right
                        {
                            apbArray[px + 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py - 1] = true;
                            GlobalVars.bOnHighlightMode = true;
                        }
                    }
                    if (px - 2 >= 0 && py - 2 >= 0)
                    {
                        if ((acPiece[px - 1, py - 1] == 'O' || acPiece[px - 1, py - 1] == 'P') && acPiece[px - 2, py - 2] == 'B')
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                            GlobalVars.bOnHighlightMode = true;
                        }
                    }
                    if (px + 2 <= 7 && py - 2 >= 0)
                    {
                        if ((acPiece[px + 1, py - 1] == 'O' || acPiece[px + 1, py - 1] == 'P') && acPiece[px + 2, py - 2] == 'B')
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                            GlobalVars.bOnHighlightMode = true;
                        }
                    }
                    GlobalVars.bOnHighlightMode = true;
                } // end if host and red piece

                //if your red king
                if (GlobalVars.bHost == true && acPiece[px, py] == 'L')
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.highredpieceking; // highlighted king
                    abHighlight[px, py] = true;
                    GlobalVars.iMovePieceX = px;
                    GlobalVars.iMovePieceY = py;
                    if (px + 1 <= 7 && py + 1 <= 7)
                    {
                        if (acPiece[px + 1, py + 1] == 'B') // down right
                        {
                            apbArray[px + 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py + 1] = true;
                        }
                    }
                    if (px - 1 >= 0 && py + 1 <= 7)
                    {
                        if (acPiece[px - 1, py + 1] == 'B') // down left
                        {
                            apbArray[px - 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py + 1] = true;
                        }
                    }
                    if (px + 2 <= 7 && py + 2 <= 7)
                    {
                        if ((acPiece[px + 1, py + 1] == 'O' || acPiece[px + 1, py + 1] == 'P') && acPiece[px + 2, py + 2] == 'B')
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                    }
                    if (px - 2 >= 0 && py + 2 <= 7)
                    {
                        if ((acPiece[px - 1, py + 1] == 'O' || acPiece[px - 1, py + 1] == 'P') && acPiece[px - 2, py + 2] == 'B')
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                    }
                    if (px - 1 >= 0 && py - 1 >= 0)
                    {
                        if (acPiece[px - 1, py - 1] == 'B')// up left
                        {
                            apbArray[px - 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py - 1] = true;
                        }
                    }
                    if (px + 1 <= 7 && py - 1 >= 0)
                    {
                        if (acPiece[px + 1, py - 1] == 'B') // up right
                        {
                            apbArray[px + 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py - 1] = true;
                        }
                    }
                    if (px - 2 >= 0 && py - 2 >= 0)
                    {
                        if ((acPiece[px - 1, py - 1] == 'O' || acPiece[px - 1, py - 1] == 'P') && acPiece[px - 2, py - 2] == 'B')
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                        }
                    }
                    if (px + 2 <= 7 && py - 2 >= 0)
                    {
                        if ((acPiece[px + 1, py - 1] == 'O' || acPiece[px + 1, py - 1] == 'P') && acPiece[px + 2, py - 2] == 'B')
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                        }
                    }
                    GlobalVars.bOnHighlightMode = true;
                } // end if red king

                // if your orange
                if (GlobalVars.bPlayer2 == true && acPiece[px, py] == 'O')
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.Highlighted_Orange;
                    abHighlight[px, py] = true;
                    GlobalVars.iMovePieceX = px;
                    GlobalVars.iMovePieceY = py;
                    if (px + 1 <= 7 && py + 1 <= 7)
                    {
                        if (acPiece[px + 1, py + 1] == 'B') // down right
                        {
                            apbArray[px + 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py + 1] = true;
                        }
                    }
                    if (px - 1 >= 0 && py + 1 <= 7)
                    {
                        if (acPiece[px - 1, py + 1] == 'B') // down left
                        {
                            apbArray[px - 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py + 1] = true;
                        }
                    }
                    if (px + 2 <= 7 && py + 2 <= 7)
                    {
                        if ((acPiece[px + 1, py + 1] == 'R' || acPiece[px + 1, py + 1] == 'L') && acPiece[px + 2, py + 2] == 'B')
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                    }
                    if (px - 2 >= 0 && py + 2 <= 7)
                    {
                        if ((acPiece[px - 1, py + 1] == 'R' || acPiece[px - 1, py + 1] == 'L') && acPiece[px - 2, py + 2] == 'B')
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                    }
                    GlobalVars.bOnHighlightMode = true;
                } // end if player 2 and orange piece

                //if your orange king
                if (GlobalVars.bPlayer2 == true && acPiece[px, py] == 'P')
                {
                    apbArray[px, py].Image = Final_Project.Properties.Resources.highorangepieceking; // add highlighted orange
                    abHighlight[px, py] = true;
                    GlobalVars.iMovePieceX = px;
                    GlobalVars.iMovePieceY = py;
                    if (px - 1 >= 0 && py - 1 >= 0)
                    {
                        if (acPiece[px - 1, py - 1] == 'B')// up left
                        {
                            apbArray[px - 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py - 1] = true;
                        }
                    }
                    if (px + 1 <= 7 && py - 1 >= 0)
                    {
                        if (acPiece[px + 1, py - 1] == 'B') // up right
                        {
                            apbArray[px + 1, py - 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py - 1] = true;
                        }
                    }
                    if (px - 2 >= 0 && py - 2 >= 0)
                    {
                        if ((acPiece[px - 1, py - 1] == 'R' || acPiece[px - 1, py - 1] == 'L') && acPiece[px - 2, py - 2] == 'B')
                        {
                            apbArray[px - 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py - 2] = true;
                        }
                    }
                    if (px + 2 <= 7 && py - 2 >= 0)
                    {
                        if ((acPiece[px + 1, py - 1] == 'R' || acPiece[px + 1, py - 1] == 'L') && acPiece[px + 2, py - 2] == 'B')
                        {
                            apbArray[px + 2, py - 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py - 2] = true;
                        }
                    }
                    if (px + 1 <= 7 && py + 1 <= 7)
                    {
                        if (acPiece[px + 1, py + 1] == 'B') // down right
                        {
                            apbArray[px + 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 1, py + 1] = true;
                        }
                    }
                    if (px - 1 >= 0 && py + 1 <= 7)
                    {
                        if (acPiece[px - 1, py + 1] == 'B') // down left
                        {
                            apbArray[px - 1, py + 1].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 1, py + 1] = true;
                        }
                    }
                    if (px + 2 <= 7 && py + 2 <= 7)
                    {
                        if ((acPiece[px + 1, py + 1] == 'R' || acPiece[px + 1, py + 1] == 'L') && acPiece[px + 2, py + 2] == 'B')
                        {
                            apbArray[px + 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px + 2, py + 2] = true;
                        }
                    }
                    if (px - 2 >= 0 && py + 2 <= 7)
                    {
                        if ((acPiece[px - 1, py + 1] == 'R' || acPiece[px - 1, py + 1] == 'L') && acPiece[px - 2, py + 2] == 'B')
                        {
                            apbArray[px - 2, py + 2].Image = Final_Project.Properties.Resources.Highlight_black_square;
                            abHighlight[px - 2, py + 2] = true;
                        }
                    }
                    GlobalVars.bOnHighlightMode = true;
                } // end if orange king

            }
        }

        private void pb15_Click(object sender, EventArgs e)
        {
            ClickPiece(1, 5); // peice 1,5 was clicked

        }

        private void ClickPiece(int p1, int p2)
        {
            if (GlobalVars.bYourTurn == true) // make sure it's your turn
            {
                if (GlobalVars.bOnHighlightMode == false && abHighlight[p1, p2] == false)
                {
                    HighlightPieces(p1, p2); // highlight pieces if not already highlighted
                }
                else if (GlobalVars.bOnHighlightMode == true && abHighlight[p1, p2] == true)
                {
                    if (p1 == GlobalVars.iMovePieceX && p2 == GlobalVars.iMovePieceY)
                    {
                        GlobalVars.bOnHighlightMode = false;
                        abHighlight[p1, p2] = false;
                        UnHighLight(); // unhighlight pieces
                        if (acPiece[p1, p2] == 'R')
                        {
                            apbArray[p1, p2].Image = Final_Project.Properties.Resources.redpiece;
                        }
                        if (acPiece[p1, p2] == 'O')
                        {
                            apbArray[p1, p2].Image = Final_Project.Properties.Resources.orangepiece;
                        }
                        if (acPiece[p1, p2] == 'L')
                        {
                            apbArray[p1, p2].Image = Final_Project.Properties.Resources.redpieceking;

                        }
                        if (acPiece[p1, p2] == 'P')
                        {
                            apbArray[p1, p2].Image = Final_Project.Properties.Resources.orangepieceking;
                        }
                        return;
                    }

                    // check to see if you jumped a piece
                    GlobalVars.iDifferenceX = Math.Abs(p1 - GlobalVars.iMovePieceX);
                    GlobalVars.iDifferenceY = Math.Abs(p2 - GlobalVars.iMovePieceY);

                    if (GlobalVars.iDifferenceX == 2 && GlobalVars.iDifferenceY == 2)
                    {
                        FindJumpingDirection(GlobalVars.iMovePieceX, GlobalVars.iMovePieceY, p1, p2); // find out which way you jumped
                        SendServerGameInfo(GlobalVars.iMovePieceX, GlobalVars.iMovePieceY, p1, p2, GlobalVars.iJumpPieceX, GlobalVars.iJumpPieceY); // send info to server
                    }
                    else
                    {
                        SendServerGameInfo(GlobalVars.iMovePieceX, GlobalVars.iMovePieceY, p1, p2);
                    }
                    UnHighLight();
                } // end else if highlight

            } // end bYourTurn
            else if (GlobalVars.bYourTurn == false)
            {
                GlobalVars.bClearScreen = false;
                return;
            }

        } // end void ClickPiece

        private void SendServerGameInfo(int pxMoving, int pyMoving, int pxMoveTo, int pyMoveTo, int pxJump = -1, int pyJump = -1)
        {
            // Method SendServerGameInfo - send moving info to server
            string sSendGameData = "";
            if (pxJump < 0 && pyJump < 0)
            {
                sSendGameData = "*" + Convert.ToString(pxMoving) + Convert.ToString(pyMoving) + Convert.ToString(pxMoveTo) + Convert.ToString(pyMoveTo);
            }
            if (pxJump >= 0 && pyJump >= 0)
            {
                sSendGameData = "*" + Convert.ToString(pxMoving) + Convert.ToString(pyMoving) + Convert.ToString(pxMoveTo) + Convert.ToString(pyMoveTo) + Convert.ToString(pxJump) + Convert.ToString(pyJump);
            }

            // MessageBox.Show(sSendGameData);

            byte[] outStreamGame = System.Text.Encoding.ASCII.GetBytes(sSendGameData + "$");
            serverStream.Write(outStreamGame, 0, outStreamGame.Length);
            serverStream.Flush();
        }

        private void FindJumpingDirection(int pxMoving, int pyMoving, int pxMoveTo, int pyMoveTo)
        {
            // Method FindJumpingDirection
            // finds the direction you are jumping

            // find x value
            if (pxMoveTo > pxMoving)
            {
                GlobalVars.iJumpPieceX = pxMoving + 1;
            }
            else if (pxMoveTo < pxMoving)
            {
                GlobalVars.iJumpPieceX = pxMoving - 1;
            }

            // find y value
            if (GlobalVars.bPlayer2 == true && pyMoveTo > pyMoving)
            {
                GlobalVars.iJumpPieceY = pyMoving + 1;
            } // orange piece
            else if (GlobalVars.bHost == true && pyMoveTo < pyMoving)
            {
                GlobalVars.iJumpPieceY = pyMoving - 1;
            } // red piece
            else if (GlobalVars.bHost == true && pyMoveTo > pyMoving)
            {
                GlobalVars.iJumpPieceY = pyMoving + 1;
            } // red king
            else if (GlobalVars.bPlayer2 == true && pyMoveTo < pyMoving)
            {
                GlobalVars.iJumpPieceY = pyMoving - 1;
            } // orange king

        } // end method FindJumpingDirection

        #region Pieces
        // click on pieces
        private void pb66_Click(object sender, EventArgs e)
        {
            ClickPiece(6, 6);

        }

        private void pb75_Click(object sender, EventArgs e)
        {
            ClickPiece(7, 5);
        }

        private void pb24_Click(object sender, EventArgs e)
        {
            ClickPiece(2, 4);
        }

        private void pb33_Click(object sender, EventArgs e)
        {
            ClickPiece(3, 3);
        }

        private void pb04_Click(object sender, EventArgs e)
        {
            ClickPiece(0, 4);
        }

        private void pb11_Click(object sender, EventArgs e)
        {
            ClickPiece(1, 1);
        }

        private void pb02_Click(object sender, EventArgs e)
        {
            ClickPiece(0, 2);
        }

        private void pb22_Click(object sender, EventArgs e)
        {
            ClickPiece(2, 2);
        }

        private void pb42_Click(object sender, EventArgs e)
        {
            ClickPiece(4, 2);
        }

        private void pb62_Click(object sender, EventArgs e)
        {
            ClickPiece(6, 2);
        }

        private void pb35_Click(object sender, EventArgs e)
        {
            ClickPiece(3, 5);
        }

        private void pb55_Click(object sender, EventArgs e)
        {
            ClickPiece(5, 5);
        }

        private void pb64_Click(object sender, EventArgs e)
        {
            ClickPiece(6, 4);
        }

        private void pb44_Click(object sender, EventArgs e)
        {
            ClickPiece(4, 4);
        }

        private void pb13_Click(object sender, EventArgs e)
        {
            ClickPiece(1, 3);
        }

        private void pb53_Click(object sender, EventArgs e)
        {
            ClickPiece(5, 3);
        }

        private void pb73_Click(object sender, EventArgs e)
        {
            ClickPiece(7, 3);
        }



        private void pb20_Click(object sender, EventArgs e)
        {
            ClickPiece(2, 0);
        }

        private void pb40_Click(object sender, EventArgs e)
        {
            ClickPiece(4, 0);
        }

        private void pb60_Click(object sender, EventArgs e)
        {
            ClickPiece(6, 0);
        }

        private void pb71_Click(object sender, EventArgs e)
        {
            ClickPiece(7, 1);
        }

        private void pb51_Click(object sender, EventArgs e)
        {
            ClickPiece(5, 1);
        }

        private void pb31_Click(object sender, EventArgs e)
        {
            ClickPiece(3, 1);
        }

        private void pb17_Click(object sender, EventArgs e)
        {
            ClickPiece(1, 7);
        }

        private void pb06_Click(object sender, EventArgs e)
        {
            ClickPiece(0, 6);
        }

        private void pb00_Click(object sender, EventArgs e)
        {
            ClickPiece(0, 0);
        }

        private void pb26_Click(object sender, EventArgs e)
        {
            ClickPiece(2, 6);
        }

        private void pb37_Click(object sender, EventArgs e)
        {
            ClickPiece(3, 7);
        }

        private void pb46_Click(object sender, EventArgs e)
        {
            ClickPiece(4, 6);
        }

        private void pb57_Click(object sender, EventArgs e)
        {
            ClickPiece(5, 7);
        }

        private void pb77_Click(object sender, EventArgs e)
        {
            ClickPiece(7, 7);
        }

        #endregion



        private void btnDraw_Click(object sender, EventArgs e)
        {
            // Offer a draw
            btnDraw.Enabled = false;
            OfferDraw();
            //GlobalVars.bInDrawMode = true;

            
        }


        private void OfferDraw()
        {
            // Method OfferDraw - Send info to server to ask the other player if they want to make a draw
                if (GlobalVars.bHost == true)
                {
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes("&H" + "$"); // you are the host
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
                else if (GlobalVars.bPlayer2 == true)
                {
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes("&P" + "$"); // you r the 2nd player
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
            
        }

        private void timLimbo_Tick(object sender, EventArgs e)
        {
            // Disconnects clients who are in a state of Limbo when they
            // try to connect either as a 2nd player when there is already one
            // or if the game has already started
            if (txtChat.Text == "")
            {
                timLimbo.Enabled = false;
                if (GlobalVars.bPlayer2 == true)
                {
                    MessageBox.Show("There is already a 2nd player. Please join as a Spectator."); // there is already a 2nd player
                }
                else if (GlobalVars.bSpectator == true)
                {
                    MessageBox.Show("Sorry, but the game has already started. Please wait until the next game."); // game has started
                }
                this.Close();
            }
            timLimbo.Enabled = false;

        }

        private void btnCheckIP_Click(object sender, EventArgs e)
        {
            // code to check IP
                IPHostEntry host;

                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        txtChat.Text += "\nYour IP Address is: " + ip.ToString(); // Send IP address to chat box
                    }
                }
            
        }





    } // end class
} // end namespace