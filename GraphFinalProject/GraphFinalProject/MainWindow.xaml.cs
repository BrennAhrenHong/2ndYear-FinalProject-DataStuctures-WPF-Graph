using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// TODO
    /// Matrix Adjacency List 10%
    /// Vertices List 10%
    /// Shortest Path 10%
    /// UI Revamp (Buttons) 0%
    /// Control Panel 10% 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool MousePress = false;
        private Nullable<Point> dragStart = null;

        private char PrevLetter = 'Z';
        private List<Label> VertexList = new List<Label>();
        private int LabelNumber = 0;
        private int IDNumber = 0;
        public double XaxisAuto { get; set; }
        public double YaxisAuto { get; set; }


        private List<Border> VertexBorder = new List<Border>();



        public Label CreateLabel(char prevLetter, int labelNumber)
        {
            string labelContent = "";
            char currentLetter;
            int currentLabelNumber = LabelNumber;

            //Assigning of Current Letter to PrevLetter & Creating the Label content letter
            if (prevLetter != 'Z')
            {
                currentLetter = ++prevLetter;
                PrevLetter = currentLetter;
            }
            else
            {
                currentLetter = 'A';
                PrevLetter = 'A';
            }

            if (labelNumber % 26 == 0 && labelNumber > 25)
            {
                LabelNumber++;
            }

            if (labelNumber > 26)
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

        public void AddVertex(double x, double y)
        {
            Label label;

            if (VertexList.Count != 0)
                label = CreateLabel(PrevLetter, VertexList.Count + 1);
            else
                label = CreateLabel(PrevLetter, 0);

            VertexList.Add(label);

            Border vertexBorder = new Border();

            int characters = label.Content.ToString().Length;
            characters = characters * 10 + 25;

            vertexBorder.Height = characters;
            vertexBorder.Width = characters;

            vertexBorder.BorderBrush = Brushes.Black;
            vertexBorder.BorderThickness = new Thickness(1);
            vertexBorder.Background = Brushes.LightSkyBlue;
            vertexBorder.Child = label;
            IDNumber++;

            this.ListViewVerticesList.Items.Add(new ListViewItem {ID = IDNumber, Name = TxtbName.Text});


            vertexBorder.CornerRadius = new CornerRadius(50);


            Canvas.SetLeft(vertexBorder, x - 15);
            Canvas.SetTop(vertexBorder, y - 15);

            VertexBorder.Add(vertexBorder);
            CanvasPlane.Children.Add(vertexBorder);
        }

        public void AddEdge(int x, int y)
        {
            Line edgeLine = new Line();
            edgeLine.X1 = 50;
            edgeLine.X2 = 10;
            edgeLine.Y1 = 5;
            edgeLine.Y2 = 5;
            edgeLine.Stroke = Brushes.Black;
            edgeLine.StrokeThickness = 5;
            edgeLine.Fill = Brushes.Black;

            Canvas.SetLeft(edgeLine, 100);
            Canvas.SetTop(edgeLine, 100);
            CanvasPlane.Children.Add(edgeLine);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddVertex(Convert.ToDouble(TxtbXaxisManual.Text), Convert.ToDouble(TxtbYaxisManual.Text));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.OriginalSource is Ellipse)
            //{
            //    var element = (UIElement) sender;
            //    MousePress = true;
            //    dragStart = e.GetPosition(element);
            //    element.CaptureMouse();
            //}
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (MousePress == true)
            //{
            //    MousePress = false;
            //    var element = (UIElement) sender;
            //    dragStart = null;
            //    element.ReleaseMouseCapture();
            //}
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            //if(dragStart != null && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    var element = (UIElement)sender;
            //    var p2 = e.GetPosition(c);
            //    Canvas.SetLeft(element, p2.X - dragStart.Value.X);
            //    Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
            //}

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddEdge(1, 1);
        }

        private void CanvasPlane_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ContextMenuCanvas.IsOpen)
            {
                var x = e.GetPosition(CanvasPlane).X;
                var y = e.GetPosition(CanvasPlane).Y;

                TxtbXaxisAuto.Text = x.ToString();
                TxtbYaxisAuto.Text = y.ToString();
            }
        }

        private void CMitemAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            AddVertex(XaxisAuto, YaxisAuto);
        }

        private void CanvasPlane_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            XaxisAuto = Convert.ToDouble(TxtbXaxisAuto.Text);
            YaxisAuto = Convert.ToDouble(TxtbYaxisAuto.Text);
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if(ListViewVerticesList.SelectedIndex > -1)
                ListViewVerticesList.Items.RemoveAt(ListViewVerticesList.SelectedIndex);
            CanvasPlane.Children.RemoveAt(0);
        }
    }
}
