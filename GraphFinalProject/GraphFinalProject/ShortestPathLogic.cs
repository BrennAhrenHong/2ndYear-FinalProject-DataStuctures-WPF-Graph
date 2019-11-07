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
        private MainWindow MainWindow { get; set; }
        public ShortestPathLogic(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }
        public void FindShortestDistance(int sourceVertex)
        {
            DataStorage.PredecessorList.Clear();
            var edges = new List<WeightedEdge>();
            foreach (var edge in DataStorage.EdgeList)
            {
                edges.Add(new WeightedEdge(edge.FromVertex.IDNumber, edge.ToVertex.IDNumber, edge.Weight));
            }

            var vertices = new List<string>();
            foreach (var vertex in DataStorage.VerticesList)
            {
                vertices.Add(vertex.Name);
            }

            var weightedGraph = new WeightedGraph<string>(edges, vertices);
            var shortestPaths = weightedGraph.GetShortestPath(sourceVertex);
            PrintPath(shortestPaths);

            //var vertices = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k" };
            //var edges = new List<WeightedEdge>{
            //    new WeightedEdge(0,7,10),//10
            //    new WeightedEdge(0,4,1),//
            //    new WeightedEdge(1,2,2),
            //    new WeightedEdge(3,0,4),
            //    new WeightedEdge(3,7,1),
            //    new WeightedEdge(4,5,3),   
            //    new WeightedEdge(5,1,1),
            //    new WeightedEdge(5,8,1),
            //    new WeightedEdge(5,6,7),
            //    new WeightedEdge(7,4,5),//5
            //    new WeightedEdge(7,8,9),
            //    new WeightedEdge(8,9,2),
            //    new WeightedEdge(9,6,1),
            //};
            //var wg = new WeightedGraph<string>(edges, vertices);
            //var shortestPaths = wg.GetShortestPath(3);
            //PrintPath(shortestPaths);
        }

        public void PrintPath(Path path)
        {
            MainWindow.TxtbCost.Clear();
            MainWindow.TxtbCost.Text = "Costs: \n";
            for (int i = 0; i < path.Costs.Count; i++)
                MainWindow.TxtbCost.Text += ($"{i,5}");

            MainWindow.TxtbCost.Text += "\n";
            for (int i = 0; i < path.Costs.Count; i++)
            {
                if (path.Costs[i] >= double.MaxValue) MainWindow.TxtbCost.Text += $"{"\u221e",5}";
                else MainWindow.TxtbCost.Text += ($"{path.Costs[i],5}");
            }
            MainWindow.TxtbPredecessor.Clear();


            MainWindow.TxtbPredecessor.Text = "Predecessors:";
            MainWindow.TxtbPredecessor.Text += "\n";

            for (int i = 0; i < path.Costs.Count; i++)
                MainWindow.TxtbPredecessor.Text += $"{i,5}";

            MainWindow.TxtbPredecessor.Text += "\n";
            for (int i = 0; i < path.Costs.Count; i++)
                MainWindow.TxtbPredecessor.Text +=$"{path.Predecessor[i],5}";
            MainWindow.TxtbPredecessor.Text += "\n";

            for (int i = 0; i < path.Costs.Count; i++)
                DataStorage.PredecessorList.Add(path.Predecessor[i]);
        }
    }
}