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
    /// TODO
    /// Matrix Adjacency List 10%
    /// Vertices List 90%
    /// Shortest Path 10%
    /// UI Revamp (Buttons) 0%
    /// Control Panel 10%
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private bool MousePress = false;
        //private Nullable<Point> dragStart = null;

        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }

        public void AddVertex()
        {
            Vertex<Border> addVertex = new Vertex<Border>(Xcoordinate, Ycoordinate, TxtbName.Text);
            addVertex.CreateVertex();
            DataStorage.VertexBorder.Add(addVertex);
            AddElementsToCanvas();

            if (TxtbName.Text == "")
                TxtbName.Text = DataStorage.RecentIDMade;
            

            ListViewVerticesList.Items.Add(new ListViewItem { ID = DataStorage.RecentIDMade, Name = TxtbName.Text});
            TxtbName.Clear();
        }

        public void AddElementsToCanvas()
        {
            CanvasPlane.Children.Clear();

            foreach (var edge in DataStorage.EdgeList)
            {
                bool parentExist1 = false;
                bool parentExist2 = false;
                foreach (var vertex in DataStorage.VertexBorder)
                {
                    if (vertex.Name == edge.Parent1.Name)
                        parentExist1 = true;
                    

                    if (vertex.Name == edge.Parent2.Name)
                        parentExist2 = true;
                }
                if(parentExist1 && parentExist2)
                    CanvasPlane.Children.Add(edge.CreateEdge());
            }

            foreach (var uiElement in DataStorage.CanvasChildrenList)
            {
                CanvasPlane.Children.Add(uiElement);
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            DataStorage.CanvasChildrenList.RemoveAt(ListViewVerticesList.SelectedIndex);
            DataStorage.VertexBorder.RemoveAt(ListViewVerticesList.SelectedIndex);
            DataStorage.VertexLabelList.RemoveAt(ListViewVerticesList.SelectedIndex);
            AddElementsToCanvas();

            if (ListViewVerticesList.SelectedIndex > -1)
                ListViewVerticesList.Items.RemoveAt(ListViewVerticesList.SelectedIndex);
        }
        public void AddEdge()
        {
            string[] tmpArray = TxtbAddEdge.Text.Split(',');
            int counter = 0;
            Vertex<Border>[] getVertex = new Vertex<Border>[2];

            for (int i = 0; i < 2; i++)
            {
                foreach (var vertex in DataStorage.VertexBorder)
                {
                    if (vertex.Name == tmpArray[i])
                    {
                        getVertex[i] = vertex;
                        break;
                    }
                }
            }
            UnWeightedEdge<UIElement> newUnWeightedEdge = new UnWeightedEdge<UIElement>(getVertex[0], getVertex[1]);
            newUnWeightedEdge.CreateEdge();
            DataStorage.EdgeList.Add(newUnWeightedEdge);
            AddElementsToCanvas();

            //Line edgeLine = new Line();
            //edgeLine.X1 = 50;
            //edgeLine.X2 = 10;
            //edgeLine.Y1 = 25;
            //edgeLine.Y2 = 50;
            //edgeLine.Stroke = Brushes.Black;
            //edgeLine.StrokeThickness = 5;
            //edgeLine.Fill = Brushes.Black;

            //Canvas.SetLeft(edgeLine, 100);
            //Canvas.SetTop(edgeLine, 100);
            //CanvasPlane.Children.Add(edgeLine);
        }

        private void ButtonAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            Xcoordinate = Convert.ToDouble(TxtbXaxisManual.Text);
            Ycoordinate = Convert.ToDouble(TxtbYaxisManual.Text);
            AddVertex();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.OriginalSource is Ellipse)
            //{
            //    var element = (UIElement) sender;
            //    MousePress = true;
            //    dragStart = e.GetPosition(element);
            //    element.CaptureMouse();
            //}
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (MousePress == true)
            //{
            //    MousePress = false;
            //    var element = (UIElement) sender;
            //    dragStart = null;
            //    element.ReleaseMouseCapture();
            //}
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            //if(dragStart != null && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    var element = (UIElement)sender;
            //    var p2 = e.GetPosition(c);
            //    Canvas.SetLeft(element, p2.X - dragStart.Value.X);
            //    Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
            //}

        }


        private void CanvasPlane_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.GetPosition(CanvasPlane).X; 
            var y = e.GetPosition(CanvasPlane).Y;

            TxtbXaxisAuto.Text = Math.Round(Convert.ToDouble(x),2).ToString();
            TxtbYaxisAuto.Text = Math.Round(Convert.ToDouble(y), 2).ToString();
        }

        private void CMitemAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            AddVertex();
        }

        private void CanvasPlane_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Xcoordinate = Math.Round(Convert.ToDouble(TxtbXaxisAuto.Text), 4);
            Ycoordinate = Math.Round(Convert.ToDouble(TxtbYaxisAuto.Text), 4);
        }

        private void ButtonAddEdgeClick(object sender, RoutedEventArgs e)
        {
            AddEdge();
        }
    }
}
