using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public abstract class AbstractAlgorithm2 : Algorithm
	{
		public AbstractAlgorithm2(uint numberOfElements, uint numberOfSets) : base(2000,50)
		{
			FolderName = "Results2\\";
			NumberOfElements = numberOfElements;
			NumberOfSets = numberOfSets;
			tempResults = new double[NumberOfElements, NumberOfSets];
			results = new double[NumberOfElements];
		}
	}

	//
	public class Algorithm2_1 : AbstractAlgorithm2
	{
		public override string FileName => "result2_1.csv";

		public Algorithm2_1() : base(2000,50)
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//
	public class Algorithm2_2 : AbstractAlgorithm2
	{
		public override string FileName => "result2_2.csv";

		public Algorithm2_2() : base(2000,50)
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//
	public class Algorithm2_3 : AbstractAlgorithm2
	{
		public override string FileName => "result2_3.csv";

		public Algorithm2_3() : base(2000, 50)
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//Нахождение самой длинной восходящей последовательности
	public class Algorithm2_4 : AbstractAlgorithm2
	{
		public override string FileName => "result2_4.csv";
		public List<int> ValuesList { get; set; }

		public Algorithm2_4() : base(10000, 50)
		{
			ValuesList = GenerateList(NumberOfElements);
		}

		public override void AlgorithmBody(int j)
		{
			int res = GetAnswer(CopyList(ValuesList, j));
		}

		private static int GetAnswer(List<int> l)
		{
			int n = l.Count;
			//int n = length;

			var tempL = new List<int>() { 1 };
			FenwickTree.Assign(tempL, n + 1, 0);

			FenwickTree fenwickTree = new FenwickTree(tempL);

			var sorted_arr = GetSortedList(l);

			var dictionary = new Dictionary<int, int>();
			for (int i = 0; i < n; i++)
				dictionary[sorted_arr[i]] = i + 1;

			for (int i = 0; i < n; i++) //Сопоставление данных arr
				l[i] = dictionary[l[i]];

			for (int i = 0; i < n; i++)
			{
				int index = l[i]; //берем ранг элементов как индекс дерева

				int x = DoQuery(fenwickTree, index - 1); //максимальная длина в дереве до этого индекса

				int val = x + 1; //увеличивающаяся длина

				while (index <= n) //от родителя к ребенку
				{
					fenwickTree.ft[index] = Math.Max(val, fenwickTree.ft[index]); //длина заполнения при соответствующих индексах
					index += index & (-index); //получение индекса следующего узла в дереве
				}
			}
			return DoQuery(fenwickTree, n);
		}

		private static int DoQuery(FenwickTree tree, int index)
		{
			int ans = 0;

			while (index > 0) //от ребенка к родителю
			{
				ans = Math.Max(tree.ft[index], ans);
				index -= index & (-index); //получение индекса предыдущего узла в дереве
			}
			return ans;
		}

		private static List<int> GetSortedList(List<int> l)
		{
			var sortedList = new List<int>();
			foreach (var e in l)
			{
				sortedList.Add(e);
			}
			sortedList.Sort();
			return sortedList;
		}

		private List<int> GenerateList(uint length)
		{
			Random random = new Random(1);
			var list = new List<int>();

			for(int i = 0; i < length; i++)
			{
				list.Add(random.Next());
			}

			return list;
		}

		private List<int> CopyList(List<int> list, int length)
		{
			var res = new List<int>();

			for(int i = 0; i < length; i++)
			{
				res.Add(list[i]);
			}

			return res;
		}
	}

	//Нахождение самой длинной общей строки
	public class Algorithm2_5 : AbstractAlgorithm2
	{
		public override string FileName => "result2_5.csv";
		public string Str1 { get; set; }
		public string Str2 { get; set; }

		public Algorithm2_5() : base(1500,50)
		{
			Str1 = GenerateRandomString(NumberOfElements);
			Str2 = GenerateRandomString(NumberOfElements);
		}

		private string GenerateRandomString(uint length)
		{
			string str = "";
			Random random = new(1);

			for(int i = 0; i < length; i++)
			{
				str += (char)random.Next(255);
			}

			return str;
		}

		public override void AlgorithmBody(int j)
		{
			string res = FindLongestCommonSubstring(Str1, Str2, j);
		}

		private static string FindLongestCommonSubstring(string a, string b, int length)
		{
			var n = length;
			var m = length;
			var array = new int[n, m];
			var maxValue = 0;
			var maxI = 0;
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					if (a[i] == b[j])
					{
						array[i, j] = (i == 0 || j == 0)
							? 1
							: array[i - 1, j - 1] + 1;
						if (array[i, j] > maxValue)
						{
							maxValue = array[i, j];
							maxI = i;
						}
					}
				}
			}

			return a.Substring(maxI + 1 - maxValue, maxValue);
		}
	}

	//Find Fibonacci Number
	public class Algorithm2_6 : AbstractAlgorithm2
	{
		public override string FileName => "result2_6.csv";

		public Algorithm2_6() : base(30000,50)
		{
		}

		public override void AlgorithmBody(int j)
		{
			//double num = FindNNumberFibonacci1_6(j);
			int k = FindNNumberFibonacci2_6(j);
		}

		private double FindNNumberFibonacci1_6(int number)
		{
			double phi1 = Math.Pow(((1 + Math.Sqrt(5)) * 0.5), number);
			double phi2 = Math.Pow(((1 - Math.Sqrt(5)) * 0.5), number);
			return Math.Round((phi1 - phi2) / Math.Sqrt(5));
		}

		private int FindNNumberFibonacci2_6(int number)
		{
			int perv = 1;
			int vtor = 1;
			int sum;

			int j = 2;
			while (j <= number)
			{
				sum = perv + vtor;
				perv = vtor;
				vtor = sum;
				j++;
			}
			return perv;
		}
	}
}
