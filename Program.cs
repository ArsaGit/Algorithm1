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
			//RunAlgorithm();

			
		}

		private static List<char> FindSingleClique_3(Dictionary<char, List<char>> graph)
		{
			var clique = new List<char>();
			var vertices = new List<char>();
			foreach (var e in graph)
				vertices.Add(e.Key);

			var rnd = new Random();
			for (int i = 0; i <= 1; i++) //возвращает случайно выбранное число из последовательности
				clique.Add(vertices[rnd.Next(vertices.Count)]);

			foreach (var v in vertices)
			{
				if (clique.Contains(v))
					continue;
				var isNext = true;
				foreach (var u in clique)
				{
					if (graph[v].Contains(u))
						continue;
					else
						isNext = false;
					break;
				}
				if (isNext)
					clique.Add(v);
			}
			clique.Sort();
			return clique;
		}

		private static void Alg2_3()
		{
			var graph = new Dictionary<char, List<char>>
			{
				['A'] = new List<char> { 'B', 'C', 'E' },
				['B'] = new List<char> { 'A', 'C', 'D', 'F' },
				['C'] = new List<char> { 'A', 'B', 'D', 'F' },
				['D'] = new List<char> { 'C', 'E', 'B', 'F' },
				['E'] = new List<char> { 'A', 'D' },
				['F'] = new List<char> { 'B', 'C', 'D' }
			};

			var l = FindSingleClique_3(graph);

			foreach (var e in l)
				Console.WriteLine(e);
		}






		private static int[,] GenerateGraphArray(uint length)
		{
			Random random = new();
			var arr = new int[length, length];

			for (int i = 0; i < length; i++)
			{
				arr[i, i] = 0;
				for (int j = 0; j < i; j++)
				{
					arr[i, j] = random.Next(2);
					arr[j, i] = arr[i, j];
				}
			}

			return arr;
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





