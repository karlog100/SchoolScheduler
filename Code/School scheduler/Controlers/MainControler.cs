using System.Windows;
using ViewModels;
using Views;
using Modules;

namespace Controlers
{
    public class MainControler : BaseControlModel
    {
        BaseViewModel Current_ViewModel;
        User CurrentUser;

        private ViewModes CurrentView;
        enum ViewModes { 
            Main,
            Editor,
            Close
        }

        public MainControler() 
        {
            CurrentUser = null;
            CurrentView = ViewModes.Main;
            ViewHandler();
        }

        private void ViewHandler() 
        {
            while (CurrentView != ViewModes.Close) 
            {
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                switch (CurrentView) {
                    case ViewModes.Main:
                        ViewWindow MainWindow = new ViewWindow();
                        Current_ViewModel = new LoginViewModel();
                        ((LoginViewModel)Current_ViewModel).OnUserLogin += new LoginViewModel.UserLoginHandler(User_Login);
                        Current_ViewModel.OnUserClosedWindow += new BaseViewModel.WindowsClosedHandler(User_Close);
                        Current_ViewModel.ShowInWindow(MainWindow);
                        break;
                    case ViewModes.Editor:
                        ViewWindow EditorWindow = new ViewWindow();
                        Current_ViewModel = new EditViewModel(CurrentUser);
                        ((EditViewModel)Current_ViewModel).OnUserEdited += new EditViewModel.UserEditingHandler(User_Edited);
                        Current_ViewModel.OnUserClosedWindow += new BaseViewModel.WindowsClosedHandler(User_Close);
                        Current_ViewModel.ShowInWindow(EditorWindow);
                        break;
                }
            }
        }

        private void User_Close(object sender) {
            ((BaseViewModel)sender).Dispose();
            if (CurrentView == ViewModes.Editor)
            {
                CurrentView = ViewModes.Main;
            }
            else
            {
                CurrentView = ViewModes.Close;
            }
        }

        private void User_Login(object sender, User Currentuser)
        {
            ((BaseViewModel)sender).Dispose();
            CurrentUser = Currentuser;
            CurrentView = ViewModes.Editor;
        }

        private void User_Edited(object sender, User Currentuser)
        {
            ((BaseViewModel)sender).Dispose();
            CurrentView = ViewModes.Main;
        }
    }
}