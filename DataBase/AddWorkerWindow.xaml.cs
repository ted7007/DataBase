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

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        public string AgeStr { get; private set; }

        public string NameStr { get; private set; }

        public string TypeOfWorkerStr { get; private set; }

        public AddWorkerWindow()
        {
            InitializeComponent();
            string[] typesOfWorker = new string[]{ "Intern","Member" };
            TypeOfWorkerBox.ItemsSource = typesOfWorker;
            TypeOfWorkerBox.SelectedItem = TypeOfWorkerBox.Items[0];
        }

        public void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            byte result;
            if(String.IsNullOrEmpty(AgeBox.Text)|| !Byte.TryParse(AgeBox.Text, out result))
            {
                MessageBox.Show("Введите возраст!");
                return;
            }
            if(String.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("Введите имя!");
                return;
            }

            AgeStr = AgeBox.Text;
            NameStr = NameBox.Text;
            TypeOfWorkerStr = TypeOfWorkerBox.SelectedItem as string;
            this.DialogResult = true;
        }
    }
}
