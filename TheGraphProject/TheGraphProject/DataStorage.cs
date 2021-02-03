using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheGraphProject
{
    public class DataTemplate
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int Weight { get; set; }
        public string Edge { get; set; }

        //public void IDAdder)
        //{
        //    char comparer = 'A';
        //    for (int i = 0; i < DataStorage.VerticesListViewItems[i].ID; i++)
        //    {
        //        if (DataStorage.VerticesListViewItems[i + 1].ID == comparer++)
        //            continue;
        //        else
        //        {
        //            DataTemplate x = new DataTemplate();
        //            DataStorage.VerticesListViewItems[i]
        //        }
        //    }
        //}
    }
    
    public static class DataStorage
    {
        public static char SelectedStartingVertex { get; set; }
        public static char SelectedEndingVertex { get; set; }

        public static List<DataTemplate> VerticesListViewItems = new List<DataTemplate>();
        public static List<DataTemplate> EdgesListViewItems = new List<DataTemplate>();
        public static List<Vertex> VerticesList = new List<Vertex>();
        public static List<LineEdge> EdgeList = new List<LineEdge>();
        public static List<LineEdge> EdgeLabelList = new List<LineEdge>();
        public static List<int> PredecessorList = new List<int>();
        //public static List<char> ListViewIDArray = new List<char>();
        public static List<int> UniqueIDList = new List<int>();
        public static Stack<char> IDStack = new Stack<char>();
    }
}
