using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Cell
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool IsDestroyed { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsDestroyed = false;
        }

        public void Destroy()
        {
            IsDestroyed = true;
        }
    }
}
