using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab1
{
    public class FenwickTree
    {
        public List<int> ft = new List<int>();

        public int LSB(int x)
        {
            return x & (-x);
        }

        public int Query(int x)
        {
            int sum = 0;
            while (x > 0)
            {
                sum = sum + ft[x];
                x = x - LSB(x);
            }
            return sum;
        }

        public int Query(int start, int end)
        {
            return Query(end) - Query(start - 1);
        }

        public void Update(int pos, int value)
        {
            while (pos < ft.Count) //array[pos] += value
            {
                ft[pos] += value;
                pos += LSB(pos);
            }
        }

        public FenwickTree(List<int> array)
        {
            int n = array.Count;
            Assign(ft, n + 1, 0); //инициализация ft
            for (int i = 0; i < n; ++i)
                Update(i + 1, array[i]);
        }

        public static void Assign(List<int> l, int n, int m)
        {
            if (!(l is null))
                l.Clear();

            for (int i = 0; i <= n; i++)
                l.Add(m);
        }
    }
}
