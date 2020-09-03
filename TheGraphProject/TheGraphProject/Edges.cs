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
        /// <summary>
        /// Create vertex neighbor property
        /// </summary>
        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }

        public Vertex VertexA { get; set; }
        public Vertex VertexB { get; set; }

        public LinkedList<Vertex> VerticesConnectedList = new LinkedList<Vertex>();
        public int Weight { get; protected set; }
        public Line EdgeLine { get; protected set; }

        public Edge(Vertex vertexA, Vertex vertexB)
        {
            VertexA = vertexA;
            VertexB = vertexB;
        }

        public Line AddEdge()
        {
            TextBlock textBlockLine = new TextBlock();
            //textBlockLine.ADD

            Line edgeLine = new Line();

            edgeLine.X1 = VertexA.VertexXCoords;
            edgeLine.Y1 = VertexA.VertexYCoords;

            edgeLine.X2 = VertexB.VertexXCoords;
            edgeLine.Y2 = VertexB.VertexYCoords;

            //double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
            //double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;



            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 2;
            edgeLine.Fill = Brushes.Black;

            //textBlockLine.Inlines.Add(new Line{});
            EdgeLine = edgeLine;
            return edgeLine;
        }

        public void ChangeLine()
        {
            EdgeLine.X1 = VertexA.VertexXCoords;
            EdgeLine.Y1 = VertexA.VertexYCoords;

            EdgeLine.X2 = VertexB.VertexXCoords;
            EdgeLine.Y2 = VertexB.VertexYCoords;
        }
    }
}
