using MathSkillsTrainer.Common;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Data.Repositories;
using MathSkillsTrainer.Services.Interfaces;
using MathSkillsTrainer.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MathSkillsTrainer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;
        private readonly INavigationService _navigationService;

        public ICommand BackToAuthCommand { get; }

        public MainWindowViewModel(IUserRepository userRepository, INavigationService navigationService)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;

            GetUsernameList();
            BackToAuthCommand = new RelayCommand(OnBackToAuthCommandExecute);
        }
        private async void GetUsernameList() 
        {
            var userLists = await _userRepository.GetAllUsersAsync();
            
            foreach (var user in userLists)
            {
                usernameList.AppendLine(user.Username);
            }
        }

        private StringBuilder usernameList = new StringBuilder();

        public string UserList
        {
            get => usernameList.ToString();
            set => usernameList.ToString();
        }

        private void OnBackToAuthCommandExecute(object p) 
        {
            _navigationService.ChangeWindowTo<Authorization>();
        }
    }
}
