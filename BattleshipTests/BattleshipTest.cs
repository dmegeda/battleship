using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;
using System;

namespace BattleshipTests
{
    [TestClass]
    public class BattleshipTest
    {
        [TestMethod]
        public void TestCheckCells_TryAddShipToX2Y3DirRightIfShipX9Y2DirLeftNearby_ReturnFalse()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            int x = 9;
            int y = 2;
            Directions dir = Directions.LEFT;

            int testShipX = 2;
            int testShipY = 3;
            Directions testShipDir = Directions.RIGHT;
            Ship ship = new Ship(x, y, ShipTypes.BATTLESHIP, dir);
            int testDeckCount = board.GetDeckCount(ShipTypes.BATTLESHIP);
            board.AddShip(ship);
            Ship testShip = new Ship(testShipX, testShipY, ShipTypes.BATTLESHIP, testShipDir);
            bool success = board.CheckCells(testShipX, testShipY, board, testDeckCount, testShipDir);
            if (success)
            {
                board.AddShip(testShip);
            }
            Assert.IsFalse(board.ShipsList.Contains(testShip));
        }

        [TestMethod]
        public void TestCheckSquare_CheckNeighborCellIsFreeIfCellX4Y3IsNotFree_ReturnFalse()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            Ship ship = new Ship(4, 3, ShipTypes.CRUISER, Directions.UP);
            board.AddShip(ship);
            Assert.IsFalse(board.CheckSquare(3, 4, board));
        }

        [TestMethod]
        public void TestHitStateHit_HitShipX4Y3DirRight_ShipIsDestroyedTrue()
        {
            BattleshipBoard playerBoard = new BattleshipBoard(10);
            BattleshipBoard enemyBoard = new BattleshipBoard(10);
            Ship ship = new Ship(4, 3, ShipTypes.DESTROYER, Directions.RIGHT);
            enemyBoard.AddShip(ship);
            ship.CellsList[0].Destroy();
            
            IShipState hitState = new HitState();
            IShipState noHitState = new NoHitState();
            Game g = new Game(hitState);
            g.HitX = 4;
            g.HitY = 3;
            g.HitShip = ship;
            while (!ship.IsDestroyed())
            {
                g.Hit(playerBoard, enemyBoard);
            }
            Assert.IsTrue(ship.IsDestroyed());
        }

        [TestMethod]
        public void TestIsOver_AddOneShipX4Y3DirUpThenDestroyIt_ReturnTrue()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            Ship ship = new Ship(4, 3, ShipTypes.BOAT, Directions.UP);
            board.AddShip(ship);
            ship.CellsList[0].IsDestroyed = true;
            Game g = new Game(new NoHitState());
            Assert.IsTrue(g.IsOver(board));
        }

        [TestMethod]
        public void TestDrawAllShips_AddShipX3Y3AndShipX7Y7_MainArrayShipXYValueMustbeOne()
        {
            BattleshipBoard board = new BattleshipBoard(10);
            Ship ship1 = new Ship(3, 3, ShipTypes.BOAT, Directions.UP);
            Ship ship2 = new Ship(7, 7, ShipTypes.BOAT, Directions.UP);
            board.AddShip(ship1);
            board.AddShip(ship2);
            Draw.DrawAllShips(board);
            int y1 = ship1.CellsList[0].Y;
            int x1 = ship1.CellsList[0].X;
            int y2 = ship2.CellsList[0].Y;
            int x2 = ship2.CellsList[0].X;
            Assert.IsTrue(board.MainBoard[y1, x1] == 1 && board.MainBoard[y2, x2] == 1);
        }

        [TestMethod]
        public void TestDrawAfterKill_AddShipX3Y3DestroyIt_ValueInHitsArrayAroundTheShipMustBeThree()
        {
            BattleshipBoard playerBoard = new BattleshipBoard(10);
            BattleshipBoard enemyBoard = new BattleshipBoard(10);
            Ship ship = new Ship(4, 3, ShipTypes.BOAT, Directions.RIGHT);
            enemyBoard.AddShip(ship);
            Draw.DrawAllShips(enemyBoard);
            ship.CellsList[0].IsDestroyed = true;
            int x = ship.CellsList[0].X;
            int y = ship.CellsList[0].Y;
            Draw.DrawAfterKill(playerBoard, enemyBoard, ship);
            Assert.IsTrue(playerBoard.HitsBoard[y, x - 1] == 3 && playerBoard.HitsBoard[y, x + 1] == 3
                && playerBoard.HitsBoard[y - 1, x] == 3 && playerBoard.HitsBoard[y - 1, x - 1] == 3
                && playerBoard.HitsBoard[y - 1, x + 1] == 3 && playerBoard.HitsBoard[y + 1, x] == 3
                && playerBoard.HitsBoard[y + 1, x + 1] == 3 && playerBoard.HitsBoard[y + 1, x - 1] == 3);
        }
    }
}
