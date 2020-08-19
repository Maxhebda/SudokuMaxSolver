﻿using System;
using System.Diagnostics;

namespace SudokuMaxSolver
{
    class Sudoku_AI
    {
        public enum difficultyLevel
        {   // number of ready digits
            Trywialna =45, BardzoLatwa=42, Latwa=39, Przecietna=36, DosycTrudna=33, Trudna=30, BardzoTrudna=27, Diaboliczna=23, Niemozliwa=19
        }

        byte[,] boardAI = new byte[9, 9];
        Random rand = new Random();

        public Sudoku_AI()
        {
            //generate9square();        //generate full good board  (basic board)
            generate9squareNewWay();    //generate full good board  (from 20 ready-made boards)
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
            byte deleteDigits =(byte)(81 - level);
            byte deleteDigitsCounter = 0;
            while(deleteDigitsCounter<deleteDigits)
            {
                byte x = (byte)rand.Next(0, 9);
                byte y = (byte)rand.Next(0, 9);
                if (boardAI[y,x]!=0)
                {
                    boardAI[y, x] = 0;
                    deleteDigitsCounter++;
                }
            }
        }
        private void generate9squareNewWay()    //generate full good board  (from 20 ready-made boards)
        {
            byte[,] tab =
            {
                {
                1,6,4,9,5,7,2,8,3,
                3,8,5,6,2,1,9,7,4,
                7,2,9,4,3,8,6,5,1,
                5,3,7,2,8,9,4,1,6,
                4,1,2,7,6,3,8,9,5,
                6,9,8,5,1,4,3,2,7,
                8,4,3,1,9,5,7,6,2,
                9,5,6,3,7,2,1,4,8,
                2,7,1,8,4,6,5,3,9
                },
                {
                8,2,9,4,3,1,6,7,5,
                5,4,6,9,7,2,3,1,8,
                1,7,3,8,5,6,9,2,4,
                3,6,1,7,2,8,4,5,9,
                7,9,8,1,4,5,2,3,6,
                4,5,2,6,9,3,1,8,7,
                2,1,4,5,8,9,7,6,3,
                9,3,5,2,6,7,8,4,1,
                6,8,7,3,1,4,5,9,2
                },
                {
                2,9,1,6,5,7,3,8,4,
                4,3,5,1,2,8,7,6,9,
                6,7,8,9,3,4,2,1,5,
                3,1,9,2,4,6,5,7,8,
                5,4,2,7,8,1,9,3,6,
                8,6,7,3,9,5,4,2,1,
                9,8,6,4,7,2,1,5,3,
                1,2,3,5,6,9,8,4,7,
                7,5,4,8,1,3,6,9,2
                },
                {
                7,2,4,8,6,3,9,1,5,
                5,3,9,2,1,4,8,7,6,
                8,1,6,9,7,5,2,3,4,
                4,5,1,3,2,7,6,8,9,
                3,8,2,1,9,6,5,4,7,
                6,9,7,5,4,8,3,2,1,
                2,6,5,4,8,1,7,9,3,
                9,4,3,7,5,2,1,6,8,
                1,7,8,6,3,9,4,5,2
                },
                {
                2,3,8,7,5,4,1,6,9,
                9,1,4,6,8,2,7,3,5,
                6,5,7,3,9,1,4,8,2,
                1,2,6,4,7,8,5,9,3,
                8,9,3,1,6,5,2,4,7,
                7,4,5,2,3,9,8,1,6,
                5,6,9,8,4,7,3,2,1,
                3,8,1,5,2,6,9,7,4,
                4,7,2,9,1,3,6,5,8
                },
                {
                9,3,8,6,1,2,4,5,7,
                2,7,5,9,3,4,6,1,8,
                4,1,6,5,7,8,2,9,3,
                3,6,4,7,5,9,8,2,1,
                7,9,1,2,8,6,3,4,5,
                8,5,2,1,4,3,7,6,9,
                1,4,7,3,6,5,9,8,2,
                6,2,3,8,9,1,5,7,4,
                5,8,9,4,2,7,1,3,6
                },
                {
                5,3,4,7,1,8,9,6,2,
                9,7,6,3,2,5,1,4,8,
                1,8,2,9,6,4,7,5,3,
                2,6,9,1,8,3,4,7,5,
                3,4,5,2,9,7,8,1,6,
                7,1,8,5,4,6,2,3,9,
                6,9,7,8,5,1,3,2,4,
                4,2,1,6,3,9,5,8,7,
                8,5,3,4,7,2,6,9,1
                },
                {
                4,8,1,5,7,2,3,9,6,
                9,7,3,4,8,6,1,2,5,
                5,6,2,9,3,1,8,4,7,
                6,5,9,2,1,7,4,8,3,
                3,4,8,6,9,5,2,7,1,
                1,2,7,3,4,8,5,6,9,
                2,9,6,8,5,3,7,1,4,
                7,3,4,1,2,9,6,5,8,
                8,1,5,7,6,4,9,3,2
                },

            };
            byte nrTab = (byte)rand.Next(0, tab.Length/81);
            Debug.WriteLine(tab.Length/81);
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    boardAI[y, x] = tab[nrTab,y*9+x];
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
