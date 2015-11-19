using System;
using System.Windows.Controls;
using Views;

namespace ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IDisposable
    {
        public delegate void WindowsClosedHandler(object sender);
        public event WindowsClosedHandler OnUserClosedWindow;

        private BaseView view;
        protected internal BaseView View 
        {
            get 
            {
                return view;
            }
            set 
            {
                view = value;
                view.DataContext = this;
            }
        }

        public BaseViewModel()
            : this(null)
        {
        }
        public BaseViewModel(BaseView view)
        {
            if (view != null)
            {
                this.view = view;
            }
        }

        public void ShowInWindow(ViewWindow MainWindow)
        {
            if (view != null)
            {
                View.OnUserClosedWindow += new BaseView.WindowsClosedHandler(View_OnUserClosedWindow);
                View.ShowInWindow(MainWindow, "School Scheduler", 0, 0, Dock.Top);
            }
        }

        private void View_OnUserClosedWindow(object sender) {
            if (OnUserClosedWindow != null) {
                OnUserClosedWindow(this);
            }
        }

        public void Dispose()
        {
            if (view != null)
            {
                View.Dispose();
            }
        }
    }
}
