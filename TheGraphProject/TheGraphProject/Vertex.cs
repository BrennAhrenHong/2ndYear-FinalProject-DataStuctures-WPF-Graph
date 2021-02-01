﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TheGraphProject
{
    public class Vertex
    {
        public Vertex(MainWindow mainWindow, string name, double x, double y)
        {
            MainWindow = mainWindow;
            Name = name;
            VertexXCoords = x;
            VertexYCoords = y;
        }

        public MainWindow MainWindow { get; set; }
        public Border GetVertex { get; set; }
        public string Name { get; protected set; }
        public int VertexIdNumber { get; protected set; }
        public double VertexXCoords { get; set; }
        public double VertexYCoords { get; set; }
        public bool IsStartingVertex { get; set; } = false;

        public LinkedList<Line> EdgesConnected = new LinkedList<Line>();

        public Label CreateLabel()
        {
            Label vertexLabel = new Label();
            vertexLabel.VerticalAlignment = VerticalAlignment.Center;
            vertexLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            vertexLabel.FontSize = 12;
            vertexLabel.Content = VertexIdNumber;// CHANGED SOMETHING
            return vertexLabel;
        }

        public void CreateVertex()
        {
            Border newVertex = new Border();


            newVertex.Height = 25;
            newVertex.Width = 25;
            newVertex.BorderBrush = Brushes.Black;
            newVertex.BorderThickness = new Thickness(1);
            newVertex.Background = Brushes.DodgerBlue;
            newVertex.CornerRadius = new CornerRadius(50);



            VertexIdNumber = DataStorage.UniqueIDList.Count;
            if (DataStorage.UniqueIDList.Count == 0)
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList.Count);
            else
                DataStorage.UniqueIDList.Add(DataStorage.UniqueIDList.Count + 1);
            newVertex.Child = CreateLabel();

            //SetLeft = X-Axis
            Canvas.SetLeft(newVertex, VertexXCoords - 12.5);
            //SetTop = Y-Axis
            Canvas.SetTop(newVertex, VertexYCoords - 12.5);

            Canvas.SetBottom(newVertex, VertexYCoords);

            Canvas.SetRight(newVertex, VertexXCoords);


            Panel.SetZIndex(newVertex, 1);

            newVertex.MouseLeftButtonDown += MainWindow.Vertex_MouseLeftButtonDown;
            newVertex.MouseMove += MainWindow.Vertex_MouseMove;
            newVertex.MouseLeftButtonUp += MainWindow.Vertex_MouseLeftButtonUp;



            GetVertex = newVertex;

        }
    }
}
