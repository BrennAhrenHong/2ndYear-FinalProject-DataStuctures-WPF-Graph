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

        public Vertex(double x, double y, string name)
        {
            XCoordinate = x;
            YCoordinate = y;
            Name = name;
        }

        public Label CreateLabel(char prevLetter, int listlabelCount)
        {
            string labelContent = "";
            char currentLetter;
            int currentLabelNumber = DataStorage.LabelNumber;

            //Assigning of Current Letter to PrevLetter & Creating the Label content letter
            if (prevLetter != 'Z')
            {
                currentLetter = ++prevLetter;
                DataStorage.PrevLetter = currentLetter;
            }
            else
            {
                currentLetter = 'A';
                DataStorage.PrevLetter = 'A';
            }

            //If labelLetter reaches Z
            if (listlabelCount % 26 == 0 && listlabelCount > 25)
            {
                DataStorage.LabelNumber++;
            }

            if (listlabelCount > 26)
            {
                labelContent += currentLetter + "" + currentLabelNumber;
            }
            else
            {
                labelContent += currentLetter;
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
            Label label;

            if (DataStorage.VertexLabelList.Count != 0)
                label = CreateLabel(DataStorage.PrevLetter, DataStorage.VertexLabelList.Count + 1);
            else
                label = CreateLabel(DataStorage.PrevLetter, 0);

            DataStorage.ID.Add(label.ContentStringFormat);
            DataStorage.VertexLabelList.Add(label);
            DataStorage.RecentIDMade = label.Content.ToString();
            if (Name == "")
                Name = DataStorage.RecentIDMade;

            Border vertexBorder = new Border();

            int characters = label.Content.ToString().Length;
            characters = characters * 10 + 25;

            vertexBorder.Height = characters;
            vertexBorder.Width = characters;

            vertexBorder.BorderBrush = Brushes.Black;
            vertexBorder.BorderThickness = new Thickness(1);
            vertexBorder.Background = Brushes.LightSkyBlue;
            vertexBorder.Child = label;

            //this.ListViewVerticesList.Items.Add(new ListViewItem { ID = DataStorage.IDNumber, Name = TxtbName.Text });


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
