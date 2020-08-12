using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaxSolver
{
    class BoardTab
    {
        byte [,] board = new byte[9,9];
        public BoardTab()
        {
            clear();
        }
        public void clear()
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                    board[y, x] = 0;
        }
        public void set(byte y, byte x, byte value)
        {
            if (x > 9 || y > 9) return;
            board[y, x] = value;
        }
        public byte get(byte y, byte x)
        {
            return board[y, x];
        }
    }
}
