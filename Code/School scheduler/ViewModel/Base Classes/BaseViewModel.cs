using System;
using System.Windows.Controls;
using Views;

namespace ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IDisposable
    {
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
                View.ShowInWindow(MainWindow, "School Scheduler", 0, 0, Dock.Top);
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
