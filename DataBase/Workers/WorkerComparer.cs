using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Workers
{
    class WorkerComparer : IComparer<Worker>
    {
        private PropertiesToCompare propertiesToCompare;

        public PropertiesToCompare PropertiesToCompare { get { return propertiesToCompare; } }

        public WorkerComparer(PropertiesToCompare PropertiesToCompare)
        {
            propertiesToCompare = PropertiesToCompare;
        }

        public int Compare(Worker x, Worker y)
        {
            switch (PropertiesToCompare)
            {
                case PropertiesToCompare.Age:
                    return (x.Age - y.Age);

                case PropertiesToCompare.Salary:
                    return (x.Salary - y.Salary);

                case PropertiesToCompare.AgeAndSalary:
                    
                    // 1: salary++&&age--||salary==&&age--||salary++&&age++||salary++age==
                    // 0: salary==&&age==
                    //-1: salary--&&age++||salary==&&age++||salary--&&age--||salary--&&age==
                    if ((x.Salary - y.Salary) > 0 ||( (x.Salary - y.Salary) == 0 && (x.Age - y.Age) < 0))
                        return 1;
                    else if ((x.Salary - y.Salary) < 0 || ((x.Salary - y.Salary) == 0 && (x.Age - y.Age) > 0))
                        return -1;
                    else
                        return 0;
                default:
                    throw new Exception("No property to Compare");
            }
        }

    }

    public enum PropertiesToCompare
    {
        Age,
        Salary,
        AgeAndSalary
    }
}
