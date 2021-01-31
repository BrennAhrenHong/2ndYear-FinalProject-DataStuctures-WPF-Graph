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
                Neighbors[secondVertex].Add(firstVertex);
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
