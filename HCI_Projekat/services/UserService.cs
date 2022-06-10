using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCI_Projekat.services
{
    public class UserService
    {
        public static List<User> Users { get; set; } = new List<User>();

        public UserService()
        {
            User user = new User("Uros", "Prijovic", "admin", "admin", UserType.Manager);
            Users.Add(user);
        }

        public string GetUserType(string username)
        {
            return GetUserByUsername(username).UserType;
        }

        public string LoginUser(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            User user = GetUserByUsername(username);
            if (username == "")
            {
                return "Корисничко име не може бити празно!";
            }
            else if (password == "")
            {
                return "Лозинка не може бити празна!";
            }
            else if (user == null)
            {
                return "Не постоји налог са датим корисничким именом.";
            }
            else if (user.Password != password)
            {
                return "Нетачна лозинка!";
            }
            return null;
        }

        public bool AddUser(User user)
        {
           if (NameIsValid(user.Name) && SurnameIsValid(user.Surname) && UsernameIsValid(user.Username) && PasswordIsValid(user.Password))
            {
                Users.Add(user);
                return true;
            }
            return false;
        }

        public string BasicInfoValidate(string info, string infoType)
        {
            if (info == "")
            {
                return $"{infoType} не може бити празнo!";
            }
            else if (!Regex.IsMatch(info, @"[A-Za-z -]"))
            {
                return $"{infoType} садржи илегалне карактере!";
            }
            return null;
        }

        public string PasswordValidate(string password, string confirmationPassword)
        {
            if (password == "")
            {
                return "Лозинка не може бити празна!";
            }
            else if (confirmationPassword == "")
            {
                return "Потврдна лозинка не може бити празна!";
            }
            else if (!Regex.IsMatch(password, @"[A-Za-z.0-9@!#$]"))
            {
                return "Лозинка сме да садржи: латинична слова, бројеве и специјалне карактере ('.', '!', '@', '#', '$').";
            }
            else if (password.Length < 8 || password.Length > 20)
            {
                return "Лозинка мора садржати између 8 и 20 карактера.";
            }
            else if (password != confirmationPassword)
            {
                return "Лозинке се не поклапају!";
            }
            return null;
        }

        public string UsernameValidate(string username)
        {
            if (username == "")
            {
                return "Корисничко име не може бити празно!";
            }
            else if (!Regex.IsMatch(username, @"[A-Za-z -]"))
            {
                return "Корисничко име сме да садржи: латинична слова, бројеве и специјалне карактере ('.', '!', '@', '#', '$').";
            }
            else
            {
                for (int i = 0; i < Users.Count; i++)
                {
                    if (Users.ElementAt(i).Username == username)
                    {
                        return "Корисничко име је већ у употреби!";
                    }
                }
            }
            return null;
        }

        private User GetUserByUsername(string username)
        {
            foreach (User user in Users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        private bool NameIsValid(string name)
        {
            return BasicInfoValidate(name, "name") == null;
        }

        private bool SurnameIsValid(string surname)
        {
            return BasicInfoValidate(surname, "surname") == null;
        }

        private bool PasswordIsValid(string password)
        {
            return PasswordValidate(password, password) == null;
        }

        private bool UsernameIsValid(string username)
        {
            return UsernameValidate(username) == null;
        }
    }
}
