using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Media;
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
        //Add LineEdge Functionality 90%
        //UnWeighted & Undirected 100%
        //Unweighted & directed 100%
        //Delete Vertex Functionality 100%
        //Delete LineEdge Functionality 0%
        //Collision Checker Functionality 0%
        //Add adjacency matrix

        private double SelectedCanvas_XCoordinate { get; set; }
        private double SelectedCanvas_YCoordinate { get; set; }
        
        protected bool IsDragging;

        private WeightedGraph<string> _WeightedGraph;

        private Path _shortestPath;
        private Stack<int> _shortestDestinationPath;
        private Queue<string> PathQueue = new Queue<string>();

        private Vertex x;
        private Line z;
        public MainWindow()
        {
            InitializeComponent();
            //VerticesListViewItems = new List<ListViewVerticesTemplate>();
            //DataStorage.VerticesListViewItems.Add(new ListViewVerticesTemplate() {ID = 1 , Name = "Brenn", });
            //DataStorage.VerticesListViewItems.Add(new ListViewVerticesTemplate() {ID = 2 , Name = "Ahren", });
            //DataStorage.VerticesListViewItems.Add(new ListViewVerticesTemplate() {ID = 3 , Name = "C", });
            //DataStorage.VerticesListViewItems.Add(new ListViewVerticesTemplate() {ID = 4 , Name = "H", });
            //VerticesList.ItemsSource = DataStorage.VerticesListViewItems;
            //LinkedList<Int32> x = new LinkedList<int>();
            TextBlockLineEdgeErrorMsg.Visibility = Visibility.Hidden;

            RadioButtonUndirected.IsChecked = true;
            RadioButtonUnweighted.IsChecked = true;

            if (RadioButtonUnweighted.IsChecked.Value)
                TxtbWeight.IsReadOnly = true;

            ListViewVerticesList.ItemsSource = DataStorage.VerticesListViewItems;
            ListViewEdgeList.ItemsSource = DataStorage.EdgesListViewItems;

        }

        //Methods
        public void AddVertex()
        {
            Vertex newVertex = new Vertex(this, TxtbVertexName.Text,
                Convert.ToInt32(SelectedCanvas_XCoordinate), Convert.ToInt32(SelectedCanvas_YCoordinate));
            newVertex.CreateVertex();
            DataStorage.VerticesList.Add(newVertex);

            DataStorage.VerticesListViewItems.Add(new ListViewVerticesTemplate(newVertex.Name, newVertex.ID.ToString())
                {ID = DataStorage.VerticesList[DataStorage.VerticesList.Count - 1].ID.ToString(), Name = newVertex.Name,});
            ListViewVerticesList.Items.Refresh();

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
                    if(vertex.IsDeleted)
                        continue;
                    //Canvas.SetLeft(vertex.GetVertex,vertex.VertexXCoords);
                    //Canvas.SetTop(vertex.GetVertex,vertex.VertexYCoords); 
                    CanvasGraph.Children.Add(vertex.GetVertex);
                    CanvasGraph.Children.Add(vertex.VertexLabel);
                    vertex.UpdateNameLabelLocation();
                }

                foreach (var edge in DataStorage.EdgeList)
                {
                    if (edge.IsDeleted)
                        continue;

                    CanvasGraph.Children.Add(edge.EdgeLine);
                    if (edge.IsWeighted == true && edge.TxtBlockWeight != null)
                    {
                        CanvasGraph.Children.Add(edge.TxtBlockWeight);
                    }
                }
            }
        }

        // Adding Edge
        public void AddEdge()
        {
            if (CmbStartingVertex.Text == "" || CmbEndingVertex.Text == "")
                return;

            MainWindow mainWindow = this;

            bool isDirectedOn = RadioButtonDirected.IsChecked.Value;

            Vertex StartingVertex = null;
            Vertex EndingVertex = null;

            foreach (var vertex in DataStorage.VerticesList)
            {
                if (vertex.ID.ToString() == CmbStartingVertex.SelectedItem.ToString())
                    StartingVertex = vertex;
                else if (vertex.ID.ToString() == CmbEndingVertex.SelectedItem.ToString())
                    EndingVertex = vertex;
            }

            if (StartingVertex != null && EndingVertex != null)
            {

                if (isDirectedOn)
                {

                    if (!RadioButtonWeighted.IsChecked.Value)
                    {
                        var newLineEdge =
                            new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn);
                        newLineEdge.AddDirectedEdge();
                        DataStorage.EdgeList.Add(newLineEdge);

                        newLineEdge.VertexA.EdgeList.AddLast(newLineEdge);
                        newLineEdge.VertexB.EdgeList.AddLast(newLineEdge);


                        DataStorage.EdgesListViewItems.Add(new ListViewEdgeTemplate(newLineEdge.VertexA, newLineEdge.VertexB, newLineEdge.Weight.ToString())
                            {
                                Edge = newLineEdge.VertexA.ID + " -> " + newLineEdge.VertexB.ID,
                            Name = newLineEdge.VertexA.Name + " -> " + newLineEdge.VertexB.Name, Weight = "N/A"});
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var newLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        newLineEdge.AddDirectedEdge();
                        DataStorage.EdgeList.Add(newLineEdge);

                        newLineEdge.VertexA.EdgeList.AddLast(newLineEdge);
                        newLineEdge.VertexB.EdgeList.AddLast(newLineEdge);

                        DataStorage.EdgesListViewItems.Add(new ListViewEdgeTemplate(newLineEdge.VertexA, newLineEdge.VertexB, newLineEdge.Weight.ToString())
                            {
                                Edge = newLineEdge.VertexA.ID + " -> " + newLineEdge.VertexB.ID,
                                Name = newLineEdge.VertexA.Name + " -> " + newLineEdge.VertexB.Name});
                    }

                }
                else
                {
                    if (!RadioButtonWeighted.IsChecked.Value)
                    {
                        var newLineEdge =
                            new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn);
                        newLineEdge.AddUndirectedEdge();
                        DataStorage.EdgeList.Add(newLineEdge);

                        newLineEdge.VertexA.EdgeList.AddLast(newLineEdge);
                        newLineEdge.VertexB.EdgeList.AddLast(newLineEdge);


                        DataStorage.EdgesListViewItems.Add(new ListViewEdgeTemplate(newLineEdge.VertexA, newLineEdge.VertexB, newLineEdge.Weight.ToString())
                        {
                            Edge = newLineEdge.VertexA.ID + " <-> " + newLineEdge.VertexB.ID,
                            Name = newLineEdge.VertexA.Name + " <-> " + newLineEdge.VertexB.Name, Weight = "N/A"
                        });
                    }
                    else if (RadioButtonWeighted.IsChecked.Value)
                    {
                        var newLineEdge = new LineEdge(mainWindow, StartingVertex, EndingVertex, isDirectedOn,
                            Convert.ToInt32(TxtbWeight.Text));
                        newLineEdge.AddUndirectedEdge();
                        DataStorage.EdgeList.Add(newLineEdge);

                        newLineEdge.VertexA.EdgeList.AddLast(newLineEdge);
                        newLineEdge.VertexB.EdgeList.AddLast(newLineEdge);


                        DataStorage.EdgesListViewItems.Add(new ListViewEdgeTemplate(newLineEdge.VertexA, newLineEdge.VertexB, newLineEdge.Weight.ToString())
                        {
                            Edge = newLineEdge.VertexA.ID + " <-> " + newLineEdge.VertexB.ID,
                            Name = newLineEdge.VertexA.Name + " <-> " + newLineEdge.VertexB.Name
                        });
                    }
                }
            }

            RefreshCanvas();
        }

        //Logic to Solve Shortest Path
        public void GraphShortestPathLogic()
        {
            var edges = new List<WeightedEdge>(); // Need to store edges to Type WeightedEdge


            foreach (var edge in DataStorage.EdgeList)
            {
                    edges.Add(new WeightedEdge(edge.VertexA.ID, edge.VertexB.ID, edge.Weight));
                    if(edge.IsDirected == false)  //Need to create another edge going to the opposite in order to make it undirected.
                        edges.Add(new WeightedEdge(edge.VertexB.ID, edge.VertexA.ID, edge.Weight));
            }
            


            _WeightedGraph = new WeightedGraph<string>(edges, DataStorage.VerticesList.Count);//Instantiate

            var neighbors = _WeightedGraph.Neighbors;//Create Neighbors List

            //Adjacency List Output Box
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

            //NeighborList Output
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


            //GetShortestPath From the source to all vertices
            _shortestPath = _WeightedGraph.GetShortestPath(Convert.ToInt32(CmbStartingVertexSp.Text));


            try
            {
                //Find Shortest path to Destination from Source
                _shortestDestinationPath = _WeightedGraph.ShortestPathDestination(_shortestPath, Convert.ToInt32(CmbEndingVertexSp.Text));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Data);
                Console.WriteLine(exception.StackTrace);
                //MessageBox.Show("Impossible to traverse", "Warning", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);

                TxtboxPath.Text = "Impossible to traverse. No Path Available!";

                return;
            }


            //Get path for visualization
            //(A,B)
            foreach (var i in _shortestDestinationPath)
            {
                PathQueue.Enqueue(i.ToString());
                DataStorage.VerticesList[i].GetVertex.BorderBrush = Brushes.Orange;
            }


            var sb = new StringBuilder();
            while (_shortestDestinationPath.Count > 1)
            {
                sb.Append(_shortestDestinationPath.Pop() + " -> ");
            }

            sb.Append(_shortestDestinationPath.Pop());


            TxtboxPath.Text = sb.ToString();
            string path = TxtboxPath.Text;

            
            //foreach (var p in path)
            //{
            //    pathQueue.Enqueue(p);
            //}

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

            if (CmbStartingVertexSp.Text == CmbEndingVertexSp.Text)
            {
                TxtboxPath.Text = CmbStartingVertexSp.Text;
                return;
            }

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
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewEdgeList.SelectedIndex > -1 && ListViewVerticesList.SelectedIndex > -1)
                return;

            #region ListView

            //var getItem = ListViewVerticesList.Items[ListViewVerticesList.SelectedIndex];
            //var getVertex = ((ListViewVerticesTemplate)getItem).ID;

            //LinkedList<LineEdge> getVertexEdgeList = null;
            //foreach (var vertex in DataStorage.VerticesList)
            //{
            //    if (vertex.ID == Convert.ToInt32(getVertex))
            //    {
            //        getVertexEdgeList = vertex.EdgeList;
            //        vertex.IsDeleted = true;
            //        foreach (var lineEdge in vertex.EdgeList)
            //        {
            //            lineEdge.IsDeleted = true;
            //        }

            //        break;
            //    }
            //}

            //var edgesToBeDeletedStack = new Stack<ListViewEdgeTemplate>();
            //int counter = 0;
            //foreach (var lineEdge in DataStorage.EdgesListViewItems)
            //{
            //    var getId = lineEdge.Edge.TrimEnd('1', '2', '3', '4', '5', '6', '7', '8', '9', '0');
            //    getId = getId.TrimEnd('>', '-', '<', ' ');

            //    foreach (var edge in getVertexEdgeList)
            //    {
            //        if (getId == edge.VertexA.ID.ToString())
            //        {
            //            edgesToBeDeletedStack.Push(lineEdge);
            //            counter++;
            //            break;
            //        }
            //    }

            //    if (counter == getVertexEdgeList.Count)
            //        break;
            //}

            //while (edgesToBeDeletedStack.Count > 0)
            //{
            //    DataStorage.EdgesListViewItems.Remove(edgesToBeDeletedStack.Pop());
            //}
            //DataStorage.VerticesListViewItems.Remove((ListViewVerticesTemplate)getItem);

            //CmbStartingVertex.SelectedIndex = -1;
            //CmbEndingVertex.SelectedIndex = -1;

            //CmbStartingVertexSp.SelectedIndex = -1;
            //CmbEndingVertexSp.SelectedIndex = -1;

            //ListViewVerticesList.Items.Refresh();
            //ListViewEdgeList.Items.Refresh();
            //RefreshCanvas();

            #endregion

            if (ListViewVerticesList.SelectedIndex > -1)
            {
                var getItem = ListViewVerticesList.Items[ListViewVerticesList.SelectedIndex];
                var getVertex = ((ListViewVerticesTemplate) getItem);

                LinkedList<LineEdge> getVertexEdgeList = null;

                //Get edgelist of vertex
                foreach (var vertex in DataStorage.VerticesList)
                {
                    if (vertex.ID.ToString() == getVertex.ID)    
                    {
                        getVertexEdgeList = vertex.EdgeList;
                        vertex.IsDeleted = true;
                        foreach (var lineEdge in vertex.EdgeList)
                        {
                            lineEdge.IsDeleted = true;
                        }

                        break;
                    }
                }

                var edgesToBeDeletedStack = new Stack<ListViewEdgeTemplate>();
                int counter = 0;
                //To remove ListViewItems
                foreach (var lineEdge in DataStorage.EdgesListViewItems)
                {
                    var getId = lineEdge.VertexA.ID;

                    foreach (var edge in getVertexEdgeList)
                    {
                        if (getId == edge.VertexA.ID)
                        {
                            edgesToBeDeletedStack.Push(lineEdge);
                            counter++;
                            break;
                        }
                    }

                    if (counter == getVertexEdgeList.Count)
                        break;
                }

                while (edgesToBeDeletedStack.Count > 0)
                {
                    DataStorage.EdgesListViewItems.Remove(edgesToBeDeletedStack.Pop());
                }
                DataStorage.VerticesListViewItems.Remove((ListViewVerticesTemplate)getItem);

                CmbStartingVertex.SelectedIndex = -1;
                CmbEndingVertex.SelectedIndex = -1;

                CmbStartingVertexSp.SelectedIndex = -1;
                CmbEndingVertexSp.SelectedIndex = -1;

                ListViewVerticesList.Items.Refresh();
                ListViewEdgeList.Items.Refresh();
                RefreshCanvas();
            }
            else if (ListViewEdgeList.SelectedIndex > -1)
            {
                var getItem = ListViewEdgeList.Items[ListViewEdgeList.SelectedIndex];
                var getEdge = ((ListViewEdgeTemplate) getItem);

                foreach (var lineEdge in DataStorage.EdgeList)
                {
                    if (lineEdge.VertexA == getEdge.VertexA)
                    {
                        lineEdge.IsDeleted = true;
                        lineEdge.VertexA.EdgeList.Remove(lineEdge);
                        lineEdge.VertexA.EdgesConnected.Remove(lineEdge.EdgeLine);

                        lineEdge.VertexB.EdgeList.Remove(lineEdge);
                        lineEdge.VertexB.EdgesConnected.Remove(lineEdge.EdgeLine);
                        //DataStorage.EdgesListViewItems.Remove(lineEdge.EdgeLine);
                    }
                }


                ListViewEdgeList.Items.Refresh();
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
            if (TextBlockLineEdgeErrorMsg.Visibility == Visibility.Visible)
                TextBlockLineEdgeErrorMsg.Visibility = Visibility.Hidden;

            if (CmbStartingVertex.Text == CmbEndingVertex.Text)
            {
                SystemSounds.Exclamation.Play();
                TextBlockLineEdgeErrorMsg.Visibility = Visibility.Visible;
                return;
            }

            //DataStorage.VerticesList.Find(CmbStartingVertex.Text);

            try
            {
                AddEdge();
                ListViewEdgeList.Items.Refresh();
            }
            catch (Exception exception)
            {
                SystemSounds.Exclamation.Play();
                TextBlockLineEdgeErrorMsg.Visibility = Visibility.Visible;
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            CmbStartingVertex.Items.Clear();
            foreach (var vertex in DataStorage.VerticesList)
            {
                if(vertex.IsDeleted)
                    continue;

                CmbStartingVertex.Items.Add(vertex.ID);
            }
        }

        private void CmbEndingVertex_DropDownOpened(object sender, EventArgs e)
        {

            CmbEndingVertex.Items.Clear();
            foreach (var vertex in DataStorage.VerticesList)
            {
                if (vertex.IsDeleted)
                    continue;

                CmbEndingVertex.Items.Add(vertex.ID);
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

        //Tracks pointer position
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
            IsDragging = true;

            try
            {
                //if (draggableControl.Background != Brushes.Orange)
                    draggableControl.Background = Brushes.ForestGreen;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }


            draggableControl.CaptureMouse();
        }
        private Vertex LastDraggedVertex { get; set; }
        private Edge LastDraggedLine { get; set; }
        public void Vertex_MouseMove(object sender, MouseEventArgs e)
        {
            //draggablecontrol is a UIelement(Datatype)
            var draggableControl = sender as Border;
<<<<<<< HEAD

=======
            bool noMatch = true;
>>>>>>> parent of cffbbe3... need arrowhead
            // "sender as type Border" allows to move the vertex since the original datatype of the
            // vertex is a Border thus, this further allows interaction between the border and mouse pointer
            // virtually any object in the Canvas with the type "Border" can be interacted
            if (IsDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(CanvasGraph);

                Canvas.SetLeft(draggableControl,currentPosition.X - 12.5); // X
                Canvas.SetTop(draggableControl,currentPosition.Y - 12.5); // Y

<<<<<<< HEAD

=======
>>>>>>> parent of cffbbe3... need arrowhead
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
                                vertex.UpdateNameLabelLocation();
                                break;
                            }

                            LastDraggedVertex = vertex;
                            vertex.VertexXCoords = currentPosition.X;
                            vertex.VertexYCoords = currentPosition.Y;
                            vertex.UpdateNameLabelLocation();

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

                            LastDraggedVertex.UpdateNameLabelLocation();

                            edgeLine.UpdateLine();

                        }
                        else if (edgeLine.VertexB.GetVertex == LastDraggedVertex.GetVertex)
                        {
                            edgeLine.EdgeLine.X2 = currentPosition.X;
                            edgeLine.EdgeLine.Y2 = currentPosition.Y;

                            LastDraggedVertex.VertexXCoords = currentPosition.X;
                            LastDraggedVertex.VertexYCoords = currentPosition.Y;

                            LastDraggedVertex.UpdateNameLabelLocation();

                            edgeLine.UpdateLine();
                        }
                    }
                }

                RefreshCanvas();
            }
        }

        public void Vertex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging = false;
            var draggable = sender as Border;
            //if(draggable.Background != Brushes.Orange)
                draggable.Background = Brushes.DeepSkyBlue;

            draggable.ReleaseMouseCapture();

            var x = e.GetPosition(CanvasGraph).X; //(Canvas.GetLeft(SelectedVertexOrigin) + 12.5) - (ClickStart.X - e.GetPosition(CanvasGraph).X);
            var y = e.GetPosition(CanvasGraph).Y; //(Canvas.GetTop(SelectedVertexOrigin) + 12.5) - (ClickStart.Y - e.GetPosition(CanvasGraph).Y);

            RefreshCanvas();
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

        #endregion

        private void CmbStartingVertexSp_DropDownOpened(object sender, EventArgs e)
        {
            CmbStartingVertexSp.Items.Clear();

            foreach (var vertex in DataStorage.VerticesList)
            {
                if (vertex.IsDeleted)
                    continue;

                CmbStartingVertexSp.Items.Add(vertex.ID);
            }
        }

        private void CmbEndingVertexSp_DropDownOpened(object sender, EventArgs e)
        {
            CmbEndingVertexSp.Items.Clear();

            foreach (var vertex in DataStorage.VerticesList)
            {
                if(vertex.IsDeleted)
                    continue;

                CmbEndingVertexSp.Items.Add(vertex.ID);
            }
        }

        private void ListViewVerticesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewEdgeList.SelectedIndex = -1;
        }

        private void ListViewEdge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewVerticesList.SelectedIndex = -1;
        }
    }
}