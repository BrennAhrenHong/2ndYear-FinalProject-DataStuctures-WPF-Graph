using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        protected bool isDragging;

        private WeightedGraph<string> _WeightedGraph;

        private UnWeightedGraph<string> _UnWeightedgraph;

        private GraphLogic.Path _shortestPath;
        private Stack<int> _shortestDestinationPath;

        private Point clickPosition;
        private Point ClickStart;
        private Border SelectedVertexOrigin;
        private TranslateTransform originTT;
        private Vertex x;
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


            MainWindow y = this;
            x = new Vertex(y, "Brenn", 25, 25);
            DataStorage.VerticesList.AddLast(x);
            x.CreateVertex();
            AddChildrenToCanvas();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(x.GetVertex,Convert.ToInt32(TxtbWeight.Text));
            Canvas.SetTop(x.GetVertex, Convert.ToInt32(TxtbWeight.Text));
            AddChildrenToCanvas();
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

            AddChildrenToCanvas();
        }

        public void AddChildrenToCanvas()
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
                    if (edge.IsWeighted == true)
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
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        createLineEdge.AddDirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);
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
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var createLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        createLineEdge.AddUndirectedEdge();
                        DataStorage.EdgeList.Add(createLineEdge);
                    }
                }
            }

            AddChildrenToCanvas();
        }
        
    
        //public void AddWeightedEdge()
        //{
        //    MainWindow mainWindow = this;

        //    string EdgeType = "";
        //    if (RadioButtonDirected.IsChecked == true)
        //    {
        //        EdgeType = "Directed";
        //    }

        //    if (RadioButtonUndirected.IsChecked == true)
        //    {
        //        EdgeType = "UnDirected";
        //    }

        //    Vertex StartingVertex = null;
        //    Vertex EndingVertex = null;

        //    foreach (var vertex in DataStorage.VerticesList)
        //    {
        //        if (vertex.Name == CmbStartingVertex.SelectedItem)
        //            StartingVertex = vertex;
        //        else if (vertex.Name == CmbEndingVertex.SelectedItem)
        //            EndingVertex = vertex;
        //    }

        //    if (StartingVertex != null && EndingVertex != null)
        //    {
        //        switch (EdgeType)
        //        {
        //            case "Directed":
        //            {
        //                if (RadioButtonWeighted.IsChecked != true)
        //                {
        //                        var createDirectedLineEdge =
        //                    new LineEdge(mainWindow, StartingVertex, EndingVertex, Convert.ToInt32(TxtbWeight.Text));

        //                CanvasGraph.Children.Add(createDirectedLineEdge.AddEdge());
        //                DataStorage.EdgeList.Add(createDirectedLineEdge);
        //                }
        //                else
        //                {
        //                    var createDirectedLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex,
        //                        Convert.ToInt32(TxtbWeight.Text));

        //                    createDirectedLineEdge.AddUndirectedEdge(Convert.ToInt32(TxtbWeight.Text));
        //                    DataStorage.EdgeList.Add(createDirectedLineEdge);
        //                }
                        
        //                break;
        //            }

        //            case "UnDirected":
        //                {
        //                    StartingVertex.IsStartingVertex = true;

        //                    if (RadioButtonWeighted.IsChecked != true)
        //                    {
        //                        LineEdge createLineEdge = new LineEdge(mainWindow,StartingVertex, EndingVertex);
        //                        createLineEdge.AddEdge();
        //                        DataStorage.EdgeList.Add(createLineEdge);
        //                    }
        //                    else
        //                    {
        //                        LineEdge createLineEdge = new LineEdge(mainWindow,StartingVertex, EndingVertex,
        //                            Convert.ToInt32(TxtbWeight.Text));
        //                        createLineEdge.AddUndirectedEdge(Convert.ToInt32(TxtbWeight.Text));
        //                        DataStorage.EdgeList.Add(createLineEdge);
        //                    }

        //                    //DataStorage.VerticesList.Find(EdgesConnected.Addlast(createUndirectedEdge));
        //                    //DataStorage.VerticesList[EndingVertexIndex].EdgesConnected.Add(createUndirectedEdge);
        //                    break;
        //                }
        //            default:
        //                break;
        //        }

        //        AddChildrenToCanvas();
        //    }

        //}


        public void UnWeightedGraphLogic()
        {
            var edges = new List<WeightedEdge>();
            foreach (var edge in DataStorage.EdgeList)
            {
                edges.Add(new WeightedEdge(edge.VertexA.VertexIdNumber, edge.VertexB.VertexIdNumber, 0));
            }


            bool isDirected = RadioButtonDirected.IsChecked.Value;

            //var unWeightedGraph = new UnWeightedGraph<string>(edges, DataStorage.VerticesList.Count, isDirected);

            _UnWeightedgraph = new UnWeightedGraph<string>(edges, DataStorage.VerticesList.Count);

            _shortestPath = _UnWeightedgraph.GetShortestPath(Convert.ToInt32(ComboBoxStartingVertexSp.Text));

            try
            {
                    _shortestDestinationPath = _UnWeightedgraph.ShortestPathDestination(_shortestPath, Convert.ToInt32(ComboBoxEndingVertexSp.Text));
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

            int vertexId = 0;
            foreach (var i in DataStorage.UniqueIDList)
            {
                if (i == Convert.ToInt32(ComboBoxStartingVertexSp.Text)) //Must modify to compare name of vertex rather than Id, given selection is by vertex name.
                {
                    vertexId = i;
                }
            }


            //_graph = new WeightedGraph<string>(edges, DataStorage.VerticesList.Count);


            //string path = unWeightedGraph.BreadthFirstSearch(vertexId);
            //TxtboxPath.Text = path.TrimEnd(' ');
            //var shortestPaths = weightedGraph.GetShortestPath(sourceVertex);
            //UnWeightedGraph<int> x = new UnWeightedGraph<int>();
        }

        public void WeightedGraphLogic()
        {
            var edges = new List<WeightedEdge>();

            {
                foreach (var edge in DataStorage.EdgeList)
                {
                    edges.Add(new WeightedEdge(edge.VertexA.VertexIdNumber, edge.VertexB.VertexIdNumber, edge.Weight));
                    if(edge.IsDirected == false)  
                        edges.Add(new WeightedEdge(edge.VertexB.VertexIdNumber, edge.VertexA.VertexIdNumber, edge.Weight));
                }
            }


            _WeightedGraph = new WeightedGraph<string>(edges, DataStorage.VerticesList.Count);
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

            //bool isDirected = RadioButtonDirected.IsChecked.Value;

            //var WeightedGraph = new WeightedGraph<string>(edges, DataStorage.VerticesList.Count);


            //int vertexId = 0;
            //foreach (var i in DataStorage.UniqueIDList)
            //{
            //    if (i == Convert.ToInt32(ComboBoxStartingVertexSp.Text)) //Must modify to compare name of vertex rather than Id, given selection is by vertex name.
            //    {
            //        vertexId = i;
            //    }
            //}


            //var path = WeightedGraph.GetShortestPath(vertexId);
        }

        public void ShowPath()
        {

        }



        public void PrintPath(GraphLogic.Path path)
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

        public void AdjacencyMatrix()
        {

        }


        //Events

        #region Events

        private void BtnSolveShortestPath_Click(object sender, RoutedEventArgs e)
        {
            TxtboxPath.Clear();


            try
            {
                WeightedGraphLogic();
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
        //    if (isDragging && draggableControl != null)
        //    {
        //        Point currentPosition = e.GetPosition(CanvasGraph);
        //        var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
        //        transform.X = originTT.X + (currentPosition.X - clickPosition.X);
        //        transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);

        //        draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
        //    }
        //}

        public void Vertex_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as Border;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(CanvasGraph);

                foreach (var vertex in DataStorage.VerticesList)
                {
                    if (draggableControl == vertex.GetVertex)
                    {

                        vertex.VertexXCoords = currentPosition.X;
                        vertex.VertexYCoords = currentPosition.Y;

                        foreach (var edge in DataStorage.EdgeList)
                        {
                            if (edge.VertexA.VertexIdNumber == vertex.VertexIdNumber)
                            {
                                //edge.VertexA.VertexXCoords = vertex.VertexXCoords;
                                //edge.VertexA.VertexYCoords = vertex.VertexYCoords;
                                Canvas.SetLeft(edge.VertexA.GetVertex, edge.VertexA.VertexXCoords);
                                Canvas.SetTop(edge.VertexA.GetVertex, edge.VertexA.VertexYCoords);
                            }

                            if (edge.VertexB.VertexIdNumber == vertex.VertexIdNumber)
                            {
                                /*edge.VertexB.VertexXCoords = vertex.VertexXCoords;
                                edge.VertexB.VertexYCoords = vertex.VertexYCoords;*/
                                Canvas.SetLeft(edge.VertexB.GetVertex, edge.VertexB.VertexXCoords);
                                Canvas.SetTop(edge.VertexB.GetVertex, edge.VertexB.VertexYCoords);
                            }

                            edge.ChangeLine();
                        }

                        break;
                    }
                }
                AddChildrenToCanvas();
            }
        }


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

            Point currentPosition = e.GetPosition(CanvasGraph);

            var x = e.GetPosition(CanvasGraph).X; //(Canvas.GetLeft(SelectedVertexOrigin) + 12.5) - (ClickStart.X - e.GetPosition(CanvasGraph).X);
            var y = e.GetPosition(CanvasGraph).Y; //(Canvas.GetTop(SelectedVertexOrigin) + 12.5) - (ClickStart.Y - e.GetPosition(CanvasGraph).Y);
            Console.WriteLine($"{x} {y}");

            foreach (var vertex in DataStorage.VerticesList)
            {
                if (draggable == vertex.GetVertex)
                {

                    vertex.VertexXCoords = currentPosition.X;
                    vertex.VertexYCoords = currentPosition.Y;

                    foreach (var edge in DataStorage.EdgeList)
                    {
                        if (edge.VertexA.VertexIdNumber == vertex.VertexIdNumber)
                        {
                            edge.VertexA.VertexXCoords = vertex.VertexXCoords;
                            edge.VertexA.VertexYCoords = vertex.VertexYCoords;
                            //Canvas.SetLeft(edge.VertexA.GetVertex, edge.VertexA.VertexXCoords);
                            //Canvas.SetTop(edge.VertexA.GetVertex, edge.VertexA.VertexYCoords);
                        }

                        if (edge.VertexB.VertexIdNumber == vertex.VertexIdNumber)
                        {
                            edge.VertexB.VertexXCoords = vertex.VertexXCoords;
                            edge.VertexB.VertexYCoords = vertex.VertexYCoords;
                            //Canvas.SetLeft(edge.VertexB.GetVertex, edge.VertexB.VertexXCoords);
                            //Canvas.SetTop(edge.VertexB.GetVertex, edge.VertexB.VertexYCoords);
                        }

                        edge.ChangeLine();
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