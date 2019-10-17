using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Tree
    {
        public int Root
        {
            get
            {
                if (SearchOrder.Count == 0) return -1;
                return SearchOrder[0];
            }
        }
        /// <summary>
        /// The order of the visited index of the vertices for the tree.
        /// </summary>
        public IList<int> SearchOrder { get; }

        /// <summary>
        /// The number of vertices visited for this tree
        /// </summary>
        public int NumberOfVertices => SearchOrder.Count;
        /// <summary>
        /// The forward edge (path) formed by traversing the graph.
        /// </summary>
        public IList<Edge> ForwardEdges { get; private set; }

        public Tree(IList<int> searchOrder, IList<Edge> forwardEdges)
        {
            SearchOrder = searchOrder;
            ForwardEdges = forwardEdges;
        }
    }
}
