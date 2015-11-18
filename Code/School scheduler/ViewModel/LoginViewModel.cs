using System;
using System.Windows.Input;
using Modules;
using Views;
using System.Windows;

namespace ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public delegate void UserLoginHandler(object sender, User arg);
        public event UserLoginHandler OnUserLogin;

        private User LoginUser;
        private bool _ErrorActive = false;
        private string _ErrorText = "";

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

        public string UserEmail
        {
            get 
            {
                return LoginUser.Email; 
            }
            set
            {
                LoginUser.Email = value;
                try
                {
                    LoginUser.Validate_Email();
                    ErrorText = "";
                }
                catch (Exception ex)
                {
                    ErrorText = ex.Message;
                }
                ErrorActive = false;
                RaisePropertyChanged();
            }
        }

        public LoginViewModel()
        {
            LoginUser = new User();
            this.View = new MainLoginView();
        }

        public ICommand LoginCommand
        {
            get
            {
                return new ActionCommand(p => Login());
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new ActionCommand(p => Register());
            }
        }

        private void Login()
        {
            if (ErrorText == "" && !String.IsNullOrEmpty(UserEmail))
            {
                try
                {
                    LoginUser.GetUser_ByEmail();
                    if (OnUserLogin != null)
                    {
                        OnUserLogin(this, LoginUser);
                    }
                }
                catch (Exception ex)
                {
                    ErrorText = ex.Message;
                    ErrorActive = true;
                }
            }
            else 
            {
                ErrorActive = true;
            }
        }

        private void Register()
        {
            if (!String.IsNullOrEmpty(UserEmail))
            {
                try
                {
                    LoginUser.GetUser_ByEmail();
                    ErrorText = "User already exists";
                    ErrorActive = true;
                    LoginUser = new User();
                }
                catch (ArgumentNullException ex)
                {
                    if (ex.Message.Equals("Value cannot be null.\r\nParameter name: User does not exists"))
                    {
                        ViewWindow RegisterWindow;
                        BaseViewModel RegisterWindow_ViewModel;

                        RegisterWindow = new ViewWindow();
                        RegisterWindow_ViewModel = new RegisterViewModel(LoginUser);
                        ((RegisterViewModel)RegisterWindow_ViewModel).OnUserRegistrated += Registradet;
                        RegisterWindow_ViewModel.ShowInWindow(RegisterWindow);
                    }
                    else 
                    {
                        ErrorText = ex.Message;
                        ErrorActive = true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorText = ex.Message;
                    ErrorActive = true;
                }
            }
            else
            {
                ErrorText = "Missing email";
                ErrorActive = true;
            }
        }

        private void Registradet(object sender, User NewUser) {
            LoginUser = NewUser;
            ((BaseViewModel)sender).Dispose();
            Login();
        }
    }
}
