using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public abstract class AbstractAlgorithm2 : Algorithm
	{
		public AbstractAlgorithm2() : base(2000,50)
		{
			FolderName = "Results2\\";
		}
	}

	//
	public class Algorithm2_1 : AbstractAlgorithm2
	{
		public override string FileName => "result2_1.csv";

		public Algorithm2_1() : base()
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

		public Algorithm2_2() : base()
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

		public Algorithm2_3() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//
	public class Algorithm2_4 : AbstractAlgorithm2
	{
		public override string FileName => "result2_4.csv";

		public Algorithm2_4() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//
	public class Algorithm2_5 : AbstractAlgorithm2
	{
		public override string FileName => "result2_5.csv";

		public Algorithm2_5() : base()
		{
		}

		public override void AlgorithmBody(int j)
		{
			throw new NotImplementedException();
		}
	}

	//Find Fibonacci Number
	public class Algorithm2_6 : AbstractAlgorithm2
	{
		public override string FileName => "result2_6.csv";

		public Algorithm2_6() : base()
		{
			NumberOfElements = 5000;
		}

		public override void AlgorithmBody(int j)
		{
			double num = FindNNumberFibonacci1_6(j);
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
