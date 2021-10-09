using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace AlgorithmLab1
{
	public abstract class Algorithm
	{
		private Stopwatch stopWatch;

		private uint numberOfElements;
		private uint numberOfSets;

		private double[] results;	//надо бы сделать udouble, но лень
		private double[,] tempResults;	//здесь тоже

		public Algorithm(uint numberOfElements, uint numberOfSets)
		{
			stopWatch = new Stopwatch();
			this.numberOfElements = numberOfElements;
			this.numberOfSets = numberOfSets;

			tempResults = new double[numberOfElements, numberOfSets];
			results = new double[numberOfElements];
		}

		public void Run()
		{
			GetFullTimeArray();
			GetFinalTimeArray();
		}

		public abstract void AlgorithmBody();

		private void GetFullTimeArray()
		{
			TimeSpan timeSpan;

			for (int i = 0; i < numberOfSets; i++)
			{
				for (int j = 0; j < numberOfElements; j++)
				{
					stopWatch.Start();
					AlgorithmBody();
					stopWatch.Stop();
					timeSpan = stopWatch.Elapsed;
					tempResults[j, i] = Math.Round(timeSpan.TotalSeconds, 6);
					stopWatch.Reset();
				}
				stopWatch.Reset();
			}
		}

		private void GetFinalTimeArray()
		{
			results = GetMin(tempResults);
		}

		private double[] GetMin(double[,] array)
		{
			double[] res = new double[numberOfElements];
			for (int i = 0; i < numberOfElements; i++)
			{
				double temp = array[i, 0];
				for (int j = 0; j < numberOfSets; j++)
				{
					if (array[i, j] < temp) temp = array[i, j];
				}
				res[i] = temp;
			}

			return res;
		}

		private void WriteInFile(string[] results)
		{
			string writePath = GetPath();

			using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
			{
				foreach (var element in results)
				{
					sw.WriteLine(element);
				}
			}
		}

		public void WriteInFile()
		{
			string writePath = GetPath();

			using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
			{
				for (int i = 0;i<results.Length;i++)
				{
					sw.WriteLine("{0};{1: 0.000000}",i,results[i]);
				}
			}
		}

		private string GetPath(string path = "result.csv")
		{
			string filePath = Environment.CurrentDirectory;
			filePath = filePath.Substring(0, filePath.IndexOf("bin")) + path;
			return filePath;
		}

		public void Print()
		{
			foreach(var element in results)
			{
				Console.WriteLine("{0: 0.000000}",element);
			}
		}
	}

	public class Algorithm1 : Algorithm
	{
		private double number;

		public Algorithm1(double number = 1000) : base(2000, 50)
		{
			this.number = number;
		}

		public override void AlgorithmBody()
		{
			double temp = Factorial(number);
		}

		private double Factorial(double n)
		{
			if (n == 0) return 1;
			else if (n > 0) return n * Factorial(n - 1);
			else throw new ArgumentException("Negative argument");
		}
	}


}

