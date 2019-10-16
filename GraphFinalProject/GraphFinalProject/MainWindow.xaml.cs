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
    /// Vertices List 90%
    /// Shortest Path 10%
    /// UI Revamp (Buttons) 0%
    /// Control Panel 10%
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private bool MousePress = false;
        //private Nullable<Point> dragStart = null;

        private List<Label> VertexList = new List<Label>();
        private List<UnWeightedEdge<UIElement>> EdgeList = new List<UnWeightedEdge<UIElement>>();
        private List<UIElement> _canvasChildrenList = new List<UIElement>();

        public double Xaxis { get; set; }
        public double Yaxis { get; set; }
        //private List<Border> VertexBorder = new List<Border>();


        #region CreateLabel/Vertex

        //public Label CreateLabel(char prevLetter, int labelNumber)
        //{
        //    string labelContent = "";
        //    char currentLetter;
        //    int currentLabelNumber = DataStorage.LabelNumber;

        //    //Assigning of Current Letter to PrevLetter & Creating the Label content letter
        //    if (prevLetter != 'Z')
        //    {
        //        currentLetter = ++prevLetter;
        //        PrevLetter = currentLetter;
        //    }
        //    else
        //    {
        //        currentLetter = 'A';
        //        PrevLetter = 'A';
        //    }

        //    if (labelNumber % 26 == 0 && labelNumber > 25)
        //    {
        //        LabelNumber++;
        //    }

        //    if (labelNumber > 26)
        //    {
        //        labelContent += currentLetter + "" + currentLabelNumber;
        //    }
        //    else
        //    {
        //        labelContent += currentLetter;
        //    }

        //    //Create label for vertex
        //    Label vertexLabel = new Label();

        //    vertexLabel.VerticalAlignment = VerticalAlignment.Center;
        //    vertexLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
        //    vertexLabel.FontSize = 12;
        //    vertexLabel.Content = labelContent;

        //    return vertexLabel;
        //}


        //public void AddVertex(double x, double y)
        //{
        //    Label label;

        //    if (VertexList.Count != 0)
        //        label = CreateLabel(PrevLetter, VertexList.Count + 1);
        //    else
        //        label = CreateLabel(PrevLetter, 0);

        //    VertexList.Add(label);

        //    Border vertexBorder = new Border();

        //    int characters = label.Content.ToString().Length;
        //    characters = characters * 10 + 25;

        //    vertexBorder.Height = characters;
        //    vertexBorder.Width = characters;

        //    vertexBorder.BorderBrush = Brushes.Black;
        //    vertexBorder.BorderThickness = new Thickness(1);
        //    vertexBorder.Background = Brushes.LightSkyBlue;
        //    vertexBorder.Child = label;
        //    IDNumber++;

        //    this.ListViewVerticesList.Items.Add(new ListViewItem {ID = IDNumber, Name = TxtbName.Text});


        //    vertexBorder.CornerRadius = new CornerRadius(50);


        //    Canvas.SetLeft(vertexBorder, x - 15);
        //    Canvas.SetTop(vertexBorder, y - 15);

        //    VertexBorder.Add(vertexBorder);

        //    _canvasChildrenList.Add(vertexBorder);

        //    AddElementsToCanvas();
        //    //CanvasPlane Children Adding
        //    //CanvasPlane.Children.Add(vertexBorder);
        //}



        #endregion

        public void AddVertex()
        {
            Vertex addVertex = new Vertex(Xaxis, Yaxis);

            addVertex.CreateVertex();
            AddElementsToCanvas();
        }

        public void AddElementsToCanvas()
        {
            CanvasPlane.Children.Clear();

            foreach (var uiElement in DataStorage.CanvasChildrenList)
            {
                CanvasPlane.Children.Add(uiElement);
            }

            foreach (var uiElement in EdgeList)
            {
                CanvasPlane.Children.Add(uiElement.CreateEdge());
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ListViewVerticesList.SelectedIndex;
            _canvasChildrenList.RemoveAt(selectedIndex);
            AddElementsToCanvas();

            if (ListViewVerticesList.SelectedIndex > -1)
                ListViewVerticesList.Items.RemoveAt(ListViewVerticesList.SelectedIndex);
        }
        public void AddEdge(double x1, double y1, double x2, double y2)
        {
            UnWeightedEdge<UIElement> newUnWeightedEdge = new UnWeightedEdge<UIElement>(x1, y1, x2,y2);
            EdgeList.Add(newUnWeightedEdge);
            AddElementsToCanvas();

            //Line edgeLine = new Line();
            //edgeLine.X1 = 50;
            //edgeLine.X2 = 10;
            //edgeLine.Y1 = 25;
            //edgeLine.Y2 = 50;
            //edgeLine.Stroke = Brushes.Black;
            //edgeLine.StrokeThickness = 5;
            //edgeLine.Fill = Brushes.Black;

            //Canvas.SetLeft(edgeLine, 100);
            //Canvas.SetTop(edgeLine, 100);
            //CanvasPlane.Children.Add(edgeLine);
        }

        private void ButtonAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            Xaxis = Convert.ToDouble(TxtbXaxisManual.Text);
            Yaxis = Convert.ToDouble(TxtbYaxisManual.Text);
            AddVertex();
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


        private void CanvasPlane_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ContextMenuCanvas.IsOpen)
            {
                var x = e.GetPosition(CanvasPlane).X;
                var y = e.GetPosition(CanvasPlane).Y;

                TxtbXaxisAuto.Text = Math.Round(Convert.ToDouble(x),2).ToString();
                TxtbYaxisAuto.Text = Math.Round(Convert.ToDouble(y), 2).ToString();
            }
        }

        private void CMitemAddVertex_OnClick(object sender, RoutedEventArgs e)
        {
            AddVertex();
        }

        private void CanvasPlane_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Xaxis = Math.Round(Convert.ToDouble(TxtbXaxisAuto.Text), 4);
            Yaxis = Math.Round(Convert.ToDouble(TxtbYaxisAuto.Text), 4);
        }

        private void ButtonAddEdgeClick(object sender, RoutedEventArgs e)
        {
            var x = TxtbAddEdge.Text.Split();
            //AddEdge();
        }
    }
}
