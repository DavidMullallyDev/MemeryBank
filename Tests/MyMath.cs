using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    //This is purley as an example... noramlly the class to be tested would be in another class library eg. Services
    internal class MyMath
    {
        public int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }
}
