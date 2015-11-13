using System.Windows;
using ViewModels;
using Views;

namespace Controlers
{
    public class MainControler : BaseControlModel
    {
        private ViewWindow Current_View;

        public MainControler() {
            Current_View = new ViewWindow();
            MainViewModel View = new MainViewModel(Current_View);

            Current_View.ShowDialog();
        }
    }
}