using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus
{
    /// <summary>
    /// Game grid where the player move the tiles
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Grid score
        /// </summary>
        public int Score { get; set; }
        public Tile[,] Tiles { get; set; }
        public bool Calculating { get; set; }


        /// <summary>
        /// To check if there is more movement in every side
        /// </summary>
        public bool MovingLeft, MovingRight, MovingTop, MovingDown;

        /// <summary>
        /// Init the Grid
        /// </summary>
        /// <param name="engine"></param>
        public Grid() 
        { 
            Tiles = new Tile[4, 4];
            RandomTiles();
            Calculating = false;
            Score = 0;
            MovingLeft = false;
            MovingRight = true;
            MovingTop = false;
            MovingDown = false;

        }

        private static Random random = new Random();

        /// <summary>
        /// Generate 4 Random tiles on the start of the game
        /// </summary>
        private void RandomTiles()
        {
            for (int i = 0; i < 4; i++)
            {
                int x = random.Next(0, 3);
                int y = random.Next(0, 3);
                Tile tile = new Tile(x * 100, y * 100);
                Tiles[x, y] = tile;
            }
        }

        /// <summary>
        /// Move the Grid Tiles to down
        /// </summary>
        public void MoveDown()
        {
            MovingDown = false;
            for (int x = 0;x < 4; x++)
            {
                for(int y = 2; y >= 0; y--)
                {
                    if (Tiles[x, y + 1] != null && Tiles[x, y] != null) {
                        if (Tiles[x, y + 1].Value == Tiles[x, y].Value)
                        {
                            Tiles[x, y] = null;
                            MovingDown = true;
                            Tiles[x, y + 1].DoubleIt();
                        }
                    }
                    else if(Tiles[x, y + 1] == null && Tiles[x, y] != null)
                    {
                        Tile tile = new Tile(x * 100, (y + 1) * 100);
                        Tiles[x, y + 1] = tile;
                        Tiles[x, y + 1].Value = Tiles[x, y].Value;
                        Score += Tiles[x, y].Value;
                        MovingDown = true;
                        Tiles[x, y] = null;
                    }
                }
            }

            if (Tiles[0, 0] == null)
            {
                Tile tile = new Tile(0, 0);
                Tiles[0, 0] = tile;
            }

        }

        /// <summary>
        /// Move the Grid Tiles to up
        /// </summary>
        public void MoveUp()
        {
            MovingTop = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 1; y < 4; y++)
                {
                    if (Tiles[x, y - 1] != null && Tiles[x, y] != null)
                    {
                        if (Tiles[x, y - 1].Value == Tiles[x, y].Value)
                        {
                            Tiles[x, y] = null;
                            MovingTop = true;
                            Tiles[x, y - 1].DoubleIt();
                        }
                    }
                    else if (Tiles[x, y - 1] == null && Tiles[x, y] != null)
                    {
                        Tile tile = new Tile(x * 100, (y - 1) * 100);
                        Tiles[x, y - 1] = tile;
                        Tiles[x, y - 1].Value = Tiles[x, y].Value;
                        Score += Tiles[x, y].Value;
                        MovingTop = true;
                        Tiles[x, y] = null;
                    }
                }
            }

            if (Tiles[0, 3] == null)
            {
                Tile tile = new Tile(0, 300);
                Tiles[0, 3] = tile;
            }
        }

        /// <summary>
        /// Move the Grid tiles to left
        /// </summary>
        public void MoveLeft()
        {
            MovingLeft = false;
            for (int x = 1; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (Tiles[x - 1, y] != null && Tiles[x, y] != null)
                    {
                        if (Tiles[x - 1, y].Value == Tiles[x, y].Value)
                        {
                            Tiles[x, y] = null;
                            MovingLeft = true;
                            Tiles[x - 1, y].DoubleIt();
                        }
                    }
                    else if (Tiles[x - 1, y] == null && Tiles[x, y] != null)
                    {
                        Tile tile = new Tile((x - 1) * 100, y * 100);
                        Tiles[x - 1, y] = tile;
                        Tiles[x - 1, y].Value = Tiles[x, y].Value;
                        Score += Tiles[x, y].Value;
                        MovingLeft = true;
                        Tiles[x, y] = null;
                    }
                }
            }

            if (Tiles[3, 0] == null)
            {
                Tile tile = new Tile(300, 0);
                Tiles[3, 0] = tile;
            }
        }

        /// <summary>
        /// Move the Grid tiles to Right
        /// </summary>
        public void MoveRight()
        {
            MovingRight = false;
            for (int x = 2; x >= 0; x--)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (Tiles[x + 1, y] != null && Tiles[x, y] != null)
                    {
                        if (Tiles[x + 1, y].Value == Tiles[x, y].Value)
                        {
                            Tiles[x, y] = null;
                            Tiles[x + 1, y].DoubleIt();
                            MovingRight = true;
                        }
                    }
                    else if (Tiles[x + 1, y] == null && Tiles[x, y] != null)
                    {
                        Tile tile = new Tile((x + 1) * 100, y * 100);
                        Tiles[x + 1, y] = tile;
                        Tiles[x + 1, y].Value = Tiles[x, y].Value;
                        Score += Tiles[x, y].Value;
                        MovingRight = true;
                        Tiles[x, y] = null;
                    }
                }
            }

            if (Tiles[0, 0] == null)
            {
                Tile tile = new Tile(0, 0);
                Tiles[0, 0] = tile;
            }
        }

        /// <summary>
        /// Check if the Grid has more moves
        /// </summary>
        /// <returns></returns>
        public bool GameOn()
        {
            if (!MovingDown && !MovingLeft && !MovingRight && !MovingTop)
                return false;

            return true;
        }

    }
}
