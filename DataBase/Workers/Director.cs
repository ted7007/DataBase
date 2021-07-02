using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Workers
{
    class Director : Worker
    {
        public Director(string FullName, byte Age, string IdDepartament)
        {
            fullName = FullName;
            age = Age;
            idDepartament = IdDepartament;
            id = Convert.ToString(Worker.GetNext());
            CalculateSalary();
            Worker.allWorkers.Add(this);
        }

        public Director(string IdDepartament) : this("Director "+Worker.Count, 18, IdDepartament)
        {
            Random rnd = new Random();
            age = Convert.ToByte(rnd.Next(18, 80));
        }

        public override void CalculateSalary()
        {
            uint depSalary = GetAllSalary(idDepartament);
            salary = Convert.ToUInt32(depSalary * 0.15) < 1000? 1000: Convert.ToUInt32(depSalary * 0.15);

        }
        
        /// <summary>
        /// Метод, возвращающий сумму зарплат сотрудников департамента
        /// </summary>
        /// <param name="IdDepartament"></param>
        /// <returns></returns>
        private uint GetAllSalary(string IdDepartament)
        {
            uint result = 0;
            Departament dep = Departament.Find(IdDepartament);
            foreach (var i in dep.Workers)
            {
                result += i.Salary;
            }

            foreach(var i in dep.Departaments)
            {
                result += GetAllSalary(i.Id);
            }
            return result;
            
        }
        
    }
}
