using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Workers
{
    class Intern : Worker
    {

        public Intern(string FullName, byte Age, string IdDepartament)
        {
            fullName = FullName;
            age = Age;
            idDepartament = IdDepartament;
            id = Convert.ToString(Worker.GetNext());
            CalculateSalary();
            Worker.allWorkers.Add(this);
        }
        public Intern(string IdDepartament) : this("Intern " + Worker.Count, 18, IdDepartament)
        {
            Random rnd = new Random();
            age = Convert.ToByte(rnd.Next(18, 30));
        }

        /// <summary>
        /// Метод считающий зар. плату
        /// </summary>
        public override void CalculateSalary()
        {
            salary = 1000;
        }
    }
}
