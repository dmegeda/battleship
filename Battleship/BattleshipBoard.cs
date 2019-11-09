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
        public int[,] MainBoard { get; set; }
        public int[,] HitsBoard { get; set; }

        public List<Ship> ShipsList { get; set; }
        public BattleshipBoard(int boardsize)
        {
            ShipsList = new List<Ship>();
            BoardSize = boardsize;
            Draw.DrawBoards(boardsize, this);
        }
        public void AddShipToBoard(Random r)
        {
            AddShip(CreateShip(r, ShipTypes.BOAT));
            AddShip(CreateShip(r, ShipTypes.DESTROYER));
            AddShip(CreateShip(r, ShipTypes.CRUISER));
            AddShip(CreateShip(r, ShipTypes.BATTLESHIP));
            //Ship ship1 = CreateShip(r, ShipTypes.BATTLESHIP);
            //Ship ship2 = CreateShip(r, ShipTypes.CRUISER);
            //CreateShip(r, ShipTypes.BATTLESHIP);
            //CreateShip(r, ShipTypes.CRUISER);
        }

        public void AddShip(Ship ship)
        {
            ShipsList.Add(ship);
        }

        public Ship CreateShip(Random r, ShipTypes shipType)
        {   
            bool success;
            int max = MainBoard.GetLength(0) - 2;
            int min = 1;
            int dirCount = 4;
            int x = r.Next(min, max);
            int y = r.Next(min, max);

            int dirNum = r.Next(0, dirCount);
            Directions dir = (Directions)dirNum;
            int deckCount = GetDeckCount(shipType);
            //Ship ship = new Ship(x, y, shipType, dir);
            Ship ship;
            success = CheckCells(y, x, this, deckCount, dir);
            while (success == false)
            {
                x = r.Next(min, max);
                y = r.Next(min, max);
                dirNum = r.Next(0, dirCount);
                success = CheckCells(y, x, this, deckCount, dir);
            }
            //if (success == true)
            //{

            //}
            ship = new Ship(x, y, shipType, dir);
            //Ship ship = new Ship(x, y, shipType, dir);
            //Draw.DrawShip(this, ship);
            //ShipsList.Add(ship);
            return ship;
        }

        public void AddShip(Random r, ShipTypes shipType)
        {
            bool success;
            int max = MainBoard.GetLength(0) - 2;
            int min = 1;
            int dirCount = 4;
            int x = r.Next(min, max);
            int y = r.Next(min, max);
            int dirNum = r.Next(0, dirCount);
            Directions dir = (Directions)dirNum;
            Ship ship = new Ship(x, y, shipType, dir);
            success = CheckCells(y, x, this, GetDeckCount(shipType), dir);
            while (success == false)
            {
                x = r.Next(min, max);
                y = r.Next(min, max);
                dirNum = r.Next(0, dirCount);
                success = CheckCells(y, x, this, ship.DeckCount, dir);
            }
            ship = new Ship(x, y, shipType, dir);
            ShipsList.Add(ship);
            Draw.DrawShip(this, ship);
        }
        public int GetDeckCount(ShipTypes shipType)
        {
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
                        if (y > 0 && y < board.BoardSize - 1 && x > 0 && y < board.BoardSize - 1)
                        {
                            for (int i = 0; i < cells.Count; i++)
                            {
                                if (cell.X != cells[i].X && cell.Y != cells[i].Y)
                                {
                                   
                                }
                                else
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
    }
}
