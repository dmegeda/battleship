using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class NoHitState : IShipState
    {
        public void Hit(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard, Game game)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, firstPlayerBoard.BoardSize);
            int y = rnd.Next(0, firstPlayerBoard.BoardSize);
            if (firstPlayerBoard.HitsBoard[y, x] != 2 && firstPlayerBoard.HitsBoard[y, x] != 3)
            {
                bool success = false;
                foreach (var enemyShip in secondPlayerBoard.ShipsList)
                {
                    foreach (var cell in enemyShip.CellsList)
                    {
                        if (cell.X == x && cell.Y == y)
                        {
                            game.HitShip = enemyShip;
                            cell.Destroy();
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
                            success = true;
                            break;
                        }
                    }
                }
                if(success == true)
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
            else
            {
                while(firstPlayerBoard.HitsBoard[y, x] == 2 || firstPlayerBoard.HitsBoard[y, x] == 3)
                {
                    x = rnd.Next(0, firstPlayerBoard.BoardSize);
                    y = rnd.Next(0, firstPlayerBoard.BoardSize);
                }
            }
        }
    }
}
