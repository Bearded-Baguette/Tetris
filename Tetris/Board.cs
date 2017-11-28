using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class Board
    {
        Panel[,] boardArray = new Panel[12, 23];
        Panel[,] demoBoard = new Panel[4, 4];
        int panelWH = 20;

        // Creates the actual game board
        public void createBoard(Form TetrisForm)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int k = 0; k < 23; k++)
                {
                    Point pntCurrent = new Point(20 + (i * panelWH), 20 + (k * panelWH));
                    createBlock(TetrisForm, i, k, pntCurrent);
                }
            }
        }

        // Creates the border for the game
        public void setBorder()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int k = 0; k < 23; k++)
                {
                    if(i == 0 || i == 11 || k == 22 || k == 0)
                    {
                        boardArray[i, k].BackColor = Color.Black;
                        fixedBlocks.Add(boardArray[i,k]); 
                    }
                }
            }
            placePieceInList();
        }

        // Create each panel of the gameboard
        private void createBlock(Form TetrisForm, int i, int k, Point pntCurrent)
        {
            boardArray[i, k] = new Panel();
            boardArray[i, k].Enabled = true;
            boardArray[i, k].Visible = true;
            boardArray[i, k].BackColor = Color.Gray;
            boardArray[i, k].Size = new System.Drawing.Size(panelWH, panelWH);
            boardArray[i, k].Location = pntCurrent;
            boardArray[i, k].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            TetrisForm.Controls.Add(boardArray[i, k]);
        }

        // Specifies the location of a panel in the demo board
        public void createNextPieceBoard(Form TetrisForm)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Point newPoint = new Point(354 + (i * panelWH), 42 + (k * panelWH));
                    createDemoBlock(TetrisForm, i, k, newPoint);
                }
            }
        }

        // Creates a panel for the demo board
        private void createDemoBlock(Form TetrisForm, int i, int k, Point pntCurrent)
        {
            demoBoard[i, k] = new Panel();
            demoBoard[i, k].Enabled = true;
            demoBoard[i, k].Visible = true;
            demoBoard[i, k].BackColor = Color.White;
            demoBoard[i, k].Size = new System.Drawing.Size(panelWH, panelWH);
            demoBoard[i, k].Location = pntCurrent;
            TetrisForm.Controls.Add(demoBoard[i, k]);
        }

        private Point activePoint;
        private int currentPiece;
        private int rotation;
        private int totalScore = 0;
        Tetromino tetromino;
        List<List<Point>> rotations;
        List<Panel> fixedBlocks = new List<Panel>();
        Color pieceColor;
        private Queue<int> nextPieces = new Queue<int>();

        // places random pieces in the list of next pieces to be dropped
        public void placePieceInList()
        {
            Random rnd = new Random();
            activePoint = new Point(5, 1);
            rotation = 0;
            if (nextPieces.Count == 0 || nextPieces.Count == 1)
            {
                for (int i = 0; i <= 5; i++)
                {
                    nextPieces.Enqueue(rnd.Next(1, 100) % 7);
                }
            }
            currentPiece = nextPieces.Dequeue();
        }

        // creates the next piece on the board
        public void createInitialPiece()
        {
            tetromino = new Tetromino(currentPiece);
            rotations = tetromino.getRotations();
            pieceColor = tetromino.getColor();
            foreach(Point p in rotations[rotation])
            {
                boardArray[p.X + activePoint.X, p.Y + activePoint.Y].BackColor = pieceColor;
                if (fixedBlocks.Contains(boardArray[p.X + activePoint.X, p.Y + activePoint.Y]))
                {
                    gameOver();
                }
            }
            showNextPiece();      
        }

        // Creates the next piece in the demoBoard
        public void showNextPiece()
        {
            Tetromino nextTetromino = new Tetromino(nextPieces.Peek());
            List<List<Point>> nextRotations = nextTetromino.getRotations();
            Color nextPieceColor = nextTetromino.getColor();
            for(int i=0; i<4;i++)
            {
                for(int j=0; j<4; j++)
                {
                    demoBoard[j, i].BackColor = Color.White;
                    demoBoard[j, i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //demoBoard[j, i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    
                }
            }
            foreach(Point p in nextRotations[0])
            {
                demoBoard[p.X, p.Y].BackColor = nextPieceColor;
                demoBoard[p.X, p.Y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }

        }

        // Returns true if there is a collision between the current piece and a fixed piece
        public Boolean collisionAt(int x, int y, int rotation)
        {
            foreach(Point p in rotations[rotation])
            {
                if((p.Y + y) > 21)
                {
                    return true;
                }
                if(fixedBlocks.Contains(boardArray[p.X + x, p.Y + y]))
                {
                    return true;
                }
            }
            return false;
        }

        // Moves the active piece left or right by one space
        public void moveLeftOrRight(int i)
        {
            if (!collisionAt(activePoint.X + i, activePoint.Y, rotation))
            {
                activePoint.X += i;
                if(i == 1)
                    repaintRightMovement(activePoint.X, activePoint.Y, rotation);
                if (i == -1)
                    repaintLeftMovement(activePoint.X, activePoint.Y, rotation);

            }

        }

        // Moves the active piece down one space
        public void dropDownOneSpace()
        {
            if (!collisionAt(activePoint.X, activePoint.Y + 1, rotation))
            {
                activePoint.Y += 1;
                repaintDroppingPiece(activePoint.X, activePoint.Y, rotation);
            }
            else
            {
                fixPiece();
            } 
        }

        // Rotates the active piece
        public void rotatePiece()
        {
            int nextRotation = rotation + 1;
            if (nextRotation == rotations.Count)
            {
                nextRotation = 0;
            }
            if (!collisionAt(activePoint.X, activePoint.Y, nextRotation))
            {
                foreach (Point p in rotations[rotation])
                {
                    boardArray[p.X + activePoint.X, p.Y + activePoint.Y].BackColor = Color.Gray;
                }
                rotation += 1;
                if (rotation == rotations.Count)
                {
                    rotation = 0;
                }
                foreach (Point p in rotations[rotation])
                {
                    boardArray[p.X + activePoint.X, p.Y + activePoint.Y].BackColor = pieceColor;
                }
            }
        }

        // Adds the active piece to a fixedPiece list
        public void fixPiece()
        {
            foreach (Point p in rotations[rotation])
            {
                fixedBlocks.Add(boardArray[activePoint.X + p.X, activePoint.Y + p.Y]);
            }
            clearRows();
            placePieceInList();
            createInitialPiece();
        }

        // Checks for a full row
        public void clearRows()
        {
            bool fullRow;
            int numberOfDeletedRows = 0;
            for(int i = 21; i>0;i--)
            {
                fullRow = true;
                for(int j = 1; j<11; j++)
                {
                    if(boardArray[j,i].BackColor == Color.Gray)
                    {
                        fullRow = false;
                        break;
                    }
                }
                if(fullRow)
                {
                    deleteRow(i);
                    i += 1;
                    numberOfDeletedRows++;
                }
            }
            if (numberOfDeletedRows == 1)
                totalScore += 100;
            else if (numberOfDeletedRows == 2)
                totalScore += 300;
            else if (numberOfDeletedRows == 3)
                totalScore += 600;
            else if (numberOfDeletedRows == 4)
                totalScore += 1000;
        }


        // Deletes a full row
        public void deleteRow(int row)
        {
            int numberOfDeletedRow = 0;
            for(int i = row-1; i > 0; i--)
            {
                for (int j = 1; j < 11; j++)
                {
                    boardArray[j, (i + 1)].BackColor = boardArray[j, i].BackColor;

                    if (boardArray[j, (i + 1)].BackColor == Color.Gray)
                    {
                        fixedBlocks.Remove(boardArray[j, i+1]);
                    }   
                }
            }
        }

        // Redraws a piece moving down
        public void repaintDroppingPiece(int x, int y, int rotation)
        {
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + x, p.Y + (y-1)].BackColor = Color.Gray;
            }
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + x, p.Y + y].BackColor = pieceColor;
            }
        }

        // Redraws a piece moving to the left
        public void repaintLeftMovement(int x, int y, int rotation)
        {
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + (x + 1), p.Y + y].BackColor = Color.Gray;
            }
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + x, p.Y + y].BackColor = pieceColor;
            }
        }

        // Redraws a piece moving to the right
        public void repaintRightMovement(int x, int y, int rotation)
        {
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + (x - 1), p.Y + y].BackColor = Color.Gray;
            }
            foreach (Point p in rotations[rotation])
            {
                boardArray[p.X + x, p.Y + y].BackColor = pieceColor;
            }
        }

        // Game over. Is true if a fixed piece is in the top row.
        public bool gameOver()
        {
                for (int i = 1; i < 11; i++)
                {
                    if (fixedBlocks.Contains(boardArray[i, 1]))
                    {
                        return true;
                    }
                }
            return false;
        }

        public string getTotalScore()
        {
            return totalScore.ToString();
        }
    }
}
