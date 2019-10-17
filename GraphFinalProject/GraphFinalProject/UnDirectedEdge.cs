using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphFinalProject
{
    public class UnDirectedEdge<T>
    {
        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }
        public int Weight { get; protected set; }

        public Vertex<Border> FromVertex { get; protected set; }
        public Vertex<Border> ToVertex { get; protected set; }

        public UnDirectedEdge(Vertex<Border> fromVertex, Vertex<Border> toVertex, int weight)
        {
            XCoordinateLine1 = fromVertex.XCoordinate;
            YCoordinateLine1 = fromVertex.YCoordinate;

            XCoordinateLine2 = toVertex.XCoordinate;
            YCoordinateLine2 = toVertex.YCoordinate;

            FromVertex = fromVertex;
            ToVertex = toVertex;

            Weight = weight;
        }

        public Line CreateEdge()
        {
            Line edgeLine = new Line();
            edgeLine.X1 = XCoordinateLine1;
            edgeLine.Y1 = YCoordinateLine1;

            edgeLine.X2 = XCoordinateLine2;
            edgeLine.Y2 = YCoordinateLine2;


            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 3;
            edgeLine.Fill = Brushes.Black;

            //DataStorage.CanvasEdgeList.Add(edgeLine);

            return edgeLine;
        }
    }
}
