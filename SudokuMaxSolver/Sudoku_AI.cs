using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace SudokuMaxSolver
{
    class Sudoku_AI
    {
        public enum difficultyLevel 
            {
                Trywialna, BardzoLatwa, Latwa, Przecietna, DosycTrudna, Trudna, BardzoTrudna, Diaboliczna, Niemozliwa
            }
        byte[,] boardAI = new byte[9, 9];

        public void clear()
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                    boardAI[y, x] = 0;
        }
        public Sudoku_AI()
        {
            clear();
        }
        public void generateNewBoard(difficultyLevel level)
        {
            generate9square();
        }
        private void generate9square()
        {
            byte reduceBy8(int a)
            {
                while(a>9)
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
                        if (y<6)
                    {
                        boardAI[y, x] = reduceBy8(2 + x + y * 3);
                    }
                    else
                    {
                        boardAI[y, x] = reduceBy8(3 + x + y * 3);
                    }

                }
        }
        public byte get(byte y, byte x)
        {
            return boardAI[y, x];
        }
    }
}
