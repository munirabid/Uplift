using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
    public class MultiDataSets
    {
        public IEnumerable<Category> Categories { get; set; }

        public List<Frequency> Frequencies { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
