using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaxSolver
{
    class FakeCandidatesBoard
    {
        protected List<byte>[,] fakeCandidateBoard = new List<byte>[9, 9];

        //create an empty board "lists of fake candidates"
        public FakeCandidatesBoard()
        {
            ClearFakeCandidateBoard();
        }

        //add a false candidate to the list if it does not exist
        public void AddFakeCandidate(byte y, byte x, byte fCandidate)
        {
            foreach (byte value in fakeCandidateBoard[y,x])
            {
                if (value == fCandidate)
                {
                    return;
                }
            }
            fakeCandidateBoard[y, x].Add(fCandidate);
        }

        //check if a false candidate is on the list
        public bool ExistsFakeCandidate(byte y, byte x, byte fCandidate)
        {
            foreach (byte value in fakeCandidateBoard[y, x])
            {
                if (value == fCandidate)
                {
                    return true;
                }
            }
            return false;
        }

        public void ClearFakeCandidateBoard()
        {
            for (byte y = 0; y < 9; y++)
            {
                for (byte x = 0; x < 9; x++)
                {
                    fakeCandidateBoard[y, x] = new List<byte>();
                }
            }
        }
    }
}
