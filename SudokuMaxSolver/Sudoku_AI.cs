using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SudokuMaxSolver
{
    class Sudoku_AI
    {
        public enum difficultyLevel
        {   // number of ready digits
            Trywialna =48, BardzoLatwa=45, Latwa=42, Przecietna=39, DosycTrudna=36, Trudna=33, BardzoTrudna=30, Diaboliczna=28, Niemozliwa=26
            // trivial, very easy, easy, average, quite difficult, difficult, very difficult, diabolical, impossible
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
            byte oldDigit;
            int counterTrying=0;
            while(deleteDigitsCounter<deleteDigits)
            {
                byte x = (byte)rand.Next(0, 9);
                byte y = (byte)rand.Next(0, 9);
                if (boardAI[y,x]!=0)
                {
                    //removing digits and checking if sudoku has only one solution
                    oldDigit = boardAI[y, x];
                    boardAI[y, x] = 0;

                    //checking if sudoku has only one solution
                    if (Sudoku_AI.autoSolver_numberOfSolutions(new BoardTab(boardAI))!=1)
                    {
                        boardAI[y, x] = oldDigit;
                        counterTrying++;

                        //if looking for a long time, stop!
                        if (counterTrying>50)
                        {
                            //Debug.WriteLine("abortet deleting digits!");
                            return;
                        }
                    }
                    else
                    {
                        counterTrying = 0;
                        deleteDigitsCounter++;
                    }
                }
            }
        }
        private void generate9squareNewWay()    //generate full good board  (from 20 ready-made boards)
        {
            byte[,] tab =   // 20 board
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
                {
                6,5,3,4,7,8,2,9,1,
                1,2,7,6,9,3,5,4,8,
                9,4,8,1,2,5,6,3,7,
                2,3,9,5,6,1,8,7,4,
                4,8,5,9,3,7,1,2,6,
                7,1,6,8,4,2,3,5,9,
                8,9,2,3,1,4,7,6,5,
                5,7,4,2,8,6,9,1,3,
                3,6,1,7,5,9,4,8,2
                },
                {
                3,2,8,7,4,1,6,9,5,
                9,5,7,6,3,2,4,1,8,
                1,4,6,8,9,5,2,7,3,
                7,1,2,9,8,4,5,3,6,
                6,9,5,3,2,7,8,4,1,
                4,8,3,1,5,6,9,2,7,
                8,7,1,4,6,9,3,5,2,
                5,3,4,2,7,8,1,6,9,
                2,6,9,5,1,3,7,8,4
                },
                {
                3,7,1,9,2,5,8,4,6,
                6,2,8,3,7,4,1,5,9,
                5,9,4,6,8,1,2,3,7,
                2,4,3,1,9,8,7,6,5,
                7,1,5,2,6,3,9,8,4,
                8,6,9,5,4,7,3,1,2,
                4,5,7,8,1,9,6,2,3,
                1,3,2,7,5,6,4,9,8,
                9,8,6,4,3,2,5,7,1
                },
                {
                9,2,4,3,5,6,1,7,8,
                7,5,1,4,8,2,6,9,3,
                3,8,6,9,1,7,4,5,2,
                8,3,9,7,2,1,5,4,6,
                4,6,2,5,9,3,8,1,7,
                1,7,5,6,4,8,2,3,9,
                6,1,7,8,3,4,9,2,5,
                5,4,3,2,6,9,7,8,1,
                2,9,8,1,7,5,3,6,4
                },
                {
                9,8,2,5,4,3,7,1,6,
                1,5,6,8,7,9,4,3,2,
                7,4,3,6,1,2,9,8,5,
                8,2,4,7,9,5,3,6,1,
                6,3,1,2,8,4,5,9,7,
                5,9,7,1,3,6,2,4,8,
                3,7,5,9,6,1,8,2,4,
                4,1,8,3,2,7,6,5,9,
                2,6,9,4,5,8,1,7,3
                },
                {
                7,1,4,8,2,3,6,5,9,
                9,2,6,7,5,4,1,3,8,
                3,8,5,6,9,1,2,7,4,
                1,5,9,2,8,6,7,4,3,
                6,3,7,1,4,9,5,8,2,
                8,4,2,5,3,7,9,1,6,
                5,9,3,4,1,2,8,6,7,
                2,7,8,3,6,5,4,9,1,
                4,6,1,9,7,8,3,2,5
                },
                {
                4,3,5,1,6,9,7,2,8,
                1,9,7,5,2,8,4,3,6,
                8,2,6,4,3,7,9,1,5,
                2,5,8,6,9,3,1,7,4,
                3,7,1,8,5,4,2,6,9,
                6,4,9,7,1,2,8,5,3,
                7,6,3,9,4,1,5,8,2,
                5,8,4,2,7,6,3,9,1,
                9,1,2,3,8,5,6,4,7
                },
                {
                2,8,5,9,6,1,7,4,3,
                3,7,1,8,5,4,2,6,9,
                9,4,6,7,3,2,8,1,5,
                4,2,3,6,9,8,1,5,7,
                1,9,7,3,2,5,6,8,4,
                6,5,8,1,4,7,9,3,2,
                5,1,9,4,7,6,3,2,8,
                8,3,2,5,1,9,4,7,6,
                7,6,4,2,8,3,5,9,1
                },
                {
                5,6,1,4,9,7,8,3,2,
                8,3,2,6,5,1,4,7,9,
                7,9,4,8,3,2,5,1,6,
                6,1,8,5,4,9,7,2,3,
                4,7,9,1,2,3,6,5,8,
                2,5,3,7,6,8,1,9,4,
                3,4,7,2,8,5,9,6,1,
                1,2,6,9,7,4,3,8,5,
                9,8,5,3,1,6,2,4,7
                },
                {
                5,3,9,2,8,1,7,6,4,
                1,7,4,5,9,6,3,2,8,
                2,6,8,4,7,3,5,9,1,
                8,5,1,6,4,7,9,3,2,
                4,2,6,3,5,9,8,1,7,
                3,9,7,1,2,8,4,5,6,
                7,8,3,9,6,2,1,4,5,
                9,4,2,8,1,5,6,7,3,
                6,1,5,7,3,4,2,8,9
                },
                {
                5,1,2,4,6,8,3,9,7,
                9,6,3,1,2,7,5,8,4,
                7,8,4,5,3,9,2,6,1,
                2,9,6,7,8,5,1,4,3,
                4,5,7,3,9,1,8,2,6,
                8,3,1,2,4,6,7,5,9,
                3,4,8,6,1,2,9,7,5,
                6,7,9,8,5,3,4,1,2,
                1,2,5,9,7,4,6,3,8
                },
                {
                1,8,9,5,6,4,3,7,2,
                3,5,6,7,2,1,9,4,8,
                2,7,4,9,3,8,6,1,5,
                4,6,3,1,8,9,5,2,7,
                5,9,8,2,7,6,1,3,4,
                7,1,2,3,4,5,8,9,6,
                9,4,5,8,1,2,7,6,3,
                6,3,1,4,5,7,2,8,9,
                8,2,7,6,9,3,4,5,1
                }
            };
            byte nrTab = (byte)rand.Next(0, tab.Length/81);

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
        public void analyzerLoad(BoardTab tab)
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    boardAI[y, x] = tab.get(y, x);
                }
        }
        static private bool goNextCell(ref byte y, ref byte x)
        {
            if (x < 8)
            {
                x++;
            }
            else
            {
                if (y < 8)
                {
                    y++;
                    x = 0;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        static public bool autoSolver_brutal(ref BoardTab boardOriginal)
        {
            //create copy board
            BoardTab boardCopy = new BoardTab(boardOriginal);

            //a reference function that checks all possible possibilities
            bool findGoodCell(byte yStart, byte xStart, byte NrStart, BoardTab board)
            {
                if (board.get(yStart,xStart)==0)
                { 
                    // the cell is empty, we can enter the value and check

                    // we check if we can enter a value
                    if (board.isInColumn(yStart, xStart, NrStart) || board.isInRow(yStart, xStart, NrStart) || board.isInSquare(yStart, xStart, NrStart))
                    {
                        // value cannot be entered! we increase the value
                        if (NrStart < 9)
                        {
                            // we increase the value and check
                            NrStart++;
                            return findGoodCell(yStart, xStart, NrStart, board);
                        }
                        else
                        {
                            // Debug.WriteLine("(" + yStart + "," + xStart + "->x)");
                            // you can no longer increase the value. Error!
                            return false;
                        }
                    }
                    else
                    {
                        board.set(yStart, xStart, NrStart);
                        // after entering, I check if the board is correct
                        if (findGoodCell(yStart, xStart, NrStart, board) == false)
                        {

                            // destroyed the boards! we increase the value, clear value in table
                            board.set(yStart, xStart, 0);
                            if (NrStart < 9)
                            {
                                NrStart++;
                                return findGoodCell(yStart, xStart, NrStart, board);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else    
                        {

                            // did not destroy! we move on to the next cell. we start with 1
                            if (goNextCell(ref yStart, ref xStart))
                            {
                                return findGoodCell(yStart, xStart, 1, board);
                            }
                            else
                            {
                                // we are at the end of the board + we managed to enter the value = true
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // the cell is full
                    if (goNextCell(ref yStart, ref xStart))
                    {
                        // we move on to the next cell. we start with 1
                        return findGoodCell(yStart, xStart, 1, board);
                    }
                    else
                    {
                        // end board
                        return true;
                    }
                }
            }

            //start position 0,0 and number 1
            if (findGoodCell(0, 0, 1, boardCopy))
            {
                // solved correctly. we replace boards
                boardOriginal = new BoardTab(boardCopy);
                return true;
            }
            else
            {
                return false;
            }
        }

        //solves sudoku, the function returns the number of solutions. 0 - none, 1 - one, 2 - more than one.
        static public byte autoSolver_numberOfSolutions(BoardTab boardCopy)
        {
            byte numberOfSolution = 0;

            //a reference function that checks all possible possibilities
            bool findGoodCell(byte yStart, byte xStart, byte NrStart, BoardTab board)
            {
                if (board.get(yStart, xStart) == 0)
                {
                    //the cell is empty, we can enter the value and check

                    //we check if we can enter a value
                    if (board.isInColumn(yStart, xStart, NrStart) || board.isInRow(yStart, xStart, NrStart) || board.isInSquare(yStart, xStart, NrStart))
                    {
                        // value cannot be entered! we increase the value
                        if (NrStart < 9)
                        {
                            // we increase the value and check 
                            NrStart++;
                            return findGoodCell(yStart, xStart, NrStart, board);
                        }
                        else
                        {
                            //Debug.WriteLine("(" + yStart + "," + xStart + "->x)");
                            // you can no longer increase the value. Error!
                            return false;
                        }
                    }
                    else
                    {
                        board.set(yStart, xStart, NrStart);
                        // after entering, I check if the board is correct
                        if (findGoodCell(yStart, xStart, NrStart, board) == false)
                        {

                            // destroyed the boards! we increase the value, clear value in table
                            board.set(yStart, xStart, 0);
                            if (NrStart < 9)
                            {
                                NrStart++;
                                return findGoodCell(yStart, xStart, NrStart, board);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else  
                        {

                            // did not destroy! we move on to the next cell. we start with 1
                            if (goNextCell(ref yStart, ref xStart))
                            {
                                return findGoodCell(yStart, xStart, 1, board);
                            }
                            else
                            {
                                // GOOD
                                //-------
                                if (numberOfSolution == 0)
                                {
                                    numberOfSolution=1;
                                    // we return the first solution to false to keep looking
                                    return false;
                                }
                                else
                                {
                                    if (numberOfSolution==1)
                                    {
                                        numberOfSolution=2;
                                    }
                                    // if we find the second solution, we exit the function = set true
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // the cell is full
                    if (goNextCell(ref yStart, ref xStart))
                    {
                        // we move on to the next cell. we start with 1
                        return findGoodCell(yStart, xStart, 1, board);
                    }
                    else
                    {
                        // GOOD
                        //-------
                        if (numberOfSolution == 0)
                        {
                            numberOfSolution = 1;
                            // we return the first solution to false to keep looking
                            return false;
                        }
                        else
                        {
                            if (numberOfSolution == 1)
                            {
                                numberOfSolution = 2;
                            }
                            // if we find the second solution, we exit the function = set true
                            return true;
                        }
                    }
                }
            }

            //start position 0,0 and number 1
            findGoodCell(0, 0, 1, boardCopy);
            return numberOfSolution;
        }
        public static SolutionInformation ManualSolver01_TheOnlyPossible(ref BoardTab board, byte[] listSquareToCheck = null, BoardTab imaginaryBoard = null)
        {
            if (listSquareToCheck == null)
            {
                listSquareToCheck = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }

            //if the imaginary board is active, we will read from the imaginaryBoard one and write it to the oryginal board
            bool imaginaryBoardisActive;
            if (imaginaryBoard == null)
            {
                imaginaryBoardisActive = false;
            }
            else
            {
                imaginaryBoardisActive = true;
            }

            SolutionInformation tmp = new SolutionInformation();
            byte counterPossible;
            byte findValue = 0;
            byte ystart=0, ystop=0, xstart=0, xstop=0;
            foreach (byte square in listSquareToCheck)
            {
                switch (square)
                {
                    case 1:
                        ystart = 0; ystop = 3; xstart = 0; xstop = 3; break;
                    case 2:
                        ystart = 0; ystop = 3; xstart = 3; xstop = 6; break;
                    case 3:
                        ystart = 0; ystop = 3; xstart = 6; xstop = 9; break;
                    case 4:
                        ystart = 3; ystop = 6; xstart = 0; xstop = 3; break;
                    case 5:
                        ystart = 3; ystop = 6; xstart = 3; xstop = 6; break;
                    case 6:
                        ystart = 3; ystop = 6; xstart = 6; xstop = 9; break;
                    case 7:
                        ystart = 6; ystop = 9; xstart = 0; xstop = 3; break;
                    case 8:
                        ystart = 6; ystop = 9; xstart = 3; xstop = 6; break;
                    case 9:
                        ystart = 6; ystop = 9; xstart = 6; xstop = 9; break;
                }

                for (byte y = ystart; y < ystop; y++)
                {
                    for (byte x = xstart; x < xstop; x++)
                    {
                        counterPossible = 0;
                        for (byte value = 1; value <= 9; value++)
                        {
                            if (imaginaryBoardisActive)
                            {
                                if (imaginaryBoard.get(y, x) == 0 && !imaginaryBoard.isInColumn(y, x, value) && !imaginaryBoard.isInRow(y, x, value) && !imaginaryBoard.isInSquare(y, x, value))
                                {
                                    findValue = value;
                                    counterPossible++;
                                    if (counterPossible > 1)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (board.get(y, x) == 0 && !board.isInColumn(y, x, value) && !board.isInRow(y, x, value) && !board.isInSquare(y, x, value))
                                {
                                    findValue = value;
                                    counterPossible++;
                                    if (counterPossible > 1)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        // found the only possible
                        if (counterPossible == 1)
                        {
                            board.set(y, x, findValue);
                            tmp.Add("Znaleziono jedynego możliwego kandydata", y, x, findValue);
                        }
                    }
                }
            }
            return tmp;
        }
        public static SolutionInformation ManualSolver02_SingleCandidateInRow(ref BoardTab board, byte[] listRowToCheck = null, BoardTab imaginaryBoard = null)
        {
            if (listRowToCheck==null)
            {
                listRowToCheck = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            }

            //if the imaginary board is active, we will read from the imaginaryBoard one and write it to the oryginal board
            bool imaginaryBoardisActive;
            if (imaginaryBoard == null)
            {
                imaginaryBoardisActive = false;
            }
            else
            {
                imaginaryBoardisActive = true;
            }

            SolutionInformation tmp = new SolutionInformation();
            List<Candidate> candidates = new List<Candidate>();
            byte[] counterCandidate = new byte[9];
            byte singleCandidate = 0;
            byte row;
            for (byte lrow = 0; lrow < listRowToCheck.Length; lrow++)
            {
                row = listRowToCheck[lrow];

                //clear candodates list
                candidates.Clear();

                //searching for candidates
                for (byte x = 0; x < 9; x++)
                {
                    foreach (byte c in (imaginaryBoardisActive?imaginaryBoard.allCandidates(row, x): board.allCandidates(row, x)))
                    {
                        candidates.Add(new Candidate(row, x, c));
                    }
                }

                //clear counter candidate
                for (byte i = 0; i < 9; i++)
                {
                    counterCandidate[i] = 0;
                }

                //clear singleCandidate
                singleCandidate = 0;

                //summing up all candidates
                foreach (Candidate candidate in candidates)
                {
                    counterCandidate[candidate.Value - 1]++;
                }

                //checking if there is a single candidate
                for (byte i = 0; i < 9; i++)
                {
                    if (counterCandidate[i]==1)
                    {
                        singleCandidate = (byte)(i + 1);
                        break;
                    }
                }

                //finding and entering a candidate
                if (singleCandidate!=0)
                {
                    foreach (Candidate candidate in candidates)
                    {
                        if (candidate.Value==singleCandidate)
                        {
                            board.set(candidate.Y, candidate.X, singleCandidate);
                            tmp.Add("Znaleziono pojedynczego kandydata we wierszu", candidate.Y, candidate.X, singleCandidate);
                            break;
                        }
                    }
                }

            }
            return tmp;
        }
        public static SolutionInformation ManualSolver03_SingleCandidateInColumn(ref BoardTab board, byte[] listColumnToCheck = null, BoardTab imaginaryBoard = null)
        {
            if (listColumnToCheck == null)
            {
                listColumnToCheck = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            }

            //if the imaginary board is active, we will read from the imaginaryBoard one and write it to the oryginal board
            bool imaginaryBoardisActive;
            if (imaginaryBoard == null)
            {
                imaginaryBoardisActive = false;
            }
            else
            {
                imaginaryBoardisActive = true;
            }

            SolutionInformation tmp = new SolutionInformation();
            List<Candidate> candidates = new List<Candidate>();
            byte[] counterCandidate = new byte[9];
            byte singleCandidate = 0;
            byte column;
            for (byte lcol = 0; lcol < listColumnToCheck.Length; lcol++)
            {
                column = listColumnToCheck[lcol];

                //clear candodates list
                candidates.Clear();

                //searching for candidates
                for (byte y = 0; y < 9; y++)
                {
                    foreach (byte c in (imaginaryBoardisActive?imaginaryBoard.allCandidates(y, column): board.allCandidates(y, column)))
                    {
                        candidates.Add(new Candidate(y, column, c));
                    }
                }

                //clear counter candidate
                for (byte i = 0; i < 9; i++)
                {
                    counterCandidate[i] = 0;
                }

                //clear singleCandidate
                singleCandidate = 0;

                //summing up all candidates
                foreach (Candidate candidate in candidates)
                {
                    counterCandidate[candidate.Value - 1]++;
                }

                //checking if there is a single candidate
                for (byte i = 0; i < 9; i++)
                {
                    if (counterCandidate[i] == 1)
                    {
                        singleCandidate = (byte)(i + 1);
                        break;
                    }
                }

                //finding and entering a candidate
                if (singleCandidate != 0)
                {
                    foreach (Candidate candidate in candidates)
                    {
                        if (candidate.Value == singleCandidate)
                        {
                            board.set(candidate.Y, candidate.X, singleCandidate);
                            tmp.Add("Znaleziono pojedynczego kandydata w kolumnie", candidate.Y, candidate.X, singleCandidate);
                            break;
                        }
                    }
                }

            }
            return tmp;
        }
        public static SolutionInformation ManualSolver04_SingleCandidateInSquare(ref BoardTab board, byte[] listSquareToCheck = null, BoardTab imaginaryBoard = null)
        {
            if (listSquareToCheck == null)
            {
                listSquareToCheck = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }

            //if the imaginary board is active, we will read from the imaginaryBoard one and write it to the oryginal board
            bool imaginaryBoardisActive;
            if (imaginaryBoard == null)
            {
                imaginaryBoardisActive = false;
            }
            else
            {
                imaginaryBoardisActive = true;
            }

            SolutionInformation tmp = new SolutionInformation();
            byte[] counterCandidate = new byte[9];
            byte singleCandidate;
            byte square;
            byte ystart = 0, ystop = 0, xstart = 0, xstop = 0;
            for (byte lsqu = 0; lsqu < listSquareToCheck.Length; lsqu++)
            {
                square = listSquareToCheck[lsqu];

                //clear Candidate table
                for (byte i = 0; i < 9; i++)
                {
                    counterCandidate[i] = 0;
                }

                //clear singleCandidate
                singleCandidate = 0;

                //summing all candidates in a square
                switch (square)
                {
                    case 1:
                        ystart = 0; ystop = 3; xstart = 0; xstop = 3; break;
                    case 2:
                        ystart = 0; ystop = 3; xstart = 3; xstop = 6; break;
                    case 3:
                        ystart = 0; ystop = 3; xstart = 6; xstop = 9; break;
                    case 4:
                        ystart = 3; ystop = 6; xstart = 0; xstop = 3; break;
                    case 5:
                        ystart = 3; ystop = 6; xstart = 3; xstop = 6; break;
                    case 6:
                        ystart = 3; ystop = 6; xstart = 6; xstop = 9; break;
                    case 7:
                        ystart = 6; ystop = 9; xstart = 0; xstop = 3; break;
                    case 8:
                        ystart = 6; ystop = 9; xstart = 3; xstop = 6; break;
                    case 9:
                        ystart = 6; ystop = 9; xstart = 6; xstop = 9; break;
                }

                for (byte y = ystart; y < ystop; y++)
                {
                    for (byte x = xstart; x < xstop; x++)
                    {
                        foreach (byte candidate in (imaginaryBoardisActive?imaginaryBoard.allCandidates(y, x):board.allCandidates(y, x)))
                        {
                            counterCandidate[candidate - 1]++;
                        }
                    }
                }


                //checking if there is only one candidate
                for (byte i = 0; i < 9; i++)
                {
                    if (counterCandidate[i] == 1)
                    {
                        singleCandidate = (byte)(i + 1);
                        break;
                    }
                }

                //looking for a candidate in a row
                if (singleCandidate != 0)
                {

                    for (byte y = ystart; y < ystop; y++)
                        for (byte x = xstart; x < xstop; x++)
                        {
                            for (byte i = 0; i < (imaginaryBoardisActive ? imaginaryBoard.allCandidates(y, x).Count : board.allCandidates(y, x).Count); i++)
                            {
                                if ((imaginaryBoardisActive ? imaginaryBoard.allCandidates(y, x)[i] : board.allCandidates(y, x)[i]) == singleCandidate)
                                {
                                    //editing board
                                    board.set(y, x, singleCandidate);
                                    tmp.Add("Znaleziono pojedynczego kandydata w kwadracie "+square+".", y, x, singleCandidate);
                                    break; //exit loop
                                }
                            }
                        }
                }

            }
            return tmp;
        }

        //the function checks if the candidates are twins. returns bool: null = they are not, true = horizontal, false = vertical
        private static bool? isTwins(List<Candidate> candidates)   //true - horizontal, false - vertical, null - is no Twins
        {
            //if they are not brothers or there are too few or too many brothers then NULL
            if (candidates.Count<2 || candidates.Count > 3)
            {
                return null;
            }
            if (candidates[0].Value!=candidates[1].Value)
            {
                return null;
            }
            if (candidates.Count==3)
            {
                if (candidates[0].Value != candidates[2].Value)
                {
                    return null;
                }
            }

            //check double twins
            if (candidates.Count==2)
            {
                if (candidates[0].X == candidates[1].X) //double twins horizontal
                {
                    return false;
                }
                if (candidates[0].Y == candidates[1].Y) //double twins vertical
                {
                    return true;
                }
            }
            else                //check triple twins
            {
                if (candidates[0].X == candidates[1].X && candidates[1].X == candidates[2].X) //triple twins horizontal
                {
                    return false;
                }
                if (candidates[0].Y == candidates[1].Y && candidates[1].Y == candidates[2].Y) //triple twins vertical
                {
                    return true;
                }
            }
            return null;
        }

        //the function finds "twins" and "triplets" in squares
        public static SolutionInformation ManualSolver05_TwinsInSquare(ref BoardTab board)
        {
            SolutionInformation tmp = new SolutionInformation();
            List<Candidate> candidates = new List<Candidate>();
            for (var square = 1; square <= 9; square++)
            {
                candidates.Clear();
                switch (square)
                {
                    case 1:
                        for (byte y = 0; y < 3; y++)
                            for (byte x = 0; x < 3; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 2:
                        for (byte y = 0; y < 3; y++)
                            for (byte x = 3; x < 6; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 3:
                        for (byte y = 0; y < 3; y++)
                            for (byte x = 6; x < 9; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 4:
                        for (byte y = 3; y < 6; y++)
                            for (byte x = 0; x < 3; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 5:
                        for (byte y = 3; y < 6; y++)
                            for (byte x = 3; x < 6; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 6:
                        for (byte y = 3; y < 6; y++)
                            for (byte x = 6; x < 9; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 7:
                        for (byte y = 6; y < 9; y++)
                            for (byte x = 0; x < 3; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 8:
                        for (byte y = 6; y < 9; y++)
                            for (byte x = 3; x < 6; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                    case 9:
                        for (byte y = 6; y < 9; y++)
                            for (byte x = 6; x < 9; x++)
                            {
                                foreach (byte candidate in board.allCandidates(y, x))
                                {
                                    candidates.Add(new Candidate(y, x, candidate));
                                }
                            }
                        break;
                }

                //search for twins in all candidates
                List<Candidate> brother = new List<Candidate>();
                bool? direction = null;
                for (byte nr = 1; nr <= 9; nr++)
                {
                    brother.Clear();
                    foreach (var candidate in candidates)
                    {
                        if (nr == candidate.Value)
                            brother.Add(candidate);
                    }
                    //check if the brothers are twins
                    direction = isTwins(brother);
                    if (direction!=null)
                    {
                        string stringTmp = (direction == true ? "Poziome" : "Pionowe")+" bliźniaki, kwadrat:" + square + ") pozostałe :";
                        foreach (byte squareTmp in MultiSolutionPositionsAfterFindingTheTwins(brother[0].Y, brother[0].X, direction == true ? true : false, 1))
                        {
                            stringTmp += squareTmp;
                        }
                         tmp.Add(stringTmp, brother[0].Y, brother[0].X, brother[0].Value);
                        


                        //-------------------------------------------------------------------------------------------------------

                        //test manual solver 01 TheOnlyPossible
                        //create new imaginary board witch imaginary value in cell
                        BoardTab ImaginaryBoard = new BoardTab(board);
                        ImaginaryBoard.set(brother[0].Y, brother[0].X, brother[0].Value);
                        SolutionInformation InformationTmp = new SolutionInformation();
                        InformationTmp = ManualSolver01_TheOnlyPossible(ref board, MultiSolutionPositionsAfterFindingTheTwins(brother[0].Y, brother[0].X, direction == true ? true : false, 1), ImaginaryBoard);
                        if (InformationTmp.Count() > 0)
                        {
                            for (int i = 0; i < InformationTmp.Count(); i++)
                            {
                                tmp.Add("Dzięki bliżniakom znaleziono jedynego możliwego kandydata", InformationTmp.Get_Y(i), InformationTmp.Get_X(i), InformationTmp.Get_Value(i));
                            }
                        }

                        //test manual solver 02 SingleCandidateInRow
                        //create new imaginary board witch imaginary value in cell
                        ImaginaryBoard.load(board);
                        ImaginaryBoard.set(brother[0].Y, brother[0].X, brother[0].Value);
                        InformationTmp.Clear();
                        InformationTmp = ManualSolver02_SingleCandidateInRow(ref board, MultiSolutionPositionsAfterFindingTheTwins(brother[0].Y, brother[0].X, direction == true ? true : false, 2), ImaginaryBoard);
                        if (InformationTmp.Count() > 0)
                        {
                            for (int i = 0; i < InformationTmp.Count(); i++)
                            {
                                tmp.Add("Dzięki bliżniakom znaleziono pojedynczego kandydata we wierszu", InformationTmp.Get_Y(i), InformationTmp.Get_X(i), InformationTmp.Get_Value(i));
                            }
                        }

                        //test manual solver 03 SingleCandidateInColumn
                        //create new imaginary board witch imaginary value in cell
                        ImaginaryBoard.load(board);
                        ImaginaryBoard.set(brother[0].Y, brother[0].X, brother[0].Value);
                        InformationTmp.Clear();
                        InformationTmp = ManualSolver03_SingleCandidateInColumn(ref board, MultiSolutionPositionsAfterFindingTheTwins(brother[0].Y, brother[0].X, direction == true ? true : false, 3), ImaginaryBoard);
                        if (InformationTmp.Count() > 0)
                        {
                            for (int i = 0; i < InformationTmp.Count(); i++)
                            {
                                tmp.Add("Dzięki bliżniakom znaleziono pojedynczego kandydata w kolumnie", InformationTmp.Get_Y(i), InformationTmp.Get_X(i), InformationTmp.Get_Value(i));
                            }
                        }
                        //-------------------------------------------------------------------------------------------------------
                    }
                }
            }
            return tmp;
        }

        //the number of the square in which the cell is located
        private static byte nrSquare(byte y, byte x)
        {
            if (y < 3 && x < 3) return 1;
            if (y < 3 && x < 6) return 2;
            if (y < 3) return 3;
            if (y > 5 && x < 3) return 7;
            if (y > 5 && x < 6) return 8;
            if (y > 5) return 9;
            if (y > 2 && y < 6 && x < 3) return 4;
            if (y > 2 && y < 6 && x < 6) return 5;
            if (y > 2 && y < 6) return 6;
            return 0;
        }

        //the function returns the numbers of ditches, columns, squares for a given solution method.
        private static byte[] MultiSolutionPositionsAfterFindingTheTwins(byte y, byte x, bool direction, byte nrSolution)
        {
            byte[] tmp;
            byte square = nrSquare(y, x);

            byte[] FindRow(bool direction2)
            {
                if (direction2)
                {
                    if (y == 0) return new byte[] { 1, 2 };
                    if (y == 1) return new byte[] { 0, 2 };
                    if (y == 2) return new byte[] { 0, 1 };
                    if (y == 3) return new byte[] { 4, 5 };
                    if (y == 4) return new byte[] { 3, 5 };
                    if (y == 5) return new byte[] { 3, 4 };
                    if (y == 6) return new byte[] { 7, 8 };
                    if (y == 7) return new byte[] { 6, 8 };
                    return             new byte[] { 6, 7 };
                }
                else
                {
                    if (y < 3) return new byte[] { 3, 4, 5, 6, 7, 8 };
                    if (y < 6) return new byte[] { 0, 1, 2, 6, 7, 8 };
                    return            new byte[] { 0, 1, 2, 3, 4, 5 };
                }
            }


            byte[] FindColumn(bool direction2)
            {
                if (direction2)
                {
                    if (x < 3) return new byte[] { 3, 4, 5, 6, 7, 8 };
                    if (x < 6) return new byte[] { 0, 1, 2, 6, 7, 8 };
                    return new byte[] { 0, 1, 2, 3, 4, 5 };

                }
                else
                {
                    if (x == 0) return new byte[] { 1, 2 };
                    if (x == 1) return new byte[] { 0, 2 };
                    if (x == 2) return new byte[] { 0, 1 };
                    if (x == 3) return new byte[] { 4, 5 };
                    if (x == 4) return new byte[] { 3, 5 };
                    if (x == 5) return new byte[] { 3, 4 };
                    if (x == 6) return new byte[] { 7, 8 };
                    if (x == 7) return new byte[] { 6, 8 };
                    return new byte[] { 6, 7 };
                }
            }


            byte[] FindSquares(byte square2, bool direction2)
            {
                byte[] tmp2;
                switch(square2)
                {
                    case 1:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 2, 3 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 4, 7 };
                        }
                        break;
                    case 2:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 1, 3 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 5, 8 };
                        }
                        break;
                    case 3:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 1, 2 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 6, 9 };
                        }
                        break;
                    case 4:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 5, 6 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 1, 7 };
                        }
                        break;
                    case 5:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 4, 6 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 2, 8 };
                        }
                        break;
                    case 6:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 4, 5 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 3, 9 };
                        }
                        break;
                    case 7:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 8, 9 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 1, 4 };
                        }
                        break;
                    case 8:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 7, 9 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 2, 5 };
                        }
                        break;
                    case 9:
                        if (direction2)
                        {
                            tmp2 = new byte[] { 7, 8 };
                        }
                        else
                        {
                            tmp2 = new byte[] { 3, 6 };
                        }
                        break;
                    default:
                        {
                            tmp2 = null;
                        }
                        break;
                }
                return tmp2;
            }

            switch(nrSolution)
            {
                case 1:
                    {
                        tmp = new byte[FindSquares(square, direction).Length];
                        for(byte i = 0; i < FindSquares(square, direction).Length; i++)
                        {
                            tmp[i] = FindSquares(square, direction)[i];
                        }
                    }
                    break;
                case 2:
                    {
                        tmp = new byte[FindRow(direction).Length];
                        for (byte i = 0; i < FindRow(direction).Length; i++)
                        {
                            tmp[i] = FindRow(direction)[i];
                        }
                    }
                    break;
                case 3:
                    {
                        tmp = new byte[FindColumn(direction).Length];
                        for (byte i = 0; i < FindColumn(direction).Length; i++)
                        {
                            tmp[i] = FindColumn(direction)[i];
                        }
                    }
                    break;
                default:
                    {
                        tmp = null;
                    }
                    break;
            }
            return tmp;   
        }

        public static SolutionInformation ManualSolver06_XYWing(ref BoardTab board, byte[] listSquareToCheck = null, BoardTab imaginaryBoard = null)
        {
            if (listSquareToCheck == null)
            {
                listSquareToCheck = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }

            //if the imaginary board is active, we will read from the imaginaryBoard one and write it to the oryginal board
            bool imaginaryBoardisActive;
            if (imaginaryBoard == null)
            {
                imaginaryBoardisActive = false;
            }
            else
            {
                imaginaryBoardisActive = true;
            }

            SolutionInformation tmp = new SolutionInformation();


            return tmp;
        }

     }
}
