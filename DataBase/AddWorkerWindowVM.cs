using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataBase
{
    class AddWorkerWindowVM : INotifyPropertyChanged
    {
        private string ageStr;

        private string nameStr;

        private ComboBoxItem typeOfWorkerStr;

        private ButtonCommand acceptButtonClick;

        public string AgeStr
        {
            get { return ageStr; }
            set
            {
                OnPropertyChanged("AgeStr");
                ageStr = value;
            }
        }

        public string NameStr
        {
            get { return nameStr; }
            set
            {
                OnPropertyChanged("NameStr");
                nameStr = value;
            }
        }

        public ComboBoxItem TypeOfWorkerStr 
        { 
            get { return typeOfWorkerStr; }
            set
            {
                OnPropertyChanged("TypeOfWorkerStr");
                typeOfWorkerStr = value;
            } 
        }

        public ButtonCommand AcceptButtonClick
        {
            get
            {
                return acceptButtonClick ??
                                        (acceptButtonClick = new ButtonCommand(o =>
                                        {
                                            AcceptButton_Click(o);
                                        }));
            }
        }

        public void AcceptButton_Click(object sender)
        {
            byte result;
            if(TypeOfWorkerStr is null||String.IsNullOrWhiteSpace(TypeOfWorkerStr.Content as string))
            {
                MessageBox.Show("Выберите должность!");
                return;
            }

            if (String.IsNullOrEmpty(AgeStr) || !Byte.TryParse(AgeStr, out result))
            {
                MessageBox.Show("Введите возраст!");
                return;
            }
            if (String.IsNullOrEmpty(NameStr))
            {
                MessageBox.Show("Введите имя!");
                return;
            }
            Window window = sender as Window;

            window.DialogResult = true;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
