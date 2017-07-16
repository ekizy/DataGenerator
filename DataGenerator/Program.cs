using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator dataGenerator = new Generator();

            dataGenerator.generateUserWorkout();

            int a = 3;
        }
    }
}
