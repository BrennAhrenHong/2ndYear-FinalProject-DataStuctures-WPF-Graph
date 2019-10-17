using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class UnweightedGraph<T> : Graph<T>
    {
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

            //create the list of vertices in the adjacency list
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

        public UnweightedGraph(int[,] edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
        }

        public UnweightedGraph(int[,] edges, int vertexCount)
        {
            Vertices = new List<T>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(default); //if its a class null and structure 0
            }
            CreateAdjacencyList(edges, vertexCount);
        }

        public UnweightedGraph(IList<Edge> edges, IList<T> vertices)
        {
            Vertices = vertices;
            CreateAdjacencyList(edges, vertices.Count);
        }

        public UnweightedGraph(IList<Edge> edges, int vertexCount)
        {
            Vertices = new List<T>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(default);
            }

            CreateAdjacencyList(edges, vertexCount);
        }
    }
}
