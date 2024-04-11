using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogwarts
{
    struct Status
    {
        bool hungry;
        bool thirsty;
        bool sleepy;
    }
    public class Human
    {
        string firstName;
        string lastName;
        private int age;
        public Human(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            Age = age;
        }

        public int Age { get => age; set => age = value; }

        public void CelebrateBirtday()
        {
            Age++;
        }

    }
}
