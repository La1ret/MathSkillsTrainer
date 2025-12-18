using MathSkillsTrainer.Common;
using MathSkillsTrainer.Data;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Data.Repositories;
using MathSkillsTrainer.Models;
using MathSkillsTrainer.Services.Interfaces;
using MathSkillsTrainer.Views.Pages;
using MathSkillsTrainer.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathSkillsTrainer.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        #region Объявление сервисов и контекста

        private readonly IServiceProvider _serviceProvider;

        private readonly INavigationService _navigationService;

        private readonly IUserRepository _userRepository;
        #endregion

        #region Инициализатор

        public AuthentificationService(IUserRepository userRepository, INavigationService navigationService, IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Переменные

        private bool isWillLocked;
        private int _failedLoginAttempts = 0;
        #endregion

        #region Свойство блокировки

        private bool _isLocked = false;

        public bool IsLocked
        {
            get => _isLocked;
            private set { _isLocked = value; OnPropertyChanged(); }
        }
        #endregion

        public async Task<string> AuthenticateOrGetErrorAsync(string username, string password)
        {
            if (_userRepository.AuthenticateAsync(username, password).Result == null)
            {
                IncrementFailedAttempts();
                return "Неверный логин или пароль.";
            }

            _failedLoginAttempts = 0;
            _navigationService.ChangeWindowTo<MainWindow>();
            return null;
        }

        #region Методы блокировки окна

        private async Task LockWindowAsync(int seconds)
        {
            IsLocked = true;
            await Task.Delay(seconds * 1000);
            IsLocked = false;
            _failedLoginAttempts = 0;
        }

        private void IncrementFailedAttempts()
        {
            _failedLoginAttempts++;

            if (_failedLoginAttempts >= 3)
            {
                _ = LockWindowAsync(15);
            }
        }
        #endregion 

    }
}
