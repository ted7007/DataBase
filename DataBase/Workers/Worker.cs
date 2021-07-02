using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    abstract class Worker
    {
        #region static

        static private uint count;

        static public List<Worker> allWorkers;

        static public uint Count { get {  return count;} }

        static Worker()
        {
            Init();
        }
        static public void Init()
        {
            Worker.count = 0;
            Worker.allWorkers = new List<Worker>();
        }
        
        /// <summary>
        /// Метод, возвращающий номер следующего сотрудника.
        /// </summary>
        /// <returns></returns>
        static public uint GetNext()
        {
            uint result = count;
            count++;
            return result;
        }

        static public Worker Find(string id)
        {
            foreach (var i in allWorkers)
            {
                if (i.Id == id)
                    return i;
            }
            return null;
        }

        #endregion

        #region fields

        protected string fullName;

        protected string id;

        protected byte age;

        protected uint salary;

        protected string idDepartament;

        #endregion

        #region propeties

        public string FullName { get { return fullName; } }

        public string Id { get { return id; } }

        public byte Age { get { return age; } }

        public uint Salary { get { return salary; } }

        public string IdDepartament { get { return idDepartament; } }

        #endregion

        #region methods

        public abstract void CalculateSalary();


        #endregion

    }
}
