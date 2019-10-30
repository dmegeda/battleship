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
        private bool success = false;
        //Direction: 1 - Up, 3 - Down, 2 - Right, 4 - Left
        public void Hit(BattleshipBoard board, BattleshipBoard enemyBoard, Game game)
        {
            int x = game.HitX;
            int y = game.HitY;
            if(x > 0 && x < enemyBoard.MainBoard.GetLength(0) - 1)
            {
                if(y > 0 && y < enemyBoard.MainBoard.GetLength(0) - 1)
                {
                    ChangeCoordinates(ref y, ref x, dir);
                    if (enemyBoard.MainBoard[y, x] == 1)
                    {
                        
                        board.HitsBoard[y, x] = 2;
                        enemyBoard.MainBoard[y, x] = 2;
                        game.HitX = x;
                        game.HitY = y;
                    }
                    else
                    {
                        board.HitsBoard[y, x] = 3;
                        if (dir == Directions.LEFT)
                        {
                            game.State = new NoHitState();
                        }
                        else
                        {
                            dir = ChangeDir(dir);
                        }
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
