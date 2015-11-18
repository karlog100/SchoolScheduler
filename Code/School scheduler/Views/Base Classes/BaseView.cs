using System;
using System.Windows;
using System.Windows.Controls;

namespace Views
{
    public abstract class BaseView : UserControl, IDisposable
    {
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

            ViewWindow.ShowDialog();
        }

        public void ShowInWindow(string windowTitle, double windowWidth, double windowHeight, Dock dock)
        {
            ShowInWindow(ViewWindow, windowTitle, windowWidth, windowHeight, dock);
        }

        public void Dispose()
        {
            ViewWindow.WindowDockPanel.Children.Remove(this);
            if (ViewWindow.WindowDockPanel.Children.Count.Equals(0)) 
            {
                ViewWindow.Close();
            }
            viewWindow = null;
        }
    }
}
