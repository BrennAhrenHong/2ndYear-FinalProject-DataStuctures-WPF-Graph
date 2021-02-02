using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NUnit.Framework;

namespace TheGraphProject
{

    //public class DirectedLineEdge
    //{
    //    public DirectedLineEdge(MainWindow mainWindow, Vertex vertexA, Vertex vertexB)
    //    {
    //        VertexA = vertexA;
    //        VertexB = vertexB;
    //        Weight = 0;
    //    }

    //    public DirectedLineEdge(MainWindow mainWindow, Vertex vertexA, Vertex vertexB, int weight)
    //    {
    //        VertexA = vertexA;
    //        VertexB = vertexB;
    //        Weight = weight;
    //    }

    //    public double XCoordinateLine1 { get; protected set; }
    //    public double XCoordinateLine2 { get; protected set; }
    //    public double YCoordinateLine1 { get; protected set; }
    //    public double YCoordinateLine2 { get; protected set; }
    //    public readonly bool IsDirected = true;

    //    public Vertex VertexA { get; set; }
    //    public Vertex VertexB { get; set; }

    //    public LinkedList<Vertex> VerticesConnectedList = new LinkedList<Vertex>();
    //    public int Weight { get; protected set; }
    //    public Line EdgeLine { get; protected set; }
    //    public TextBlock TxtBlockWeight { get; protected set; }

    //    public Line AddUndirectedEdge()
    //    {

    //        Line edgeLine = new Line();

    //        edgeLine.X1 = VertexA.VertexXCoords;
    //        edgeLine.Y1 = VertexA.VertexYCoords;

    //        edgeLine.X2 = VertexB.VertexXCoords;
    //        edgeLine.Y2 = VertexB.VertexYCoords;

    //        //double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
    //        //double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;


    //        edgeLine.Stroke = Brushes.Black;
    //        edgeLine.StrokeThickness = 2;
    //        edgeLine.Fill = Brushes.Black;

    //        //textBlockLine.Inlines.Add(new Line{});
    //        EdgeLine = edgeLine;
    //        VertexA.EdgesConnected.AddLast(edgeLine);
    //        VertexB.EdgesConnected.AddLast(edgeLine);

    //        return edgeLine;
    //    }
    //    public Line AddUndirectedEdge(int weight)
    //    {
    //        Line edgeLine = new Line();

    //        edgeLine.X1 = VertexA.VertexXCoords;
    //        edgeLine.Y1 = VertexA.VertexYCoords;

    //        edgeLine.X2 = VertexB.VertexXCoords;
    //        edgeLine.Y2 = VertexB.VertexYCoords;




    //        //textBlock Weight
    //        TextBlock textBlockLine = new TextBlock();
    //        textBlockLine.Text = weight.ToString();

    //        double XmidPointCoordinates = (edgeLine.X1 + edgeLine.X2) / 2;
    //        double YmidPointCoordinates = (edgeLine.Y1 + edgeLine.Y2) / 2;

    //        Canvas.SetLeft(textBlockLine, XmidPointCoordinates);
    //        Canvas.SetTop(textBlockLine, YmidPointCoordinates);
    //        Canvas.SetZIndex(textBlockLine, 1);

    //        TxtBlockWeight = textBlockLine;

    //        edgeLine.Stroke = Brushes.Black;
    //        edgeLine.StrokeThickness = 2;
    //        edgeLine.Fill = Brushes.Black;

    //        //textBlockLine.Inlines.Add(new Line{});
    //        EdgeLine = edgeLine;
    //        VertexA.EdgesConnected.AddLast(edgeLine);
    //        VertexB.EdgesConnected.AddLast(edgeLine);

    //        return edgeLine;
    //    }

    //    public void ChangeLine()
    //    {
    //        EdgeLine.X1 = VertexA.VertexXCoords;
    //        EdgeLine.Y1 = VertexA.VertexYCoords;

    //        EdgeLine.X2 = VertexB.VertexXCoords;
    //        EdgeLine.Y2 = VertexB.VertexYCoords;



    //        double XmidPointCoordinates = (EdgeLine.X1 + EdgeLine.X2) / 2;
    //        double YmidPointCoordinates = (EdgeLine.Y1 + EdgeLine.Y2) / 2;

    //            Canvas.SetLeft(TxtBlockWeight, XmidPointCoordinates);
    //            Canvas.SetTop(TxtBlockWeight, YmidPointCoordinates);
    //            Canvas.SetZIndex(TxtBlockWeight, 1);
    //    }
    //}

    public class LineEdge
    {
        /// <summary>
        /// Create vertex neighbor property
        /// </summary>
        ///
        ///
        public LineEdge(MainWindow mainWindow, Vertex vertexA, Vertex vertexB, bool isDirected)
        {
            MainWindow = mainWindow;
            VertexA = vertexA;
            VertexB = vertexB;
            IsDirected = isDirected;
            Weight = 0;
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

        public double XCoordinateLine1 { get; protected set; }
        public double XCoordinateLine2 { get; protected set; }
        public double YCoordinateLine1 { get; protected set; }
        public double YCoordinateLine2 { get; protected set; }
        public MainWindow MainWindow { get; protected set; }
        public bool IsDirected { get; protected set; }
        public bool IsWeighted { get; protected set; }

        public Vertex VertexA { get; set; }
        public Vertex VertexB { get; set; }

        public LinkedList<Vertex> VerticesConnectedList = new LinkedList<Vertex>();
        public int Weight { get; protected set; }
        public Line EdgeLine { get; protected set; }
        public TextBlock TxtBlockWeight {get; protected set; }

   


        public Line AddDirectedEdge()
        {

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
            Canvas.SetZIndex(textBlockLine,1);

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

        public void ChangeLine()
        {
            EdgeLine.X1 = VertexA.VertexXCoords;
            EdgeLine.Y1 = VertexA.VertexYCoords;

            EdgeLine.X2 = VertexB.VertexXCoords;
            EdgeLine.Y2 = VertexB.VertexYCoords;



            double XmidPointCoordinates = (EdgeLine.X1 + EdgeLine.X2) / 2;
            double YmidPointCoordinates = (EdgeLine.Y1 + EdgeLine.Y2) / 2;
            if (MainWindow.RadioButtonWeighted.IsChecked.Value)
            {
                Canvas.SetLeft(TxtBlockWeight, XmidPointCoordinates);
                Canvas.SetTop(TxtBlockWeight, YmidPointCoordinates);
                Canvas.SetZIndex(TxtBlockWeight, 1);
            }

        }
    }
}
