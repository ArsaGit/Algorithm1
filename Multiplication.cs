using System;

namespace AlgorithmLab1
{
	public class Multiplication
	{
		public static double[,] MultiplyMatrix(double[,] A, double[,] B)
		{
			double[,] result = new double[A.GetLength(0), B.GetLength(1)];

			if (A.GetLength(1) == B.GetLength(0))
			{
				for(int i = 0; i < A.GetLength(0); i++)
				{
					for(int j = 0; j < A.GetLength(1); j++)
					{
						double sum = 0;

						for(int k = 0; k < B.GetLength(1); k++)
						{
							sum += A[i, k] * B[j, k];
						}

						result[i, j] = sum;
					}
				}

				return result;
			}
			else throw new ArgumentException("Cannot multiply matrixes");
		}

		public static double[,] MultiplyMatrix(double[,] A, double[,] B, int size)
		{
			double[,] result = new double[size, size];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					double sum = 0;

					for (int k = 0; k < size; k++)
					{
						sum += A[i, k] * B[j, k];
					}

					result[i, j] = sum;
				}
			}

			return result;
		}
	}
}
