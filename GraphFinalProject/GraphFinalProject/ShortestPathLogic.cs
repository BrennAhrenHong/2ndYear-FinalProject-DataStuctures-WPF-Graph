using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Graphs;

namespace GraphFinalProject
{
    public class ShortestPathLogic
    {
        public void FindShortestDistance()
        {
            var edges = new List<WeightedEdge>();
            foreach (var edge in DataStorage.EdgeList)
            {
                edges.Add(new WeightedEdge(edge.FromVertex.IDNumber,edge.ToVertex.IDNumber,edge.Weight));
            }

            var vertices = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k" };
            var Zedges = new List<WeightedEdge>{
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
            var wg = new WeightedGraph<string>(Zedges, vertices);
            var shortestPaths = wg.GetShortestPath(3);
            //PrintPath(shortestPaths);
        }
    }
}