using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Edge
    {
        public Edge(int fromVertex, int vertex)
        {
            FromVertex = fromVertex;
            ToVertex = vertex;
        }

        public int FromVertex { get; protected set; }
        public int ToVertex { get; protected set; }
    }

    public class WeightedEdge : Edge, IComparable<WeightedEdge>
    {
        public WeightedEdge(int fromVertex, int toVertex, double weight) : base(fromVertex, toVertex)
        {
            Weight = weight;
        }

        public double Weight { get; set; }

        public int CompareTo(WeightedEdge other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if (Weight > other.Weight) return 1;
            if (Weight < other.Weight) return -1;
            return 0;
        }
    }
}
