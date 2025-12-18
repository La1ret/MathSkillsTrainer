using MathSkillsTrainer.Common;
using MathSkillsTrainer.Models;
using MathSkillsTrainer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSkillsTrainer.Services
{
    public class UserSessionService : ViewModelBase, IUserSessionService
    {
        #region Объявление сервисов
        #endregion

        #region Инициализатор
        public UserSessionService() 
        {
        }
        #endregion

        #region Поля

        private User _currentUser = null;
        #endregion

        #region Свойства
        public User CurrentUser 
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion

        #region Основные методы
        public void StartSession(User user) 
        { 
        
        }

        public void CreateGuestSession() 
        {
        
        }
        #endregion

        #region Вспомогательные
        #endregion
    }
}
