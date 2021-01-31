using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGraphProject
{
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
}
