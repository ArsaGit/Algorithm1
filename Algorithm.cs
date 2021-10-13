using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;	//библиотека для работы с таймером
using System.IO;

namespace AlgorithmLab1
{
	public abstract class Algorithm
	{
		private Stopwatch stopWatch;	//сам таймер

		public abstract string FileName { get; }
		public uint NumberOfElements { get; set; }
		public uint NumberOfSets { get; set; }

		private double[] results;	//надо бы сделать udouble, но лень
		private double[,] tempResults;  //здесь тоже

		public Algorithm(uint numberOfElements, uint numberOfSets)
		{
			stopWatch = new Stopwatch();
			NumberOfElements = numberOfElements;
			NumberOfSets = numberOfSets;

			tempResults = new double[numberOfElements, numberOfSets];
			results = new double[numberOfElements];
		}

		public void Run()
		{
			GetFullTimeArray();
			//GetFinalTimeArray();
			GetFinalTimeArray2();
			WriteInFile();
		}

		public abstract void AlgorithmBody(int j);

		private void GetFullTimeArray()
		{
			TimeSpan timeSpan;

			for (int i = 0; i < NumberOfSets; i++)
			{
				for (int j = 0; j < NumberOfElements; j++)
				{
					stopWatch.Start();
					AlgorithmBody(j);
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
			double[] res = new double[NumberOfElements];
			for (int i = 0; i < NumberOfElements; i++)
			{
				double temp = array[i, 0];
				for (int j = 0; j < NumberOfSets; j++)
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
			string fullPath = GetPath(FileName);

			CreateFile(fullPath);

			using (StreamWriter sw = new StreamWriter(fullPath, false, System.Text.Encoding.Default))
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
			filePath = filePath.Substring(0, filePath.IndexOf("bin")) + "Results\\" + path;
			return filePath;
		}

		private void CreateFile(string fullPath)
		{
			FileInfo fileInfo = new FileInfo(fullPath);

			if(!fileInfo.Exists)
			{
				fileInfo.Create();
			}
		}

		public void Print()
		{
			foreach(var element in results)
			{
				Console.WriteLine("{0: 0.000000}",element);
			}
		}

		private void GetFinalTimeArray2()
		{
			double[] upperBound = new double[NumberOfElements];
			double[] lowerBound = new double[NumberOfElements];
			double threshold = 3;

			double avg, std;

			for(int i = 0; i < NumberOfElements; i++)
			{
				double[] oneDimArray = ExtractOneDimArray(tempResults, i);
				avg = GetAverage(oneDimArray);
				std = GetAverage(oneDimArray);

				upperBound[i] = avg + threshold * std;
				lowerBound[i] = avg - threshold * std;
			}

			for (int i = 0; i < NumberOfElements; i++)
			{
				double sum = 0;
				int anomaliesCount = 0;

				for (int j = 0; j < NumberOfSets; j++)
				{
					if (tempResults[i, j] < upperBound[i] &&
						tempResults[i, j] > lowerBound[i])
					{
						sum += tempResults[i, j];
					}
					else
					{
						anomaliesCount++;
					}
				}

				results[i] = sum / (NumberOfSets - anomaliesCount);
			}
		}

		private double[] ExtractOneDimArray(double[,] twoDimArray, int dimension)
		{
			double[] oneDimArray = new double[twoDimArray.GetLength(1)];

			for(int i = 0; i < oneDimArray.Length; i++)
			{
				oneDimArray[i] = twoDimArray[dimension, i];
			}

			return oneDimArray;
		}

		private double GetAverage(params double[] arr)
		{
			double sum = 0;
			int length = arr.Length;

			for(int i = 0; i < length; i++)
			{
				sum += arr[i];
			}

			return sum / length;
		}

		private double GetStd(params double[] values)
		{
			double avg = GetAverage(values);
			double[] stdSqrt = new double[values.Length];

			for (int i = 0; i < values.Length; i++)
			{
				stdSqrt[i] = Math.Pow(values[i] - avg, 2);
			}

			return Math.Sqrt(GetAverage(stdSqrt));
		}
	}

	public class Algorithm1 : Algorithm
	{
		private double number;
		public override string FileName => "result-1.csv";

		public Algorithm1(double number = 10000) : base(2000, 50)
		{
			this.number = number;
		}

		public override void AlgorithmBody(int j)
		{
			double temp = Factorial(number);
			double temp1 = Factorial(number);
		}

		private double Factorial(double n)
		{
			if (n == 0) return 1;
			else if (n > 0) return n * Factorial(n - 1);
			else throw new ArgumentException("Negative argument");
		}
	}

	public abstract class AlgorithmWithArray : Algorithm
	{
		public double[] ValuesArray { get; set; }

		public AlgorithmWithArray() : base(2000, 50)
		{
			ValuesArray = new double[NumberOfElements];
			FillArray();
		}

		public void FillArray(int minValue = 1000000, int maxValue = 1000000000, int seed = 1)
		{
			Random random = new Random(seed);
			
			for(int i = 0; i < NumberOfElements; i++)
			{
				ValuesArray[i] = random.Next(minValue,maxValue);
			}
		}
	}

	public class Algorithm2 : AlgorithmWithArray
	{
		public override string FileName => "result-2.csv";

		public Algorithm2() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			double res = 0;
			for(int i = 0; i < j; i++)
			{
				res += ValuesArray[i];
			}	
		}
	}

	public class Algorithm3 : AlgorithmWithArray
	{
		public override string FileName => "result-3.csv";

		public Algorithm3() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			double res = 0;
			for (int i = 0; i < j; i++)
			{
				res *= ValuesArray[i];
			}
		}
	}

	public class Algorithm4 : AlgorithmWithArray
	{
		public override string FileName => "result-4.csv";

		public Algorithm4() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			double res = 0;

			for(int i = 0; i < j; i++)
			{
				res += ValuesArray[i] + Math.Pow(1.5d, i);
			}
		}
	}

	public class Algorithm5 : AlgorithmWithArray
	{
		public override string FileName => "result-5.csv";

		public Algorithm5() : base()
		{
		}

		public override void AlgorithmBody(int arrayLength)
		{
			double temp;

			for(int j = 0; j < arrayLength - 1; j++)
			{
				for(int i = 0; i < arrayLength - 1; i++)
				{
					if(ValuesArray[i] > ValuesArray[i + 1])
					{
						temp = ValuesArray[i + 1];
						ValuesArray[i + 1] = ValuesArray[i];
						ValuesArray[i] = temp;
					}
				}
			}
		}
	}

	public class Algorithm6 : AlgorithmWithArray
	{
		public override string FileName => "result-6.csv";

		public Algorithm6() : base()
		{
		}

		public override void AlgorithmBody(int arrayLength)
		{
			Quick_Sort(ValuesArray, 0, arrayLength - 1);
		}

		private void Quick_Sort(double[] VariablesArray, int left, int right)
		{
			if (left < right)
			{
				int pivot = Partition(VariablesArray, left, right);

				if (pivot > 1)
				{
					Quick_Sort(VariablesArray, left, pivot - 1);
				}
				if (pivot + 1 < right)
				{
					Quick_Sort(VariablesArray, pivot + 1, right);
				}
			}

		}

		private int Partition(double[] arr, int left, int right)
		{
			double pivot = arr[left];
			while (true)
			{
				while (arr[left] < pivot)
				{
					left++;
				}

				while (arr[right] > pivot)
				{
					right--;
				}

				if (left < right)
				{
					if (arr[left] == arr[right]) return right;

					double temp = arr[left];
					arr[left] = arr[right];
					arr[right] = temp;
				}
				else
				{
					return right;
				}
			}
		}
	}

	public class Algorithm7 : AlgorithmWithArray
	{
		public override string FileName => "result-7.csv";
		private TimSort timSort;

		public Algorithm7() : base()
		{
			timSort = new TimSort();
		}

		public override void AlgorithmBody(int j)
		{
			timSort.Sort(ValuesArray, j);
		}
	}

	public class Algorithm8 : AlgorithmWithArray
	{
		public override string FileName => "result-8.csv";
		private double Number { get; }
		private uint Power { get; }

		public Algorithm8(double number = 13.7d, uint power = 20000) : base()
		{
			Number = number;
			Power = power;
		}

		public override void AlgorithmBody(int j)
		{
			double res = RecPow(Number, Power);
		}

		private double Pow(double number, uint power)
		{
			double res = 1;
			int k = 0;
			
			while(k < power)
			{
				res *= number;
				k++;
			}

			return res;
		}

		private double RecPow(double number, uint power)
		{
			if (power == 0) return 1;
			else
			{
				if (power % 2 == 1)
				{
					return RecPow(number, power / 2) * RecPow(number, power / 2) * number;
				}
				else
				{
					return RecPow(number, power / 2) * RecPow(number, power / 2);
				}
			}

		}

		private double QuickPow(double number, uint power)
		{
			double f;
			double c = number;
			uint k = power;

			if(k % 2 == 1)
			{
				f = c;
			}
			else
			{
				f = 1;
			}

			do
			{
				k /= 2;
				c *= c;

				if(k % 2 == 1)
				{
					f *= c;
				}
			} while (k != 0);

			return f;
		}
	}

	public class Algorithm9 : Algorithm
	{
		public override string FileName => "result-9.csv";
		public double[,] A { get; set; }
		public double[,] B { get; set; }

		public Algorithm9() : base(100, 50)
		{
			A = FillArray(NumberOfElements, NumberOfElements);
			B = FillArray(NumberOfElements, NumberOfElements);
		}

		public override void AlgorithmBody(int j)
		{
			double[,] result = Multiplication.MultiplyMatrix(A, B , j);
		}

		private double[,] FillArray(uint rows, uint cols)
		{
			Random random = new Random(1);
			double[,] arr = new double[rows, cols];

			for(int i = 0; i < rows; i++)
			{
				for(int j = 0; j < cols; j++)
				{
					arr[i, j] = random.NextDouble();
				}
			}

			return arr;
		}
	}

	public class Algorithm10 : AlgorithmWithArray
	{
		public override string FileName => "result-10.csv";

		public Algorithm10() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			double[] sortedArray = TreeSort.Sort(ValuesArray, j);
		}
	}
}

