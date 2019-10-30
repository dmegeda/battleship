using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class BattleshipBoard
    {
        private int boardsize;
        public int BoardSize
        {
            get { return boardsize; }
            set
            {
                if(value > 9)
                {
                    boardsize = value;
                }
            }
        }
        private int[,] mainBoard;
        public int[,] MainBoard
        {
            get { return mainBoard; }
            set { mainBoard = value; }
        }
        private int[,] hitsBoard;
        public int[,] HitsBoard
        {
            get { return hitsBoard; }
            set { hitsBoard = value; }
        }

        public List<Ship> Ships { get; set; }
        public BattleshipBoard(int boardsize)
        {
            BoardSize = boardsize;
            MainBoard = new int[boardsize, boardsize];
            HitsBoard = new int[boardsize, boardsize];
            for (int i = 0; i < boardsize; i++)
            {
                MainBoard[i, i] = 0;
                HitsBoard[i, i] = 0;
            }
            //shipSetter.AddShipToBoard(this, );
        }
    }
}
