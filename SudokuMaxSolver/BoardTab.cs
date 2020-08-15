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
        byte[,] board = new byte[9, 9];
        public BoardTab()
        {
            clear();
        }
        public void clear()
        {
            Random rand = new Random();
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                    //        board[y, x] = 0;
                    board[y, x] = (byte)rand.Next(0, 10);
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
        public List<byte> valuesInRow(byte row)     //all values in the row (+1 overload)
        {
            List<byte> list = new List<byte>();
            byte tmp;
            for (byte x = 0; x < 9; x++)
            {
                tmp = board[row, x];
                if (tmp != 0)
                {
                    list.Add(tmp);
                }
            }
            return list;
        }
        public List<byte> valuesInRow(byte y, byte x)     //all values in the row
        {
            return valuesInRow(y);
        }
        public List<byte> valuesInColumn(byte column)     //all values in the column (+1 overload)
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
        public List<byte> valuesInColumn(byte y, byte x)  //all values in the column
        {
            return valuesInColumn(x);
        }
        public List<byte> valuesInSquare(byte square)     //all values in the square 1..9 (+1 overload)
        {
            List<byte> list = new List<byte>();
            byte tmp;
            switch (square)
            {
                case 1:
                    for (byte y = 0; y < 3; y++)
                        for (byte x = 0; x < 3; x++)
                        {
                            tmp = board[y, x];
                            if (tmp != 0)
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
        public List<byte> valuesInSquare(byte y, byte x)     //all values in the square 1..9
        {
            if (y < 3 && x < 3) return valuesInSquare(1);
            if (y < 3 && x > 2 && x < 6) return valuesInSquare(2);
            if (y < 3 && x > 5) return valuesInSquare(3);

            if (y > 2 && y < 6 && x < 3) return valuesInSquare(4);
            if (y > 2 && y < 6 && x > 2 && x < 6) return valuesInSquare(5);
            if (y > 2 && y < 6 && x > 5) return valuesInSquare(6);

            if (y > 5 && x < 3) return valuesInSquare(7);
            if (y > 5 && x > 2 && x < 6) return valuesInSquare(8);
            return valuesInSquare(9);                
        }

        public bool isInColumn(byte column, byte nr)
        {
            for (byte x = 0; x < 9; x++)
            {
                if (board[column, x] == nr)
                    return true;
            }
            return false;
        }
        public bool isInRow(byte row, byte nr)
        {
            for (byte y = 0; y < 9; y++)
            {
                if (board[y, row] == nr)
                    return true;
            }
            return false;
        }

    }
}
