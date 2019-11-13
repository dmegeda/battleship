using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class BattleshipBoard
    {
        //Убрать в Draw
        public int BoardSize { get; set; }
        public int[,] MainBoard { get; set; }
        public int[,] HitsBoard { get; set; }
        //
        public List<Ship> ShipsList { get; set; }
        private List<ShipCells> combinedCells = new List<ShipCells>();
        public BattleshipBoard(int boardsize)
        {
            ShipsList = new List<Ship>();
            BoardSize = boardsize;
            Draw.DrawBoards(boardsize, this);
            CombineCells(boardsize, combinedCells);
        }
        public void AddShipToBoard(Random r)
        {
            //AddShip(CreateShip(r, ShipTypes.BOAT));
            AddShip(CreateShip(r, ShipTypes.DESTROYER));
            //AddShip(CreateShip(r, ShipTypes.CRUISER));
            //AddShip(CreateShip(r, ShipTypes.BATTLESHIP));
        }

        public void AddShip(Ship ship)
        {
            ShipsList.Add(ship);
        }

        public Ship CreateShip(Random r, ShipTypes shipType)
        {
            List<ShipCells> cells = combinedCells;
            //CombineCells(BoardSize, combinedCells);
            bool success;
            int max = MainBoard.GetLength(0) - 2;
            int min = 1;
            int dirCount = 4;
            int i = r.Next(cells.Count);
            ShipCells c = combinedCells[i];
            int x = c.X;
            int y = c.Y;
            int dirNum = r.Next(0, dirCount);
            Directions dir = (Directions)dirNum;
            int deckCount = GetDeckCount(shipType);
            success = CheckCells(y, x, this, GetDeckCount(shipType), dir);
            //Вынести отдельно 
            while (success == false)
            {
                int dirN = 0;
                bool suc = false;
                while (dirN < 4)
                {
                    
                    Directions newDir = (Directions)dirN;
                    success = CheckCells(y, x, this, deckCount, newDir);
                    if (success == true)
                    {
                        dir = (Directions)dirN;
                        suc = true;       
                    }
                    dirN++;
                }
                if (suc != true)
                {
                    cells.RemoveAt(i);
                    dirNum = r.Next(0, 4);
                    dir = (Directions)dirNum;
                    i = r.Next(cells.Count);
                    c = cells[i];
                    x = c.X;
                    y = c.Y;
                    success = CheckCells(y, x, this, deckCount, dir);
                }
                else { break; }
                
            }
            Ship ship = new Ship(x, y, shipType, dir);
            //ShipsList.Add(ship);
            //Draw.DrawShip(this, ship);
            return ship;
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
            
            bool success;
            bool check = false;
            bool suc = true;
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
                                suc = false;
                            }
                            y--;
                        }
                    }
                    else { suc = false; }
                    break;
                case Directions.RIGHT:
                    if (x + shipDeckCount <= board.BoardSize - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                suc = false;
                            }
                            x++;
                        }
                    }
                    else { suc = false; }
                    break;
                case Directions.DOWN:
                    if (y + shipDeckCount <= board.BoardSize - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                suc = false;
                            }
                            y++;
                        }
                    }
                    else { suc = false; }
                    break;
                case Directions.LEFT:
                    if (x - shipDeckCount >= 0)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
                        {
                            check = CheckSquare(y, x, this);
                            if (check == false)
                            {
                                suc = false;
                            }
                            x--;
                        }
                    }
                    else { suc = false; }
                    break;
            }
            success = suc;
            return success;
            
        }

        public bool CheckSquare(int y, int x, BattleshipBoard board)
        {
            ShipCells cellCenter = new ShipCells(x, y);
            ShipCells cellCenterUp = new ShipCells(x, y - 1);
            ShipCells cellCenterDown = new ShipCells(x, y + 1);

            ShipCells cellCenterLeft = new ShipCells(x - 1, y);
            ShipCells cellUpLeft = new ShipCells(x - 1, y - 1);
            ShipCells cellDownLeft = new ShipCells(x - 1, y + 1);

            ShipCells cellCenterRight = new ShipCells(x + 1, y);
            ShipCells cellUpRight = new ShipCells(x + 1, y - 1);
            ShipCells cellDownRight = new ShipCells(x + 1, y + 1);

            List<ShipCells> cells = new List<ShipCells>() { cellCenter, cellCenterDown, cellCenterLeft, cellCenterRight,
                cellCenterUp, cellDownLeft, cellUpLeft, cellUpRight, cellDownRight };

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
                                if (cell.X != cells[i].X || cell.Y != cells[i].Y)
                                {
                                }
                                else
                                {
                                    check = false;
                                }
                            }
                        }
                        //else if(y == 0 && x > 0 && x < board.BoardSize - 1)
                        //{

                        //}
                    }
                }
                success = check;
            }
            else { success = true; }
            
            return success;
        }
        //
        public void CombineCells(int max, List<ShipCells> cells)
        {
            int x = 1;
            int y = 1;
            for (int i = 0; i < max - 2; i++)
            {
                for (int j = 0; j < max - 2; j++)
                {
                    ShipCells c = new ShipCells(x + i, y + j);
                    cells.Add(c);
                }
            }
        }
    }
}
