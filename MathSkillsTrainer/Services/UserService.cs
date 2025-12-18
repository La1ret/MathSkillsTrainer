using MathSkillsTrainer.Common;
using MathSkillsTrainer.Data;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Models;
using MathSkillsTrainer.Services.Interfaces;
using MathSkillsTrainer.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSkillsTrainer.Services
{
    public class UserService : IUserService
    {
        #region Объявление сервисов и контекста
        
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly INavigationService _navigationService;
        #endregion

        #region Инициализатор

        public UserService(IServiceProvider serviceProvider, IUserRepository userRepository, IRoleRepository roleRepository, INavigationService navigationService)
        {
            _serviceProvider = serviceProvider;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _navigationService = navigationService;
        }
        #endregion

        public string CreateUserOrGetErrorMessage(string fullName, string email, string username, string password) 
        {
            bool isFullName = fullName.Split().Count() >= 2 && fullName.Split().Count() < 4 ? true : false;

            if (!isFullName) 
            {
                return "Вы некорректно ввели ФИО регистрация не возможна";
            }

            if (_userRepository.GetUserByUsernameAsync(username).Result == null)
            {
                var passwordHash = PasswordHasher.HashPassword(password);

                var ф = _roleRepository.GetRoleBySystemNameAsync("User").Result.RoleId;

                User user = new User
                {
                    FullName = fullName,
                    Email = email,
                    Username = username,
                    PasswordHash = passwordHash,
                    RoleId = _roleRepository.GetRoleBySystemNameAsync("User").Result.RoleId
                };

                _userRepository.AddUserAsync(user);

                _navigationService.ChangeWindowTo<MainWindow>(); 
                return null;
            }

            else return "Пользователь с таким логином уже существует. Выберете другой";
        }
    }
}
