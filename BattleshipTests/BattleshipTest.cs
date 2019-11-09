using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;

namespace BattleshipTests
{
    [TestClass]
    public class BattleshipTest
    {
        private int[,] GetBoard(int boardsize)
        {
            int[,] board = new int[boardsize, boardsize];
            for (int i = 0; i < boardsize; i++)
            {
                board[i, i] = 0;
            }
            return board;
        }
        [TestMethod]
        public void TestCheckCells_AddShipToX3Y3IfShipNearby_ReturnFalse()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            Random r = new Random();
            //Ship ship = new Ship(5, 5, ShipTypes.BATTLESHIP, Directions.LEFT);
            //board.AddShip(ship);
            Assert.IsFalse(board.CheckCells(6, 2, board, 4, Directions.LEFT));
        }

        [TestMethod]
        public void TestCheckSquare()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            Random r = new Random();
            Ship ship = new Ship(5, 5, ShipTypes.BOAT, Directions.UP);
            board.AddShip(ship);
            Assert.IsFalse(board.CheckSquare(4, 5, board));
        }
    }
}
