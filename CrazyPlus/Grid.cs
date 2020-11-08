using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus
{
    public class Grid
    {
        public int Score { get; set; }
        public Tile[,] Tiles { get; set; }
        public bool Calculating { get; set; }

        Audio Add;
        Audio UpDown;
        Audio LeftRight;

        public bool MovingLeft, MovingRight, MovingTop, MovingDown;
        public Grid(Engine engine) 
        { 
            Tiles = new Tile[4, 4];
            RandomTiles();
            Calculating = false;
            Score = 0;
            MovingLeft = false;
            MovingRight = true;
            MovingTop = false;
            MovingDown = false;
            Add = new Audio(engine.Ressources("add_1.wav"));
            Add.SetVolume(0);
            UpDown = new Audio(engine.Ressources("click_1.wav"));
            LeftRight = new Audio(engine.Ressources("click_2.wav"));

        }

        private static Random random = new Random();

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
                            Add.Play(false);
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
                        UpDown.Play(false);
                    }
                }
            }

            if (Tiles[0, 0] == null)
            {
                Tile tile = new Tile(0, 0);
                Tiles[0, 0] = tile;
            }

        }

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
                            Add.Play(false);
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
                        UpDown.Play(false);
                    }
                }
            }

            if (Tiles[0, 3] == null)
            {
                Tile tile = new Tile(0, 300);
                Tiles[0, 3] = tile;
            }
        }

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
                            Add.Play(false);
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
                        LeftRight.Play(false);
                    }
                }
            }

            if (Tiles[3, 0] == null)
            {
                Tile tile = new Tile(300, 0);
                Tiles[3, 0] = tile;
            }
        }

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
                            Add.Play(false);
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
                        LeftRight.Play(false);
                    }
                }
            }

            if (Tiles[0, 0] == null)
            {
                Tile tile = new Tile(0, 0);
                Tiles[0, 0] = tile;
            }
        }

        public bool GameOn()
        {
            if (!MovingDown && !MovingLeft && !MovingRight && !MovingTop)
                return false;

            return true;
        }

    }
}
