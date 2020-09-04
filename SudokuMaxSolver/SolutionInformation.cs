using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaxSolver
{
    struct InfoStruct
    {
        public InfoStruct(String destription, byte y, byte x, byte value)
        {
            this.destription = destription;
            this.y = y;
            this.x = x;
            this.value = value;
        }
        public string destription;
        public byte y;
        public byte x;
        public byte value;
    }
    //  storing basic information about a single manual solution
    class SolutionInformation
    {
        List<InfoStruct> dataSolutionInformation;
        public SolutionInformation()
        {
            dataSolutionInformation = new List<InfoStruct>();
        }
        public SolutionInformation(String destription, byte y, byte x, byte value)
        {
            dataSolutionInformation = new List<InfoStruct>();
            dataSolutionInformation.Add(new InfoStruct(destription, y, x, value));
        }
        public void Add(String destription, byte y, byte x, byte value)
        {
            dataSolutionInformation.Add(new InfoStruct(destription, y, x, value));
        }
        public void Add(SolutionInformation oldSolutionInformation)
        {
            InfoStruct tmp;
            for (int i=0; i < oldSolutionInformation.Count(); i++)
            {
                tmp.destription = oldSolutionInformation.Get_Destription(i);
                tmp.y = oldSolutionInformation.Get_Y(i);
                tmp.x = oldSolutionInformation.Get_X(i);
                tmp.value = oldSolutionInformation.Get_Value(i);
                dataSolutionInformation.Add(tmp);
            }
        }
        public void Clear()
        {
            dataSolutionInformation.Clear();
        }
        public int Count()
        {
            return dataSolutionInformation.Count();
        }
        public string Get_Destription(int index)
        {
            return dataSolutionInformation[index].destription;
        }
        public byte Get_Y(int index)
        {
            return dataSolutionInformation[index].y;
        }
        public byte Get_X(int index)
        {
            return dataSolutionInformation[index].x;
        }
        public byte Get_Value(int index)
        {
            return dataSolutionInformation[index].value;
        }
    }
}
