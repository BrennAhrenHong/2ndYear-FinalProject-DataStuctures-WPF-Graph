using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Vertices 100%
    /// Edges 80%
    /// Shortest Path 10%
    /// UI Revamp (Buttons) 70%
    /// Control Panel 70%
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           TheMainWindow = this;
        }

        //private bool MousePress = false;
        //private Nullable<Point> dragStart = null;

        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }
        public MainWindow TheMainWindow { get; protected set; }

        public void AddVertex()
        {
            if (DataStorage.VertexLabelList.Count != 26)
            {
                Vertex<Border> addVertex = new Vertex<Border>(Xcoordinate, Ycoordinate, TxtbName.Text, DataStorage.IDNumber);
                addVertex.CreateVertex();
                DataStorage.VerticesList.Add(addVertex);
                AddElementsToCanvas();

                if (TxtbName.Text == "")
                    TxtbName.Text = DataStorage.RecentIDMade;


                ListViewVerticesList.Items.Add(new ListViewItem {ID = DataStorage.RecentIDMade, Name = TxtbName.Text});
                TxtbName.Clear();


                foreach (var col in ((GridView) ListViewVerticesList.View).Columns)
                {
                    if (double.IsNaN(col.Width)) col.Width = col.ActualWidth;
                    col.Width = double.NaN;
                }
            }
        }

        public void AddElementsToCanvas()
        {
            CanvasPlane.Children.Clear();
            
            var tmpList = new List<UnDirectedEdge<UIElement>>();

            foreach (var edge in DataStorage.EdgeList)
            {
                bool parentExist1 = false;
                bool parentExist2 = false;
                foreach (var vertex in DataStorage.VerticesList)
                {
                    if (vertex.Name == edge.FromVertex.Name)
                        parentExist1 = true;
                    

                    if (vertex.Name == edge.ToVertex.Name)
                        parentExist2 = true;
                }

                if (parentExist1 && parentExist2)
                    CanvasPlane.Children.Add(edge.CreateEdge());
                else
                    tmpList.Add(edge);
            }

            foreach (var edge in tmpList)
            {
                DataStorage.EdgeList.Remove(edge);
            }

            foreach (var uiElement in DataStorage.CanvasChildrenList)
            {
                CanvasPlane.Children.Add(uiElement);
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListViewVerticesList.SelectedIndex > -1)
            {
                DataStorage.CanvasChildrenList.RemoveAt(ListViewVerticesList.SelectedIndex);
                DataStorage.VerticesList.RemoveAt(ListViewVerticesList.SelectedIndex);
                DataStorage.VertexLabelList.RemoveAt(ListViewVerticesList.SelectedIndex);
                DataStorage.IDNumber--;
                AddElementsToCanvas();

                ListViewVerticesList.Items.RemoveAt(ListViewVerticesList.SelectedIndex);
            }
        }
        public void AddEdge()
        {
            string[] tmpArray = TxtbAddEdge.Text.Split(',');
            int counter = 0;
            Vertex<Border>[] getVertex = new Vertex<Border>[2];

            for (int i = 0; i < 2; i++)
            {
                foreach (var vertex in DataStorage.VerticesList)
                {
                    if (vertex.Name == tmpArray[i])
                    {
                        getVertex[i] = vertex;
                        break;
                    }
                }
            }
            UnDirectedEdge<UIElement> newUnDirectedEdge = new UnDirectedEdge<UIElement>(getVertex[0], getVertex[1], Convert.ToInt32(TxtbWeight.Text));
            newUnDirectedEdge.CreateEdge();
            DataStorage.EdgeList.Add(newUnDirectedEdge);
            AddElementsToCanvas();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShortestPathLogic logic = new ShortestPathLogic(TheMainWindow);
            logic.FindShortestDistance();
        }

        private void Button_ShowVerticesClick(object sender, RoutedEventArgs e)
        {
            LabelDockPanel.Content = "Vertices List";
            ListViewVerticesList.Visibility = Visibility.Visible;
        }
    }
}
