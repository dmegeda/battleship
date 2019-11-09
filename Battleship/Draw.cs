using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public static class Draw
    {

        public static void DrawShip(BattleshipBoard board, Ship ship)
        {
            int x = 0;
            int y = 0;
            foreach (var cell in ship.CellsList)
            {
                try
                {
                    board.MainBoard[cell.Y, cell.X] = 1;
                    y = cell.Y;
                    x = cell.X;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Cell: y" + y + " x" + x);
                }
            }
        }

        public static void DrawAllShips(BattleshipBoard board)
        {
            int x = 0;
            int y = 0;
            foreach (var ship in board.ShipsList)
            {
                try
                {
                    foreach (var cell in ship.CellsList)
                    {
                        board.MainBoard[cell.Y, cell.X] = 1;
                        x = cell.X;
                        y = cell.Y;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("" + x + " " + y);
                }
            }
        }

        public static void DrawBoards(int boardsize, BattleshipBoard board)
        {
            board.MainBoard = new int[boardsize, boardsize];
            board.HitsBoard = new int[boardsize, boardsize];
            for (int i = 0; i < boardsize; i++)
            {
                board.MainBoard[i, i] = 0;
                board.HitsBoard[i, i] = 0;
            }
        }

        public static void DrawHits(int x, int y, bool success, BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard)
        {
            if (success)
            {
                firstPlayerBoard.HitsBoard[y, x] = 2;
                secondPlayerBoard.MainBoard[y, x] = 2;
            }
            else
            {
                firstPlayerBoard.HitsBoard[y, x] = 3;
                secondPlayerBoard.MainBoard[y, x] = 3;
            }
        }

        public static void DrawAfterKill(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard,Ship ship)
        {

            foreach (var cell in ship.CellsList)
            {
                if (ship.DeckCount != 1)
                {
                    if (ship.Dir == Directions.UP || ship.Dir == Directions.DOWN)
                    {
                        DrawUpDownDirSurround(cell.X, cell.Y, firstPlayerBoard.HitsBoard);
                        DrawUpDownDirSurround(cell.X, cell.Y, secondPlayerBoard.MainBoard);
                    }
                    else
                    {
                        DrawLeftRightDirSurround(cell.X, cell.Y, firstPlayerBoard.HitsBoard);
                        DrawLeftRightDirSurround(cell.X, cell.Y, secondPlayerBoard.MainBoard);
                    }
                }
                else
                {
                    DrawSquare(cell.X, cell.Y, firstPlayerBoard.HitsBoard);
                    DrawSquare(cell.X, cell.Y, secondPlayerBoard.MainBoard);
                }
            }
        }
        public static void DrawSquare(int x, int y, int[,] board)
        {
            board[y, x + 1] = 3;
            board[y, x - 1] = 3;

            board[y + 1, x] = 3;
            board[y + 1, x + 1] = 3;
            board[y + 1, x - 1] = 3;

            board[y - 1, x] = 3;
            board[y - 1, x + 1] = 3;
            board[y - 1, x - 1] = 3;
        }
        public static void DrawUpDownDirSurround(int x, int y, int[,] board)
        {
            if (board[y + 1, x] != 2)
            {
                board[y, x + 1] = 3;
                board[y, x - 1] = 3;
                board[y + 1, x] = 3;
                board[y + 1, x - 1] = 3;
                board[y + 1, x + 1] = 3;
            }
            else if (board[y - 1, x] != 2)
            {
                board[y, x + 1] = 3;
                board[y, x - 1] = 3;
                board[y - 1, x] = 3;
                board[y - 1, x - 1] = 3;
                board[y - 1, x + 1] = 3;
            }
            else
            {
                board[y, x + 1] = 3;
                board[y, x - 1] = 3;
            }
        }
        public static void DrawLeftRightDirSurround(int x, int y, int[,] board)
        {
            if (board[y, x + 1] != 2)
            {
                board[y, x + 1] = 3;
                board[y - 1, x + 1] = 3;
                board[y + 1, x + 1] = 3;

                board[y + 1, x] = 3;
                board[y - 1, x] = 3;
            }
            else if (board[y, x - 1] != 2)
            {
                board[y, x - 1] = 3;
                board[y - 1, x - 1] = 3;
                board[y + 1, x - 1] = 3;

                board[y + 1, x] = 3;
                board[y - 1, x] = 3;
            }
            else
            {
                board[y - 1, x] = 3;
                board[y + 1, x] = 3;
            }
        }
    }
}
