using AccessAdministrator.ViewModels;
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

namespace AccessAdministrator.Views
{
    /// <summary>
    /// Логика взаимодействия для SecurityManagemenetWindow.xaml
    /// </summary>
    public partial class SecurityManagemenetWindow : Window
    {
        public SecurityManagemenetWindow(Models.UserWorker userWorker)
        {
            InitializeComponent();
            DataContext = new SecurityManagemenetWindowVM(userWorker);
        }
    }
}
