using System;
using System.Collections.Generic;
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
    class SortConfigVM : INotifyPropertyChanged
    {
        public PropertiesToCompare PropertiesToSort { get;  set; }

        private ComboBoxItem propertiesToSortString;

        private bool isReverse;

        public ComboBoxItem PropertiesToSortString 
        {
            get 
            {
                return propertiesToSortString; 
            }
            set
            {
                OnPropertyChanged("PropertiesToSortString");
                propertiesToSortString = value;
            } 
        }

        public bool IsReverse 
        {
            get
            {
               return isReverse;
            }
            set
            {
                OnPropertyChanged("IsReverse");
                isReverse = value;
            } 
        }

        private ButtonCommand acceptButtonClick;

        public ButtonCommand AcceptButtonClick
        {
            get
            {
                
                return acceptButtonClick ??
                (acceptButtonClick = new ButtonCommand(o =>
                {
                    if (PropertiesToSortString is null || String.IsNullOrEmpty(PropertiesToSortString.Content as string))
                    {
                        return;
                    }
                    switch (PropertiesToSortString.Content as string)
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
                    
                    
                    Window wnd = o as Window;
                    wnd.DialogResult = true; 
                    
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

        }
    }
}
