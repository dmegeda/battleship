using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class ShipSetter
    {
        private static ShipSetter instance;
        public void AddShipToBoard(BattleshipBoard board, Random r)
        {

            AddShip(board, r, ShipTypes.BOAT);
            AddShip(board, r, ShipTypes.DESTROYER);
            AddShip(board, r, ShipTypes.CRUISER);
            AddShip(board, r, ShipTypes.BATTLESHIP);
        }

        public void AddShip(BattleshipBoard board, Random r, ShipTypes shipType)
        {
            bool success;
            int max = board.MainBoard.GetLength(0) - 2;
            int min = 1;
            int dirCount = 4;
            int x = r.Next(min, max);
            int y = r.Next(min, max);
            //ship.X = x;
            //ship.Y = y;
            
            int dirNum = r.Next(0, dirCount);
            Directions dir = (Directions)dirNum;
            Ship ship = new Ship(x, y, shipType, dir);
            int counter = 0;
            success = CheckCells(y, x, board.MainBoard, ship, dir);
            while (success == false)
            {
                x = r.Next(min, max);
                y = r.Next(min, max);
                success = CheckCells(y, x, board.MainBoard, ship, dir);
            }

            while (counter < ship.DeckCount)
            {
                board.MainBoard[y, x] = 1;

                WorkWithCoordinates(ref y, ref x, dir);
                counter++;
            }

        }

        public void WorkWithCoordinates(ref int y, ref int x, Directions dir)
        {
            switch (dir)
            {
                case Directions.UP:
                    y--;
                    break;
                case Directions.RIGHT:
                    x++;
                    break;
                case Directions.DOWN:
                    y++;
                    break;
                case Directions.LEFT:
                    x--;
                    break;
            }
        }

        public bool CheckCells(int y, int x, int[,] board, Ship ship, Directions dir)
        {
            bool success;
            bool check = false;
            bool suc = true;
            int j = 0;
            switch (dir)
            {
                case Directions.UP:
                    if (y - ship.DeckCount >= 0)
                    {
                        for (int i = 0; i < ship.DeckCount; i++)
                        {
                            check = CheckSquare(y, x, board);
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
                    if (x + ship.DeckCount <= board.GetLength(0) - 1)
                    {
                        for (int i = 0; i < ship.DeckCount; i++)
                        {
                            check = CheckSquare(y, x, board);
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
                    if (y + ship.DeckCount <= board.GetLength(0) - 1)
                    {
                        for (int i = 0; i < ship.DeckCount; i++)
                        {
                            check = CheckSquare(y, x, board);
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
                    if (x - ship.DeckCount >= 0)
                    {
                        check = CheckSquare(y, x, board);
                        for (int i = 0; i < ship.DeckCount; i++)
                        {
                            check = CheckSquare(y, x, board);
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

        public static ShipSetter getInstance()
        {
            if(instance == null)
            {
                instance = new ShipSetter();
            }
            return instance;
        }
        public bool CheckSquare(int y, int x, int[,] board)
        {
            bool success = false;
            int k = 1;
            //try
            //{

            //}
            //catch
            //{
            //    Console.WriteLine("x " + y + " y" + x);
            //}
            if (board[y, x] != 1)
            {
                if (y == board.GetLength(0) - 1)
                {
                    if (x > 0 && x < board.GetLength(0) - 1)
                    {
                        if (board[y, x - 1] != 1 && board[y, x + 1] != 1
                            && board[y - 1, x] != 1 && board[y - 1, x + 1] != 1 && board[y - 1, x - 1] != 1)
                        {
                            success = true;
                        }
                    }
                }
                else if (y == 0)
                {
                    if (x > 0 && x < board.GetLength(0) - 1)
                    {
                        if (board[y, x + 1] != 1 && board[y + 1, x] != 1 && board[y + 1, x + 1] != 1)
                        {
                            success = true;
                        }
                    }
                }
                else
                {
                    if (x > 0 && x < board.GetLength(0) - 1)
                    {
                        if (board[y + 1, x + 1] != 1 && board[y + 1, x - 1] != 1 && board[y + 1, x] != 1
                                    && board[y - 1, x - 1] != 1 && board[y - 1, x + 1] != 1 && board[y - 1, x] != 1
                                    && board[y, x + 1] != 1 && board[y, x - 1] != 1)
                        {
                            success = true;
                        }
                        else { success = false; }
                    }
                }
            }
            return success;
        }
        protected ShipSetter() { }
    }
}
