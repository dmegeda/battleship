using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Game
    {
        private int hitX;
        private int hitY;

        public int HitX
        {
            get { return hitX; }
            set { hitX = value; }
        }
        public int HitY
        {
            get { return hitY; }
            set { hitY = value; }
        }
        public IShipState State { get; set; }

        public void Hit(BattleshipBoard playerBoard, BattleshipBoard botBoard)
        {
            State.Hit(playerBoard, botBoard, this);
        }
        public Game(IShipState state)
        {
            State = state;
        }
    }
}
