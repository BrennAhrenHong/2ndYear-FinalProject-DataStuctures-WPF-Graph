using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightedGraph<T> : Graph<T>, IWeightedGraph<T>
    {
        #region  Constructors
        public WeightedGraph(IList<WeightedEdge> edges, int vertexCount)
        {
            Vertices = new List<T>();

            // Create vertices since only the vertex count is given
            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(default);
            CreateAdjacencyList(edges, vertexCount);
            CreateNeighborEdges(edges, vertexCount);
        }

        protected void CreateAdjacencyList(IList<WeightedEdge> edges, int vertices)
        {
            Neighbors = new List<IList<int>>();

            // create the list of vertices in the adjacency list
            for (int i = 0; i < vertices; i++)
                Neighbors.Add(new List<int>());

            // List the neighbors of each vertex
            foreach (var edge in edges)
            {
                // gets the edge(Vi Vj)
                int firstVertex = edge.FromVertex; // Vi
                int secondVertex = edge.ToVertex; // Vj

                Neighbors[firstVertex].Add(secondVertex);
            }
        }
        public WeightedGraph(IList<WeightedEdge> edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
            CreateNeighborEdges(edges, vertices.Count);
        }
        #endregion


        #region IWeightedGraph<T>
        public Path GetShortestPath(int sourceVertex)
        {
            // stores the previous vertex of v in the path
            var predecessors = new List<int>();

            // stores the currDist of each vertex. 
            // The index of this variable refers to the index of the vertex.
            var toBeChecked = new List<int>();
            var currDist = new List<double>();
            for (int i = 0; i < VertexCount; i++)
            {
                predecessors.Add(-1);
                toBeChecked.Add(i);
                currDist.Add(double.MaxValue);
            }
            // the parent of the source is -1; root
            predecessors[sourceVertex] = -1;

            // currDist(sourceVertex) = 0
            currDist[sourceVertex] = 0;

            //// remove isolated vertices from toBeChecked
            //var toBeCheckedCopy = new List<int>(toBeChecked);
            //foreach (int i in toBeCheckedCopy)
            //{
            //    if (Neighbors[i].Count == 0) toBeChecked.Remove(i);
            //}

            while (toBeChecked.Count > 0)
            {
                // v = a vertex in toBeChecked with minimal currDist(v)
                int vertexWithMinimalCurrDist = -1;
                double minimalCurrDist = double.MaxValue;
                foreach (var index in toBeChecked)
                {
                    double value = currDist[index];
                    if (value <= minimalCurrDist)
                    {
                        minimalCurrDist = value;
                        vertexWithMinimalCurrDist = index;
                    }
                }

                // remove v from toBeChecked
                toBeChecked.Remove(vertexWithMinimalCurrDist);

                // for all vertices u adjacent to v and in toBeChecked
                var neighbors = Neighbors[vertexWithMinimalCurrDist];
                foreach (int u in neighbors)
                {
                    if (toBeChecked.Contains(u))
                    {
                        // edge(vu)
                        var edgeVu = _neighborEdges[vertexWithMinimalCurrDist].First(c => c.ToVertex == u);
                        double weight = currDist[vertexWithMinimalCurrDist] + edgeVu.Weight;
                        if (currDist[u] > weight)
                        {
                            currDist[u] = weight;
                            predecessors[u] = vertexWithMinimalCurrDist;
                        }
                    }
                }

            }

            return new Path(predecessors, currDist);
        }
        #endregion


        #region Private Methods
        private void CreateNeighborEdges(IList<WeightedEdge> edges, int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
                _neighborEdges.Add(new List<WeightedEdge>());

            foreach (var weightedEdge in edges)
            {
                int firstVertex = weightedEdge.FromVertex;
                _neighborEdges[firstVertex].Add(weightedEdge);
            }
        }
        #endregion


        #region  Fields
        private readonly List<List<WeightedEdge>> _neighborEdges = new List<List<WeightedEdge>>();
        #endregion
    }

    public class Path
    {
        public IList<double> Costs { get; }
        public IList<int> Predecessor { get; }
        public IList<int> SearchOrder { get; }
        public int Root
        {
            get
            {
                if (SearchOrder.Count == 0) return -1;
                return SearchOrder[0];
            }
        }

        public Path(IList<int> predecessor, IList<double> costs)
        {
            Costs = costs;
            Predecessor = predecessor;
        }

    }

    public class Sample
    {

        public void ShortestPathSample()
        {
            var vertices = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k" };
            var edges = new List<WeightedEdge>{
                            new WeightedEdge(0,7,10),//10
                            new WeightedEdge(0,4,1),//
                            new WeightedEdge(1,2,2),
                            new WeightedEdge(3,0,4),
                            new WeightedEdge(3,7,1),
                            new WeightedEdge(4,5,3),
                            new WeightedEdge(5,1,1),
                            new WeightedEdge(5,8,1),
                            new WeightedEdge(5,6,7),
                            new WeightedEdge(7,4,5),//5
                            new WeightedEdge(7,8,9),
                            new WeightedEdge(8,9,2),
                            new WeightedEdge(9,6,1),
            };
            var wg = new WeightedGraph<string>(edges, vertices);
            var shortestPaths = wg.GetShortestPath(3);
            PrintPath(shortestPaths);
        }

        public void PrintPath(Path path)
        {
            Console.WriteLine("Costs:");
            for (int i = 0; i < path.Costs.Count; i++)
                Console.Write($"{i,5}");
            Console.WriteLine();
            for (int i = 0; i < path.Costs.Count; i++)
            {
                if (path.Costs[i] >= double.MaxValue) Console.Write($"{"\u221e",5}");
                else Console.Write($"{path.Costs[i],5}");
            }


            Console.WriteLine();
            Console.WriteLine("Predecessors:");

            for (int i = 0; i < path.Costs.Count; i++)
                Console.Write($"{i,5}");
            Console.WriteLine();
            for (int i = 0; i < path.Costs.Count; i++)
                Console.Write($"{path.Predecessor[i],5}");
            Console.WriteLine();
        }
    }

}
