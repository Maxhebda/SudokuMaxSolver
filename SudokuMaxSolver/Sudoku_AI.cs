using System;
using System.Diagnostics;

namespace SudokuMaxSolver
{
    class Sudoku_AI
    {
        public enum difficultyLevel
        {
            Trywialna, BardzoLatwa, Latwa, Przecietna, DosycTrudna, Trudna, BardzoTrudna, Diaboliczna, Niemozliwa
        }
        byte[,] boardAI = new byte[9, 9];
        Random rand = new Random();

        public Sudoku_AI()
        {
            generate9square();
        }
        public void generateNewBoard(difficultyLevel level)
        {
            //generate full good board
            generateBigColumnAround(); 
            generateBigRowAround();
            generateSmallColumnAround();
            generateSmallRowAround();
            for (byte a = 0; a < 4; a++)
            {
                generateAroundNumbers();
            }
        }
        private void generate9square()      //generate full good board  (basic board)
        {
            byte reduceBy8(int a)
            {
                while (a > 9)
                {
                    a = a - 9;
                }
                return (byte)a;
            }
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    if (y < 3)
                    {
                        boardAI[y, x] = reduceBy8(1 + x + y * 3);
                    }
                    else
                        if (y < 6)
                    {
                        boardAI[y, x] = reduceBy8(2 + x + y * 3);
                    }
                    else
                    {
                        boardAI[y, x] = reduceBy8(3 + x + y * 3);
                    }
                }
        }
        private void generateSmallColumnAround()        //move the small columns around
        {
            byte nrOfColumn1;
            byte nrOfColumn2;
            for (byte i = 0; i < 3; i++)
            {
                nrOfColumn1 = (byte)rand.Next(i * 3, 3 + i * 3);        // column 0,1,2   3,4,5   6,7,8
                nrOfColumn2 = (byte)rand.Next(i * 3, 3 + i * 3);
                while (nrOfColumn1 == nrOfColumn2)
                {
                    nrOfColumn2 = (byte)rand.Next(i * 3, 3 + i * 3);
                }
                byte tmp;
                for (byte y = 0; y < 9; y++)
                {
                    tmp = boardAI[y, nrOfColumn1];
                    boardAI[y, nrOfColumn1] = boardAI[y, nrOfColumn2];
                    boardAI[y, nrOfColumn2] = tmp;
                }
            }
        }
        private void generateSmallRowAround()        //move the small row around
        {
            byte nrOfRow1;
            byte nrOfRow2;
            for (byte i = 0; i < 3; i++)
            {
                nrOfRow1 = (byte)rand.Next(i * 3, 3 + i * 3);        // row 0,1,2   3,4,5   6,7,8
                nrOfRow2 = (byte)rand.Next(i * 3, 3 + i * 3);
                while (nrOfRow1 == nrOfRow2)
                {
                    nrOfRow2 = (byte)rand.Next(i * 3, 3 + i * 3);
                }
                byte tmp;
                for (byte x = 0; x < 9; x++)
                {
                    tmp = boardAI[nrOfRow1, x];
                    boardAI[nrOfRow1, x] = boardAI[nrOfRow2, x];
                    boardAI[nrOfRow2, x] = tmp;
                }
            }
        }
        private void generateBigColumnAround()        //move the big columns around
        {
            byte nrOfColumngroup1;
            byte nrOfColumngroup2;

            nrOfColumngroup1 = (byte)rand.Next(0, 3);        // column group (0,1,2), (3,4,5), (6,7,8)
            nrOfColumngroup2 = (byte)rand.Next(0, 3);
            while (nrOfColumngroup1 == nrOfColumngroup2)
            {
                nrOfColumngroup2 = (byte)rand.Next(0, 3);
            }
            byte tmp;
            for (byte y = 0; y < 9; y++)
            {
                tmp = boardAI[y, nrOfColumngroup1 * 3];
                boardAI[y, nrOfColumngroup1 * 3] = boardAI[y, nrOfColumngroup2 * 3];
                boardAI[y, nrOfColumngroup2 * 3] = tmp;
                tmp = boardAI[y, nrOfColumngroup1 * 3+1];
                boardAI[y, nrOfColumngroup1 * 3+1] = boardAI[y, nrOfColumngroup2 * 3+1];
                boardAI[y, nrOfColumngroup2 * 3+1] = tmp;
                tmp = boardAI[y, nrOfColumngroup1 * 3+2];
                boardAI[y, nrOfColumngroup1 * 3+2] = boardAI[y, nrOfColumngroup2 * 3+2];
                boardAI[y, nrOfColumngroup2 * 3+2] = tmp;
            }
        }
        private void generateBigRowAround()        //move the big row around
        {
            byte nrOfRowGroup1;
            byte nrOfRowGroup2;

            nrOfRowGroup1 = (byte)rand.Next(0, 3);        // row group (0,1,2), (3,4,5), (6,7,8)
            nrOfRowGroup2 = (byte)rand.Next(0, 3);
            while (nrOfRowGroup1 == nrOfRowGroup2)
            {
                nrOfRowGroup2 = (byte)rand.Next(0, 3);
            }
            byte tmp;
            for (byte x = 0; x < 9; x++)
            {
                tmp = boardAI[nrOfRowGroup1 * 3, x];
                boardAI[nrOfRowGroup1 * 3, x] = boardAI[nrOfRowGroup2 * 3, x];
                boardAI[nrOfRowGroup2 * 3, x] = tmp;
                tmp = boardAI[nrOfRowGroup1 * 3 + 1, x];
                boardAI[nrOfRowGroup1 * 3 + 1, x] = boardAI[nrOfRowGroup2 * 3 + 1, x];
                boardAI[nrOfRowGroup2 * 3 + 1, x] = tmp;
                tmp = boardAI[nrOfRowGroup1 * 3 + 2, x];
                boardAI[nrOfRowGroup1 * 3 + 2, x] = boardAI[nrOfRowGroup2 * 3 + 2, x];
                boardAI[nrOfRowGroup2 * 3 + 2, x] = tmp;
            }
        }
        private void generateAroundNumbers()        //around two random numbers
        {
            byte nr1;
            byte nr2;

            nr1 = (byte)rand.Next(1, 10);        // random number 1..9
            nr2 = (byte)rand.Next(1, 10);
            while (nr1 == nr2)
            {
                nr2 = (byte)rand.Next(1, 10);
            }
            for (byte y = 0; y < 9; y++)
            {
                for (byte x = 0; x < 9; x++)
                {
                    if (boardAI[y, x] == nr1)
                    {
                        boardAI[y, x] = nr2;
                    }
                    else if (boardAI[y, x] == nr2)
                    {
                        boardAI[y, x] = nr1;
                    }
                }
            }
        }
        public byte get(byte y, byte x)
        {
            return boardAI[y, x];
        }
    }
}
