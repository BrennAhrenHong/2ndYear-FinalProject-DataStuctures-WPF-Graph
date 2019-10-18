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
    public class DirectedEdge<T>
    {
        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }
        public int Weight { get; protected set; }
        public Label weightLabel { get; protected set; }
        public Vertex<Border> FromVertex { get; protected set; }
        public Vertex<Border> ToVertex { get; protected set; }

        public DirectedEdge(Vertex<Border> fromVertex, Vertex<Border> toVertex, int weight)
        {
            XCoordinateLine1 = fromVertex.XCoordinate;
            YCoordinateLine1 = fromVertex.YCoordinate;

            XCoordinateLine2 = toVertex.XCoordinate;
            YCoordinateLine2 = toVertex.YCoordinate;

            FromVertex = fromVertex;
            ToVertex = toVertex;

            Weight = weight;
        }

        public void CreateWeightLabel()
        {
            Label createWeightLabel = new Label();
            createWeightLabel.Content = Weight.ToString();
            
        }

        public Line CreateEdge()
        {
            TextBlock textBlockLine = new TextBlock();
            //textBlockLine.ADD

            Line edgeLine = new Line();

            edgeLine.X1 = XCoordinateLine1;
            edgeLine.Y1 = YCoordinateLine1;

            edgeLine.X2 = XCoordinateLine2;
            edgeLine.Y2 = YCoordinateLine2;

            double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2)/2;
            double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2)/2;




            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 2;
            edgeLine.Fill = Brushes.Black;

            //textBlockLine.Inlines.Add(new Line{});

            return edgeLine;
        }
    }
}
