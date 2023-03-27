using AccessAdministrator.DB;
using AccessAdministrator.Models;
using AccessAdministrator.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Type = AccessAdministrator.Models.Type;

namespace AccessAdministrator.ViewModels
{
    public class SecurityManagemenetWindowVM : BaseVM
    {
        private List<UserWorker> editUserWorker;
        private List<Type> types;
        private List<UserWorker> accessUserWorker;

        public UserWorker UserWorker { get; set; }
        public Command GetApproved { get ; set;}
        public Command Apply { get; set; }
        public string FIO { get; set; }

        public List<UserWorker> EditUserWorker
        {
            get => editUserWorker;
            set
            {
                editUserWorker = value;
                SignalChanged();
            }
        }

        public List<UserWorker> AccessUserWorker
        {
            get => accessUserWorker;
            set
            {
                accessUserWorker = value;
                SignalChanged();
            }
        }

        public List<Type> Types
        {
            get => types;
            set
            {
                types = value;
                SignalChanged();
            }
        }

        public SecurityManagemenetWindowVM(Models.UserWorker userWorker)
        {
            UserWorker = userWorker;
            FIO = UserWorker.Surname + " " + UserWorker.Name.First() + "." + UserWorker.Patronymic.First() + ".";
            EditUserWorker = user50_2Context.GetInstance().UserWorkers.Include(s => s.Type).Include(s => s.Position).Where(s => s.Approved == 0).ToList();
            AccessUserWorker = user50_2Context.GetInstance().UserWorkers.Include(s => s.Type).Include(s => s.Position).Where(s => s.Approved == 1 && s.CanAddData == 0 && s.CanViewData == 0 && s.CanReport == 0).ToList();
            Types = user50_2Context.GetInstance().Types.ToList();

            GetApproved = new Command(() =>
            {
                foreach (var worker in EditUserWorker)
                {
                    if (worker.IsApproved)
                    {
                        if (worker.Type == null || string.IsNullOrEmpty(worker.Login) || string.IsNullOrEmpty(worker.Password) || string.IsNullOrEmpty(worker.SecretWord))
                        {
                            MessageBox.Show("У пользователя заполнены не все поля");
                            return;
                        }
                    }
                }
                user50_2Context.GetInstance().SaveChanges();
                EditUserWorker = new List<UserWorker>(user50_2Context.GetInstance().UserWorkers.Include(s => s.Type).Include(s => s.Position).Where(s => s.Approved == 0).ToList());
            });

            Apply = new Command(() =>
            {
                user50_2Context.GetInstance().SaveChanges();
                AccessUserWorker = new List<UserWorker>(user50_2Context.GetInstance().UserWorkers.Include(s => s.Type).Include(s => s.Position).Where(s => s.Approved == 1 && s.CanAddData == 0 && s.CanViewData == 0 && s.CanReport == 0).ToList());
            });
        }
    }
}
