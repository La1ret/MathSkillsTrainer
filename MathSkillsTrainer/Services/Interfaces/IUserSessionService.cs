using MathSkillsTrainer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSkillsTrainer.Services.Interfaces
{
    public interface IUserSessionService
    {
        User CurrentUser { get; }
        void StartSession(User user);
        void CreateGuestSession(); 
    }
}
