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

            DB = new Departament();
            DB.AddDepartament(Departament.GenerateNewDataBase("DataBase", 20, 10));
            Departaments.ItemsSource = DB.Departaments;
            Departaments.SelectedItemChanged += Departaments_SelectedItemChanged;
        }

        private void Departaments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = sender as TreeView;
            Departament dep = tree.SelectedItem as Departament;
            Workers.ItemsSource = dep.Workers.ToArray();
            Workers.Items.Refresh();
        }
    }
}
