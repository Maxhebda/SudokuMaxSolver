using System.Collections.Generic;
using System.Windows.Automation.Peers;

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

        //  square number square number based on the coordinates
        public static byte squareNumber(byte y, byte x)
        {
            if (y < 3 && x < 3) return 1;
            if (y < 3 && x > 2 && x < 6) return 2;
            if (y < 3 && x > 5) return 3;

            if (y > 2 && y < 6 && x < 3) return 4;
            if (y > 2 && y < 6 && x > 2 && x < 6) return 5;
            if (y > 2 && y < 6 && x > 5) return 6;

            if (y > 5 && x < 3) return 7;
            if (y > 5 && x > 2 && x < 6) return 8;
            return 9;
        }

        //  list of nine cells in a square (only coordinates)
        public static List<Candidate> cellsInASquare(byte squareNumber)
        {
            List<Candidate> tmp = new List<Candidate>();
            if (squareNumber < 1 && squareNumber > 9)
            {
                return tmp; //return empty list;
            }

            byte y1Square = 0;
            byte y2Square = 0;
            byte x1Square = 0;
            byte x2Square = 0;
            switch (squareNumber)
            {
                case 1:
                    y1Square = 0;
                    y2Square = 3;
                    x1Square = 0;
                    x2Square = 3;
                    break;
                case 2:
                    y1Square = 0;
                    y2Square = 3;
                    x1Square = 3;
                    x2Square = 6;
                    break;
                case 3:
                    y1Square = 0;
                    y2Square = 3;
                    x1Square = 6;
                    x2Square = 9;
                    break;
                case 4:
                    y1Square = 3;
                    y2Square = 6;
                    x1Square = 0;
                    x2Square = 3;
                    break;
                case 5:
                    y1Square = 3;
                    y2Square = 6;
                    x1Square = 3;
                    x2Square = 6;
                    break;
                case 6:
                    y1Square = 3;
                    y2Square = 6;
                    x1Square = 6;
                    x2Square = 9;
                    break;
                case 7:
                    y1Square = 6;
                    y2Square = 9;
                    x1Square = 0;
                    x2Square = 3;
                    break;
                case 8:
                    y1Square = 6;
                    y2Square = 9;
                    x1Square = 3;
                    x2Square = 6;
                    break;
                case 9:
                    y1Square = 6;
                    y2Square = 9;
                    x1Square = 6;
                    x2Square = 9;
                    break;
            }
            for (byte y = y1Square; y < y2Square; y++)
                for (byte x = x1Square; x < x2Square; x++)
                {
                    tmp.Add(new Candidate(y, x, 0));
                }
            return tmp;
        }

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

                    //copy fake candidate
                    fakeCandidateBoard[y, x] = boardOld.fakeCandidateBoard[y, x];
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
            return valuesInSquare(squareNumber( y , x ));
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

        public bool isACandidate(byte y, byte x, byte candidateChecked)   //is In The Set Of Candidates
        {
            foreach (var candidate in allCandidates(y,x))
            {
                if (candidate == candidateChecked)
                {
                    return true;
                }
            }
            return false;
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
            clear();
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
            clear();
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    board[y, x].value = oldBoard.get(y, x);
                    board[y, x].readOnly = oldBoard.getReadOnly(y, x);
                }
        }
    }
}
