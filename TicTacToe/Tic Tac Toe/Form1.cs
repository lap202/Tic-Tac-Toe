/* Author: Andrew Bach
 * Date: 11/12/2020
 * Program Description: A Tic Tac Toe Game vs a, probably too hard coded, AI. The board is stored in a 2D array. When the player clicks
 *                      a  value of 1 is put in the corresponding spot on the array. When the AI "clicks" a value of -1 is put in the
 *                      corresponding spot on the array.
 *                      The CheckScore method then adds up the slots. if it adds up to 3, Player 1 has won the game.
 *                      If it adds up to -3, the AI has won the game. If it doesn't add up to 3 or -3 and turnsRemaining is 0,
 *                      The game ends in a draw. New game, simulate, and Exit are placed in the menu strip.
 *                      
 *                      So I read the problem to make sure I completed it and realized it was supposed to simulate the game. I added a
 *                      button that now simulates a game. If I had built from the ground up to simulate, I would have used 1 method for
 *                      the AI and used arguments to determine whos turn it was and what was being put in the array. Instead I have 3 AI
 *                      Methods which take up 900 lines. I put these at the bottom of my code to make it easier to see rest of program.
 */                     

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

namespace Tic_Tac_Toe
{
    public partial class FormGameBoard : Form
    {
        //Declare variable to store player turn
        int playerTurn = 1;
        int turnsRemaining = 9;
        int simming = 0;

        //Declare an array to store board data
        int[,] board = new int[3, 3];

        //Declare an array to store pictureboxes
        PictureBox[,] pictureBoxSlot = new PictureBox[3 , 3];

        public FormGameBoard()
        {
            InitializeComponent();

            //Set board array elements to 0
            foreach (int number in board)
            {
                board[number, 0] = 0;
                board[number, 1] = 0;
                board[number, 2] = 0;
            }

            //Assign the Slots to the pictureBoxSlot array
            pictureBoxSlot[0 , 0] = pictureBoxSlot0;
            pictureBoxSlot[0 , 1] = pictureBoxSlot1;
            pictureBoxSlot[0 , 2] = pictureBoxSlot2;
            pictureBoxSlot[1 , 0] = pictureBoxSlot3;
            pictureBoxSlot[1 , 1] = pictureBoxSlot4;
            pictureBoxSlot[1 , 2] = pictureBoxSlot5;
            pictureBoxSlot[2 , 0] = pictureBoxSlot6;
            pictureBoxSlot[2 , 1] = pictureBoxSlot7;
            pictureBoxSlot[2 , 2] = pictureBoxSlot8;
        }

        //Reset variables and the game board for the next game
        private void ResetGame()
        {
            //Set board back to 0s
            for (int x = 0; x < board.GetLength(0); x++)
            {
                board[x, 0] = 0;
                board[x, 1] = 0;
                board[x, 2] = 0;
            }

            //Set turns back to 9 and next turn to player 1
            playerTurn = 1;
            turnsRemaining = 9;
            simming = 0;

            //Update board graphics
            UpdateGraphics();
            if (simming == 0)
            {
                labelInfo.Text = "The Player is up! Place an X on the game board. If you get 3 in a row, you win!";
            }
            else
            {
                labelInfo.Text = "AI (X) is up.";
            }
        }

        //Timer function I found online to make my AI seem like its thinking
        //Would pretend I fully understand it, but still have some learning to do.
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        //UpdateGraphics Method simply checks the board array and updates the GUI
        private void UpdateGraphics()
        {
            //Set the proper image for the slot the player clicked
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (board[x, y] == 1)
                    {
                        pictureBoxSlot[x, y].Image = Image.FromFile("X.PNG");
                    }
                    else if (board[x, y] == -1)
                    {
                        pictureBoxSlot[x, y].Image = Image.FromFile("O.PNG");
                    }
                    else
                    {
                        pictureBoxSlot[x, y].Image = null;
                    }
                }
            }

            //Set player turn label
            if (simming == 0)
            {
                if (playerTurn == 1)
                {
                    labelTurnWho.Text = "Player";
                    labelInfo.Text = "The Player is up! Place an X on the game board. If you get 3 in a row, you win!";
                }
                else
                {
                    labelTurnWho.Text = "AI";
                    labelInfo.Text = "The AI is up! It is currently debating how to crush your hopes and dreams.";
                }
            }
            //Set Simming turn label
            if (simming == 1)
            {
                if (playerTurn == 1)
                {
                    labelTurnWho.Text = "AI (X)";
                    labelInfo.Text = "AI (X) is up.";
                }
                else
                {
                    labelTurnWho.Text = "AI (O)";
                    labelInfo.Text = "AI (O) is up.";
                }
            }
        }

        private void CheckScore()
        {
            //This method checks all possible combinations to win
            //Check slot 0, 1, 2
            if ((board[0, 0] + board[0, 1] + board[0, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[0, 0] + board[0, 1] + board[0, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 3, 4, 5
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == 3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "You have won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (X) has won the game.";
                }
            }
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == -3)
            {
                turnsRemaining = 0;
                if (simming == 0)
                {
                    labelInfo.Text = "The AI has won the game!";
                }
                else
                {
                    labelInfo.Text = "AI (O) has won the game.";
                }
            }

            //If no winner, check if turns remaining and declare a draw if noone won.
            else if (turnsRemaining == 0)
            {
                labelInfo.Text = "This game has ended in a draw!";
            }
            else if (playerTurn == 2 && simming != 1)
            {
                //AI Turn
                AiTurn();
            }
        }

        //PlayerClicked Method uses 2 arguments to determine what location on the board/in the array was clicked
        private void PlayerClicked(int x, int y)
        {
            if (playerTurn == 1 && simming == 0)
            {
                //If turns remain, determine what player clicked and set the value for the board array
                if (turnsRemaining > 0)
                {
                    if (board[x, y] == 0)
                    {
                        board[x, y] = 1;
                        playerTurn = 2;
                        turnsRemaining--;
                        labelInfo.Text = "The AI is up! It is currently debating how to crush your hopes and dreams.";
                    }
                    else
                    {
                        labelInfo.Text = "That slot is taken, choose a different 1!";
                    }
                }

                //Update the boards graphics
                UpdateGraphics();

                //Check if anyone has won
                CheckScore();
            }
        }
        private void pictureBoxSlot0_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(0, 0);
        }

        private void pictureBoxSlot1_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(0, 1);
        }

        private void pictureBoxSlot2_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(0, 2);
        }

        private void pictureBoxSlot3_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(1, 0);
        }

        private void pictureBoxSlot4_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(1, 1);
        }

        private void pictureBoxSlot5_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(1, 2);
        }

        private void pictureBoxSlot6_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(2, 0);
        }

        private void pictureBoxSlot7_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(2, 1);
        }

        private void pictureBoxSlot8_Click(object sender, EventArgs e)
        {
            //Using the PlayerClicked Method, pass what slot was clicked to the method.
            PlayerClicked(2, 2);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the board
            ResetGame();
        }

        private void viewQuickGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show the Quick Guide
            MessageBox.Show("How to play:" + Environment.NewLine + Environment.NewLine + "The objective of 'Tic Tac Toe' is to get 3 in a row. The player will take" +
                " turns placing X's and O's on the game board Vs the AI. If no one is able to get 3 X's or 3 O's in a row the game will end in a draw."
                + Environment.NewLine + Environment.NewLine + "Have Fun!");
        }

        private void aboutTicTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show the about info
            MessageBox.Show("Tic Tac Toe" + Environment.NewLine + Environment.NewLine + "Programmed by Andrew Bach" + Environment.NewLine + Environment.NewLine + "November 11, 2020");
        }

        private void simulateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declare random variable
            Random num = new Random();
            int number = num.Next(2);

            //Start from scratch
            ResetGame();

            //Start the game simulation
            simming = 1;
            simulateTurnX();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Close the program
            this.Close();
        }


        /*-------------------------------------------------AI METHODS START------------------------------------------------------*/
        //Simulation AI Method for X.
        private void simulateTurnX()
        {
            wait(750);
            //Check for wins
            //Check slot 0, 1, 2
            if ((board[0, 0] + board[0, 1] + board[0, 2]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[0, 1] == 0)
                {
                    board[0, 1] = 1;
                }
                else
                {
                    board[0, 2] = 1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == 2)
            {
                if (board[1, 0] == 0)
                {
                    board[1, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[1, 2] = 1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == 2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = 1;
                }
                else if (board[2, 1] == 0)
                {
                    board[2, 1] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[1, 0] == 0)
                {
                    board[1, 0] = 1;
                }
                else
                {
                    board[2, 0] = 1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == 2)
            {
                if (board[0, 1] == 0)
                {
                    board[0, 1] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[2, 1] = 1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == 2)
            {
                if (board[0, 2] == 0)
                {
                    board[0, 2] = 1;
                }
                else if (board[1, 2] == 0)
                {
                    board[1, 2] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == 2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[0, 2] = 1;
                }
            }

            //Check for Blocks
            //Check slot 0, 1, 2
            else if ((board[0, 0] + board[0, 1] + board[0, 2]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[0, 1] == 0)
                {
                    board[0, 1] = 1;
                }
                else
                {
                    board[0, 2] = 1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == -2)
            {
                if (board[1, 0] == 0)
                {
                    board[1, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[1, 2] = 1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == -2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = 1;
                }
                else if (board[2, 1] == 0)
                {
                    board[2, 1] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[1, 0] == 0)
                {
                    board[1, 0] = 1;
                }
                else
                {
                    board[2, 0] = 1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == -2)
            {
                if (board[0, 1] == 0)
                {
                    board[0, 1] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[2, 1] = 1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == -2)
            {
                if (board[0, 2] == 0)
                {
                    board[0, 2] = 1;
                }
                else if (board[1, 2] == 0)
                {
                    board[1, 2] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[2, 2] = 1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == -2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = 1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = 1;
                }
                else
                {
                    board[0, 2] = 1;
                }
            }

            //If no Blocks or Wins available, Place randomly
            else
            {
                while (playerTurn == 1)
                {
                    Random num = new Random();
                    int number = num.Next(3);
                    int number2 = num.Next(3);

                    if (board[number, number2] == 0 && playerTurn == 1)
                    {
                        board[number, number2] = 1;
                        playerTurn = 2;
                    }
                }
            }

            playerTurn = 2;
            turnsRemaining--;
            UpdateGraphics();
            CheckScore();

            if (turnsRemaining > 0)
            {
                simulateTurnO();
            }
        }

        //Simulation AI Method for O
        private void simulateTurnO()
        {
            wait(750);
            //Check for wins
            //Check slot 0, 1, 2
            if ((board[0, 0] + board[0, 1] + board[0, 2]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[0, 1] == 0)
                {
                    board[0, 1] = -1;
                }
                else
                {
                    board[0, 2] = -1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == -2)
            {
                if (board[1, 0] == 0)
                {
                    board[1, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[1, 2] = -1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == -2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = -1;
                }
                else if (board[2, 1] == 0)
                {
                    board[2, 1] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[1, 0] == 0)
                {
                    board[1, 0] = -1;
                }
                else
                {
                    board[2, 0] = -1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == -2)
            {
                if (board[0, 1] == 0)
                {
                    board[0, 1] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[2, 1] = -1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == -2)
            {
                if (board[0, 2] == 0)
                {
                    board[0, 2] = -1;
                }
                else if (board[1, 2] == 0)
                {
                    board[1, 2] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == -2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == -2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[0, 2] = -1;
                }
            }

            //Check for Blocks
            //Check slot 0, 1, 2
            else if ((board[0, 0] + board[0, 1] + board[0, 2]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[0, 1] == 0)
                {
                    board[0, 1] = -1;
                }
                else
                {
                    board[0, 2] = -1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1, 0] + board[1, 1] + board[1, 2]) == 2)
            {
                if (board[1, 0] == 0)
                {
                    board[1, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[1, 2] = -1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2, 0] + board[2, 1] + board[2, 2]) == 2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = -1;
                }
                else if (board[2, 1] == 0)
                {
                    board[2, 1] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0, 0] + board[1, 0] + board[2, 0]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[1, 0] == 0)
                {
                    board[1, 0] = -1;
                }
                else
                {
                    board[2, 0] = -1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0, 1] + board[1, 1] + board[2, 1]) == 2)
            {
                if (board[0, 1] == 0)
                {
                    board[0, 1] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[2, 1] = -1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0, 2] + board[1, 2] + board[2, 2]) == 2)
            {
                if (board[0, 2] == 0)
                {
                    board[0, 2] = -1;
                }
                else if (board[1, 2] == 0)
                {
                    board[1, 2] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0, 0] + board[1, 1] + board[2, 2]) == 2)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[2, 2] = -1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2, 0] + board[1, 1] + board[0, 2]) == 2)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = -1;
                }
                else if (board[1, 1] == 0)
                {
                    board[1, 1] = -1;
                }
                else
                {
                    board[0, 2] = -1;
                }
            }

            //If no Blocks or Wins available, Place randomly
            else
            {
                while (playerTurn == 2)
                {
                    Random num = new Random();
                    int number = num.Next(3);
                    int number2 = num.Next(3);

                    if (board[number, number2] == 0 && playerTurn == 2)
                    {
                        board[number, number2] = -1;
                        playerTurn = 1;
                    }
                }
            }

            playerTurn = 1;
            turnsRemaining--;
            UpdateGraphics();
            CheckScore();

            if (turnsRemaining > 0)
            {
                simulateTurnX();
            }
        }

        //AI Method for vs player
        private void AiTurn()
        {
            wait(2000);
            //Check for wins
            //Check slot 0, 1, 2
            if ((board[0 , 0] + board[0 , 1] + board[0 , 2]) == -2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[0 , 1] == 0)
                {
                    board[0 , 1] = -1;
                }
                else
                {
                    board[0 , 2] = -1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1 , 0] + board[1 , 1] + board[1 , 2]) == -2)
            {
                if (board[1 , 0] == 0)
                {
                    board[1 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[1 , 2] = -1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2 , 0] + board[2 , 1] + board[2 , 2]) == -2)
            {
                if (board[2 , 0] == 0)
                {
                    board[2 , 0] = -1;
                }
                else if (board[2 , 1] == 0)
                {
                    board[2 , 1] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0 , 0] + board[1 , 0] + board[2 , 0]) == -2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[1 , 0] == 0)
                {
                    board[1 , 0] = -1;
                }
                else
                {
                    board[2 , 0] = -1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0 , 1] + board[1 , 1] + board[2 , 1]) == -2)
            {
                if (board[0 , 1] == 0)
                {
                    board[0 , 1] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[2 , 1] = -1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0 , 2] + board[1 , 2] + board[2 , 2]) == -2)
            {
                if (board[0 , 2] == 0)
                {
                    board[0 , 2] = -1;
                }
                else if (board[1 , 2] == 0)
                {
                    board[1 , 2] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0 , 0] + board[1 , 1] + board[2 , 2]) == -2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2 , 0] + board[1 , 1] + board[0 , 2]) == -2)
            {
                if (board[2 , 0] == 0)
                {
                    board[2 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[0 , 2] = -1;
                }
            }

            //Check for Blocks
            //Check slot 0, 1, 2
            else if ((board[0 , 0] + board[0 , 1] + board[0 , 2]) == 2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[0 , 1] == 0)
                {
                    board[0 , 1] = -1;
                }
                else
                {
                    board[0 , 2] = -1;
                }

            }

            //Check slot 3, 4, 5
            else if ((board[1 , 0] + board[1 , 1] + board[1 , 2]) == 2)
            {
                if (board[1 , 0] == 0)
                {
                    board[1 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[1 , 2] = -1;
                }
            }

            //Check slot 6, 7, 8
            else if ((board[2 , 0] + board[2 , 1] + board[2 , 2]) == 2)
            {
                if (board[2 , 0] == 0)
                {
                    board[2 , 0] = -1;
                }
                else if (board[2 , 1] == 0)
                {
                    board[2 , 1] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 0. 3. 6
            else if ((board[0 , 0] + board[1 , 0] + board[2 , 0]) == 2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[1 , 0] == 0)
                {
                    board[1 , 0] = -1;
                }
                else
                {
                    board[2 , 0] = -1;
                }
            }

            //Check slot 1, 4, 7
            else if ((board[0 , 1] + board[1 , 1] + board[2 , 1]) == 2)
            {
                if (board[0 , 1] == 0)
                {
                    board[0 , 1] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[2 , 1] = -1;
                }
            }

            //Check slot 2, 5, 8
            else if ((board[0 , 2] + board[1 , 2] + board[2 , 2]) == 2)
            {
                if (board[0 , 2] == 0)
                {
                    board[0 , 2] = -1;
                }
                else if (board[1 , 2] == 0)
                {
                    board[1 , 2] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 0, 4, 8
            else if ((board[0 , 0] + board[1 , 1] + board[2 , 2]) == 2)
            {
                if (board[0 , 0] == 0)
                {
                    board[0 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[2 , 2] = -1;
                }
            }

            //Check slot 6, 4, 2
            else if ((board[2 , 0] + board[1 , 1] + board[0 , 2]) == 2)
            {
                if (board[2 , 0] == 0)
                {
                    board[2 , 0] = -1;
                }
                else if (board[1 , 1] == 0)
                {
                    board[1 , 1] = -1;
                }
                else
                {
                    board[0 , 2] = -1;
                }
            }

            //If no Blocks or Wins available, Place randomly
            else
            {
                while (playerTurn == 2)
                {
                    Random num = new Random();
                    int number = num.Next(3);
                    int number2 = num.Next(3);

                    if (board[number , number2] == 0 && playerTurn == 2)
                    {
                        board[number , number2] = -1;
                        playerTurn = 1;
                    }
                }
            }

            playerTurn = 1;
            turnsRemaining--;
            UpdateGraphics();
            CheckScore();
        }
        /*-------------------------------------------------AI METHODS END------------------------------------------------------*/       
    }
}
