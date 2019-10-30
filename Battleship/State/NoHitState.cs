using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class NoHitState : IShipState
    {

        public void Hit(BattleshipBoard playerBoard, BattleshipBoard enemyBoard, Game game)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, playerBoard.BoardSize);
            int y = rnd.Next(0, playerBoard.BoardSize);
            if (playerBoard.HitsBoard[y, x] != 2 || playerBoard.HitsBoard[y, x] != 3)
            {
                if (enemyBoard.MainBoard[y, x] == 1)
                {
                    playerBoard.HitsBoard[y, x] = 2;
                    enemyBoard.MainBoard[y, x] = 2;
                    game.HitX = x;
                    game.HitY = y;
                    
                    game.State = new HitState();
                    //Success hit
                }
                else
                {
                    playerBoard.HitsBoard[y, x] = 3;
                    enemyBoard.MainBoard[y, x] = 3;
                }
            }
        }
    }
}
