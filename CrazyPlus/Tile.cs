using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus
{
    /// <summary>
    /// Grid Tile, to store every tile data
    /// </summary>
    public class Tile
    {
        //Position
        public int PosX { get; set; }
        public int PosY { get; set; }
        //Size
        public int Width { get { return 100; } }
        public int Height { get { return 100; } }
        //Value
        public int Value { get; set; }

        public Tile(int x, int y)
        {
            PosX = x;
            PosY = y;
            Value = 2;
        }

        /// <summary>
        /// Double the current value
        /// </summary>
        public void DoubleIt() { Value *= 2; }
    }
}
