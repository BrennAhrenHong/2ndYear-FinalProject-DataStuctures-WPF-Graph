using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Common;

namespace GraphLogic
{
    public class UnWeightedGraph<T> : Graph<T>
    {

        public UnWeightedGraph(int[,] edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
        }

        public UnWeightedGraph(int[,] edges, int vertexCount)
        {
            Vertices = new List<T>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(default); //if its a class null and structure 0
            }
            CreateAdjacencyList(edges, vertexCount);
        }

        public UnWeightedGraph(IList<Edge> edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
        }

        public UnWeightedGraph(IList<Edge> edges, int vertexCount)
        {
            Vertices = new List<T>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(default);
            }

            CreateAdjacencyList(edges, vertexCount);
        }
        public UnWeightedGraph(IList<WeightedEdge> edges, int vertexCount)
        {
            //Vertices = new List<T>();

            //for (int i = 0; i < vertexCount; i++)
            //{
            //    Vertices.Add(default);
            //}

            //CreateAdjacencyList(edges, vertexCount);

            Vertices = new List<T>();

            //Create vertices since only the vertex count is given
            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(default);

            CreateAdjacencyList(edges, vertexCount);
            CreateNeighborEdges(edges, vertexCount);
        }

        public UnWeightedGraph(IList<Edge> edges, int vertexCount, bool isDirected)
        {
            Vertices = new List<T>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(default);
            }

            CreateAdjacencyList(edges, vertexCount, isDirected);
        }

        private void CreateAdjacencyList(int[,] edges, int verticesCount)
        {
            Neighbors = new List<IList<int>>();

            //create the list of vertices in the adjacency list
            for (int i = 0; i < verticesCount; i++)
            {
                Neighbors.Add(new List<int>());
            }

            // list the neighbors of each vertex
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                //Get the edge
                int firstVertex = edges[i, 0];
                int secondVertex = edges[i,1];

                Neighbors[firstVertex].Add(secondVertex);
            }
        }

        private void CreateAdjacencyList(IList<Edge> edges, int verticesCount)
        {
            Neighbors = new List<IList<int>>();
            
            // create the list of vertices in the adjacency list
            for (int i = 0; i < verticesCount; i++)
            {
                Neighbors.Add(new List<int>());
            }

            // list the neighbors of each vertex
            foreach (var edge in edges)
            {
                int firstVertex = edge.FromVertex;
                int secondVertex = edge.ToVertex;

                Neighbors[firstVertex].Add(secondVertex);
            }
        }

        private void CreateAdjacencyList(IList<Edge> edges, int verticesCount, bool isDirected)
        {
            Neighbors = new List<IList<int>>();

            // create the list of vertices in the adjacency list
            for (int i = 0; i < verticesCount; i++)
            {
                Neighbors.Add(new List<int>());
            }

            // list the neighbors of each vertex
            foreach (var edge in edges)
            {
                int firstVertex = edge.FromVertex;
                int secondVertex = edge.ToVertex;

                Neighbors[firstVertex].Add(secondVertex);
                if(!isDirected)
                    Neighbors[secondVertex].Add(firstVertex);
            }
        }

        private void CreateAdjacencyList(IList<WeightedEdge> edges, int verticesCount)
        {
            Neighbors = new List<IList<int>>();

            //create the list of vertices in the adjacency list
            for (int i = 0; i < verticesCount; i++)
            {
                Neighbors.Add(new List<int>());
            }

            // list the neighbors of each vertex
            foreach (var edge in edges)
            {
                //Get the edge
                int firstVertex = edge.FromVertex;
                int secondVertex = edge.ToVertex;

                Neighbors[firstVertex].Add(secondVertex);
            }
        }


        private void CreateNeighborEdges(IList<WeightedEdge> edges, int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
                _neighborEdges.Add(new List<WeightedEdge>());

            foreach (var weightedEdge in edges)
            {
                int fromVertex = weightedEdge.FromVertex;
                _neighborEdges[fromVertex].Add(weightedEdge);
            }
        }

        private readonly IList<List<WeightedEdge>> _neighborEdges = new List<List<WeightedEdge>>();

        //Produces the Predecessor List
        public Path GetShortestPath(int sourceVertex)
        {
            //stores the previous vertex of v in the path
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

        private readonly Stack<int> _shortestPathDestination = new Stack<int>();
        public Stack<int> ShortestPathDestination(Path path, int vertexIndex)
        {
            if (path.Costs[vertexIndex] >= double.MaxValue)
                throw new InvalidOperationException("origin and destination are not connected"); //if distance is infinity then exit

            if (_shortestPathDestination.Count > 0) _shortestPathDestination.Clear();

            _shortestPathDestination.Push(vertexIndex); //get the destination

            _shortestPathDestination.Push(path.Predecessor[vertexIndex]); // get predecessor of the destination
            return ShortestPathDestinationHelper(path, path.Predecessor[vertexIndex]); //get previous predecessor recursively
        }

        private Stack<int> ShortestPathDestinationHelper(Path path, int vertexIndex)
        {
            if (path.Predecessor[vertexIndex] != -1)
            {
                _shortestPathDestination.Push(path.Predecessor[vertexIndex]);

                if (path.Costs[vertexIndex] >= double.MaxValue)
                    throw new InvalidOperationException("origin and destination are not connected"); //if distance is infinity then exit

                ShortestPathDestinationHelper(path, path.Predecessor[vertexIndex]);
            }
            return _shortestPathDestination;
        }

        public new string BreadthFirstSearch(int vertexOrigin)
        {
            var currentVertex = Neighbors[vertexOrigin];
            var currentVertexList = currentVertex.ToList();
            currentVertexList.Sort();

            string path = "";
            Queue<int> visitQueue = new Queue<int>();
            var visitedList = new List<int>();
            visitQueue.Enqueue(vertexOrigin);
            visitedList.Add(vertexOrigin);

            while (visitQueue.Count > 0)
            {

                foreach (var i in currentVertexList)
                {
                    if(visitedList.Contains(i))
                        continue;
                    visitQueue.Enqueue(i);
                    visitedList.Add(i);
                }
                path += visitQueue.Dequeue() + " ";
                if (visitQueue.Count > 0)
                {
                    currentVertex = Neighbors[visitQueue.Peek()];
                    currentVertexList = currentVertex.ToList();
                    currentVertexList.Sort();
                }
            }

            return path;
        }
    }
}
