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
            AddShip(r, ShipTypes.BOAT);
            AddShip(r, ShipTypes.DESTROYER);
            AddShip(r, ShipTypes.CRUISER);
            AddShip(r, ShipTypes.BATTLESHIP);
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
            success = CheckCells(y, x, MainBoard, ship.DeckCount, dir);
            while (success == false)
            {
                x = r.Next(min, max);
                y = r.Next(min, max);
                dirNum = r.Next(0, dirCount);
                success = CheckCells(y, x, MainBoard, ship.DeckCount, dir);
            }
            ship = new Ship(x, y, shipType, dir);
            ShipsList.Add(ship);
            Draw.DrawShip(this, ship);
        }

        public bool CheckCells(int y, int x, int[,] board, int shipDeckCount, Directions dir)
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
                    if (x + shipDeckCount <= board.GetLength(0) - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
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
                    if (y + shipDeckCount <= board.GetLength(0) - 1)
                    {
                        for (int i = 0; i < shipDeckCount; i++)
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
                    if (x - shipDeckCount >= 0)
                    {
                        check = CheckSquare(y, x, board);
                        for (int i = 0; i < shipDeckCount; i++)
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
        public bool CheckSquare(int y, int x, int[,] board)
        {
            bool success = false;

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
    }
}
