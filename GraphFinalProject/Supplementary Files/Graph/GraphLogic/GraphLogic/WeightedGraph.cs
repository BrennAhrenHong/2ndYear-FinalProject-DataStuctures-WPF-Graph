using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightedGraph<T> : Graph<T>, IWeightedGraph<T>
    {
        #region Constructors
        public WeightedGraph(IList<WeightedEdge> edges, int vertexCount)
        {
            Vertices = new List<T>();

            // Create vertices since only the vertex count is given
            for (int i = 0; i < VertexCount; i++) Vertices.Add(default);
            CreateAdjacencyList(edges, vertexCount);
            CreateNeighborEdges(edges, vertexCount);
        }

        public WeightedGraph(IList<WeightedEdge> edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
            CreateNeighborEdges(edges, vertices.Count);
        }
        #endregion

        private IList<List<WeightedEdge>> _neighborEdges = new List<List<WeightedEdge>>();

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
        public Path GetShortestPath(int sourceVertex)
        {
            throw new NotImplementedException();
        }
    }
}
