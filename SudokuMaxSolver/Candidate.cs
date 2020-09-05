using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaxSolver
{
    class Candidate
    {
        public Candidate(byte y, byte x, byte value)
        {
            Y = y;
            X = x;
            Value = value;
        }
        public byte Value
        {
            get;
            set;
        }
        public byte X
        {
            get;
            set;
        }
        public byte Y
        {
            get;
            set;
        }
    }
}
