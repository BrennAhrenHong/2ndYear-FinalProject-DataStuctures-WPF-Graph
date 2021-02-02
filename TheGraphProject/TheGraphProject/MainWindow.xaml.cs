using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using GraphLogic;
using Path = GraphLogic.Path;

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
        //Add LineEdge Functionality 50%
        //UnWeighted & Undirected 100%
        //Unweighted & directed 100%
        //Delete Vertex Functionality 0%
        //Delete LineEdge Functionality 0%
        //Collision Checker Functionality 0%
        //Add adjacency matrix

        private double SelectedCanvas_XCoordinate { get; set; }
        private double SelectedCanvas_YCoordinate { get; set; }
        
        protected bool IsDragging;

        private WeightedGraph<string> _WeightedGraph;

        private Path _shortestPath;
        private Stack<int> _shortestDestinationPath;

        //private Point clickPosition;
        //private Point ClickStart;
        //private Border SelectedVertexOrigin;
        //private TranslateTransform originTT;

        private Vertex x;
        private Line z;
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

            RadioButtonUndirected.IsChecked = true;
            RadioButtonUnweighted.IsChecked = true;

            LoadStackWithIDStartUp();


            //MainWindow y = this;
            //x = new Vertex(y, "Brenn", 25, 25);
            //DataStorage.VerticesList.AddLast(x);
            //x.CreateVertex();
            //RefreshCanvas();

            //z = new Line();
            //z.X1 = 105;
            //z.X2 = 150;
            //z.Y1 = 105;
            //z.Y2 = 150;
            //z.StrokeThickness = 5;
            //z.Fill = Brushes.Black;
            //z.Stroke = Brushes.Black;
            //CanvasGraph.Children.Add(z);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Canvas.SetLeft(x.GetVertex,Convert.ToInt32(TxtbWeight.Text));
            //Canvas.SetTop(x.GetVertex, Convert.ToInt32(TxtbWeight.Text));
            //z.X1 = 200;
            //z.Y1 = 200;
        }
        //Methods
        public void AddVertex()
        {
            Vertex newVertex = new Vertex(this, TxtbVertexName.Text,
                Convert.ToInt32(SelectedCanvas_XCoordinate), Convert.ToInt32(SelectedCanvas_YCoordinate));
            newVertex.CreateVertex();
            DataStorage.VerticesList.AddLast(newVertex);
            DataStorage.ListViewItems.Add(new DataTemplate()
                {ID = DataStorage.VerticesList.Last.Value.VertexIdNumber, Name = TxtbVertexName.Text,});
            ListViewVerticesList.ItemsSource = "";
            ListViewVerticesList.ItemsSource = DataStorage.ListViewItems;

            RefreshCanvas();
        }

        //Refresh the Canvas to display updated values of vertices or edges.
        public void RefreshCanvas()
        {
            CanvasGraph.Children.Clear();
            if (DataStorage.VerticesList.Count != 0)
            {
                foreach (var vertex in DataStorage.VerticesList)
                {
                    //Canvas.SetLeft(vertex.GetVertex,vertex.VertexXCoords);
                    //Canvas.SetTop(vertex.GetVertex,vertex.VertexYCoords); 
                    CanvasGraph.Children.Add(vertex.GetVertex);
                }

                foreach (var edge in DataStorage.EdgeList)
                {
                    CanvasGraph.Children.Add(edge.EdgeLine);
                    if (edge.IsWeighted == true && edge.TxtBlockWeight != null)
                    {
                        CanvasGraph.Children.Add(edge.TxtBlockWeight);
                    }
                }
            }
        }

        // Adding Edge
        public void AddUnweightedEdge()
        {
            MainWindow mainWindow = this;

            bool isDirectedOn = RadioButtonDirected.IsChecked.Value;

            Vertex StartingVertex = null;
            Vertex EndingVertex = null;

            foreach (var vertex in DataStorage.VerticesList)
            {
                if (vertex.VertexIdNumber.ToString() == CmbStartingVertex.SelectedItem.ToString())
                    StartingVertex = vertex;
                else if (vertex.VertexIdNumber.ToString() == CmbEndingVertex.SelectedItem.ToString())
                    EndingVertex = vertex;
            }

            if (StartingVertex != null && EndingVertex != null)
            {

                if (isDirectedOn)
                {

                    if (!RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge =
                            new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn);
                        createLineEdge.AddDirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);

                        createLineEdge.VertexA.EdgeList.AddLast(createLineEdge);
                        createLineEdge.VertexB.EdgeList.AddLast(createLineEdge);
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        createLineEdge.AddDirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);

                        createLineEdge.VertexA.EdgeList.AddLast(createLineEdge);
                        createLineEdge.VertexB.EdgeList.AddLast(createLineEdge);
                    }

                }
                else
                {
                    if (!RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge =
                            new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn);
                        createLineEdge.AddUndirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);

                        createLineEdge.VertexA.EdgeList.AddLast(createLineEdge);
                        createLineEdge.VertexB.EdgeList.AddLast(createLineEdge);
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        createLineEdge.AddUndirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);

                        createLineEdge.VertexA.EdgeList.AddLast(createLineEdge);
                        createLineEdge.VertexB.EdgeList.AddLast(createLineEdge);
                    }
                }
            }

            RefreshCanvas();
        }

        public void GraphShortestPathLogic()
        {
            var edges = new List<WeightedEdge>();

            MainWindow main = this;

            //DataStorage.VerticesList.AddLast(new Vertex(main, "b", 3, 4));
            //DataStorage.VerticesList.AddLast(new Vertex(main, "b", 3, 4));
            //DataStorage.VerticesList.AddLast(new Vertex(main, "b", 3, 4));
            //DataStorage.VerticesList.AddLast(new Vertex(main, "b", 3, 4));


            //DataStorage.EdgeList.Add(new LineEdge(main,DataStorage.VerticesList.First,1,false ));

            {
                foreach (var edge in DataStorage.EdgeList)
                {
                    edges.Add(new WeightedEdge(edge.VertexA.VertexIdNumber, edge.VertexB.VertexIdNumber, edge.Weight));
                    if(edge.IsDirected == false)  
                        edges.Add(new WeightedEdge(edge.VertexB.VertexIdNumber, edge.VertexA.VertexIdNumber, edge.Weight));
                }
            }



            _WeightedGraph = new WeightedGraph<string>(edges, DataStorage.VerticesList.Count);

            var neighbors = _WeightedGraph.Neighbors;

            TxtbAdjacencyList.Clear();
            int counter = 0;
            TxtbAdjacencyList.Text = "Adjacency List: \n";
            foreach (var i in neighbors)
            {
                TxtbAdjacencyList.Text += $"{counter}: ";
                foreach (var array in i)
                {
                    TxtbAdjacencyList.Text += $"({counter},{array}) ";
                }

                TxtbAdjacencyList.Text += "\n";
                counter++;
            }

            TxtbNeighborList.Clear();
            TxtbNeighborList.Text = "Neighbor List: \n";
            counter = 0;
            foreach (var neighbor in neighbors)
            {
                TxtbNeighborList.Text += $"{counter}: ";
                foreach (var i in neighbor)
                {
                    TxtbNeighborList.Text += $"{i}, ";
                }

                TxtbNeighborList.Text += $"\n";

                counter++;
            }



            _shortestPath = _WeightedGraph.GetShortestPath(Convert.ToInt32(ComboBoxStartingVertexSp.Text));


            try
            {
                _shortestDestinationPath = _WeightedGraph.ShortestPathDestination(_shortestPath, Convert.ToInt32(ComboBoxEndingVertexSp.Text));
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                return;
            }

            var sb = new StringBuilder();
            foreach (var i in _shortestDestinationPath)
            {
                sb.Append(i);
            }

            TxtboxPath.Text = sb.ToString();
        }

        public void ShowPath()
        {

        }



        public void PrintPath(Path path)
        {
            TxtbCost.Clear();
            TxtbCost.Text = "Costs: \n";
            for (int i = 0; i < path.Costs.Count; i++)
                TxtbCost.Text += ($"{i,5}");

            TxtbCost.Text += "\n";
            for (int i = 0; i < path.Costs.Count; i++)
            {
                if (path.Costs[i] >= double.MaxValue) TxtbCost.Text += $"{"\u221e",5}";
                else TxtbCost.Text += ($"{path.Costs[i],5}");
            }
            TxtbPredecessor.Clear();

            TxtbPredecessor.Text = "Predecessors:";
            TxtbPredecessor.Text += "\n";

            for (int i = 0; i < path.Costs.Count; i++)
                TxtbPredecessor.Text += $"{i,5}";

            TxtbPredecessor.Text += "\n";
            for (int i = 0; i < path.Costs.Count; i++)
                TxtbPredecessor.Text += $"{path.Predecessor[i],5}";
            TxtbPredecessor.Text += "\n";

            for (int i = 0; i < path.Costs.Count; i++)
                DataStorage.PredecessorList.Add(path.Predecessor[i]);
        }


        //Events

        #region Events

        private void BtnSolveShortestPath_Click(object sender, RoutedEventArgs e)
        {
            TxtboxPath.Clear();


            try
            {
                GraphShortestPathLogic();
            }
            catch (Exception exception)
            {

                CmbStartingVertex.SelectedIndex = -1;
                CmbEndingVertex.SelectedIndex = -1;

            }

        }

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
            AddUnweightedEdge();
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
            //CmbStartingVertex.Items.Clear();
            //foreach (var item in DataStorage.ListViewItems)
            //{
            //    if (DataStorage.SelectedEndingVertex != item.ID)
            //        CmbStartingVertex.Items.Add(item.ID);
            //}

            CmbStartingVertex.Items.Clear();
            foreach (var vertex in DataStorage.VerticesList)
            {
                CmbStartingVertex.Items.Add(vertex.VertexIdNumber);
            }
        }

        private void CmbEndingVertex_DropDownOpened(object sender, EventArgs e)
        {
            //CmbEndingVertex.Items.Clear();
            //foreach (var item in DataStorage.ListViewItems)
            //{
            //    if (DataStorage.SelectedStartingVertex != item.ID)
            //        CmbEndingVertex.Items.Add(item.ID);
            //}

            CmbEndingVertex.Items.Clear();
            foreach (var vertex in DataStorage.VerticesList)
            {
                CmbEndingVertex.Items.Add(vertex.VertexIdNumber);
            }
        }

        private void CmbStartingVertex_DropDownClosed(object sender, EventArgs e)
        {
            //DataStorage.SelectedStartingVertex = Convert.ToChar(CmbStartingVertex.SelectedItem);
        }

        private void CmbEndingVertex_DropDownClosed(object sender, EventArgs e)
        {
            //DataStorage.SelectedEndingVertex = Convert.ToChar(CmbEndingVertex.SelectedItem);
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

        //Moving Vertex

        //public void Vertex_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var draggableControl = sender as Border;
        //    if (IsDragging && draggableControl != null)
        //    {
        //        Point currentPosition = e.GetPosition(CanvasGraph);
        //        var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
        //        transform.X = originTT.X + (currentPosition.X - clickPosition.X);
        //        transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);

        //        draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
        //    }
        //}


        //Tracks pointer position
        private void CanvasGraph_MouseMove(object sender, MouseEventArgs e)
        {
            var draggable = sender as Border;
            var x = e.GetPosition(CanvasGraph).X;
            var y = e.GetPosition(CanvasGraph).Y;

            TxtboxAutoCaptureXCoords.Text = Math.Round(Convert.ToDouble(x)).ToString();
            TxtboxAutoCaptureYCoords.Text = Math.Round(Convert.ToDouble(y)).ToString();

        }


        public void Vertex_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as Border;
            //originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            IsDragging = true;
            //SelectedVertexOrigin = draggableControl; //THE SELECTED VERTEX
            //clickPosition = e.GetPosition(CanvasGraph);
            //ClickStart = e.GetPosition(CanvasGraph);
            draggableControl.CaptureMouse();
        }
        private Vertex LastDraggedVertex { get; set; }
        private Edge LastDraggedLine { get; set; }
        public void Vertex_MouseMove(object sender, MouseEventArgs e)
        {
            //draggablecontrol is a UIelement(Datatype)
            var draggableControl = sender as Border;
            bool noMatch = true;
            // "sender as type Border" allows to move the vertex since the original datatype of the
            // vertex is a Border thus, this further allows interaction between the border and mouse pointer
            // virtually any object in the Canvas with the type "Border" can be interacted
            if (IsDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(CanvasGraph);

                Canvas.SetLeft(draggableControl,currentPosition.X - 12.5); // X
                Canvas.SetTop(draggableControl,currentPosition.Y - 12.5); // Y

                if (LastDraggedVertex == null || LastDraggedVertex.GetVertex != draggableControl)
                {
                    foreach (var vertex in DataStorage.VerticesList)
                    {
                        if (vertex.GetVertex == draggableControl)
                        {
                            if (vertex.EdgesConnected.Count == 0)
                            {
                                vertex.VertexXCoords = currentPosition.X;
                                vertex.VertexYCoords = currentPosition.Y;
                                break;
                            }

                            LastDraggedVertex = vertex;
                            vertex.VertexXCoords = currentPosition.X;
                            vertex.VertexYCoords = currentPosition.Y;

                            break;
                        }
                    }
                }
                else if (LastDraggedVertex != null)
                {
                    foreach (var edgeLine in LastDraggedVertex.EdgeList)
                    {
                        if (edgeLine.VertexA.GetVertex == LastDraggedVertex.GetVertex)
                        {
                            edgeLine.EdgeLine.X1 = currentPosition.X;
                            edgeLine.EdgeLine.Y1 = currentPosition.Y;
                            
                            LastDraggedVertex.VertexXCoords = currentPosition.X;
                            LastDraggedVertex.VertexYCoords = currentPosition.Y;

                            edgeLine.UpdateLine();

                        }
                        else if (edgeLine.VertexB.GetVertex == LastDraggedVertex.GetVertex)
                        {
                            edgeLine.EdgeLine.X2 = currentPosition.X;
                            edgeLine.EdgeLine.Y2 = currentPosition.Y;

                            LastDraggedVertex.VertexXCoords = currentPosition.X;
                            LastDraggedVertex.VertexYCoords = currentPosition.Y;

                            edgeLine.UpdateLine();
                        }
                    }

                    //if (LastDraggedLine.VertexA.GetVertex == LastDraggedVertex)
                    //{
                    //    LastDraggedLine.EdgeLine.X1 = currentPosition.X;
                    //    LastDraggedLine.EdgeLine.Y1 = currentPosition.Y;

                    //    LastDraggedLine.VertexA.VertexXCoords = currentPosition.X - 12.5;
                    //    LastDraggedLine.VertexA.VertexYCoords = currentPosition.Y - 12.5;
                    //}
                    //else if(LastDraggedLine.VertexB.GetVertex == LastDraggedVertex)
                    //{
                    //    LastDraggedLine.EdgeLine.X2 = currentPosition.X;
                    //    LastDraggedLine.EdgeLine.Y2 = currentPosition.Y;

                    //    LastDraggedLine.VertexB.VertexXCoords = currentPosition.X - 12.5;
                    //    LastDraggedLine.VertexB.VertexYCoords = currentPosition.Y - 12.5;
                    //}

                }

                RefreshCanvas();
            }
        }



        public void Vertex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            IsDragging = false;
            var draggable = sender as Border;
            draggable.ReleaseMouseCapture();

            Point currentPosition = e.GetPosition(CanvasGraph);

            var x = e.GetPosition(CanvasGraph).X; //(Canvas.GetLeft(SelectedVertexOrigin) + 12.5) - (ClickStart.X - e.GetPosition(CanvasGraph).X);
            var y = e.GetPosition(CanvasGraph).Y; //(Canvas.GetTop(SelectedVertexOrigin) + 12.5) - (ClickStart.Y - e.GetPosition(CanvasGraph).Y);
            Console.WriteLine($"{x} {y}");

            //foreach (var vertex in DataStorage.VerticesList)
            //{
            //    if (draggable == vertex.GetVertex)
            //    {

            //        vertex.VertexXCoords = currentPosition.X;
            //        vertex.VertexYCoords = currentPosition.Y;

            //        foreach (var edge in DataStorage.EdgeList)
            //        {
            //            if (edge.VertexA.VertexIdNumber == vertex.VertexIdNumber)
            //            {
            //                edge.VertexA.VertexXCoords = vertex.VertexXCoords;
            //                edge.VertexA.VertexYCoords = vertex.VertexYCoords;
            //                //Canvas.SetLeft(edge.VertexA.GetVertex, edge.VertexA.VertexXCoords);
            //                //Canvas.SetTop(edge.VertexA.GetVertex, edge.VertexA.VertexYCoords);
            //            }

            //            if (edge.VertexB.VertexIdNumber == vertex.VertexIdNumber)
            //            {
            //                edge.VertexB.VertexXCoords = vertex.VertexXCoords;
            //                edge.VertexB.VertexYCoords = vertex.VertexYCoords;
            //                //Canvas.SetLeft(edge.VertexB.GetVertex, edge.VertexB.VertexXCoords);
            //                //Canvas.SetTop(edge.VertexB.GetVertex, edge.VertexB.VertexYCoords);
            //            }

            //            edge.UpdateLine();
            //        }

            //        break;
            //    }
            //}

            RefreshCanvas();
        }
        //public void Vertex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{

        //    IsDragging = false;
        //    var draggable = sender as Border;
        //    draggable.ReleaseMouseCapture();

        //    Point currentPosition = e.GetPosition(CanvasGraph);

        //    var x = e.GetPosition(CanvasGraph).X; //(Canvas.GetLeft(SelectedVertexOrigin) + 12.5) - (ClickStart.X - e.GetPosition(CanvasGraph).X);
        //    var y = e.GetPosition(CanvasGraph).Y; //(Canvas.GetTop(SelectedVertexOrigin) + 12.5) - (ClickStart.Y - e.GetPosition(CanvasGraph).Y);
        //    Console.WriteLine($"{x} {y}");

        //    foreach (var vertex in DataStorage.VerticesList)
        //    {
        //        if (draggable == vertex.GetVertex)
        //        {

        //            vertex.VertexXCoords = currentPosition.X;
        //            vertex.VertexYCoords = currentPosition.Y;

        //            foreach (var edge in DataStorage.EdgeList)
        //            {
        //                if (edge.VertexA.VertexIdNumber == vertex.VertexIdNumber)
        //                {
        //                    edge.VertexA.VertexXCoords = vertex.VertexXCoords;
        //                    edge.VertexA.VertexYCoords = vertex.VertexYCoords;
        //                    //Canvas.SetLeft(edge.VertexA.GetVertex, edge.VertexA.VertexXCoords);
        //                    //Canvas.SetTop(edge.VertexA.GetVertex, edge.VertexA.VertexYCoords);
        //                }

        //                if (edge.VertexB.VertexIdNumber == vertex.VertexIdNumber)
        //                {
        //                    edge.VertexB.VertexXCoords = vertex.VertexXCoords;
        //                    edge.VertexB.VertexYCoords = vertex.VertexYCoords;
        //                    //Canvas.SetLeft(edge.VertexB.GetVertex, edge.VertexB.VertexXCoords);
        //                    //Canvas.SetTop(edge.VertexB.GetVertex, edge.VertexB.VertexYCoords);
        //                }

        //                edge.UpdateLine();
        //            }

        //            break;
        //        }
        //    }
        //    RefreshCanvas();
        //}

        private void CMenuItemListViewDeleteSelected_OnClick(object sender, RoutedEventArgs e)
        {
            //ListViewVerticesList.Items.
        }

        private void RadioButtonWeighted_Click(object sender, RoutedEventArgs e)
        {
            TxtbWeight.IsReadOnly = false;
        }

        private void RadioButtonUnweighted_Click(object sender, RoutedEventArgs e)
        {
            TxtbWeight.Clear();
            TxtbWeight.IsReadOnly = true;
        }

        private void RadioButtonDirected_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void RadioButtonUndirected_OnClick(object sender, RoutedEventArgs e)
        {
            
        }


        #endregion

        private void ComboBox_DropDownOpened_1(object sender, EventArgs e)
        {
            ComboBoxStartingVertexSp.Items.Clear();

            foreach (var vertex in DataStorage.VerticesList)
            {
                ComboBoxStartingVertexSp.Items.Add(vertex.VertexIdNumber);
            }
        }

        private void ComboBoxEndingVertexSp_DropDownOpened(object sender, EventArgs e)
        {
            ComboBoxEndingVertexSp.Items.Clear();

            foreach (var vertex in DataStorage.VerticesList)
            {
                ComboBoxEndingVertexSp.Items.Add(vertex.VertexIdNumber);
            }
        }


    }
}