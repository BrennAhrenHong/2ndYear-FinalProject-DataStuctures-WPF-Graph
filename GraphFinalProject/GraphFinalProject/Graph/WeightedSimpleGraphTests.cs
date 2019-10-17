using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class WeightedSimpleGraphTests
    {
        private WeightedGraph<string> CreateWeightedSimpleGraph()
        {
            //create vertices using an array
            // a = 0 // b = 1 // c = 2
            // d = 3 // e = 4 // f = 5
            // g = 7 // h = 8 // i = 9
            var vertices = new[] {"a", "b", "c", "d", "e", "f", "g", "h", "i"};

            var edges = new List<WeightedEdge>
            {
                // neighbors of a
                new WeightedEdge(0, 1, 2), // ab
                new WeightedEdge(0, 2, 4), // ac
                new WeightedEdge(0, 4, 8), // ae

                // neighbors of b
                new WeightedEdge(1, 0, 2), // ba
                new WeightedEdge(1, 2, 3), // bc

                new WeightedEdge(2, 0, 2), // ca
                new WeightedEdge(2, 1, 3), // cb
                new WeightedEdge(2, 4, 7), // ce

                new WeightedEdge(3, 4, 6), // de
                new WeightedEdge(3, 5, 2), // df
                new WeightedEdge(3, 6, 2), // dg

                new WeightedEdge(4, 0, 8), // ea
                new WeightedEdge(4, 2, 7), // ec
                new WeightedEdge(4, 3, 6), // ed
                new WeightedEdge(4, 7, 5), // eh

                new WeightedEdge(5, 3, 2), // fd
                new WeightedEdge(5, 6, 3), // fg

                new WeightedEdge(6, 3, 2), // gd
                new WeightedEdge(6, 5, 3), // gf
                new WeightedEdge(6, 7, 3), // gh

                new WeightedEdge(7, 4, 2), // he
                new WeightedEdge(7, 6, 3), // hg
            };

            return new WeightedGraph<string>(edges, vertices);



                                //     a-------------e  ---
                                //    / \           / |     \
                                //   /   \         /  |
                                //  /     \       /   |
                                // b-------c  ----    |
                                //                    |
                                //                    |
                                //                    |

            ////create edges using an array
            //var edges = new int[,]
            //{
            //    {0, 1}, {0, 2}, {0, 4}, // neighbors of a
            //    {1, 0}, {1, 2}, // neighbors of b
            //    {2, 0}, {2, 1}, {2, 4}, // neighbors of c
            //    {3, 4}, {3, 5}, {3, 6}, // neighbors of d
            //    {4, 0}, {4, 2}, {4, 3}, {4, 7}, // neighbors of e
            //    {5, 3}, {5, 6}, // neighbors of f
            //    {6, 3}, {6, 5}, {6, 7}, // neighbors of g
            //    {7, 4}, {7, 6}, // neighbors of h
            //};

            //return new UnweightedGraph<string>(edges, vertices);
        }
    }
}
