using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetromino
    {
        List<List<Point>> Rotations;
        Color pieceColor;

        public Tetromino(int i)
        {
            Rotations = new List<List<Point>>();
            switch (i)
            {
                // I-piece
                case 0:
                    pieceColor = Color.Blue;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    Rotations[0].Add(new Point(2, 0));
                    Rotations[0].Add(new Point(3, 0));

                    Rotations.Add(new List<Point>()); 
                    Rotations[1].Add(new Point(1, 3));
                    Rotations[1].Add(new Point(1, 2));
                    Rotations[1].Add(new Point(1, 1));
                    Rotations[1].Add(new Point(1, 0));
                    break;

                // Square piece
                case 1:
                    pieceColor = Color.Cyan;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(0, 1));
                    Rotations[0].Add(new Point(1, 1));
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    break;

                // L-piece
                case 2:
                    pieceColor = Color.Orange;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(0, 1));
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    Rotations[0].Add(new Point(2, 0));

                    Rotations.Add(new List<Point>()); 
                    Rotations[1].Add(new Point(1, 2));
                    Rotations[1].Add(new Point(0, 2));
                    Rotations[1].Add(new Point(0, 1));
                    Rotations[1].Add(new Point(0, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[2].Add(new Point(0, 1));
                    Rotations[2].Add(new Point(1, 1));
                    Rotations[2].Add(new Point(2, 1));
                    Rotations[2].Add(new Point(2, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[3].Add(new Point(1, 2));
                    Rotations[3].Add(new Point(1, 1));
                    Rotations[3].Add(new Point(0, 0));
                    Rotations[3].Add(new Point(1, 0));
                    
                    
                    break;

                // J-piece
                case 3:
                    pieceColor = Color.Yellow;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(2, 1));
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    Rotations[0].Add(new Point(2, 0));
                    

                    Rotations.Add(new List<Point>());
                    Rotations[1].Add(new Point(0, 2));
                    Rotations[1].Add(new Point(0, 1));
                    Rotations[1].Add(new Point(0, 0));
                    Rotations[1].Add(new Point(1, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[2].Add(new Point(2, 1));
                    Rotations[2].Add(new Point(1, 1));
                    Rotations[2].Add(new Point(0, 1));
                    Rotations[2].Add(new Point(0, 0));
                    

                    Rotations.Add(new List<Point>());
                    Rotations[3].Add(new Point(0, 2));
                    Rotations[3].Add(new Point(1, 2));
                    Rotations[3].Add(new Point(1, 1));
                    Rotations[3].Add(new Point(1, 0));
                    break;

                // T-piece
                case 4:
                    pieceColor = Color.Green;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(1, 1));
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    Rotations[0].Add(new Point(2, 0));
                    

                    Rotations.Add(new List<Point>());
                    Rotations[1].Add(new Point(0, 2));
                    Rotations[1].Add(new Point(0, 1));
                    Rotations[1].Add(new Point(1, 1));
                    Rotations[1].Add(new Point(0, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[2].Add(new Point(0, 1));
                    Rotations[2].Add(new Point(1, 1));
                    Rotations[2].Add(new Point(2, 1));
                    Rotations[2].Add(new Point(1, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[3].Add(new Point(1, 2));
                    Rotations[3].Add(new Point(1, 1));
                    Rotations[3].Add(new Point(0, 1));
                    Rotations[3].Add(new Point(1, 0));
                    
                    break;

                // S-piece
                case 5:
                    pieceColor = Color.Pink;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(0, 1));
                    Rotations[0].Add(new Point(1, 1));
                    Rotations[0].Add(new Point(1, 0));
                    Rotations[0].Add(new Point(2, 0));

                    Rotations.Add(new List<Point>());
                    Rotations[1].Add(new Point(1, 2));
                    Rotations[1].Add(new Point(0, 1));
                    Rotations[1].Add(new Point(1, 1));
                    Rotations[1].Add(new Point(0, 0));
                    break;

                // Z-piece
                case 6:
                    pieceColor = Color.Red;

                    Rotations.Add(new List<Point>());
                    Rotations[0].Add(new Point(1, 1));
                    Rotations[0].Add(new Point(2, 1));
                    Rotations[0].Add(new Point(0, 0));
                    Rotations[0].Add(new Point(1, 0));
                    

                    Rotations.Add(new List<Point>());
                    Rotations[1].Add(new Point(0, 2));
                    Rotations[1].Add(new Point(1, 1));
                    Rotations[1].Add(new Point(0, 1));
                    Rotations[1].Add(new Point(1, 0));
                   
                    
                    break;
            }
        }


    public List<List<Point>> getRotations()
        {
            return this.Rotations;
        }

    public Color getColor()
        {
            return pieceColor;
        }

    


    }
}
