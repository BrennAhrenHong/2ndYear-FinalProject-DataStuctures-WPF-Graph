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
        public static int LabelNumber = 0;
        public static string RecentIDMade;

        public static List<Label> VertexList = new List<Label>();
        public static List<UnWeightedEdge<UIElement>> EdgeList = new List<UnWeightedEdge<UIElement>>();
        public static List<UIElement> CanvasChildrenList = new List<UIElement>();
        public static List<Border> VertexBorder = new List<Border>();
        public static List<string> ID = new List<string>();

    }
}
