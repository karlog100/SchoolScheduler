﻿using System;
using System.Windows.Input;
using Modules;
using Views;
using System.Windows;

namespace ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public delegate void UserRegistrationHandler(object sender, User arg);
        public event UserRegistrationHandler OnUserRegistrated;

        private User RegistrateUser;
        private bool _ErrorActive = false;
        private string _ErrorText = "";

        #region Error Propertys
        public string ErrorText
        {
            get
            {
                return _ErrorText;
            }
            set
            {
                _ErrorText = value;
                RaisePropertyChanged();
            }
        }

        public bool ErrorActive
        {
            get
            {
                return _ErrorActive;
            }
            set
            {
                _ErrorActive = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region User Propertys
        public string UserName
        {
            get
            {
                return RegistrateUser.Name;
            }
            set
            {
                RegistrateUser.Name = value;
                RaisePropertyChanged();
            }
        }

        public string UserEmail
        {
            get
            {
                return RegistrateUser.Email;
            }
            set
            {
            }
        }

        public string UserAddress
        {
            get
            {
                return RegistrateUser.Address;
            }
            set
            {
                RegistrateUser.Address = value;
                RaisePropertyChanged();
            }
        }

        public string UserPostCode
        {
            get
            {
                return RegistrateUser.PostCode;
            }
            set
            {
                RegistrateUser.PostCode = value;
                RaisePropertyChanged();
            }
        }

        public string UserPhone
        {
            get
            {
                return RegistrateUser.Phone;
            }
            set
            {
                RegistrateUser.Phone = value;
                RaisePropertyChanged();
            }
        }

        public User.User_Type UserType 
        {
            get {
                return (User.User_Type)RegistrateUser.Type;
            }
            set {
                RegistrateUser.Type = (int)value;
                if (RegistrateUser.Type == (int)User.User_Type.Student)
                    RegistrateUser = Student.CopyToStudents(RegistrateUser);
                if (RegistrateUser.Type == (int)User.User_Type.Teacher)
                    RegistrateUser = Teacher.CopyToTeacher(RegistrateUser);
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Student propertys
        public DateTime Education_StartDate
        {
            get
            {
                if (UserType == User.User_Type.Student)
                {
                    return ((Student)RegistrateUser).Education_StartDate;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime Time = DateTime.Parse(dateTime);
                return Time;

            }
            set
            {
                ((Student)RegistrateUser).Education_StartDate = value;
                RaisePropertyChanged();
            }
        }
        public DateTime Education_EndDate
        {
            get
            {
                if (UserType == User.User_Type.Student)
                {
                    return ((Student)RegistrateUser).Education_EndDate;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime Time = DateTime.Parse(dateTime);
                return Time;
            }
            set
            {
                ((Student)RegistrateUser).Education_EndDate = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Teacher Propertys
        public int Payrole
        {
            get
            {
                if (UserType == User.User_Type.Teacher)
                {
                    return ((Teacher)RegistrateUser).Payrole;
                }
                return 0;
            }
            set
            {
                ((Teacher)RegistrateUser).Payrole = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new ActionCommand(p => Register());
            }
        }
        #endregion
        public RegisterViewModel(User Register_User)
        {
            RegistrateUser = Register_User;
            UserType = User.User_Type.Student;
            this.View = new RegisterView();
        }

        private void Register()
        {   
            try
            {
                RegistrateUser.Save_User();
                if (OnUserRegistrated != null)
                {
                    OnUserRegistrated(this, RegistrateUser);
                }
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorActive = true;
            }
        }
    }
}
