using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphFinalProject
{
    public static class DataStorage
    {
        public static int IDNumber = 0;
        public static string RecentIDMade;

        public static List<Label> VertexLabelList = new List<Label>();
        public static List<DirectedEdge<UIElement>> EdgeList = new List<DirectedEdge<UIElement>>();
        public static List<UIElement> CanvasChildrenList = new List<UIElement>();
        public static List<Vertex<Border>> VerticesList = new List<Vertex<Border>>();
        public static List<string> ListViewId = new List<string>();
        public static List<int> Weight = new List<int>();
        public static List<int> PredecessorList = new List<int>();

    }
}
