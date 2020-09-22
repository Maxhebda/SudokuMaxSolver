using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuMaxSolver
{
    class SolutionInformation
    {
        public enum TypeOfSolution
        {
            Unknown,
            Method01_TheOnlyPossible,
            Method02_SingleCandidateInRow,
            Method03_SingleCandidateInColumn,
            Method04_SingleCandidateInSquare,
            Method05_Twins_for_Method01_TheOnlyPossible,
            Method05_Twins_for_Method02_SingleCandidateInRow,
            Method05_Twins_for_Method03_SingleCandidateInColumn,
            Method05_Twins_for_Method04_SingleCandidateInSquare,
            Method06_XWings,
            Method07_YWings,
            Method08_DoubleForcingChains
        }
        string destription;
        List<Candidate> pointsDetected;
        List<Candidate> pointsChanged;
        TypeOfSolution typeOfSolution;
        public SolutionInformation()
        {
            pointsDetected = new List<Candidate>();
            pointsChanged = new List<Candidate>();
            destription = "";
            typeOfSolution = TypeOfSolution.Unknown;
        }
        public SolutionInformation(String destription, TypeOfSolution typeOfSolution)
        {
            pointsDetected = new List<Candidate>();
            pointsChanged = new List<Candidate>();
            this.destription = destription;
            this.typeOfSolution = typeOfSolution;
        }
        public SolutionInformation(TypeOfSolution typeOfSolution)
        {
            pointsDetected = new List<Candidate>();
            pointsChanged = new List<Candidate>();
            this.typeOfSolution = typeOfSolution;
        }
        public SolutionInformation(String destription, List<Candidate> pointsChanged, List<Candidate> pointsDetected, TypeOfSolution typeOfSolution)
        {
            this.typeOfSolution = typeOfSolution;
            this.destription = destription;
            this.pointsDetected = new List<Candidate>();
            foreach (var item in pointsDetected)
            {
                this.pointsDetected.Add(item);
            }
            pointsChanged = new List<Candidate>();
            foreach (var item in pointsChanged)
            {
                this.pointsChanged.Add(item);
            }
        }
        public void Add(List<Candidate> pointsChanged, List<Candidate> pointsDetected)
        {
            if (pointsDetected != null)
            {
                foreach (var item in pointsDetected)
                {
                    this.pointsDetected.Add(item);
                }
            }
            if (pointsChanged != null)
            {
                foreach (var item in pointsChanged)
                {
                    this.pointsChanged.Add(item);
                }
            }
        }
        /*
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
        */
        public void Clear()
        {
            destription = "";
            pointsChanged.Clear();
            pointsDetected.Clear();
            typeOfSolution = TypeOfSolution.Unknown;
        }
        /*
        public int Count()
        {
            return dataSolutionInformation.Count();
        }
        */
        public string Get_destription()
        {
            return destription;
        }
        public List<Candidate> Get_pointsDetected()
        {
            return pointsDetected;
        }
        public List<Candidate> Get_pointsChanged()
        {
            return pointsChanged;
        }
        public TypeOfSolution Get_typeOfSolution()
        {
            return typeOfSolution;
        }
    }
}
