using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Graphs
{
    public class GraphsTest
    {
        private Graph<string> CreateGraph()
        {
            //create vertices using an array
            // a = 0 // b = 1 // c = 2
            // d = 3 // e = 4 // f = 5
            // g = 7 // h = 8 // i = 9
            var vertices = new[] {"a", "b", "c", "d", "e", "f", "g", "h", "i"};

            //create edges using an array
            var edges = new int[,]
            {
                {0, 1}, {0, 2}, {0, 4}, // neighbors of a
                {1, 0}, {1, 2}, // neighbors of b
                {2, 0}, {2, 1}, {2, 4}, // neighbors of c
                {3, 4}, {3, 5}, {3, 6}, // neighbors of d
                {4, 0}, {4, 2}, {4, 3}, {4, 7}, // neighbors of e
                {5, 3}, {5, 6}, // neighbors of f
                {6, 3}, {6, 5}, {6, 7}, // neighbors of g
                {7, 4}, {7, 6}, // neighbors of h
            };

            return new UnweightedGraph<string>(edges, vertices);
        }

        [Test]
        public void PrintNeighbors()
        {
            var graph = CreateGraph();

            int vertexCount = graph.VertexCount;
            var vertices = graph.Vertices;
            var neighbors = graph.Neighbors;

            for (int i = 0; i < vertexCount; i++)
            {
                Console.Write($"{vertices[i]}");
                var sb = new StringBuilder();
                foreach (var neighbor in neighbors[i])
                {
                    sb.Append($"{vertices[neighbor]},");
                }

                if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
                Console.WriteLine();
            }
        }

        [Test]
        public void PrintMatrix()
        {
            var graph = CreateGraph();

            int vertexCount = graph.VertexCount;
            var vertices = graph.Vertices;
            var neighbors = graph.Neighbors;
            char letter = 'a';
            bool check = false;

            for (int i = 0; i < vertexCount; i++)
            {
                Console.Write($"{vertices[i]}" + "| ");
                var sb = new StringBuilder();

                for (char j = 'a'; j < 'j'; j++)
                {
                    foreach (var neighbor in neighbors[i])
                    {
                        for (int x = 0; x < neighbor; x++)
                        {
                            letter++;
                        }

                        if (letter == j)
                        {
                            Console.Write(1 + " ");
                            check = true;
                            break;
                        }
                        letter = 'a';
                    }

                    if (!check)
                    {
                        Console.Write(0 + " ");
                    }
                    letter = 'a';
                    check = false;

                }

                if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
                Console.WriteLine();
            }
        }

        [Test]
        public void Example()
        {
            var graph = CreateGraph();

            int vertexCount = graph.VertexCount;
            var vertices = graph.Vertices;
            var neighbors = graph.Neighbors;
            char alpha = 'a';

            for (int i = 0; i < vertexCount; i++)
            {
                Console.Write($"{vertices[i]}");
                var sb = new StringBuilder();
                foreach (var neighbor in neighbors[i])
                {
                    for (int x = 0; x < neighbor; x++)
                    {
                        alpha++;
                    }

                    Console.Write(alpha);
                    alpha = 'a';
                }

                if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
                Console.WriteLine();
            }
        }

        [Test]
        public void SirDex()
        {
            var graph = CreateGraph();

            int vertexCount = graph.VertexCount;
            var vertices = graph.Vertices;
            var neighbors = graph.Neighbors;

            Console.Write($"{" ",5}");
            for (int i = 0; i < vertexCount; i++)
            {
                Console.Write($"{" ",5}");
            }
            Console.Write($"{" ",5}");
        }

        [TestCase("a")] [TestCase("b")][TestCase("c")]
        [TestCase("d")] [TestCase("e")][TestCase("f")]
        [TestCase("g")] [TestCase("h")][TestCase("i")]
        public void DepthFirstSeach_StartAtZero_PrintFOrwardEdges(string startingVertex)
        {
            var graph = CreateGraph();

            var tree = graph.DepthFirstSearch(graph.GetIndex(startingVertex));

            var forwardEdges = tree.ForwardEdges;
            var sb = new StringBuilder();
            Console.WriteLine($"---- Depth first traversal starting @; {startingVertex} ----");
            foreach (var edge in forwardEdges)
            {
                sb.Append($"{{" +
                          $"{graph.GetVertex(edge.FromVertex)}," +
                          $"{graph.GetVertex(edge.ToVertex)}" +
                          $"}},");
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            Console.WriteLine(sb.ToString());
        }
        [TestCase("a")]
        [TestCase("b")]
        [TestCase("c")]
        [TestCase("d")]
        [TestCase("e")]
        [TestCase("f")]
        [TestCase("g")]
        [TestCase("h")]
        [TestCase("i")]
        public void BreadthSearch_StartAtZero_PrintForwardEdges(string startingVertex)
        {
            var graph = CreateGraph();
            var tree = graph.BreadthFirstSearch(graph.GetIndex(startingVertex));

            var forwardEdges = tree.ForwardEdges;
            var sb = new StringBuilder();

            Console.WriteLine($"---- Breadth first traversal starting @: {tree.Root} ----");

            foreach (var edge in forwardEdges)
            {
                sb.Append($"{{" +
                          $"{graph.GetVertex(edge.FromVertex)}," +
                          $"{graph.GetVertex(edge.ToVertex)}" +
                          $"}},");
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            Console.WriteLine(sb.ToString());

            Console.WriteLine($"\nVertex Order:");
            sb.Clear();
            foreach (var i in tree.SearchOrder)
            {
                sb.Append($"{graph.GetVertex(i)},");
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            Console.WriteLine(sb.ToString());
        }
    
    [Test]
        public void DepthFirstSearch_AllIndexes_PrintForwardEdges()
        {
            var graph = CreateGraph();
            var trees = graph.DepthFirstSearch();

            foreach (var tree in trees)
            {
                var forwardEdges = tree.ForwardEdges;
                var sb = new StringBuilder();
                Console.WriteLine($"---- Depth first traversal starting @; {tree.Root} ----");
                Console.WriteLine($"---- Forward Edges ----");
                foreach (var edge in forwardEdges)
                {
                    sb.Append($"{{" +
                              $"{graph.GetVertex(edge.FromVertex)}," +
                              $"{graph.GetVertex(edge.ToVertex)}" +
                              $"}},");
                }

                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                Console.WriteLine(sb.ToString());

                Console.WriteLine($"Vertex Order");
                sb.Clear();
                foreach (var i in tree.SearchOrder)
                {
                    sb.Append($"{graph.GetVertex(i)},");
                }

                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                    Console.WriteLine(sb.ToString());
            }
        }
    }
}
