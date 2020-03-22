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
        public Vertex(double x, double y)
        {
            Vertex_X_Coords = x;
            Vertex_Y_Coords = y;
        }
        public string Name { get; protected set; }
        public int VertexIDNumber { get; protected set; }
        public double Vertex_X_Coords { get; protected set; }
        public double Vertex_Y_Coords { get; protected set; }
        public bool HasEdge { get; protected set; }

        public Border CreateVertex()
        {
            Border createVertex = new Border();

            createVertex.Height = 25;
            createVertex.Width = 25;
            createVertex.BorderBrush = Brushes.Black;
            createVertex.BorderThickness = new Thickness(1);

            return null;
        }
    }
}
