using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IWeightedGraph<T> : IGraph<T>
    {
        /// <summary>
        /// Gets the shortest path from the source vertex to every other vertex.
        /// </summary>
        /// <param name="sourceVertex">The index of the source vertex. Vertex indexes start at zero</param>
        /// <returns>the shortest path of the source vertex tp every other vertex.
        /// contained in the Predecessor property.</returns>
        Path GetShortestPath(int sourceVertex);
    }

    //public class Paths
    //{
    //    public IList<double> Costs { get; set; }
    //    public IList<int> Predecessor { get; set; }
    //    public IList<int> SearchOrder { get; set; }

    //    public int Root
    //    {
    //        get
    //        {
    //            if (SearchOrder.Count == 0) return -1;
    //            return SearchOrder[0];
    //        }
    //    }

    //    public Path(IList<int> predecessor, IList<double> costs)
    //    {
    //        Costs = costs;
    //        Predecessor = predecessor;
    //    }
    //}
}
