using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Label = System.Windows.Controls.Label;

namespace TheGraphProject
{
    public class Vertex
    {
        Vertex(double x, double y)
        {
            Xcords = x;
            Ycords = y;
        }
        public double Xcords { get; protected set; }
        public double Ycords { get; protected set; }

        public Border CircleBorder(String vertexText)
        {
            Border createVertex = new Border();
            createVertex.BorderBrush = Brushes.Black;
            createVertex.Width = 25;
            createVertex.Height = 25;
            createVertex.CornerRadius = new CornerRadius(50);
            createVertex.BorderThickness = new Thickness(1);

            


            createVertex.Child = CreateVertexLabel(sometext);
            return createVertex;
        }

        public Label CreateVertexLabel(string vertexText)
        {

            Label createLabel = new Label();
            createLabel.Content = vertexText;
        }
     }
}
