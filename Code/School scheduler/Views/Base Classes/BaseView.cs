using System;
using System.Windows.Controls;

namespace Views
{
    public abstract class BaseView : UserControl
    {
        public object DataContext { get; set; }

        private ViewWindow Window;
        public void OpenInWindow(ViewWindow _Window) {
            Window = _Window;
            Window.WindowDockPanel.Children.Add(this);
        }
    }
}
