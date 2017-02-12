using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Sandbox.ResizeableFlyout
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Point anchorPoint;
        private bool isInDrag;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MyFlyout.IsOpen = !this.MyFlyout.IsOpen;
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            anchorPoint = e.GetPosition(null);
            element.CaptureMouse();
            isInDrag = true;
            e.Handled = true;
            System.Console.WriteLine("Anchor:" + anchorPoint);

            Mouse.OverrideCursor = Cursors.SizeWE;
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (isInDrag)
                {
                    var element = sender as FrameworkElement;
                    element.ReleaseMouseCapture();
                    isInDrag = false;
                    e.Handled = true;


                    var delta = anchorPoint - e.GetPosition(null);
                    System.Console.WriteLine("delta:" + delta);

                    this.MyFlyout.Width = this.MyFlyout.Width + delta.X;
                }
            }
            finally 
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isInDrag)
            {
                Mouse.OverrideCursor = Cursors.SizeWE;
            }
           
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isInDrag)
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
