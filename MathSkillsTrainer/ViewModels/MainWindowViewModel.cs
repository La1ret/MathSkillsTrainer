using MathSkillsTrainer.Common;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSkillsTrainer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;
        #region Заголовок окна

        private string _Title = "Торгове аппараты";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        public MainWindowViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            GetUsernameList();
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
    }
}
