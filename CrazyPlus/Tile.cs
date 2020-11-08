using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus
{
    public class Tile
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get { return 100; } }
        public int Height { get { return 100; } }
        public int Value { get; set; }

        public Tile(int x, int y)
        {
            PosX = x;
            PosY = y;
            Value = 2;
        }

        public void DoubleIt() { Value *= 2; }
    }
}
