using AccessAdministrator.Models;
using AccessAdministrator.Tools;
using AccessAdministrator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AccessAdministrator.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private Page currentPage;

        public Page CurrentPage 
        { 
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }

        public MainWindowVM(System.Windows.Window mainWindow)
        {
            CurrentPage = new SignInPage(mainWindow);
        }


    }
}
