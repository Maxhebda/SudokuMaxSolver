using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SudokuMaxSolver
{
    class Sudoku_AI
    {
        public enum difficultyLevel
        {   // number of ready digits
            Trywialna =48, BardzoLatwa=45, Latwa=42, Przecietna=39, DosycTrudna=36, Trudna=33, BardzoTrudna=30, Diaboliczna=28, Niemozliwa=26
            //            Trywialna =45, BardzoLatwa=42, Latwa=39, Przecietna=36, DosycTrudna=33, Trudna=30, BardzoTrudna=27, Diaboliczna=23, Niemozliwa=19
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
            /*
            //generate full good board
            generateBigColumnAround(); 
            generateBigRowAround();
            generateSmallColumnAround();
            generateSmallRowAround();
            for (byte a = 0; a < 4; a++)
            {
                generateAroundNumbers();
            }
            */
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
        static private bool idzDalej(ref byte y, ref byte x)
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
                    //komorka pusta mozemy wpisac i sprawdzac

                    //srawdzamy czy mozna wipisac
                    if (board.isInColumn(yStart, xStart, NrStart) || board.isInRow(yStart, xStart, NrStart) || board.isInSquare(yStart, xStart, NrStart))
                    {
                        // nie mozna wpisac wiec zwiekszamy numer i probujemy od nowa
                        if (NrStart < 9)
                        {
                            // zwiekszamy i sprawdzamy kolejna liczbe 
                            NrStart++;
                            return findGoodCell(yStart, xStart, NrStart, board);
                        }
                        else
                        {
                            //Debug.WriteLine("(" + yStart + "," + xStart + "->x)");
                            // nie mozna juz zwiekszyc numeru - zadne nie mozna tu wsadzic! = blad
                            return false;
                        }
                    }
                    else
                    {
                        board.set(yStart, xStart, NrStart);
                        // sparwdzam czy spisanie liczby powoduje niszczenie dalszej czesci tablicy
                        if (findGoodCell(yStart, xStart, NrStart, board) == false)
                        {

                            //zniszczylo wiec zwiekszamy numer o jeden oraz czyscimy nr z tablicy
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
                        else    //nie zniszczylo wiec idziemy do nastepnej komorki
                        {

                            //idziemy dalej czyli sprawdzamy dalsza czesc tablicy od nr 1
                            if (idzDalej(ref yStart, ref xStart))
                            {
                                return findGoodCell(yStart, xStart, 1, board);
                            }
                            else
                            {
                                //jestesmy na koncu  a udało się wpisać wiec zwtacamy true;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    //komorka pelna wiec idziemy do nastepnej
                    if (idzDalej(ref yStart, ref xStart))
                    {
                        // mozna przejsc wiec sprawdzamy komorke od 1
                        return findGoodCell(yStart, xStart, 1, board);
                    }
                    else
                    {
                        // nie mozemy przejsc dalej a ostatnia komorka jest juz uzupelniona wiec tez udalo sie dotrzec do konca - zwracamy true
                        return true;
                    }
                }
            }

            //start position 0,0 and number 1
            if (findGoodCell(0, 0, 1, boardCopy))
            {
                // rozwiązano więc podmieniamy zawartosc rozwiązanego sudoku :
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
                    //komorka pusta mozemy wpisac i sprawdzac

                    //srawdzamy czy mozna wipisac
                    if (board.isInColumn(yStart, xStart, NrStart) || board.isInRow(yStart, xStart, NrStart) || board.isInSquare(yStart, xStart, NrStart))
                    {
                        // nie mozna wpisac wiec zwiekszamy numer i probujemy od nowa
                        if (NrStart < 9)
                        {
                            // zwiekszamy i sprawdzamy kolejna liczbe 
                            NrStart++;
                            return findGoodCell(yStart, xStart, NrStart, board);
                        }
                        else
                        {
                            //Debug.WriteLine("(" + yStart + "," + xStart + "->x)");
                            // nie mozna juz zwiekszyc numeru - zadne nie mozna tu wsadzic! = blad
                            return false;
                        }
                    }
                    else
                    {
                        board.set(yStart, xStart, NrStart);
                        // sparwdzam czy spisanie liczby powoduje niszczenie dalszej czesci tablicy
                        if (findGoodCell(yStart, xStart, NrStart, board) == false)
                        {

                            //zniszczylo wiec zwiekszamy numer o jeden oraz czyscimy nr z tablicy
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
                        else    //nie zniszczylo wiec idziemy do nastepnej komorki
                        {

                            //idziemy dalej czyli sprawdzamy dalsza czesc tablicy od nr 1
                            if (idzDalej(ref yStart, ref xStart))
                            {
                                return findGoodCell(yStart, xStart, 1, board);
                            }
                            else
                            {
                                //jestesmy na koncu  a udało się wpisać wiec powinnism zakonczyc szukanie rozwiazania i zwrocic true ale....;
                                //-------------------------------------------------------------- pod spodem modyfikacja
                                if (numberOfSolution == 0)
                                {
                                    numberOfSolution=1;
                                    // w przypadku znalezienia pierwszego rozwiazania zwracamy jednak false by program nie uznal rozwiazania
                                    return false;
                                }
                                else
                                {
                                    if (numberOfSolution==1)
                                    {
                                        numberOfSolution=2;
                                    }
                                    //jezeli juz mamy jedno rozwiazanie to dodajemy drugie i konczymy szukanie zwracajac true
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //komorka pelna wiec idziemy do nastepnej
                    if (idzDalej(ref yStart, ref xStart))
                    {
                        // mozna przejsc wiec sprawdzamy komorke od 1
                        return findGoodCell(yStart, xStart, 1, board);
                    }
                    else
                    {
                        // nie mozemy przejsc dalej a ostatnia komorka jest juz uzupelniona wiec tez udalo sie dotrzec do konca - zwracamy true
                        //-------------------------------------------------------------- pod spodem modyfikacja
                        if (numberOfSolution == 0)
                        {
                            numberOfSolution = 1;
                            // w przypadku znalezienia pierwszego rozwiazania zwracamy jednak false by program nie uznal rozwiazania
                            return false;
                        }
                        else
                        {
                            if (numberOfSolution == 1)
                            {
                                numberOfSolution = 2;
                            }
                            //jezeli juz mamy jedno rozwiazanie to dodajemy drugie i konczymy szukanie zwracajac true
                            return true;
                        }
                    }
                }
            }

            //start position 0,0 and number 1
            findGoodCell(0, 0, 1, boardCopy);
            return numberOfSolution;
        }
    }
}
