using System.Collections.Generic;

namespace SudokuMaxSolver
{
    class BoardTab : FakeCandidatesBoard
    {
        struct boardBody
        {
            public byte value;
            public bool readOnly;
        }
        boardBody[,] board = new boardBody[9, 9];

        public BoardTab()
        {
            clear();
        }
        public BoardTab(BoardTab boardOld)  //create copy
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = boardOld.get(y, x);
                    board[y, x].readOnly = boardOld.getReadOnly(y, x);
                }
        }
        public BoardTab(byte[,] boardAI)  //create copy at BoardAI 
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = boardAI[y,x];
                    board[y, x].readOnly = false;
                }
        }
        public void clear()
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = 0;
                    board[y, x].readOnly = false;
                }
            ClearFakeCandidateBoard();
        }
        public void set(byte y, byte x, byte value)
        {
            if (x > 9 || y > 9) return;
            board[y, x].value = value;
        }
        public byte get(byte y, byte x)
        {
            return board[y, x].value;
        }
        public bool getReadOnly(byte y, byte x)
        {
            return board[y, x].readOnly;
        }
        public void setReadOnly(byte y, byte x, bool value)
        {
            board[y, x].readOnly = value;
        }
        public List<byte> valuesInRow(byte row)     //all values in the row (+1 overload)
        {
            List<byte> list = new List<byte>();
            byte tmp;
            for (byte x = 0; x < 9; x++)
            {
                tmp = board[row, x].value;
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
                tmp = board[y, column].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
                            tmp = board[y, x].value;
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
        public List<byte> allCandidates(byte y, byte x)     //all possible candidates who can be entered in the cell
        {
            List<byte> tmp = new List<byte>();

            // if the cell is not empty it will return null
            if (board[y,x].value!=0)
            {
                return tmp;
            }

            //searching for candidates in cell
            for (byte value = 1; value <= 9; value++)
            {
                if (!isInColumn(y,x,value) && !isInRow(y,x,value) && !isInSquare(y,x,value) && !ExistsFakeCandidate(y,x,value))
                {
                    tmp.Add(value);
                }
            }
            return tmp;
        }

        public bool isInColumn(byte column, byte nr)        //check if the number is in the column (+1 overload)
        {
            for (byte y = 0; y < 9; y++)
            {
                if (board[y, column].value == nr)
                    return true;
            }
            return false;
        }
        public bool isInColumn(byte y, byte x, byte nr)     //check if the number is in the column
        {
            return isInColumn(x, nr);
        }
        public bool isInRow(byte row, byte nr)              //check if the number is in the row (+1 overload)
        {
            for (byte x = 0; x < 9; x++)
            {
                if (board[row, x].value == nr)
                    return true;
            }
            return false;
        }
        public bool isInRow(byte y, byte x, byte nr)        //check if the number is in the row
        {
            return isInRow(y, nr);
        }
        public bool isInSquare(byte square, byte nr)        //check if the number is in the square (+1 overload)
        {
            List<byte> l = valuesInSquare(square);
            for (byte a = 0; a < l.Count; a++)
            {
                if (l[a] == nr)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isInSquare(byte y, byte x, byte nr)     //check if the number is in the square
        {
            List<byte> l = valuesInSquare(y, x);
            for (byte a = 0; a < l.Count; a++)
            {
                if (l[a] == nr)
                {
                    return true;
                }
            }
            return false;
        }
        public void load(Sudoku_AI sudoku)
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = sudoku.get(y, x);
                    if (board[y, x].value != 0)
                    {
                        board[y, x].readOnly = true;
                    }
                    else
                    {
                        board[y, x].readOnly = false;
                    }
                }
        }

        public void load(BoardTab oldBoard)
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = oldBoard.get(y, x);
                    board[y, x].readOnly = oldBoard.getReadOnly(y, x);
                }
        }
    }
}
