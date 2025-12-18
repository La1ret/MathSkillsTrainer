using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using MathSkillsTrainer.Data;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Data.Repositories;
using MathSkillsTrainer.Services;
using MathSkillsTrainer.Services.Interfaces;
using MathSkillsTrainer.ViewModels;

namespace MathSkillsTrainer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private ServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            InitializeComponent();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Запуск окна входа
            var authorizationWindow = serviceProvider.GetRequiredService<Views.Windows.Authorization>();
            authorizationWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация контекста БД как Transient с использованием фабрики создания
            services.AddTransient<MathSkillsTrainerDbContext>(_ => new MathSkillsTrainerDbContext());

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IAuthentificationService, AuthentificationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();

            //Views
            services.AddTransient<Views.Windows.Authorization>();
            services.AddTransient<Views.Pages.AuthPage>();
            services.AddTransient<Views.Pages.PasswordRecovery>();
            services.AddTransient<Views.Pages.Registration>();

            services.AddTransient<Views.Windows.MainWindow>();

            //ViewModels
            services.AddTransient<AuthPageViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<PasswordRecoveryViewModel>();
            services.AddTransient<PegistrationViewModel>();

            services.AddTransient<MainWindowViewModel>();
        }
    } 
}
