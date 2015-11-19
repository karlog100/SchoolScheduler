using System;
using System.Windows;
using System.Windows.Controls;

namespace Views
{
    public abstract class BaseView : UserControl, IDisposable
    {
        public delegate void WindowsClosedHandler(object sender);
        public event WindowsClosedHandler OnUserClosedWindow;

        private ViewWindow viewWindow;
        private ViewWindow ViewWindow
        {
            get
            {
                if (viewWindow == null)
                {
                    viewWindow = new ViewWindow();
                }
                return viewWindow;
            }
        }

        public void ShowInWindow(ViewWindow window)
        {
            ShowInWindow(window, window.Title, window.Width, window.Height, Dock.Top);
        }

        public void ShowInWindow(ViewWindow window, string windowTitle, double windowWidth, double windowHeight, Dock dock)
        {
            viewWindow = window;
            ViewWindow.Title = windowTitle;

            DockPanel.SetDock(this, dock);

            ViewWindow.WindowDockPanel.Children.Add(this);

            ViewWindow.MinHeight = 100;
            ViewWindow.MinWidth = 200;

            if (windowWidth == 0 && windowHeight == 0)
            {
                viewWindow.SizeToContent = SizeToContent.WidthAndHeight;
            }
            else
            {
                ViewWindow.SizeToContent = SizeToContent.Manual;
                ViewWindow.Width = windowWidth;
                ViewWindow.Height = windowHeight;
            }

            bool? dialogResult = ViewWindow.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    // User accepted dialog box
                    break;
                case false:
                    // User canceled dialog box
                    WindowClosed();
                    break;
                default:
                    // Indeterminate
                    break;
            }
        }

        public void ShowInWindow(string windowTitle, double windowWidth, double windowHeight, Dock dock)
        {
            ShowInWindow(ViewWindow, windowTitle, windowWidth, windowHeight, dock);
        }

        private void WindowClosed(){
            if (OnUserClosedWindow != null) {
                OnUserClosedWindow(this);
            }
        }

        public void Dispose()
        {
            ViewWindow.WindowDockPanel.Children.Remove(this);
            if (ViewWindow.WindowDockPanel.Children.Count.Equals(0)) 
            {
                if(viewWindow.IsActive)
                    ViewWindow.DialogResult = true;
                ViewWindow.Close();
            }
            viewWindow = null;
        }
    }
}
