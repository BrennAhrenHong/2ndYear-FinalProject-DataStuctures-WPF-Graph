using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace TheGraphProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO
        //Main Features
        //Add Vertex Functionality 100%
        //Add Edge Functionality 25%
        //Delete Vertex Functionality 0%
        //Delete Edge Functionality 0%
        //Collision Checker Functionality 0%

        private double SelectedCanvas_XCoordinate { get; set; }
        private double SelectedCanvas_YCoordinate { get; set; }
        protected bool isDragging;
        private Point clickPosition;
        private Point ClickStart;
        private Border SelectedVertexOrigin;
        private TranslateTransform originTT;

        public MainWindow()
        {
            InitializeComponent();
            //ListViewItems = new List<DataTemplate>();
            //DataStorage.ListViewItems.Add(new DataTemplate() {ID = 1 , Name = "Brenn", });
            //DataStorage.ListViewItems.Add(new DataTemplate() {ID = 2 , Name = "Ahren", });
            //DataStorage.ListViewItems.Add(new DataTemplate() {ID = 3 , Name = "C", });
            //DataStorage.ListViewItems.Add(new DataTemplate() {ID = 4 , Name = "H", });
            //VerticesList.ItemsSource = DataStorage.ListViewItems;
            //LinkedList<Int32> x = new LinkedList<int>();
            LoadStackWithIDStartUp();
        }


        //Methods
        public void AddVertex()
        {
            Vertex newVertex = new Vertex(this, TxtbVertexName.Text, DataStorage.IDStack.Peek(),
                Convert.ToInt32(SelectedCanvas_XCoordinate), Convert.ToInt32(SelectedCanvas_YCoordinate));
            newVertex.CreateVertex();
            DataStorage.VertexList.AddLast(newVertex);
            AddChildrenToCanvas();
            DataStorage.ListViewItems.Add(new DataTemplate()
            { ID = DataStorage.IDStack.Pop(), Name = TxtbVertexName.Text, });
            ListViewVerticesList.ItemsSource = "";
            ListViewVerticesList.ItemsSource = DataStorage.ListViewItems;
        }

        public void AddChildrenToCanvas()
        {
            CanvasGraph.Children.Clear();
            if (DataStorage.VertexList.Count != 0)
            {
                foreach (var vertex in DataStorage.VertexList) {
                    CanvasGraph.Children.Add(vertex.VertexStored);
                }

                foreach (var edge in DataStorage.EdgeList){
                    CanvasGraph.Children.Add(edge.EdgeLine);
                }
            }
        }

        public void AddEdge()
        {
            string EdgeType = "";
            if (RadioButtonDirected.IsChecked == true)
            {
                EdgeType = "Directed";
            }

            if (RadioUndirected.IsChecked == true)
            {
                EdgeType = "UnDirected";
            }


            Vertex StartingVertex = null;
            Vertex EndingVertex = null;

            foreach (var vertex in DataStorage.VertexList)
            {
                if (vertex.VertexListViewIdLetter == Convert.ToChar(CmbStartingVertex.SelectedItem))
                    StartingVertex = vertex;
                else if (vertex.VertexListViewIdLetter == Convert.ToChar(CmbEndingVertex.SelectedItem))
                    EndingVertex = vertex;
            }

            if (StartingVertex != null && EndingVertex != null)
            {
                switch (EdgeType)
                {
                    case "Directed":
                    {

                            break;
                    }

                    case "UnDirected":
                    {
                            StartingVertex.IsStartingVertex = true;
                            Edge createUndirectedEdge = new Edge(StartingVertex, EndingVertex);
                            CanvasGraph.Children.Add(createUndirectedEdge.AddEdge());
                            DataStorage.EdgeList.Add(createUndirectedEdge);
                            //DataStorage.VertexList.Find(EdgesConnected.Addlast(createUndirectedEdge));
                            //DataStorage.VertexList[EndingVertexIndex].EdgesConnected.Add(createUndirectedEdge);
                            break;
                    }

                    default:
                        break;
                }
            }

        }

        //Events

        private void BtnAddVertex_Click(object sender, RoutedEventArgs e)
        {
            if (DataStorage.IDStack.Count != 0)
            {
                SelectedCanvas_XCoordinate = Convert.ToInt32(TxtBoxManualXCoords.Text);
                SelectedCanvas_YCoordinate = Convert.ToInt32(TxtBoxManualYCoords.Text);
                AddVertex();
            }
        }

        private void BtnAddEdge_Click(object sender, RoutedEventArgs e)
        {
            AddEdge();
        }

        private void LoadStackWithIDStartUp()
        {
            DataStorage.IDStack.Push('Z');
            for (char i = 'Y'; DataStorage.IDStack.Peek() != 'A'; i--)
            {
                DataStorage.IDStack.Push(i);
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            CmbStartingVertex.Items.Clear();
            foreach (var item in DataStorage.ListViewItems)
            {
                if (DataStorage.SelectedEndingVertex != item.ID)
                    CmbStartingVertex.Items.Add(item.ID);
            }
        }

        private void CmbEndingVertex_DropDownOpened(object sender, EventArgs e)
        {
            CmbEndingVertex.Items.Clear();
            foreach (var item in DataStorage.ListViewItems)
            {
                if (DataStorage.SelectedStartingVertex != item.ID)
                    CmbEndingVertex.Items.Add(item.ID);
            }
        }

        private void CmbStartingVertex_DropDownClosed(object sender, EventArgs e)
        {
            DataStorage.SelectedStartingVertex = Convert.ToChar(CmbStartingVertex.SelectedItem);
        }

        private void CmbEndingVertex_DropDownClosed(object sender, EventArgs e)
        {
            DataStorage.SelectedEndingVertex = Convert.ToChar(CmbEndingVertex.SelectedItem);
        }

        private void CanvasGraph_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedCanvas_XCoordinate = Convert.ToInt32(TxtboxAutoCaptureXCoords.Text);
            SelectedCanvas_YCoordinate = Convert.ToInt32(TxtboxAutoCaptureYCoords.Text);
        }

        private void CMenuItemAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataStorage.IDStack.Count != 0)
                AddVertex();
        }



        private void RadioButtonWeighted_Click(object sender, RoutedEventArgs e)
        {
            TxtbWeight.IsReadOnly = false;
        }


        private void RadioBUnweighted_Click(object sender, RoutedEventArgs e)
        {
            TxtbWeight.Clear();
            TxtbWeight.IsReadOnly = true;
        }

        //Moving Vertex

        public void Vertex_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as Border;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(CanvasGraph);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);

                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
            }
        }
        private void CanvasGraph_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.GetPosition(CanvasGraph).X;
            var y = e.GetPosition(CanvasGraph).Y;

            TxtboxAutoCaptureXCoords.Text = Math.Round(Convert.ToDouble(x)).ToString();
            TxtboxAutoCaptureYCoords.Text = Math.Round(Convert.ToDouble(y)).ToString();
        }

        public void Vertex_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as Border;
            originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            isDragging = true;
            SelectedVertexOrigin = draggableControl; //THE SELECTED VERTEX
            clickPosition = e.GetPosition(CanvasGraph);
            ClickStart = e.GetPosition(CanvasGraph);
            draggableControl.CaptureMouse();
        }

        public void Vertex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            isDragging = false;
            var draggable = sender as Border;
            draggable.ReleaseMouseCapture();

            var x = e.GetPosition(CanvasGraph).X; //(Canvas.GetLeft(SelectedVertexOrigin) + 12.5) - (ClickStart.X - e.GetPosition(CanvasGraph).X);
            var y = e.GetPosition(CanvasGraph).Y; //(Canvas.GetTop(SelectedVertexOrigin) + 12.5) - (ClickStart.Y - e.GetPosition(CanvasGraph).Y);
            Console.WriteLine($"{x} {y}");
                foreach (var vertex in DataStorage.VertexList)
                {
                    if (draggable == vertex.VertexStored)
                    {
                        vertex.VertexXCoords = x;
                        vertex.VertexYCoords = y;
                        foreach (var edge in DataStorage.EdgeList)
                        {
                            if (edge.VertexA.VertexIdNumber == vertex.VertexIdNumber)
                            {
                                edge.VertexA.VertexXCoords = vertex.VertexXCoords;
                                edge.VertexA.VertexYCoords = vertex.VertexYCoords;
                            }

                            if (edge.VertexB.VertexIdNumber == vertex.VertexIdNumber)
                            {
                                edge.VertexB.VertexXCoords = vertex.VertexXCoords;
                                edge.VertexB.VertexYCoords = vertex.VertexYCoords;
                            }

                            edge.ChangeLine();
                            Console.WriteLine($"{edge.EdgeLine.X1} {edge.EdgeLine.Y1} {edge.EdgeLine.X2} {edge.EdgeLine.Y2}");
                        }

                        break;
                    }
                }
            AddChildrenToCanvas();
        }

        private void CMenuItemListViewDeleteSelected_OnClick(object sender, RoutedEventArgs e)
        {
            //ListViewVerticesList.Items.
        }
    }
}