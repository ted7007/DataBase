using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Workers;

namespace DataBase
{
    class Departament : IEnumerable
    {
        #region static

        #region static fields
        static private uint count;

        static public ObservableCollection<Departament> allDepartaments;
        #endregion

        #region static Properties

        static public uint Count { get { return count; } }

        #endregion
        static Departament()
        {
            Init();
        }

        /// <summary>
        /// Метод инициализации статических полей
        /// </summary>
        static private void Init()
        {
            Departament.count = 0;
            Departament.allDepartaments = new ObservableCollection<Departament>();
            Worker.Init();
        }

        /// <summary>
        /// Метод, который возвращает uint индекс департамента, и передвигает счётчик дальше.
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
            Departament DataBase = new Departament("DataBase", "-1");
            int countDep = rnd.Next(maxCountDep), countWorkers = rnd.Next(maxCountWorkers);
            for(int i = 0; i < countDep; i++)
            {
                int newCountDep = rnd.Next(countDep), newCountWorkers = rnd.Next(countWorkers);
                DataBase.AddDepartament(Departament.GenerateDepartament(newCountDep, newCountWorkers,DataBase.Id));
            }
            return DataBase;
        }

        /// <summary>
        /// Метод генерирующий случайный департамент
        /// </summary>
        /// <param name="maxCountDep">максимальное число депратаментов в случайном департаменте</param>
        /// <param name="maxCountWorkers">максимальное число работников в случайном департаменте</param>
        /// <returns></returns>
        static public Departament GenerateDepartament(int maxCountDep, int maxCountWorkers, string parrentId)
        {
            Random rnd = new Random();
            Departament dep = new Departament(parrentId);
            int countDep = rnd.Next( maxCountDep), countWorkers = rnd.Next(maxCountWorkers);
            for (int i = 0; i < countDep; i++)
            {
                int newCountDep = rnd.Next(countDep), newCountWorkers = rnd.Next(countWorkers);
                dep.AddDepartament(Departament.GenerateDepartament(newCountDep, newCountWorkers,dep.Id));
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

        private ObservableCollection<Worker> workers;

        private ObservableCollection<Departament> departaments;

        private string parrentId;

        #endregion

        #region properties

        public string Name { get { return name; } }

        public string Id { get { return id; } }

        public string IdDirector { get { return idDirector; } }

        public ObservableCollection<Worker> Workers { get { return workers; } }

        public ObservableCollection<Departament> Departaments { get { return departaments; } }

        public string ParrentId { get { return parrentId; } }

        #endregion

        #region ctors

        public Departament(string Name, string ParrentId)
        {
            name = Name;
            id = Convert.ToString(Departament.GetNext());
            workers = new ObservableCollection<Worker>();
            departaments = new ObservableCollection<Departament>();
            allDepartaments.Add(this);
            parrentId = ParrentId;

            Director dir = new Director(Id);
            idDirector = dir.Id;
            AddWorker(dir);
        }

        public Departament(string ParrentId) : this("Departament " + Departament.Count, ParrentId)
        {

        }

        #endregion 

        #region methods

        /// <summary>
        /// Метод необходимый для интерфейса IEnumerable. В дальнейшем помогает при сортировке.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < Workers.Count; i++)
            {
                yield return Workers[i];
            }
        }

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

        /// <summary>
        /// Метод сортировки списка работников
        /// </summary>
        /// <param name="propToCompare">Свойства, по которым следует сортировать список.
        ///                             Age - по возрасту,
        ///                             Salary - по зарплате,
        ///                             AgeAndSalary - по возрасту и зарплате.</param>
        /// <param name="isReverse">Флаг обратного порядка, при значении true - сортирует в обратном порядке.</param>
        public void SortWorkers(PropertiesToCompare propToCompare, bool isReverse = false)
        {
            Worker[] workers2 = new Worker[workers.Count];
            workers.CopyTo(workers2,0);
            Array.Sort(workers2, new WorkerComparer(propToCompare));
            if(isReverse)
                Array.Reverse(workers2);
            workers.Clear();
            foreach(var i in workers2)
            {
                workers.Add(i);
            }
            
        }

        /// <summary>
        /// Метод удаления текущего департамента
        /// </summary>
        public void Delete()
        {
            if (this.Id != "1") 
                Departament.Find(ParrentId).departaments.Remove(Departament.Find(this.Id));
        }

        /// <summary>
        /// Метод удаления работника
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(string id)
        {
            for(int i = 0; i < Workers.Count; i++)
            {
                if(Workers[i].Id == id)
                {
                    Workers.Remove(Workers[i]);
                }    
            }
        }
        #endregion
    }
}
