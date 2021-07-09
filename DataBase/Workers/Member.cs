using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Workers
{
    class Member : Worker
    {

        public Member(string FullName, byte Age, string IdDepartament)
        {
            fullName = FullName;
            age = Age;
            idDepartament = IdDepartament;
            id = Convert.ToString(Worker.GetNext());
            CalculateSalary();
            Worker.allWorkers.Add(this);
        }

        public Member(string IdDepartament) : this("Member " + Worker.Count, 18, IdDepartament)
        {
            Random rnd = new Random();
            age = Convert.ToByte(rnd.Next(18, 50));
        }

        /// <summary>
        /// Метод считающий зар. плату
        /// </summary>
        public override void CalculateSalary()
        {
            salary = 30 * 40;
        }

        public override string Print() =>  "Member";

    }
}
