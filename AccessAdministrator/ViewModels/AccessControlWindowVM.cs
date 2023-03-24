using AccessAdministrator.DB;
using AccessAdministrator.Models;
using AccessAdministrator.Tools;
using AccessAdministrator.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace AccessAdministrator.ViewModels
{
    public class AccessControlWindowVM : BaseVM
    {
        
        private int i = 0;
        private List<Position> positions;
        private Position selectedPosition;

        private bool block = true;
        private byte[] photo;

        public bool Block
        {
            get => block;
            set
            {
                block = value;
                SignalChanged();
            }
        }

        public List<Position> Positions
        {
            get => positions;
            set
            {
                positions = value;
                SignalChanged();
            }
        }

        public Position SelectedPosition
        {
            get => selectedPosition;
            set
            {
                selectedPosition = value;
                SignalChanged();
            }
        }

        public UserWorker UserWorker { get; set; } // Для передачи авторизованного пользователя
        public UserWorker AddUserWorker { get; set; } = new UserWorker();
        public Command Save { get; set; }
        public Command LoadPhoto { get; set; }
        public Command Return { get; set; }

        public AccessControlWindowVM(Models.UserWorker userWorker)
        {
            UserWorker = userWorker;
            UserWorker.Name = UserWorker.Name.First()+".";
            UserWorker.Patronymic = UserWorker.Patronymic.First() + ".";
            Positions = user50_2Context.GetInstance().Positions.ToList();

            AddUserWorker.Photo = File.ReadAllBytes("Images/User.png");
            SignalChanged(nameof(AddUserWorker));

            Timer timer = new Timer();
            timer.Interval = 60 * 5 * 1000;
            timer.Elapsed += Timer_Elapsed;

            Save = new Command(() =>
            {
                if (selectedPosition == null || AddUserWorker.Surname == null || AddUserWorker.Name == null || AddUserWorker.Gender == null || AddUserWorker.PositionId == null)
                {
                    if (i == 1 || i == 3 || i == 5 || i == 7)
                    {
                        Block = false;
                        MessageBox.Show("Окно заблокировано на 5 минут");
                        timer.Start();
                    }
                    MessageBox.Show("Не все поля заполнены");
                    i++;
                }
                else
                {
                    AddUserWorker.PositionId = SelectedPosition.Id;

                    user50_2Context.GetInstance().UserWorkers.Add(AddUserWorker);
                    user50_2Context.GetInstance().SaveChanges();
                    AddUserWorker = new();
                    SelectedPosition = null;
                    AddUserWorker.Photo = File.ReadAllBytes("Images/User.png");
                    SignalChanged(nameof(AddUserWorker));
                    SignalChanged(nameof(SelectedPosition));
                    MessageBox.Show("Данные сохранены");
                }
            });

            LoadPhoto = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if(openFileDialog.ShowDialog() == true)
                {
                    AddUserWorker.Photo = File.ReadAllBytes(openFileDialog.FileName);
                    SignalChanged(nameof(AddUserWorker));
                }
            });

            Return = new Command(() =>
            {
                AddUserWorker = new();
                SelectedPosition = null;
                AddUserWorker.Photo = File.ReadAllBytes("Images/User.png");
                SignalChanged(nameof(AddUserWorker));
                SignalChanged(nameof(SelectedPosition));
                MessageBox.Show("Данные очищены");
            });
            
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Block = true;
            ((Timer)sender).Stop();
        }
    }
}
