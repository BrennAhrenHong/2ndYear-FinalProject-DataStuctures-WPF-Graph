using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphFinalProject
{
    public class UnWeightedEdge<T>
    {
        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }

        public Vertex<Border> Parent1 { get; protected set; }
        public Vertex<Border> Parent2 { get; protected set; }

        public UnWeightedEdge(Vertex<Border> vertex1, Vertex<Border> vertex2)
        {
            XCoordinateLine1 = vertex1.XCoordinate;
            YCoordinateLine1 = vertex1.YCoordinate;

            XCoordinateLine2 = vertex2.XCoordinate;
            YCoordinateLine2 = vertex2.YCoordinate;

            Parent1 = vertex1;
            Parent2 = vertex2;
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
