using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheGraphProject
{
    public class Vertex
    {
        public Vertex(string name, char vertexListViewIdLetter, double x, double y)
        {
            Name = name;
            VertexListViewIdLetter = vertexListViewIdLetter;
            Vertex_X_Coords = x;
            Vertex_Y_Coords = y;
        }

        public string Name { get; protected set; }
        public int VertexIdNumber { get; protected set; }
        public char VertexListViewIdLetter { get; protected set; }
        public double Vertex_X_Coords { get; protected set; }
        public double Vertex_Y_Coords { get; protected set; }
        public List<Char> EdgesConnected { get; protected set; }

        public Label CreateLabel()
        {
            Label vertexLabel = new Label();
            vertexLabel.VerticalAlignment = VerticalAlignment.Center;
            vertexLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            vertexLabel.FontSize = 12;
            vertexLabel.Content = DataStorage.IDStack.Peek();
            return vertexLabel;
        }

        public Border CreateVertex()
        {
            Border newVertex = new Border();

            newVertex.Height = 25;
            newVertex.Width = 25;
            newVertex.BorderBrush = Brushes.Black;
            newVertex.BorderThickness = new Thickness(1);
            newVertex.Background = Brushes.DodgerBlue;
            newVertex.CornerRadius = new CornerRadius(50);
            newVertex.Child = CreateLabel();

            Canvas.SetLeft(newVertex, Vertex_X_Coords - 15); //SetLeft = X-Axis
            Canvas.SetTop(newVertex, Vertex_Y_Coords - 15); //SetTop = Y-Axis
            Panel.SetZIndex(newVertex,1);

            VertexIdNumber = DataStorage.UniqueIDList.Count;
            if (DataStorage.UniqueIDList.Count == 0)
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList.Count);
            else
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList.Count + 1);

            

            return newVertex;
        }
    }
}
