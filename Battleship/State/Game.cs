using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Game
    {
        public int HitX { get; set; }
        public int HitY { get; set; }
        public IShipState State { get; set; }

        public Ship HitShip { get; set; }
        public void Hit(BattleshipBoard playerBoard, BattleshipBoard botBoard)
        {
            State.Hit(playerBoard, botBoard, this);
        }
        public Game(IShipState state)
        {
            State = state;
        }

        public bool IsOver(BattleshipBoard playerBoard)
        {
            int count = 0;
            for (int i = 0; i < playerBoard.ShipsList.Count; i++)
            {
                if (playerBoard.ShipsList[i].IsDestroyed()) count++;
            }
            return count == playerBoard.ShipsList.Count;
        }
    }
}
