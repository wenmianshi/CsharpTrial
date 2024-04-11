using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogwarts
{
    public class Interior(string name, string weight)
    {
        string name = "unknown";
        double weight = 0;

        public string Name { get; } = name;
        public string Weight { get; } = weight;
    }
}
