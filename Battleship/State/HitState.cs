using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class HitState : IShipState
    {
        private Directions dir = Directions.UP;
        //Direction: 1 - Up, 3 - Down, 2 - Right, 4 - Left
        public void Hit(BattleshipBoard firstPlayerBoard, BattleshipBoard secondPlayerBoard, Game game)
        {
            
            int x = game.HitX;
            int y = game.HitY;
            if(x > 0 && x < secondPlayerBoard.MainBoard.GetLength(0) - 1)
            {
                if(y > 0 && y < secondPlayerBoard.MainBoard.GetLength(0) - 1)
                {
                    ChangeCoordinates(ref y, ref x, dir);
                    bool success = false;
                    foreach (var cell in game.HitShip.CellsList)
                    {
                        if (x == cell.X && y == cell.Y)
                        {
                            cell.Destroy();
                            game.HitX = cell.X;
                            game.HitY = cell.Y;
                            success = true;
                            break;
                        }
                        
                    }
                    if(success == true)
                    {
                        Draw.DrawHits(x, y, true, firstPlayerBoard, secondPlayerBoard);
                        if (game.HitShip.IsDestroyed())
                        {
                            IShipState state = new NoHitState();
                            Draw.DrawAfterKill(firstPlayerBoard, secondPlayerBoard, game.HitShip);
                            game.State = new NoHitState();
                        }
                    }
                    else
                    {
                        
                        Draw.DrawHits(x, y, false, firstPlayerBoard, secondPlayerBoard);
                        dir = ChangeDir(dir);
                        x = game.HitX;
                        y = game.HitY;
                    }

                }
            }
            
        }
        public void ChangeCoordinates(ref int y, ref int x, Directions dir)
        {
            switch (dir)
            {
                case Directions.DOWN:
                    y++;
                    break;
                case Directions.UP:
                    y--;
                    break;
                case Directions.LEFT:
                    x--;
                    break;
                case Directions.RIGHT:
                    x++;
                    break;
            }
        }

        public Directions ChangeDir(Directions dir)
        {
            switch (dir)
            {
                case Directions.UP:
                    dir = Directions.RIGHT;
                    break;
                case Directions.RIGHT:
                    dir = Directions.DOWN;
                    break;
                case Directions.DOWN:
                    dir = Directions.LEFT;
                    break;
                case Directions.LEFT:
                    dir = Directions.UP;
                    break;
            }
            return dir;
        }
    }
}
