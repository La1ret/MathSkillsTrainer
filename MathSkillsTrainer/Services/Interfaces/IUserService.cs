using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSkillsTrainer.Services.Interfaces
{
    public interface IUserService
    {
        string CreateUserOrGetErrorMessage(string fullName, string email, string username, string password);
    }
}
