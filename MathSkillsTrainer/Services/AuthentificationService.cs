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
            User user = await _userRepository.GetUserByUsernameAsync(username);

            string error = CheckUser(user, password);

            if (string.IsNullOrEmpty(error))
            {
                _failedLoginAttempts = 0;
                _navigationService.ChangeWindowTo<MainWindow>();
            }
            return error;
        }

        #region Методы проверок

        private string CheckUser(User user, string currentPassword)
        {
            switch (user)
            {
                case null:
                    IncrementFailedAttempts(out isWillLocked);
                    if (isWillLocked)
                    {
                        return "Израсходаваны попытки входа. Система заблокированна на 15 секунд";
                    }
                    return $"Данный пользователь не существует или введён неверно! (попыток осаталось: {3 - _failedLoginAttempts})";
                default:
                    return CheckPassword(user, currentPassword);
            }
        }

        private string CheckPassword(User user, string currentPassword)
        {
            bool isPasswordCorrect = PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash);
            if (isPasswordCorrect)
            {
                _navigationService.ChangeWindowTo<MainWindow>();
            }
            else if (!isPasswordCorrect)
            {
                IncrementFailedAttempts(out isWillLocked);
                if (isWillLocked) 
                {
                    return "Израсходаваны попытки входа. Система заблокированна на 15 секунд";
                }
                return $"Не верный пароль! (попыток осаталось: {3 - _failedLoginAttempts})";
            }
            return null;
        }
        #endregion

        #region Методы блокировок

        private async Task LockWindowAsync(int seconds)
        {
            IsLocked = true;
            await Task.Delay(seconds * 1000);
            IsLocked = false;
            _failedLoginAttempts = 0;
        }

        private void IncrementFailedAttempts(out bool isWillLocked)
        {
            isWillLocked = false;
            _failedLoginAttempts++;

            if (_failedLoginAttempts >= 3)
            {
                isWillLocked = true;
                _ = LockWindowAsync(15);
            }
        }
        #endregion 

    }
}
