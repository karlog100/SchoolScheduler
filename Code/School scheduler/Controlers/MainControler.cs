using System.Windows;
using ViewModels;
using Views;
using Modules;

namespace Controlers
{
    public class MainControler : BaseControlModel
    {
        ViewWindow MainWindow;
        BaseViewModel Current_ViewModel;

        public MainControler() 
        {
            MainWindow = new ViewWindow();
            Current_ViewModel = new LoginViewModel();
            ((LoginViewModel)Current_ViewModel).OnUserLogin += new LoginViewModel.UserLoginHandler(User_Login);
            Current_ViewModel.ShowInWindow(MainWindow);
        }

        private void User_Login(object sender, User Currentuser) 
        {
            ((BaseViewModel)sender).Dispose();

        }
    }
}