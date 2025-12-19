using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSkillsTrainer.Common;
using MathSkillsTrainer.Data;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Models;


namespace MathSkillsTrainer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MathSkillsTrainerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(MathSkillsTrainerDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                     r => r.SystemName,
                     new Role
                     {
                         SystemName = "Admin",
                         DisplayName = "Администратор",
                         Description = "Полный доступ ко всем функциям системы."
                     },

                    new Role
                    {
                        SystemName = "Operator",
                        DisplayName = "Оператор",
                        Description = "Доступ к основным рабочим функциям (правка текста и прочего)."
                    },

                    new Role
                    {
                        SystemName = "User",
                        DisplayName = "Пользователь",
                        Description = "Стандартный пользователь системы."
                    },

                    new Role
                    {
                        SystemName = "Guest",
                        DisplayName = "Гость",
                        Description = "Пользователь системы с правами только на просмотр некоторых данных."
                    }
                );

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var adminRoleId = context.Roles.Single(r => r.SystemName == "Admin").RoleId;
                var operatorRoleId = context.Roles.Single(r => r.SystemName == "Operator").RoleId;

                context.Users.AddOrUpdate(
                    u => u.Username,
                    new User { Username = "Admin",
                               PasswordHash = PasswordHasher.HashPassword("1"),
                               FullName = "Майская Мирослава Андреевна",
                               Email = "may@mail.ru",
                               RoleId = adminRoleId},

                    new User { Username = "Manager",
                               PasswordHash = PasswordHasher.HashPassword("123"),
                               FullName = "Пахомов Ярослав Константинович",
                               Email = "Pahomov@yandex.ru",
                               RoleId = operatorRoleId}
                );

                context.SaveChanges();
            }
        }
    }
}
