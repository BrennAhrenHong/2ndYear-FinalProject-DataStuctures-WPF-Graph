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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool MousePress = false;
        private Nullable<Point> dragStart = null;
        public void AddVertex(int x, int y)
        {
            Ellipse vertexEllipse = new Ellipse();

            vertexEllipse.Height = 20;
            vertexEllipse.Width = 20;
            vertexEllipse.Fill = Brushes.Green;

            Canvas.SetLeft(vertexEllipse, x);
            Canvas.SetTop(vertexEllipse, y);

            CanvasPlane.Children.Add(vertexEllipse);
        }

        public void AddEdge()
        {
            Line edgeLine = new Line();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddVertex(Convert.ToInt32(TxtbXaxis.Text), Convert.ToInt32(TxtbYaxis.Text));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Ellipse)
            {
                var element = (UIElement) sender;
                MousePress = true;
                dragStart = e.GetPosition(element);
                element.CaptureMouse();
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MousePress == true)
            {
                MousePress = false;
                var element = (UIElement) sender;
                dragStart = null;
                element.ReleaseMouseCapture();
            }
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if(dragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UIElement)sender;
                var p2 = e.GetPosition(c);
                Canvas.SetLeft(element, p2.X - dragStart.Value.X);
                Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
            }
        }
    }
}
