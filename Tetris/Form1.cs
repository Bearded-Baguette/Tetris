using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisForm : Form
    {
        public TetrisForm()
        {
            InitializeComponent();
        }

        Board b = new Board();

        private void Form1_Load(object sender, EventArgs e)
        {
            exitButton.Visible = false;
            gameOverLabel.Visible = false;

            b.createBoard(this);
            b.createNextPieceBoard(this);
            b.setBorder();
            b.createInitialPiece();

            Thread t = new Thread(ticker);
            t.Start();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                b.moveLeftOrRight(-1);
            }
            if(keyData == Keys.Right)
            {
                b.moveLeftOrRight(1);
            }
            if(keyData == Keys.Down)
            {
                b.dropDownOneSpace();
            }
            if(keyData == Keys.Up)
            {
                b.rotatePiece();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void setScoreLabel()
        {
            scoreLabel.Invoke((MethodInvoker)(() => scoreLabel.Text = b.getTotalScore()));
        }

        void setGameOverScreen()
        {
            gameOverLabel.Invoke((MethodInvoker)(() => gameOverLabel.Visible = true));
            exitButton.Invoke((MethodInvoker)(() => exitButton.Visible = true));
        }

        void ticker()
        {
            while (!b.gameOver())
            {
                setScoreLabel();
                Thread.Sleep(1000);
                b.dropDownOneSpace();
            }
            setGameOverScreen();
                
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
