using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataBase.Workers;

namespace DataBase
{
    class DataBaseProvider : INotifyPropertyChanged
    {
        #region fields

        private Departament dataBase;

        private Departament selectedDepartament;

        private ObservableCollection<Worker> workers;

        private Worker selectedWorker;

        private ButtonCommand sortWorkersCommand;

        private ButtonCommand addWorkerCommand;

        private ButtonCommand addDepartamentCommand;

        private ButtonCommand removeDepartamentCommand;

        private ButtonCommand removeWorkerCommand;

        #endregion

        #region properties
        public Departament DataBase 
        {
            get { return dataBase; }
            private set
            {
                dataBase = value;
                OnPropertyChanged("DataBase");
            }
        }

        public ObservableCollection<Worker> Workers 
        {
            get
            {
                return workers;
            }
            set 
            {
                workers = value;
                OnPropertyChanged("Workers");
            }
        }

        public Departament SelectedDepartament 
        {   get { return selectedDepartament; } 
            set 
            { 
                selectedDepartament = value;
                Workers = selectedDepartament.Workers;
                OnPropertyChanged("SelectedDepartament"); 
            } 
        }

        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                OnPropertyChanged("Workers");
            }
        }

       

        #endregion

        #region commands
        public ButtonCommand SortWorkersCommand
        {

            get
            {
                return sortWorkersCommand ?? (sortWorkersCommand = new ButtonCommand(o => SortWorkersClick()));
            }
        }

        public ButtonCommand AddWorkerCommand
        {
            get
            {
                return addWorkerCommand ?? (addWorkerCommand = new ButtonCommand(o => AddWorkerClick()));
            }
        }
        
        public ButtonCommand AddDepartamentCommand
        {
            get
            {
                return addDepartamentCommand ?? (addDepartamentCommand = new ButtonCommand(o => AddDepartamentClick(o)));
            }
        }

        public ButtonCommand RemoveDepartamentCommand 
        {
            get
            {
                return removeDepartamentCommand ?? (removeDepartamentCommand = new ButtonCommand(o => RemoveDepartamentClick(o)));
            }
        }
        public ButtonCommand RemoveWorkerCommand
        {
            get
            {
                return removeWorkerCommand ?? (removeWorkerCommand = new ButtonCommand(o => RemoveWorkerClick(o)));
            }
        }

        #endregion

        public DataBaseProvider()
        {
            
            DataBase = new Departament("DataBase");
            DataBase.AddDepartament(Departament.GenerateNewDataBase("DataBase", 20, 100));
            
        }

        #region methods

        /// <summary>
        /// Метод, срабатывающий при нажатии на кнопку сортировки работников. Сортирует список работников.
        /// </summary>
        public void SortWorkersClick()
        {
            if (SelectedDepartament == null)
                return;
            SortConfig SortConfigWindow = new SortConfig();

            if (SortConfigWindow.ShowDialog() == true)
            {
                SortConfigVM data = SortConfigWindow.DataContext as SortConfigVM;
                SelectedDepartament.SortWorkers(data.PropertiesToSort, data.IsReverse);
            }
        }

        /// <summary>
        /// Метод, срабатывающий при нажатии на кнопку добавления работника. Добавляет работника.
        /// </summary>
        public void AddWorkerClick()
        {
            if (SelectedDepartament is null || SelectedDepartament.Id == "1")
                return;
            AddWorkerWindow addwindow = new AddWorkerWindow();
            var data = addwindow.DataContext as AddWorkerWindowVM;
            if (addwindow.ShowDialog() == true && int.Parse(data.AgeStr) > 17)
            {

                string nameworker = data.NameStr; byte ageworker = Convert.ToByte(data.AgeStr);
                switch (data.TypeOfWorkerStr.Content as string)
                {
                    case "Intern":
                        SelectedDepartament.AddWorker(new Workers.Intern(nameworker, ageworker, SelectedDepartament.Id));
                        break;
                    case "Member":
                        SelectedDepartament.AddWorker(new Workers.Intern(nameworker, ageworker, SelectedDepartament.Id));
                        break;
                }
            }
            else
            {
                MessageBox.Show("Worker wasnt added");
                return;
            }
        }

        /// <summary>
        /// Метод, срабатывающий при нажатии на кнопку добавления департамента. Добавляет департамент.
        /// </summary>
        /// <param name="sender">Кнопка, по расположению которой будет нахоидится департамент.</param>
        public void AddDepartamentClick(object sender)
        {
            var a = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Departament;
            if (a == null)
            {
                MessageBox.Show("error", "error");
            }
            var addDepWindow = new AddDepartamentWindow();
            var data = addDepWindow.DataContext as AddDepartamentVM;
            if (addDepWindow.ShowDialog() == true)
            {
                a.AddDepartament(new Departament(data.NewName, a.Id));
            }
            else
            {
                MessageBox.Show("error", "error");
            }
        }

        /// <summary>
        /// Метод, срабатывающий при нажатии на кнопку удаления департамента. Удаляет департамент.
        /// </summary>
        /// /// <param name="sender">Кнопка, по расположению которой будет нахоидится департамент.</param>
        public void RemoveDepartamentClick(object sender)
        {
            var dep = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Departament;
            dep.Delete();
        }

        /// <summary>
        /// Метод, срабатывающий при нажатии на кнопку удаления работника. Удаляет работника.
        /// </summary>
        /// <param name="sender">Кнопка, по расположению которой будет нахоидится работник.</param>
        private void RemoveWorkerClick(object sender)
        {
            var worker = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Worker;
            if (worker.Rank == "Director")
            {
                MessageBox.Show("You can not delete Director. Try to delete Departament.");
                return;
            }
            worker.Delete();
            OnPropertyChanged("Workers");

        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                
        }
    }
}
