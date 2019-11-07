using System;
using System.Collections.Generic;
using System.Globalization;
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
        public bool IsDragging { get; set; }
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
            
            var tmpList = new List<DirectedEdge<UIElement>>();

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
            int.TryParse(TxtbWeight.Text, out int result);
            
            if (result > 0 && CmbFromVertex.Text != null && CmbToVertex.Text != null)
            {
                string[] tmpArray = new string[2];
                tmpArray[0] = CmbFromVertex.Text;
                tmpArray[1] = CmbToVertex.Text;


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
                DirectedEdge<UIElement> newDirectedEdge = new DirectedEdge<UIElement>(getVertex[0], getVertex[1], Convert.ToInt32(TxtbWeight.Text));
                newDirectedEdge.CreateEdge();

                //ListViewVerticesList.Items.Add(new ListViewItem { Weight = DataStorage.EdgeList[] });
                
                DataStorage.EdgeList.Add(newDirectedEdge);
                AddElementsToCanvas();
            }
        }

        private void ButtonAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            Xcoordinate = Convert.ToDouble(TxtbXaxisManual.Text);
            Ycoordinate = Convert.ToDouble(TxtbYaxisManual.Text);
            AddVertex();
        }

        private void UpdateComboBox()
        {
            foreach (var vertex in DataStorage.VerticesList)
            {
                CmbStartingVertex.Items.Add(vertex.Name);
                CmbEndingVertex.Items.Add(vertex.Name);

                CmbFromVertex.Items.Add(vertex.Name);
                CmbToVertex.Items.Add(vertex.Name);
            }
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

        private void Button_ShortestPathClick(object sender, RoutedEventArgs e)
        {
            if (CmbStartingVertex.SelectedIndex > -1 && CmbEndingVertex.SelectedIndex > -1)
            {
                ShortestPathLogic logic = new ShortestPathLogic(TheMainWindow);
                logic.FindShortestDistance(GetIndexOfVertex(CmbStartingVertex.Text));
                Path();
            }
        }

        private int GetIndexOfVertex(string name)
        {
            int index = -1;
            foreach (var vertex in DataStorage.VerticesList)
            {
                if (vertex.Name == name)
                {
                    index = vertex.IDNumber;
                }
            }

            return index;
        }

        private void Path()
        {
            int prev = 0;
            var pathStack = new Stack<Vertex<Border>>();
            //Find the id of the prevIndex
            foreach (var vertex in DataStorage.VerticesList)
            {
                if (CmbEndingVertex.SelectedItem == vertex.Name)
                {
                    pathStack.Push(vertex);
                    break;
                }
            }

            while (DataStorage.PredecessorList[pathStack.Peek().IDNumber] > -1)
            {
                var x = DataStorage.PredecessorList[pathStack.Peek().IDNumber];
                pathStack.Push(DataStorage.VerticesList[x]);
            }

            TxtbPath.Clear();
            while (pathStack.Count != 1)
            {
                TxtbPath.Text += pathStack.Pop().Name + " -> ";
            }
            TxtbPath.Text += pathStack.Pop().Name;
        }

        private void Button_ShowVerticesClick(object sender, RoutedEventArgs e)
        {
            LabelDockPanel.Content = "Vertices List";
            ListViewVerticesList.Visibility = Visibility.Visible;

            TxtbCost.Visibility = Visibility.Collapsed;
            TxtbPredecessor.Visibility = Visibility.Collapsed;
            LabelDockPanel.Content = "Vertices List";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListViewVerticesList.Visibility = Visibility.Collapsed;
            TxtbCost.Visibility = Visibility.Visible;
            TxtbPredecessor.Visibility = Visibility.Visible;
            LabelDockPanel.Content = "Other Info";
        }

        private void CmbStartingVertex_DropDownOpened(object sender, EventArgs e)
        {
            CmbFromVertex.Items.Clear();
            CmbStartingVertex.Items.Clear();
            UpdateComboBox();
        }

        private void CmbEndingVertex_DropDownOpened(object sender, EventArgs e)
        {
            CmbToVertex.Items.Clear();
            CmbEndingVertex.Items.Clear();
            UpdateComboBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        { 
            DataStorage.IDNumber = 0; 
            DataStorage.RecentIDMade = "";
            
            DataStorage.VertexLabelList = new List<Label>(); 
            DataStorage.EdgeList = new List<DirectedEdge<UIElement>>(); 
            DataStorage.CanvasChildrenList = new List<UIElement>(); 
            DataStorage.VerticesList = new List<Vertex<Border>>(); 
            DataStorage.ListViewId = new List<string>(); 
            DataStorage.Weight = new List<int>(); 
            DataStorage.PredecessorList = new List<int>();
            TxtbPath.Clear();
            TxtbCost.Clear();
            TxtbPredecessor.Clear();
            CanvasPlane.Children.Clear();
            ListViewVerticesList.Items.Clear();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
