using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public List<byte> valuesInRow(byte row)     //all values in the row
        {
            List<byte> list = new List<byte>();
            byte tmp;
            for (byte x = 0; x<9; x++)
            {
                tmp = board[row, x];
                if (tmp!=0)
                {
                    list.Add(tmp);
                }
            }
            return list;
        }
        public List<byte> valuesInColumn(byte column)     //all values in the column
        {
            List<byte> list = new List<byte>();
            byte tmp;
            for (byte y = 0; y < 9; y++)
            {
                tmp = board[y, column];
                if (tmp != 0)
                {
                    list.Add(tmp);
                }
            }
            return list;
        }
        public List<byte> valuesInSquare(byte square)     //all values in the square 1..9
        {
            List<byte> list = new List<byte>();
            byte tmp;
            switch(square)
            {
                case 1:
                    for (byte y=0 ; y<3 ; y++)
                        for (byte x=0 ; x<3 ; x++)
                        {
                            tmp = board[y, x];
                            if (tmp!=0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 2:
                    for (byte y = 0; y < 3; y++)
                        for (byte x = 3; x < 6; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 3:
                    for (byte y = 0; y < 3; y++)
                        for (byte x = 6; x < 9; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 4:
                    for (byte y = 3; y < 6; y++)
                        for (byte x = 0; x < 3; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 5:
                    for (byte y = 3; y < 6; y++)
                        for (byte x = 3; x < 6; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 6:
                    for (byte y = 3; y < 6; y++)
                        for (byte x = 6; x < 9; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 7:
                    for (byte y = 6; y < 9; y++)
                        for (byte x = 0; x < 3; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 8:
                    for (byte y = 6; y < 9; y++)
                        for (byte x = 3; x < 6; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
                case 9:
                    for (byte y = 6; y < 9; y++)
                        for (byte x = 6; x < 9; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
                            {
                                list.Add(tmp);
                            }
                        }
                    break;
            }
            return list;
        }
    }
}
