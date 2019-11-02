using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum ShipTypes
    {
        BOAT,
        DESTROYER,
        CRUISER,
        BATTLESHIP
    }
    public class Ship
    {
        //private int deckCount;
        private int x;
        private int y;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private Directions dir;
        public Directions Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public List<ShipCells> CellsList;

        public int DeckCount
        {
            get
            {
                return CellsList.Count;
            }
        }

        //public int DeckCount { get { return deckCount; } }
        public Ship(int x, int y, ShipTypes shipType, Directions direction)
        {
            CellsList = new List<ShipCells>();
            int deckCount = 0;
            switch (shipType)
            {
                case ShipTypes.BOAT:
                    deckCount = 1;
                    break;
                case ShipTypes.DESTROYER:
                    deckCount = 2;
                    break;
                case ShipTypes.CRUISER:
                    deckCount = 3;
                    break;
                case ShipTypes.BATTLESHIP:
                    deckCount = 4;
                    break;
            }
            switch (direction)
            {
                case Directions.UP:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new ShipCells(x, y - i));
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new ShipCells(x + i, y));
                    }
                    break;
                case Directions.DOWN:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new ShipCells(x, y + i));
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new ShipCells(x - i, y));
                    }
                    break;
            }
        }
        public bool IsDestroyed()
        {
            int count = 0;
            for(int i = 0; i < CellsList.Count; i++)
            {
                if (CellsList[i].IsDestroyed) count++;
            }
            return count == CellsList.Count;
        }
    }
}
