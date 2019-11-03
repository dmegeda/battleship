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
            int[,] board = GetBoard(10);
            int firstShipX = 5;
            int firstShipY = 9;
            int secondShipX = 4;
            int secondShipY = 9;
            int ctr = 0;
            Ship battleship = new Ship(firstShipX, firstShipY, ShipTypes.BATTLESHIP, Directions.UP);
            Ship boat = new Ship(secondShipX, secondShipY, ShipTypes.BOAT, Directions.LEFT);
            BattleshipBoard playerBoard = new BattleshipBoard(10);
            while (ctr < battleship.DeckCount)
            {
                board[firstShipY, firstShipX + ctr] = 1;
                ctr++;
            }
            Assert.IsFalse(playerBoard.CheckCells(secondShipY, secondShipX, board, boat.DeckCount, Directions.LEFT));
        }

        [TestMethod]
        public void TestHit_IfStateEqualsHitStateAndShipDestroyedChangeState_StateEqualsNoHitState()
        {
            //IShipState noHitState = new NoHitState();
            //IShipState hitState = new HitState();
            //Game game = new Game(new HitState());
            //BattleshipBoard firstBoard = new BattleshipBoard(10);
            //BattleshipBoard secondBoard = new BattleshipBoard(10);
            //Random r = new Random();
            //secondBoard.AddShip(r, ShipTypes.DESTROYER);
            //Ship hitship;
            //foreach (var ship in secondBoard.ShipsList)
            //{
            //    hitship = ship;
            //    ship.CellsList[0].X = game.HitX;
            //    ship.CellsList[0].Y = game.HitY;
            //    ship.CellsList[0].IsDestroyed = true; 
            //}
            //Assert.IsTrue(secondBoard.ShipsList[0].IsDestroyed());
        }
    }
}
