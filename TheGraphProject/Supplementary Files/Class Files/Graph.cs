using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLogic
{
    public abstract class Graph<T> : IGraph<T>
    {
        public int VertexCount => Vertices.Count;
        public IList<T> Vertices { get; protected set; }
        public T GetVertex(int index)
        {
            return Vertices[index];
        }

        public int GetIndex(T vertex)
        {
            return Vertices.IndexOf(vertex);
        }
        public IList<T> GetNeighbors(int vertexIndex)
        {
            var vertexNeighbors = new List<T>();

            //Translate the int index of neighbors from the adjacency list
            //to the actual vertices
            foreach (int neighbor in Neighbors[vertexIndex])
            {
                vertexNeighbors.Add(Vertices[neighbor]);
            }

            return vertexNeighbors;
        }

        public IList<int> GetNeighborsByIndex(int vertexIndex)
        {
            return Neighbors[vertexIndex];
        }
        public int GetDegree(int vertexIndex)
        {
            return Neighbors[vertexIndex].Count;
        }

        public int GetDegree(T vertex, IComparer<T> comparer)
        {
            return Neighbors[GetIndex(vertex)].Count;
        }

        public int[,] AdjacencyMatrix => CreateAdjacencyMatrix();

        private int[,] CreateAdjacencyMatrix()
        {
            var adjacencyMatrix = new int[VertexCount, VertexCount];

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                var neighbors = Neighbors[i];
                foreach (var neighbor in neighbors)
                {
                    adjacencyMatrix[i, neighbor] = 1;
                }
            }

            return adjacencyMatrix;
        }

        // TODO fix this
        public IList<Tree> DepthFirstSearch()
        {
            var trees = new List<Tree>();

            // initialize visit indexes for each vertex to zero
            var visitIndexes = new List<int>();
            for (int j = 0; j < VertexCount; j++) visitIndexes.Add(0);
            int i = 1;

            var unvisitedIndexes = visitIndexes
                .Where(c => c == 0)
                .ToList();
            while (unvisitedIndexes.Count > 0)
            {
                // storage for the visit order of the vertices for this tree
                var searchOrder = new List<int>();

                // storage for the forward edges
                var forwardEdges = new List<Edge>();

                Dfs(unvisitedIndexes[0], searchOrder, forwardEdges, ref i, visitIndexes);

                trees.Add(new Tree(searchOrder, forwardEdges));

                // update the unvisited indexes
                unvisitedIndexes.Clear();
                for (int j = 0; j < visitIndexes.Count; j++)
                {
                    if(visitIndexes[j] == 0)
                        unvisitedIndexes.Add(j);
                }
            }

            return trees;
        }

        private void Dfs(int vertexIndex, List<int> searchOrder, 
            List<Edge> forwardEdges, ref int i, List<int> visitIndexes)
        {
            visitIndexes[vertexIndex] = i++;
            var neighbors = Neighbors[vertexIndex];
            searchOrder.Add(vertexIndex);

            foreach (int currentNeighbor in neighbors)
            {
                if (visitIndexes[currentNeighbor] == 0)
                {
                    forwardEdges.Add(new Edge(vertexIndex, currentNeighbor));
                    Dfs(currentNeighbor, searchOrder, forwardEdges, ref i, visitIndexes);
                }
            }
        }

        public Tree DepthFirstSearch(int vertexIndex)
        {
            var visitedIndexes = new List<int>();

            // initialize visit indexes for each vertex to zero
            for (int j = 0; j < VertexCount; j++)
            {
                visitedIndexes.Add(0);
            }

            int i = 1;

            // storage for the visit order of the vertices for this tree
            var searchOrder = new List<int>();

            // storage for the forward edges
            var forwardEdges = new List<Edge>();

            Dfs(vertexIndex, searchOrder, forwardEdges, ref i, visitedIndexes);

            return new Tree(searchOrder, forwardEdges);
        }

        public IList<Tree> BreadthFirstSearch()
        {
            throw new NotImplementedException();
        }

        #region
        public Tree BreadthFirstSearch(int vertexIndex)
        {
            // Initialize visit indexes for each vertex to zero
            var visitIndexes = new List<int>();
            for (int j = 0; j < VertexCount; j++) visitIndexes.Add(0);
            int i = 1;

            var q = new Queue<int>();

            //storage for the visit order of the vertices for this tree
            var searchOrder = new List<int>();

            // storage for the forward edges
            var forwardEdges = new List<Edge>();

            int v = vertexIndex;
            visitIndexes[v] = i++;
            q.Enqueue(v);

            searchOrder.Add(v);

            while (q.Count > 0)
            {
                int deq = q.Dequeue();

                var neighbors = GetNeighborsByIndex(deq);
                foreach (var neighbor in neighbors)
                {
                    if (visitIndexes[neighbor] == 0)
                    {
                        searchOrder.Add(neighbor);

                        visitIndexes[neighbor] = i++;
                        q.Enqueue(neighbor);
                        forwardEdges.Add(new Edge(deq, neighbor
                        ));
                    }
                }
            }

            return new Tree(searchOrder, forwardEdges);
        }
        #endregion

        public IList<IList<int>> Neighbors { get; protected set; }
    }
}

