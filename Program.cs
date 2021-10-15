using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;

namespace AlgorithmLab1
{
    class Program
    {
        static void Main(string[] args)
        {
			RunAlgorithm();
		}

		static void Execute(Algorithm algorithm)
		{
            algorithm.Run();
            algorithm.Print();
		}

		static void RunAlgorithm()
		{
			Algorithm1 algorithm1 = new Algorithm1();
			Algorithm2 algorithm2 = new Algorithm2();
			Algorithm3 algorithm3 = new Algorithm3();
			Algorithm4 algorithm4 = new Algorithm4();
			Algorithm5 algorithm5 = new Algorithm5();
			Algorithm6 algorithm6 = new Algorithm6();
			Algorithm7 algorithm7 = new Algorithm7();
			Algorithm8 algorithm8 = new Algorithm8();
			Algorithm9 algorithm9 = new Algorithm9();
			Algorithm10 algorithm10 = new Algorithm10();

			Algorithm2_4 algorithm2_4 = new();
			Algorithm2_5 algorithm2_5 = new();
			Algorithm2_6 algorithm2_6 = new();

			Execute(algorithm2_4);
		}
	}

}





