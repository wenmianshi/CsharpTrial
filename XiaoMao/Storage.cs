using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogwarts
{
    public class Storage : Interior
    {
        public Storage(string name, string weight) : base(name, weight)
        {
        }

        int capacity;
    }
}
