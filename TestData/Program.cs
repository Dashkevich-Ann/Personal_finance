using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Ninject.Modules;
using DataLayer.Models;

namespace TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new NinjectService());
            kernel.Load(Assembly.GetExecutingAssembly());
            var unit = kernel.Get<IUnitOfWork>();
            //CrudIncomeCategory(unit);
            //Console.ReadLine();
            //CrudIncome(unit);
            //Console.ReadLine();
            CrudUser(unit);
            Console.ReadLine();
        }

        private static void CrudUser(IUnitOfWork unit)
        {
            var users = unit.UserRepository.GetAll();
            ShowUsers(users);

            Console.WriteLine("Create New User");
            var newUser = new User
            {
                Login = "user1",
                Password = "user1",
                FirstName = "User",
                SecondName = "1"
            };
            unit.UserRepository.Create(newUser);
            unit.Commit();
            users = unit.UserRepository.GetAll();
            ShowUsers(users);

            Console.WriteLine("Change user's first name and second name");
            var user = unit.UserRepository.Find(o => o.FirstName == newUser.FirstName && o.SecondName == newUser.SecondName).First();
            Console.WriteLine($"Old user's name: {user.FirstName} {user.SecondName}");
            user.FirstName = "NewUser";
            user.SecondName = "New1"; 
            unit.Commit();
            user = unit.UserRepository.Find(o => o.FirstName == newUser.FirstName && o.SecondName == newUser.SecondName).First();
            Console.WriteLine($"New user: {user.FirstName} {user.SecondName}");

            Console.WriteLine("Delete Order");
            unit.UserRepository.Delete(users.FirstOrDefault(o => o.FirstName == user.FirstName));
            unit.Commit();
            users = unit.UserRepository.GetAll();
            ShowUsers(users);
        }

        private static void ShowUsers(IEnumerable<User> users)
        {
            Console.WriteLine("Show Users");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.SecondName} {user.Login} {user.Password}");
            }

            Console.WriteLine();
        }

        private static void CrudIncome(IUnitOfWork unit)
        {
            throw new NotImplementedException();
        }

        private static void CrudIncomeCategory(IUnitOfWork unit)
        {
            throw new NotImplementedException();
        }
    }

    
}
