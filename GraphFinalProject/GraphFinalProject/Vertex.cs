using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphFinalProject
{
    public class Vertex<T>
    {
        public double XCoordinate { get; protected set; }
        public double YCoordinate { get; protected set; }
        public string Name { get; protected set; }
        public int IDNumber { get; protected set; }

        public Vertex(double x, double y, string name, int iDNumber)
        {
            XCoordinate = x;
            YCoordinate = y;
            Name = name;
            IDNumber = iDNumber;
            DataStorage.IDNumber++;
        }

        public Label CreateLabel()
        {
            string labelContent = "";
            bool letterDoesNotExist = false;
            //DataStorage.VertexLabelList.FindLast();
            //Assigning of Current Letter to PrevLetter & Creating the Label content letter
            Label tmp = new Label();

            if (DataStorage.VertexLabelList.Count != 0)
            {
                for (char i = 'A'; !letterDoesNotExist; i++)
                {
                    letterDoesNotExist = true;
                    foreach (var label in DataStorage.VertexLabelList)
                    {
                        if (label.Content.ToString() == i.ToString())
                        {
                            letterDoesNotExist = false;
                            break;
                        }
                    }
                    labelContent = i.ToString();
                }
            }
            else
            {
                labelContent = "A";
            }

            //Create label for vertex
            Label vertexLabel = new Label();

            vertexLabel.VerticalAlignment = VerticalAlignment.Center;
            vertexLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            vertexLabel.FontSize = 12;
            vertexLabel.Content = labelContent;

            return vertexLabel;
        }

        public void CreateVertex()
        {
            double x = XCoordinate;
            double y = YCoordinate;


            Label label = CreateLabel();

            DataStorage.ListViewId.Add(label.ContentStringFormat);
            DataStorage.VertexLabelList.Add(label);
            DataStorage.RecentIDMade = label.Content.ToString();
            if (Name == "")
                Name = DataStorage.RecentIDMade;

            Border vertexBorder = new Border();

            vertexBorder.Height = 25;
            vertexBorder.Width = 25;

            vertexBorder.BorderBrush = Brushes.Black;
            vertexBorder.BorderThickness = new Thickness(1);
            vertexBorder.Background = Brushes.LightSkyBlue;
            vertexBorder.Child = label;

            //this.ListViewVerticesList.Items.Add(new ListViewItem { ListViewId = DataStorage.IDNumber, Name = TxtbName.Text });


            vertexBorder.CornerRadius = new CornerRadius(50);


            Canvas.SetLeft(vertexBorder, x - 15);
            Canvas.SetTop(vertexBorder, y - 15);

            //DataStorage.VertexBorder.Add(vertexBorder);
            DataStorage.CanvasChildrenList.Add(vertexBorder);

            //CanvasPlane Children Adding
            //CanvasPlane.Children.Add(vertexBorder);
        }
    }
}
