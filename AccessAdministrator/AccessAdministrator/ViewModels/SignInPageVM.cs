using AccessAdministrator.DB;
using AccessAdministrator.Models;
using AccessAdministrator.Tools;
using AccessAdministrator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccessAdministrator.ViewModels
{
    public class SignInPageVM : BaseVM
    {
        public string Login { get; set; }
        public Command SignIn { get; set; }
        private Models.Type selectedType;
        private List<Models.Type> types;

        public Models.Type SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                SignalChanged();
            }
        }

        public List<Models.Type> Types
        { 
            get => types;
            set
            {
                types = value;
                SignalChanged();
            }
        }

        public SignInPageVM(System.Windows.Controls.PasswordBox password, System.Windows.Controls.PasswordBox secretWord, Window mainWindow)
        {
            UserWorker userWorker = null;
            Types = new List<Models.Type>(user50_2Context.GetInstance().Types.ToList());
            
            SignIn = new Command(() =>
            {
                try
                {
                    var userWorker = user50_2Context.GetInstance().UserWorkers.FirstOrDefault(s => s.Password == password.Password && s.Type == SelectedType && s.Login == Login && s.SecretWord == secretWord.Password);
                    if (userWorker == null)
                    {
                        MessageBox.Show("В доступе отказано");
                        return;
                    }
                    else
                    {
                        if(SelectedType.Id == 1)
                        {
                            var window = new AccessControlWindow(userWorker);
                            SelectedType = null;
                            Login = null;
                            password.Password = null;
                            secretWord.Password = null;
                            SignalChanged(nameof(SelectedType));
                            SignalChanged(nameof(Login));
                            SignalChanged(nameof(password.Password));
                            SignalChanged(nameof(secretWord.Password));
                            window.ShowDialog();
                            
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось подключиться к БД");
                    return;
                }
                
            });
        }
    }
}
