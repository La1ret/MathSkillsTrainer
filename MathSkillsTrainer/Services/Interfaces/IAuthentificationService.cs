using MathSkillsTrainer.Common;
using MathSkillsTrainer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace MathSkillsTrainer.Services.Interfaces
{
    public interface IAuthentificationService: INotifyPropertyChanged
    {
        bool IsLocked { get; }
        Task<string> AuthenticateOrGetErrorAsync(string username, string password);
    }
}
