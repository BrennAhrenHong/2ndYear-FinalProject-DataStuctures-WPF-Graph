using System;
using System.Collections;
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
        //Add Edge Functionality 0%
        //Delete Vertex Functionality 0%
        //Delete Edge Functionality 0%
        //Collision Checker Functionality 0%


        private double SelectedCanvas_XCoordinate { get; set; }
        private double SelectedCanvas_YCoordinate { get; set; }

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


        private void CanvasGraph_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.GetPosition(CanvasGraph).X;
            var y = e.GetPosition(CanvasGraph).Y;

            TxtboxAutoCaptureXCoords.Text = Math.Round(Convert.ToDouble(x)).ToString();
            TxtboxAutoCaptureYCoords.Text = Math.Round(Convert.ToDouble(y)).ToString();
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

        private void CanvasGraph_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

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

        //Methods
        public void AddVertex()
        {
            Vertex newVertex = new Vertex(TxtbVertexName.Text, DataStorage.IDStack.Peek(),
                Convert.ToInt32(SelectedCanvas_XCoordinate), Convert.ToInt32(SelectedCanvas_YCoordinate));

            DataStorage.VertexList.Add(newVertex);
            CanvasGraph.Children.Add(newVertex.CreateVertex());

            DataStorage.ListViewItems.Add(new DataTemplate()
                {ID = DataStorage.IDStack.Pop(), Name = TxtbVertexName.Text,});
            VerticesList.ItemsSource = "";
            VerticesList.ItemsSource = DataStorage.ListViewItems;
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

            int StartingVertexIndex = 0;
            int EndingVertexIndex = 0;

            foreach (var x in DataStorage.VertexList)
            {
                if (x.VertexListViewIdLetter == DataStorage.SelectedStartingVertex)
                {
                    StartingVertex = x;
                    StartingVertexIndex = DataStorage.VertexList.IndexOf(x);
                }

                if (x.VertexListViewIdLetter == DataStorage.SelectedEndingVertex)
                {
                    EndingVertex = x;
                    EndingVertexIndex = DataStorage.VertexList.IndexOf(x);
                }
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
                        Edge createUndirectedEdge = new Edge(StartingVertex.Vertex_X_Coords,
                            StartingVertex.Vertex_Y_Coords, EndingVertex.Vertex_X_Coords, EndingVertex.Vertex_Y_Coords);
                        CanvasGraph.Children.Add(createUndirectedEdge.AddEdge());
                        DataStorage.VertexList[StartingVertexIndex].EdgesConnected.Add(createUndirectedEdge);
                        break;
                    }

                    default:
                        break;
                }
            }
        }
    }
}
