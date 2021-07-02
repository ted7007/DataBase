﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Workers;

namespace DataBase
{
    class Departament
    {
        #region static
        static private uint count;
        static public List<Departament> allDepartaments;

        static public uint Count { get { return count; } }
        static Departament()
        {
            Init();
        }
        static public void Init()
        {
            Departament.count = 0;
            Departament.allDepartaments = new List<Departament>();
            Worker.Init();
        }
        /// <summary>
        /// Метод, который возвращает uint индекс департамента и передвигающий счётчик дальше.
        /// </summary>
        /// <returns></returns>
        static public uint GetNext()
        {
            uint result = count;
            count++;
            return result;
        }

        /// <summary>
        /// Метод ищет департамент по его id
        /// </summary>
        /// <param name="id">id департамента</param>
        /// <returns></returns>
        static public Departament Find(string id)
        {
            foreach (var i in allDepartaments)
            {
                if (i.Id == id)
                    return i;
            }
            return null;
        }
        /// <summary>
        /// Генерация новой базы данных. Этот метод обнуляет статические поля
        /// </summary>
        /// <param name="name">Название новой базы данных</param>
        /// <param name="maxCountDep">Максимальное количество департаментов в БД</param>
        /// <param name="maxCountWorkers">максимальное количество работников в БД</param>
        /// <returns></returns>
        static public Departament GenerateNewDataBase(string name, int maxCountDep, int maxCountWorkers)
        {
            Random rnd = new Random();
            Departament.Init();
            Departament DataBase = new Departament("DataBase");
            int countDep = rnd.Next(maxCountDep), countWorkers = rnd.Next(maxCountWorkers);
            for(int i = 0; i < countDep; i++)
            {
                int newCountDep = rnd.Next(countDep), newCountWorkers = rnd.Next(countWorkers);
                DataBase.AddDepartament(Departament.GenerateDepartament(newCountDep, newCountWorkers));
            }

            for(int i = 0; i < countWorkers; i++)
            {
                switch(rnd.Next(2))
                {
                    case 0:
                        DataBase.AddWorker(new Intern(DataBase.Id));
                        break;
                    case 1:
                        DataBase.AddWorker(new Member(DataBase.Id));
                        break;
                }
            }
            return DataBase;
        }

        /// <summary>
        /// Метод генерирующий случайный департамент
        /// </summary>
        /// <param name="maxCountDep">максимальное число депратаментов в случайном департаменте</param>
        /// <param name="maxCountWorkers">максимальное число работников в случайном департаменте</param>
        /// <returns></returns>
        static public Departament GenerateDepartament(int maxCountDep, int maxCountWorkers)
        {
            Random rnd = new Random();
            Departament dep = new Departament();
            int countDep = rnd.Next( maxCountDep), countWorkers = rnd.Next(maxCountWorkers);
            for (int i = 0; i < countDep; i++)
            {
                int newCountDep = rnd.Next(countDep), newCountWorkers = rnd.Next(countWorkers);
                dep.AddDepartament(Departament.GenerateDepartament(newCountDep, newCountWorkers));
            }

            for (int i = 0; i < countWorkers; i++)
            {
                switch (rnd.Next(2))
                {
                    case 0:
                        dep.AddWorker(new Intern(dep.Id));
                        break;
                    case 1:
                        dep.AddWorker(new Member(dep.Id));
                        break;
                }
            }
            return dep;

        }

        #endregion

        #region fields
        private string name;

        private string id;

        private string idDirector;

        private List<Worker> workers;

        private List<Departament> departaments;

        #endregion

        #region properties

        public string Name { get { return name; } }

        public string Id { get { return id; } }

        public string IdDirector { get { return idDirector; } }

        public List<Worker> Workers { get { return workers; } }

        public List<Departament> Departaments { get { return departaments; } }

        #endregion

        #region ctors

        public Departament(string Name)
        {
            name = Name;
            id = Convert.ToString(Departament.GetNext());
            workers = new List<Worker>();
            departaments = new List<Departament>();
            allDepartaments.Add(this);

            Director dir = new Director(Id);
            idDirector = dir.Id;
            AddWorker(dir);
        }

        public Departament() : this("Departament " + Departament.Count)
        {

        }

        #endregion 

        #region methods

        /// <summary>
        /// Метод, добавляющий новый департамент
        /// </summary>
        /// <param name="departament"></param>
        public void AddDepartament(Departament departament)
        {
            departaments.Add(departament);
            Worker.Find(IdDirector).CalculateSalary();
        }


        /// <summary>
        /// Метод, добавляющий нового работника
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(Worker worker)
        {
            workers.Add(worker);
            Worker.Find(IdDirector).CalculateSalary();
        }


        #endregion
    }
}