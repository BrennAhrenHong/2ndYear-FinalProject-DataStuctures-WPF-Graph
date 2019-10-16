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

        public UnWeightedEdge(double x1, double y1, double x2, double y2)
        {
            XCoordinateLine1 = x1;
            YCoordinateLine1 = y1;

            XCoordinateLine2 = x2;
            YCoordinateLine2 = y2;
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

            Canvas.SetLeft(edgeLine, 100);
            Canvas.SetTop(edgeLine, 100);

            return edgeLine;
        }
    }
}
