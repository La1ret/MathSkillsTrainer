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
        
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly INavigationService _navigationService;
        private readonly IUserSessionService _userSessionService;
        #endregion

        #region Инициализатор

        public UserService( IUserRepository userRepository, 
                            IRoleRepository roleRepository, 
                            INavigationService navigationService, 
                            IUserSessionService userSessionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _navigationService = navigationService;
            _userSessionService = userSessionService;
        }
        #endregion

       
    }
}
