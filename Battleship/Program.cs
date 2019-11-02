﻿using System;
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

            BattleshipBoard playerBoard = new BattleshipBoard(boardsize);
            BattleshipBoard botBoard = new BattleshipBoard(boardsize);
            Random r = new Random();
            playerBoard.AddShipToBoard(r);
            botBoard.AddShipToBoard(r);
            //ShowBoard(boardsize, playerBoard.MainBoard);
            Console.WriteLine();
            ShowBoard(boardsize, playerBoard.MainBoard);
            Console.WriteLine();
            ShowBoard(boardsize, botBoard.MainBoard);
            ConsoleKeyInfo key;
            Game g = new Game(new NoHitState());
            do
            {
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    
                    g.Hit(playerBoard, botBoard);
                    Console.WriteLine("Hits:");
                    ShowBoard(boardsize, playerBoard.HitsBoard);
                    Console.WriteLine("Enemy's boards:");
                    ShowBoard(boardsize, botBoard.MainBoard);
                    
                    Console.WriteLine("State: " + g.State);
                }
            }
            
            while (key.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
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
