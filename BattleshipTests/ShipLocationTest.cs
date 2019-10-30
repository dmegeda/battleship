using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;

namespace BattleshipTests
{
    [TestClass]
    public class ShipLocationTest
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
            int[,] board = GetBoard(10);
            int firstShipX = 5;
            int firstShipY = 9;
            int secondShipX = 4;
            int secondShipY = 9;
            int ctr = 0;
            ShipSetter setter = ShipSetter.getInstance();
            Ship battleship = new Ship(firstShipX, firstShipY, ShipTypes.BATTLESHIP, Directions.UP);
            Ship boat = new Ship(secondShipX, secondShipY, ShipTypes.BOAT, Directions.LEFT);
            while (ctr < battleship.DeckCount)
            {
                board[firstShipY, firstShipX + ctr] = 1;
                ctr++;
            }
            Assert.IsFalse(setter.CheckCells(secondShipY, secondShipX, board, boat, Directions.LEFT));
        }
        [TestMethod]
        public void TestConcreteHit_HitUpShipX5Y5ContinueIfSuccessHit_X5Y4AndX5Y3Equals2()
        {
            BattleshipBoard playerBoard = new BattleshipBoard(10);
            BattleshipBoard enemyBoard = new BattleshipBoard(10);
            //int[,] board = GetBoard(10);
            int x = 5;
            int y = 5;
            Game game = new Game(new HitState());
            Ship battleship = new Ship(ShipTypes.BATTLESHIP);
            int ctr = 0;
            while (ctr < battleship.DeckCount)
            {
                enemyBoard.MainBoard[y - ctr, x] = 1;
                ctr++;
            }
            game.HitX = x;
            game.HitY = y;
            game.Hit(playerBoard, enemyBoard);
            game.Hit(playerBoard, enemyBoard);
            Assert.IsTrue(enemyBoard.MainBoard[4, 5] == 2 && enemyBoard.MainBoard[3, 5] == 2);
        }
    }
}
