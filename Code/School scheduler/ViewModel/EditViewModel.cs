using System;
using System.Windows.Input;
using Modules;
using Views;
using System.Windows;

namespace ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        public delegate void UserEditingHandler(object sender, User arg);
        public event UserEditingHandler OnUserEdited;

        private User Editing_User;
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
                return Editing_User.Name;
            }
            set
            {
                Editing_User.Name = value;
                RaisePropertyChanged();
            }
        }

        public string UserEmail
        {
            get
            {
                return Editing_User.Email;
            }
            set
            {
            }
        }

        public string UserAddress
        {
            get
            {
                return Editing_User.Address;
            }
            set
            {
                Editing_User.Address = value;
                RaisePropertyChanged();
            }
        }

        public string UserPostCode
        {
            get
            {
                return Editing_User.PostCode;
            }
            set
            {
                Editing_User.PostCode = value;
                RaisePropertyChanged();
            }
        }

        public string UserPhone
        {
            get
            {
                return Editing_User.Phone;
            }
            set
            {
                Editing_User.Phone = value;
                RaisePropertyChanged();
            }
        }

        public User.User_Type UserType 
        {
            get {
                return (User.User_Type)Editing_User.Type;
            }
        }
        #endregion
        #region Student Propertys
        public DateTime Education_StartDate
        {
            get
            {
                if (UserType == User.User_Type.Student)
                {
                    return ((Student)Editing_User).Education_StartDate;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime Time = DateTime.Parse(dateTime);
                return Time;
                
            }
            set
            {
                ((Student)Editing_User).Education_StartDate = value;
                RaisePropertyChanged();
            }
        }
        public DateTime Education_EndDate
        {
            get
            {
                if (UserType == User.User_Type.Student)
                {
                    return ((Student)Editing_User).Education_EndDate;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime Time = DateTime.Parse(dateTime);
                return Time;
            }
            set
            {
                ((Student)Editing_User).Education_EndDate = value;
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
                    return ((Teacher)Editing_User).Payrole;
                }
                return 0;
            }
            set
            {
                ((Teacher)Editing_User).Payrole = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new ActionCommand(p => Edit_Command());
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ActionCommand(p => Delete_Command());
            }
        }
        #endregion
        public EditViewModel(User Editing_User)
        {
            if (Editing_User.Type == (int)User.User_Type.Student)
            {
                Student Current = Student.CopyToStudents(Editing_User);
                Current.GetUser_ByEmail();
                this.Editing_User = Current;
            }
            else if (Editing_User.Type == (int)User.User_Type.Teacher)
            {
                Teacher Current = Teacher.CopyToTeacher(Editing_User);
                Current.GetUser_ByEmail();
                this.Editing_User = Current;
            }
            this.View = new EditView();
        }

        private void Edit_Command()
        {
            try
            {
                Editing_User.Save_User();
                if (OnUserEdited != null)
                {
                    OnUserEdited(this, Editing_User);
                }
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorActive = true;
            }
        }

        private void Delete_Command()
        {
            try
            {
                Editing_User.Delete_User();
                if (OnUserEdited != null)
                {
                    OnUserEdited(this, null);
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
