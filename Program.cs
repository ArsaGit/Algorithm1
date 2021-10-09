using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace AlgorithmLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm1 algorithm1 = new Algorithm1();
            algorithm1.Run();
            algorithm1.Print();
            Console.ReadKey();
        }
    }

}





