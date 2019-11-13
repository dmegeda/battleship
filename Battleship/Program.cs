using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Choose board size: ");
            int boardsize = Convert.ToInt32(Console.ReadLine());

            BattleshipBoard firstPlayerBoard = new BattleshipBoard(boardsize);
            BattleshipBoard secondPlayerBoard = new BattleshipBoard(boardsize);
            Random r = new Random();
            firstPlayerBoard.AddShipToBoard(r);
            secondPlayerBoard.AddShipToBoard(r);
            Draw.DrawAllShips(firstPlayerBoard);
            Draw.DrawAllShips(secondPlayerBoard);
            ShowBoard(boardsize, firstPlayerBoard.MainBoard);

            ConsoleKeyInfo key;
            Game playerGame = new Game(new NoHitState());
            Game botGame = new Game(new NoHitState());
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {

                    Console.Clear();

                    if (playerGame.IsOver(firstPlayerBoard))
                    {
                        Console.Clear();
                        Console.WriteLine("Game over! Winner: Player2");
                        Console.WriteLine("Player1:");
                        ShowBoard(boardsize, firstPlayerBoard.MainBoard);
                        Console.WriteLine("Player2:");
                        ShowBoard(boardsize, secondPlayerBoard.MainBoard);
                        break;

                    }
                    else if (botGame.IsOver(secondPlayerBoard))
                    {
                        Console.Clear();
                        Console.WriteLine("Game over! Winner: Player1");
                        Console.WriteLine("Player1:");
                        ShowBoard(boardsize, firstPlayerBoard.MainBoard);
                        Console.WriteLine("Player2:");
                        ShowBoard(boardsize, secondPlayerBoard.MainBoard);
                        break;

                    }
                    playerGame.Hit(firstPlayerBoard, secondPlayerBoard);
                    botGame.Hit(secondPlayerBoard, firstPlayerBoard);
                    Console.WriteLine("Main:");
                    ShowBoard(boardsize, firstPlayerBoard.MainBoard);
                    Console.WriteLine("Hits:");
                    ShowBoard(boardsize, firstPlayerBoard.HitsBoard);
                }
            }
            while (key.Key != ConsoleKey.Escape);

            Console.ReadLine();
        }

        public static void ShowBoard(int size, int [,] board)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0,2}", board[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
