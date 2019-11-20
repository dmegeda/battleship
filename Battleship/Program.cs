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
            int boardsize;
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.Write("Choose board size: ");
                    boardsize = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect input! \nPress any key to try again.");
                }
                Console.ReadKey();
                
            }
            
            BattleshipBoard firstBoard = new BattleshipBoard(boardsize);
            BattleshipBoard secondBoard = new BattleshipBoard(boardsize);
            Random r = new Random();
            firstBoard.AddShipToBoard(r);
            secondBoard.AddShipToBoard(r);
            Draw.DrawAllShips(firstBoard);
            Draw.DrawAllShips(secondBoard);
            ShowBoard(boardsize, firstBoard.MainBoard);

            ConsoleKeyInfo key;
            Game firstGame = new Game(new NoHitState());
            Game secondGame = new Game(new NoHitState());
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    if (firstGame.IsOver(firstBoard) == true || secondGame.IsOver(secondBoard) == true)
                    {
                        GameOver(firstBoard, secondBoard, firstGame, secondGame);
                        break;
                    }
                    firstGame.Hit(firstBoard, secondBoard);
                    secondGame.Hit(secondBoard, firstBoard);
                    Console.WriteLine("Main:");
                    ShowBoard(boardsize, firstBoard.MainBoard);
                    Console.WriteLine("Hits:");
                    ShowBoard(boardsize, firstBoard.HitsBoard);
                }
            }
            while (key.Key != ConsoleKey.Escape);
        }
        public static void GameOver(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard, Game playerGame, Game enemyGame)
        {
            Console.Clear();
            string player1Won = "Winner: Player1";
            string player2Won = "Winner: Player2";
            string finalString = "Game over! ";
            if (playerGame.IsOver(firstPlayerBoard))
            {
                finalString += player2Won;
            }
            else if (enemyGame.IsOver(secondPlayerBoard))
            {
                finalString += player1Won;
            }
            Console.WriteLine(finalString);
            Console.WriteLine("Player1:");
            ShowBoard(firstPlayerBoard.BoardSize, firstPlayerBoard.MainBoard);
            Console.WriteLine("Player2:");
            ShowBoard(secondPlayerBoard.BoardSize, secondPlayerBoard.MainBoard);
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
