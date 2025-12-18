using MathSkillsTrainer.Common;
using MathSkillsTrainer.Services.Interfaces;
using MathSkillsTrainer.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MathSkillsTrainer.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        #region Объявление сервисов

        private readonly INavigationService _navigationService;
        private readonly IAuthentificationService _authentificationService;
        #endregion

        #region Объявление команд

        public ICommand BackCommand { get; }
        //public ICommand SignUpCommand { get; }
        //public ICommand ChangePasswordVisibility { get; }
        #endregion

        #region Инициализация

        public PasswordRecoveryViewModel(INavigationService navigationService/*, IAuthentificationService authService, ...*/)
        {
            _navigationService = navigationService;
            //_authService = authService;

            BackCommand = new RelayCommand(OnBackCommandExecute);
            //SignUpCommand = new RelayCommand(OnSignUpCommandExecute);
            //ChangePasswordVisibility = new RelayCommand(OnChangePasswordVisibilityExecute, CanChangePasswordVisibilityExecute);
        }
        #endregion


        #region Команда назад

        private void OnBackCommandExecute(object p)
        {
            _navigationService.NavigateToPage<AuthPage>();
        }
        #endregion
    }
}
