using System;
using System.Collections;
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

	//Задача коммивояжера
	public class Algorithm2_1 : AbstractAlgorithm2
	{
		public override string FileName => "result2_1.csv";
		int[,] Graph;

		public Algorithm2_1() : base(15,5)
		{
			Graph = GenerateGraphArray(NumberOfElements);
		}

		public override void AlgorithmBody(int j)
		{
			int length = TravllingSalesmanProblem(Graph, 0, j);
		}

		private static void RotateRight(IList sequence, int count)
		{
			object tmp = sequence[count - 1];
			sequence.RemoveAt(count - 1);
			sequence.Insert(0, tmp);
		}

		private static IEnumerable<IList> Permutate(IList sequence, int count)
		{
			if (count == 1) yield return sequence;
			else
			{
				for (int i = 0; i < count; i++)
				{
					foreach (var perm in Permutate(sequence, count - 1))
						yield return perm;
					RotateRight(sequence, count);
				}
			}
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
					arr[i, j] = random.Next(10000);
					arr[j, i] = arr[i, j];
				}
			}

			return arr;
		}

		private static int TravllingSalesmanProblem(int[,] graph, int s, int size)
		{
			// store all vertex apart from source vertex
			List<int> vertex = new List<int>();
			for (int i = 0; i < size; i++)
				if (i != s)
					vertex.Add(i);

			var permutations = Permutate(vertex, vertex.Count);

			// store minimum weight Hamiltonian Cycle.
			int min_path = Int32.MaxValue;
			foreach (var permu in permutations)
			{
				// store current Path weight(cost)
				int current_pathweight = 0;

				// compute current path weight
				int k = s;
				for (int i = 0; i < permu.Count; i++)
				{
					current_pathweight += graph[k, Convert.ToInt32(permu[i])];
					k = Convert.ToInt32(permu[i]);
				}
				current_pathweight += graph[k, s];

				// update minimum
				min_path = Math.Min(min_path, current_pathweight);
			}

			return min_path;
		}
	}

	//Алгоритм Брона-Кербоша для поиска максимальных клик
	public class Algorithm2_2 : AbstractAlgorithm2
	{
		public override string FileName => "result2_2.csv";
		int[,] Graphs { get; set; }

		public Algorithm2_2() : base(150,50)
		{
			Graphs = GenerateGraphArray(NumberOfElements);
		}

		private int[,] GenerateGraphArray(uint length)
		{
			Random random = new();
			var arr = new int[length, length];

			for(int i = 0; i < length; i++)
			{
				arr[i, i] = 0;
				for(int j = 0; j < i; j++)
				{
					arr[i, j] = random.Next(2);
					arr[j, i] = arr[i, j];
				}
			}

			return arr;
		}

		public override void AlgorithmBody(int j)
		{
			int res = FindCliques(new Graph(Graphs, j).Nodes);
		}

		private static int FindCliques(
			List<Node> remaining_nodes = null,
			List<Node> potential_clique = null,
			List<Node> skip_nodes = null,
			int depth = 0)
		{
			if (skip_nodes is null) skip_nodes = new List<Node>();
			if (remaining_nodes.Count == 0 && skip_nodes.Count == 0 && depth != 0)
			{

				//Console.Write("This is a clique:");
				//PrintListNode(potential_clique);
				return 1;
			}

			int found_cliques = 0;

			for (int i = 0; i < remaining_nodes.Count; i++)
			{
				Node node = remaining_nodes[i];
				List<Node> new_potential_clique = potential_clique;
				if (new_potential_clique is null) new_potential_clique = new List<Node>();
				new_potential_clique.Add(node);
				List<Node> new_remaining_nodes = GetNodes(remaining_nodes, node);
				List<Node> new_skip_list = GetNodes(skip_nodes, node);
				if (new_skip_list is null) new_skip_list = new List<Node>();
				found_cliques += FindCliques(new_remaining_nodes, new_potential_clique, new_skip_list, depth + 1);

				remaining_nodes.Remove(node);
				if (skip_nodes is null) skip_nodes = new List<Node>();
				skip_nodes.Add(node);
			}

			return found_cliques;
		}

		private static List<Node> GetNodes(List<Node> listNodes, Node node)
		{
			List<Node> newListNodes = new List<Node>();

			if (!(listNodes is null))
			{
				foreach (var n in listNodes)
				{
					if (node.Neighbours.Contains(n.Name)) newListNodes.Add(n);
				}
			}
			return newListNodes;
		}

		private static void PrintListNode(List<Node> nodes)
		{
			Console.Write('[');
			for (int i = 0; i < nodes.Count; i++)
			{
				Console.Write(nodes[i].Name);
				if (i < nodes.Count - 1) Console.Write(", ");
				else Console.Write("]\n");
			}
		}
	}

	//Поиск максимальной клилки
	public class Algorithm2_3 : AbstractAlgorithm2
	{
		public override string FileName => "result2_3.csv";
		public int[,] Graph { get; set; }

		public Algorithm2_3() : base(1000, 50)
		{
			Graph = GenerateGraphArray(NumberOfElements);
		}

		public override void AlgorithmBody(int j)
		{
			List<int> maxClique = FindSingleClique_3(GenerateDictionary(Graph,j+1));
		}

		private int[,] GenerateGraphArray(uint length)
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

		private Dictionary<int, List<int>> GenerateDictionary(int[,] arr, int length)
		{
			var graph = new Dictionary<int, List<int>>();

			for (int i = 0; i < length; i++)
			{
				List<int> neighbours = new List<int>();
				for(int j = 0; j < length; j++)
				{
					if (arr[i, j] == 1) neighbours.Add(arr[i, j]);
				}
				graph[i] = neighbours;
			}

			return graph;
		}

		private static List<int> FindSingleClique_3(Dictionary<int, List<int>> graph)
		{
			var clique = new List<int>();
			var vertices = new List<int>();
			foreach (var e in graph)
				vertices.Add(e.Key);

			clique.Add(vertices[vertices.Count - 1]);

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
