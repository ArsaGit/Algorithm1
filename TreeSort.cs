using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
	public class TreeSort
	{
        public static double[] Sort(double[] array, int j)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < j; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return treeNode.Transform();
        }


    }
}
