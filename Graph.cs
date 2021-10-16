using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public class Node
	{
		public int Name { get; set; }
		public List<int> Neighbours { get; set; }

		public Node(int[] arr, int name)
		{
			Name = name;
			Neighbours = GetNeighbours(arr);
		}

		private List<int> GetNeighbours(int[] arr)
		{
			var neighbours = new List<int>();

			for(int i = 0; i < arr.Length; i++)
			{
				if (arr[i] == 1) neighbours.Add(i);
			}

			return neighbours;
		}

		public override string ToString()
		{
			return Name.ToString();
		}
	}

	public class Graph
	{
		public List<Node> Nodes;

		public Graph(int[,] graph, int length)
		{
			Nodes = CreateNodes(graph, length);
		}
		public Node this[int index]
		{
			get => Nodes[index];
			set => Nodes[index] = value;
		}

		private List<Node> CreateNodes(int[,] arr, int length)
		{
			var nodes = new List<Node>();

			for (int i = 0; i < length; i++)
			{
				int[] oneDimArr = Helper.ExtractOneDimArray(arr, i);
				nodes.Add(new Node(oneDimArr, i));
			}

			return nodes;
		}
	}
}
