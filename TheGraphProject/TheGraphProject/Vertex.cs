using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Shell;
using GraphLogic;

namespace TheGraphProject
{
    public class Vertex
    {
        public Vertex(MainWindow mainWindow, string name, double x, double y)
        {
            MainWindow = mainWindow;
            Name = name;
            VertexXCoords = x;
            VertexYCoords = y;
        }

        public MainWindow MainWindow { get; set; }
        public Border GetVertex { get; set; }
        public LinkedList<LineEdge> EdgeList = new LinkedList<LineEdge>(); //In LineEdge DataType
        public LinkedList<Line> EdgesConnected = new LinkedList<Line>(); //In Line DataType
        public string Name { get; protected set; }
        public int ID { get; protected set; }
        public double VertexXCoords { get; set; }
        public double VertexYCoords { get; set; }
        public bool IsDeleted { get; set; } = false;
        public TextBlock VertexLabel { get; set; }


        public Label CreateLabel()
        {
            Label vertexLabel = new Label();
            vertexLabel.VerticalAlignment = VerticalAlignment.Center;
            vertexLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            vertexLabel.FontSize = 12;
            vertexLabel.Content = ID;// CHANGED SOMETHING
            return vertexLabel;
        }

        public void CreateVertex()
        {
            Border newVertex = new Border();


            newVertex.Height = 25;
            newVertex.Width = 25;
            newVertex.BorderBrush = Brushes.Black;
            newVertex.BorderThickness = new Thickness(1);
            newVertex.Background = Brushes.DeepSkyBlue;
            newVertex.CornerRadius = new CornerRadius(50);



            if (DataStorage.UniqueIDList.Count == 0)
            {
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList.Count);
                ID = 0;
            }
            else
            {
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList[DataStorage.UniqueIDList.Count - 1] + 1);
                ID = DataStorage.UniqueIDList[DataStorage.UniqueIDList.Count - 1];
            }
            newVertex.Child = CreateLabel();

            //SetLeft = X-Axis
            Canvas.SetLeft(newVertex, VertexXCoords - 12.5);
            //SetTop = Y-Axis
            Canvas.SetTop(newVertex, VertexYCoords - 12.5);

            Canvas.SetBottom(newVertex, VertexYCoords);

            Canvas.SetRight(newVertex, VertexXCoords);


            Panel.SetZIndex(newVertex, 1);

            newVertex.MouseLeftButtonDown += MainWindow.Vertex_MouseLeftButtonDown;
            newVertex.MouseMove += MainWindow.Vertex_MouseMove;
            newVertex.MouseLeftButtonUp += MainWindow.Vertex_MouseLeftButtonUp;

            if (Name.Trim(' ') == "")
                Name = ID.ToString();
            CreateNameLabel();

            GetVertex = newVertex;
        }

        private void CreateNameLabel()
        {
            var newVertexLabel = new TextBlock();
            newVertexLabel.Text = Name;
            Canvas.SetLeft(newVertexLabel,VertexXCoords);
            Canvas.SetTop(newVertexLabel,VertexYCoords - 35);
            Panel.SetZIndex(newVertexLabel,3);

            newVertexLabel.HorizontalAlignment = HorizontalAlignment.Center;

            VertexLabel = newVertexLabel;
        }

        public void UpdateNameLabelLocation()
        {
            Canvas.SetLeft(VertexLabel, VertexXCoords);
            Canvas.SetTop(VertexLabel, VertexYCoords - 35);
        }
    }
}
