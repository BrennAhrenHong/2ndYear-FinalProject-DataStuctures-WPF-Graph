using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TheGraphProject
{
    public class Edges
    {

    }

    public class DirectedEdge
    {

    }

    public class Edge
    {
        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }
        public List<Vertex> VerticesConnecetedList { get; set; }
        public int Weight { get; protected set; }

        public Edge(double xCoordinateLine1, double yCoordinateLine1, double xCoordinateLine2, double yCoordinateLine2)
        {
            XCoordinateLine1 = xCoordinateLine1;
            XCoordinateLine2 = xCoordinateLine2;
            YCoordinateLine1 = yCoordinateLine1;
            YCoordinateLine2 = yCoordinateLine2;
        }

        public Line AddEdge()
        {
            TextBlock textBlockLine = new TextBlock();
            //textBlockLine.ADD

            Line edgeLine = new Line();

            edgeLine.X1 = XCoordinateLine1 -2;
            edgeLine.Y1 = YCoordinateLine1 -2;

            edgeLine.X2 = XCoordinateLine2 -2;
            edgeLine.Y2 = YCoordinateLine2 -2;

            //double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
            //double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;



            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 2;
            edgeLine.Fill = Brushes.Black;

            //textBlockLine.Inlines.Add(new Line{});

            return edgeLine;
        }
    }
}
