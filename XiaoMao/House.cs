using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hogwarts
{
    public class House(string name, int capacity)
    {
        bool MoveIn(Human human)
        {
            if (currentNumberOfHumans == capacity)
            {
                return false;
            }

            currentNumberOfHumans++;
            humans.Add(human);
            return true;
        }

        string name = name;
        int capacity = capacity;
        int currentNumberOfHumans;
        List<Human> humans;

    }
}
