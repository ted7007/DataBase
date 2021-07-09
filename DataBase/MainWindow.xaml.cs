using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Departament DB;
        public MainWindow()
        {
            InitializeComponent();

            DB = new Departament("-1");
            DB.AddDepartament(Departament.GenerateNewDataBase("DataBase", 20, 100));
            Departaments.ItemsSource = DB.Departaments;
            Departaments.SelectedItemChanged += Departaments_SelectedItemChanged;


        }

        private void Departaments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = sender as TreeView;
            Departament dep = tree.SelectedItem as Departament;
            Workers.ItemsSource = dep.Workers;
            Workers.Items.Refresh();
        }

        private void SortWorkers_Click(object sender, RoutedEventArgs e)
        {
            Departament selectedDepartament = Departaments.SelectedItem as Departament;
            if (selectedDepartament == null)
                return;
            SortConfig configs = new SortConfig();
            configs.Owner = this;
            if (configs.ShowDialog() == true)
            {
                selectedDepartament.SortWorkers(configs.PropertiesToSort, configs.IsReverse);
            }
            else
            {
                MessageBox.Show("ошибка сортировки");
            }

        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
                Departament selectedDep = Departaments.SelectedItem as Departament;
                if (selectedDep is null || selectedDep.Id == "1")
                    return;
            AddWorkerWindow addwindow = new AddWorkerWindow();

            if(addwindow.ShowDialog() == true)
            {
                
                string nameworker = addwindow.NameStr; byte ageworker = Convert.ToByte(addwindow.AgeStr);
                switch(addwindow.TypeOfWorkerStr)
                {
                    case "Intern":
                        selectedDep.AddWorker(new Workers.Intern(nameworker, ageworker, selectedDep.Id));
                        break;
                    case "Member":
                        selectedDep.AddWorker(new Workers.Intern(nameworker, ageworker, selectedDep.Id));
                        break;
                }
            }
            else
            {
                MessageBox.Show("Работник не был добавлен");
                return;
            }
        }

        private void AddDepartament_Click(object sender, RoutedEventArgs e)
        {
            var a = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Departament;
            if(a == null)
            {
                MessageBox.Show("Error", "Error");
            }
            var addDepWindow = new AddDepartamentWindow();
            if(addDepWindow.ShowDialog()==true)
            {
                a.AddDepartament(new Departament(addDepWindow.NewName, a.Id));
            }
            else
            {
                MessageBox.Show("Error", "Error");
            }

        }

        private void RemoveDepartament_Click(object sender, RoutedEventArgs e)
        {
            var dep = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Departament;
            dep.Delete();

        }

        private void RemoveWorker_Click(object sender, RoutedEventArgs e)
        {
            var worker = (((sender as Button).Parent as StackPanel).TemplatedParent as ContentPresenter).Content as Worker;
            if (worker.Rank == "Director")
            {
                MessageBox.Show("You can not delete Director. Try to delete Departament.");
                return;
            }    
            worker.Delete();

        }
    }
}
