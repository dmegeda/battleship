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
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Dir { get; set; }

        public List<Cell> CellsList;

        public int DeckCount => CellsList.Count;

        //public int DeckCount { get { return deckCount; } }
        public Ship(int x, int y, ShipTypes shipType, Directions direction)
        {
            Dir = direction;
            CellsList = new List<Cell>();
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
                        CellsList.Add(new Cell(x, y - i));
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new Cell(x + i, y));
                    }
                    break;
                case Directions.DOWN:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new Cell(x, y + i));
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i < deckCount; i++)
                    {
                        CellsList.Add(new Cell(x - i, y));
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
