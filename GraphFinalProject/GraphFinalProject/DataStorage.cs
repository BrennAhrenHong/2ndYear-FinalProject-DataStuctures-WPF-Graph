using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphFinalProject
{
    public static class DataStorage
    {
        public static char PrevLetter = 'Z';
        public static int IDNumber = 0;
        public static string RecentIDMade;

        public static List<Label> VertexLabelList = new List<Label>();
        public static List<UnDirectedEdge<UIElement>> EdgeList = new List<UnDirectedEdge<UIElement>>();
        public static List<UIElement> CanvasEdgeList = new List<UIElement>();
        public static List<UIElement> CanvasChildrenList = new List<UIElement>();
        public static List<Vertex<Border>> VertexBorder = new List<Vertex<Border>>();
        public static List<string> ListViewId = new List<string>();
        public static List<int> Weight = new List<int>();

    }
}
