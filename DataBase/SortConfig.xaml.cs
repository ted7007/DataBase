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
using System.Windows.Shapes;
using DataBase.Workers;

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для SortConfig.xaml
    /// </summary>
    public partial class SortConfig : Window
    {
        public PropertiesToCompare PropertiesToSort { get; private set; }

        public bool IsReverse { get; private set; }

        public SortConfig()
        {
            InitializeComponent();
            SortPropertiesBox.ItemsSource = new string[] { "Age", "Salary", "AgeAndSalary" };
            SortPropertiesBox.SelectedItem = SortPropertiesBox.Items[0];
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            switch(SortPropertiesBox.SelectedItem as string)
            {
                case "Age":
                    PropertiesToSort = PropertiesToCompare.Age;
                    break;
                case "Salary":
                    PropertiesToSort = PropertiesToCompare.Salary;
                    break;
                case "AgeAndSalary":
                    PropertiesToSort = PropertiesToCompare.AgeAndSalary;
                    break;

            }
            IsReverse = IsReverseBox.IsChecked.Value;
            this.DialogResult = true;        
        }
    }
}
