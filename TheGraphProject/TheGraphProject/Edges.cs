using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NUnit.Framework;

namespace TheGraphProject
{
    public class LineEdge
    {
        public LineEdge(MainWindow mainWindow, Vertex vertexA, Vertex vertexB, bool isDirected)
        {
            MainWindow = mainWindow;
            VertexA = vertexA;
            VertexB = vertexB;
            IsDirected = isDirected;
            Weight = 1; //Need to Fix this.
            IsWeighted = false;
        }

        public LineEdge(MainWindow mainWindow, Vertex vertexA, Vertex vertexB, bool isDirected, int weight)
        {
            MainWindow = mainWindow;
            VertexA = vertexA;
            VertexB = vertexB;
            IsDirected = isDirected;
            Weight = weight;
            IsWeighted = true;
        }

        public MainWindow MainWindow { get; protected set; }
        public bool IsDirected { get; protected set; }
        public bool IsWeighted { get; protected set; }

        public Vertex VertexA { get; set; }
        public Vertex VertexB { get; set; }

        public LinkedList<Vertex> VerticesConnectedList = new LinkedList<Vertex>();
        public int Weight { get; protected set; }
        public Line EdgeLine { get; protected set; }
        public TextBlock TxtBlockWeight {get; protected set; }
        public bool IsDeleted { get; set; } = false;

   


        public Line AddDirectedEdge()
        {

            Line edgeLine = new Line();


            edgeLine.X1 = VertexA.VertexXCoords;
            edgeLine.Y1 = VertexA.VertexYCoords;

            edgeLine.X2 = VertexB.VertexXCoords;
            edgeLine.Y2 = VertexB.VertexYCoords;

            //double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
            //double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;


            //textBlock Weight
            TextBlock textBlockLine = new TextBlock();
            textBlockLine.Text = Weight.ToString();

            double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
            double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;

            Canvas.SetLeft(textBlockLine, XmidPointCoordinates);
            Canvas.SetTop(textBlockLine, YmidPointCoordinates);
            Panel.SetZIndex(textBlockLine, 2);

            TxtBlockWeight = textBlockLine;

            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 2;
            edgeLine.Fill = Brushes.Black;

            //textBlockLine.Inlines.Add(new Line{});
            EdgeLine = edgeLine;

            VertexA.EdgesConnected.AddLast(edgeLine);
            VertexB.EdgesConnected.AddLast(edgeLine);

            return edgeLine;
        }
        public Line AddUndirectedEdge() 
        {
            Line edgeLine = new Line();

            edgeLine.X1 = VertexA.VertexXCoords;
            edgeLine.Y1 = VertexA.VertexYCoords;

            edgeLine.X2 = VertexB.VertexXCoords;
            edgeLine.Y2 = VertexB.VertexYCoords;


            //textBlock Weight
            TextBlock textBlockLine = new TextBlock();
            textBlockLine.Text = Weight.ToString();


            double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
            double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;

            Canvas.SetLeft(textBlockLine, XmidPointCoordinates);
            Canvas.SetTop(textBlockLine, YmidPointCoordinates);
            Panel.SetZIndex(textBlockLine,2);

            TxtBlockWeight = textBlockLine;

            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 2;
            edgeLine.Fill = Brushes.Black;

            //textBlockLine.Inlines.Add(new Line{});
            EdgeLine = edgeLine;
            VertexA.EdgesConnected.AddLast(edgeLine);
            VertexB.EdgesConnected.AddLast(edgeLine);

            return edgeLine;
        }

        public void UpdateLine()
        {
            //EdgeLine.X1 = VertexA.VertexXCoords;
            //EdgeLine.Y1 = VertexA.VertexYCoords;

            //EdgeLine.X2 = VertexB.VertexXCoords;
            //EdgeLine.Y2 = VertexB.VertexYCoords;

            double xmidPointCoordinates = (EdgeLine.X1 + EdgeLine.X2) / 2;
            double ymidPointCoordinates = (EdgeLine.Y1 + EdgeLine.Y2) / 2;

            try
            {
                if (IsWeighted)
                {
                    Canvas.SetLeft(TxtBlockWeight, xmidPointCoordinates);
                    Canvas.SetTop(TxtBlockWeight, ymidPointCoordinates);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("error");
            }


        }
    }
}
