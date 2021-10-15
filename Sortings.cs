using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public class Sortings
	{
        public static double[] TreeSort(double[] array, int j)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < j; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return treeNode.Transform();
        }

        public static double[] TimSort(double[] array, int j)
		{

            return new TimSorting().Sort(array, j);
		}

        public static double[] QuickSort(double[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);

                if (pivot > 1)
                {
                    QuickSort(array, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(array, pivot + 1, right);
                }
            }

            return array;
        }

        public static double[] BubbleSort(double[] array, int length)
		{
            double temp;

            for (int j = 0; j < length - 1; j++)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i + 1];
                        array[i + 1] = array[i];
                        array[i] = temp;
                    }
                }
            }

            return array;
        }

        private static int Partition(double[] arr, int left, int right)
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

        private class TimSorting
        {
            private const int RUN = 32;

            // This function sorts array from left index to
            // to right index which is of size atmost RUN
            private void InsertionSort(double[] arr,
                                        int left, int right)
            {
                for (int i = left + 1; i <= right; i++)
                {
                    double temp = arr[i];
                    int j = i - 1;
                    while (j >= left && arr[j] > temp)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }
                    arr[j + 1] = temp;
                }
            }

            // merge function merges the sorted runs
            private void Merge(double[] arr, int l,
                                           int m, int r)
            {
                // original array is broken in two parts
                // left and right array
                int len1 = m - l + 1, len2 = r - m;
                double[] left = new double[len1];
                double[] right = new double[len2];
                for (int x = 0; x < len1; x++)
                    left[x] = arr[l + x];
                for (int x = 0; x < len2; x++)
                    right[x] = arr[m + 1 + x];

                int i = 0;
                int j = 0;
                int k = l;

                // After comparing, we merge those two array
                // in larger sub array
                while (i < len1 && j < len2)
                {
                    if (left[i] <= right[j])
                    {
                        arr[k] = left[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = right[j];
                        j++;
                    }
                    k++;
                }

                // Copy remaining elements
                // of left, if any
                while (i < len1)
                {
                    arr[k] = left[i];
                    k++;
                    i++;
                }

                // Copy remaining element
                // of right, if any
                while (j < len2)
                {
                    arr[k] = right[j];
                    k++;
                    j++;
                }
            }

            // Iterative Timsort function to sort the
            // array[0...n-1] (similar to merge sort)
            public double[] Sort(double[] arr, int n)
            {

                // Sort individual subarrays of size RUN
                for (int i = 0; i < n; i += RUN)
                    InsertionSort(arr, i,
                                 Math.Min((i + RUN - 1), (n - 1)));

                // Start merging from size RUN (or 32).
                // It will merge
                // to form size 64, then
                // 128, 256 and so on ....
                for (int size = RUN; size < n;
                                         size = 2 * size)
                {

                    // Pick starting point of
                    // left sub array. We
                    // are going to merge
                    // arr[left..left+size-1]
                    // and arr[left+size, left+2*size-1]
                    // After every merge, we increase
                    // left by 2*size
                    for (int left = 0; left < n;
                                          left += 2 * size)
                    {

                        // Find ending point of left sub array
                        // mid+1 is starting point of
                        // right sub array
                        int mid = left + size - 1;
                        int right = Math.Min((left +
                                            2 * size - 1), (n - 1));

                        // Merge sub array arr[left.....mid] &
                        // arr[mid+1....right]
                        if (mid < right)
                            Merge(arr, left, mid, right);
                    }
                }

                return arr;
            }

            // Utility function to print the Array
            public void PrintArray(double[] arr, int n)
            {
                for (int i = 0; i < n; i++)
                    Console.Write(arr[i] + " ");
                Console.Write("\n");
            }
        }
    }
}
