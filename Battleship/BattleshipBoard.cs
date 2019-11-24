using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class BattleshipBoard
    {
        public int BoardSize { get; set; }
        //---
        public int[,] MainBoard { get; set; }
        public int[,] HitsBoard { get; set; }
        //---
        public List<Ship> ShipsList { get; set; }
        private List<Cell> allPossibleCells = new List<Cell>();
        private List<Cell> cellsForCheck = new List<Cell>();
        public BattleshipBoard(int boardsize)
        {
            ShipsList = new List<Ship>();
            BoardSize = boardsize;
            Draw.DrawBoards(boardsize, this);
            SetAllPossibleCells(boardsize, allPossibleCells);
        }

        public void AddShipToBoard(Random r)
        {
            AddShip(CreateShip(r, ShipTypes.BOAT));
            AddShip(CreateShip(r, ShipTypes.DESTROYER));
            AddShip(CreateShip(r, ShipTypes.CRUISER));
            AddShip(CreateShip(r, ShipTypes.BATTLESHIP));
        }
        public void AddShip(Ship ship)
        {
            ShipsList.Add(ship);
        }
        public Ship CreateShip(Random r, ShipTypes shipType)
        {
            List<Cell> possibleCells = allPossibleCells;
            int dirCount = 4;
            int numOfCell = r.Next(possibleCells.Count - 1);
            Cell cell = possibleCells[numOfCell];
            int x = cell.X;
            int y = cell.Y;
            int dirNum = r.Next(0, dirCount);
            Directions dir = (Directions)dirNum;
            int deckCount = GetDeckCount(shipType);
            bool success = CheckCells(y, x, this, GetDeckCount(shipType), dir);
            while (success == false)
            {                
                if (CheckAllDirections(x, y, deckCount, ref dir) != true)
                {
                    possibleCells.RemoveAt(numOfCell);
                    SetNewCoordinates(r, possibleCells, deckCount, ref x, ref y, ref dir, ref numOfCell);
                    success = CheckCells(y, x, this, deckCount, dir);
                }
                else { break; }
            }
            Ship ship = new Ship(x, y, shipType, dir);
            return ship;
        }
        public void SetNewCoordinates(Random r, List<Cell> cells, int deckCount, ref int x, ref int y, ref Directions dir, ref int numOfCell)
        {
            int dirNum = r.Next(0, 4);
            dir = (Directions)dirNum;
            numOfCell = r.Next(cells.Count - 1);
            Cell cell = cells[numOfCell];
            x = cell.X;
            y = cell.Y;
        }
        public bool CheckAllDirections(int x, int y, int deckCount, ref Directions dir)
        {
            int dirNum = 0;
            bool success = false;
            int dirCount = 4;
            while (dirNum < dirCount)
            {

                Directions newDir = (Directions)dirNum;
                if (CheckCells(y, x, this, deckCount, newDir) == true)
                {
                    dir = (Directions)dirNum;
                    success = true;
                    break;
                }
                dirNum++;
            }
            return success;
        }
        public int GetDeckCount(ShipTypes shipType)
        {
            int deckCount = 1;
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
            return deckCount;
        }
        public bool CheckCells(int y, int x, BattleshipBoard board, int shipDeckCount, Directions dir)
        {
            
            bool finalSuccess;
            bool check = false;
            bool success = true;
            switch (dir)
            {
                case Directions.UP:
                    if (y - shipDeckCount >= 0)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                success = false;
                            }
                            y--;
                        }
                    }
                    else { success = false; }
                    break;
                case Directions.RIGHT:
                    if (x + shipDeckCount <= board.BoardSize - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                success = false;
                            }
                            x++;
                        }
                    }
                    else { success = false; }
                    break;
                case Directions.DOWN:
                    if (y + shipDeckCount <= board.BoardSize - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                success = false;
                            }
                            y++;
                        }
                    }
                    else { success = false; }
                    break;
                case Directions.LEFT:
                    if (x - shipDeckCount >= 0)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                success = false;
                            }
                            x--;
                        }
                    }
                    else { success = false; }
                    break;
            }
            finalSuccess = success;
            return finalSuccess;
            
        }
        public bool CheckSquare(int y, int x, BattleshipBoard board)
        {
            //---
            Cell cellCenter = new Cell(x, y);
            Cell cellCenterUp = new Cell(x, y - 1);
            Cell cellCenterDown = new Cell(x, y + 1);

            Cell cellCenterLeft = new Cell(x - 1, y);
            Cell cellUpLeft = new Cell(x - 1, y - 1);
            Cell cellDownLeft = new Cell(x - 1, y + 1);

            Cell cellCenterRight = new Cell(x + 1, y);
            Cell cellUpRight = new Cell(x + 1, y - 1);
            Cell cellDownRight = new Cell(x + 1, y + 1);

            List<Cell> cells = new List<Cell>() { cellCenter, cellCenterDown, cellCenterLeft, cellCenterRight,
                cellCenterUp, cellDownLeft, cellUpLeft, cellUpRight, cellDownRight };
            //---
            bool success = false;
            bool check = true;
            if (board.ShipsList.Count != 0)
            {
                foreach (var ship in board.ShipsList)
                {
                    foreach (var cell in ship.CellsList)
                    {
                        if (y > 0 && y < board.BoardSize - 1 && x > 0 && x < board.BoardSize - 1)
                        {
                            for (int i = 0; i < cells.Count; i++)
                            {
                                if (!(cell.X != cells[i].X || cell.Y != cells[i].Y))
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                }
                success = check;
            }
            else { success = true; }  
            return success;
        }
        public void SetAllPossibleCells(int max, List<Cell> possibleCells)
        {
            int x = 1;
            int y = 1;
            for (int i = 0; i < max - 2; i++)
            {
                for (int j = 0; j < max - 2; j++)
                {
                    Cell cell = new Cell(x + i, y + j);
                    possibleCells.Add(cell);
                }
            }
        }
    }
}
