using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public class Helper
	{
		public static T[] ExtractOneDimArray<T>(T[,] twoDimArray, int dimension)
		{
			T[] oneDimArray = new T[twoDimArray.GetLength(1)];

			for (int i = 0; i < oneDimArray.Length; i++)
			{
				oneDimArray[i] = twoDimArray[dimension, i];
			}

			return oneDimArray;
		}
	}
}
