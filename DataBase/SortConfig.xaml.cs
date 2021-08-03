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

        public SortConfig()
        {
            InitializeComponent();

            this.DataContext = new SortConfigVM();
        }
    }
}
