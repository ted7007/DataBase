using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBase
{
    class AddDepartamentVM
    {
        private ButtonCommand acceptButtonCommand;
        public string NewName { get;  set; }

        public ButtonCommand AcceptButtonCommand { get { return acceptButtonCommand ?? (acceptButtonCommand = new ButtonCommand(o => AcceptButtonClick(o))); } }

        public void AcceptButtonClick(object sender)
        {
            if (String.IsNullOrEmpty(NewName))
            {
                MessageBox.Show("Input correct Departament's Name");
                return;
            }
            var window = sender as Window;
            window.DialogResult = true;
        }
    }
}
