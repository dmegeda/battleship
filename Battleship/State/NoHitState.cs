using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class NoHitState : IShipState
    {
        List<Cell> cells = new List<Cell>();
        public void Hit(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard, Game game)
        {
            Random rnd = new Random();
            //int x = rnd.Next(0, firstPlayerBoard.BoardSize);
            //int y = rnd.Next(0, firstPlayerBoard.BoardSize);
            Cell randomCell = GenerateRandomCell(rnd, 0, firstPlayerBoard.BoardSize);
            int x = randomCell.X;
            int y = randomCell.Y;
            if (firstPlayerBoard.HitsBoard[y, x] != 2 && firstPlayerBoard.HitsBoard[y, x] != 3)
            {
                bool success = false;
                foreach (var enemyShip in secondPlayerBoard.ShipsList)
                {
                    foreach (var cell in enemyShip.CellsList)
                    {
                        if (cell == randomCell)
                        {
                            game.HitShip = enemyShip;
                            cell.Destroy();
                            ActionAfterDestroyCell(firstPlayerBoard, secondPlayerBoard, game, enemyShip);
                            success = true;
                            break;
                        }
                    }
                }
                ActionAfterHit(success, game, ref x, ref y, firstPlayerBoard, secondPlayerBoard);
            }
            else
            {
                while(firstPlayerBoard.HitsBoard[y, x] == 2 || firstPlayerBoard.HitsBoard[y, x] == 3)
                {
                    x = rnd.Next(0, firstPlayerBoard.BoardSize);
                    y = rnd.Next(0, firstPlayerBoard.BoardSize);
                }
            }
        }

        public void ActionAfterDestroyCell(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard, Game game, Ship enemyShip)
        {
            if (!enemyShip.IsDestroyed())
            {
                game.State = new HitState();
            }
            else
            {
                if (game.HitShip.IsDestroyed())
                {
                    Draw.DrawAfterKill(firstPlayerBoard, secondPlayerBoard, game.HitShip);
                }
            }
        }
        public void ActionAfterHit(bool success, Game game, ref int x, ref int y, BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard)
        {
            if (success == true)
            {
                Draw.DrawHits(x, y, true, firstPlayerBoard, secondPlayerBoard);
                game.HitX = x;
                game.HitY = y;
            }
            else
            {
                Draw.DrawHits(x, y, false, firstPlayerBoard, secondPlayerBoard);
            }
        }
        public Cell GenerateRandomCell(Random r, int min, int max)
        {
            int x = r.Next(min, max);
            int y = r.Next(min, max);
            Cell cell = new Cell(x, y);
            return cell;
        }
    }
}
