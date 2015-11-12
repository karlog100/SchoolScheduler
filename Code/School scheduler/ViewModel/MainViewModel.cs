using System;
using Views;

namespace ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private MainView ThisView;
        public MainViewModel(ViewWindow ThisWindow){
            ThisView = new MainView();
            ThisView.OpenInWindow(ThisWindow);
        }
    }
}
