using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GraphLogic;

namespace TheGraphProject
{
    public class ListViewVerticesTemplate
    {
        public ListViewVerticesTemplate(string name, string id)
        {
            Name = name;
            ID = id;
        }
        public string Name { get; set; }
        public string ID { get; set; }
    }

    public class ListViewEdgeTemplate
    {
        public ListViewEdgeTemplate(Vertex vertexA, Vertex vertexB, string weight)
        {
            VertexA = vertexA;
            VertexB = vertexB;
            Weight = weight;
        }

        public  string Name { get; set; }
        public string Weight { get; set; }
        public string Edge { get; set; }
        public Vertex VertexA { get; set; }
        public Vertex VertexB { get; set; }
    }
    
    public static class DataStorage
    {

        public static List<ListViewVerticesTemplate> VerticesListViewItems = new List<ListViewVerticesTemplate>();
        public static List<ListViewEdgeTemplate> EdgesListViewItems = new List<ListViewEdgeTemplate>();
        public static List<Vertex> VerticesList = new List<Vertex>();
        public static List<LineEdge> EdgeList = new List<LineEdge>();
        public static List<int> PredecessorList = new List<int>();
        public static List<int> UniqueIDList = new List<int>();
    }
}
