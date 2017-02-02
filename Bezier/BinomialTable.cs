using System.Collections.Generic;

namespace Bezier
{
    public class BinomialTable
    {
        private readonly List<int[]> _coefficients = new List<int[]>
        {
            new[] {1}, // n = 0
            new[] {1, 1}, // n = 1
            new[] {1, 2, 1}, // n = 2
            new[] {1, 3, 3, 1}, // n = 3
            new[] {1, 4, 6, 4, 1}, // n = 4
            new[] {1, 5, 10, 10, 5, 1}, // n = 5
            new[] {1, 6, 15, 20, 15, 6, 1} // n = 6
        };

        public int Lookup(int n, int k)
        {
            while (n >= _coefficients.Count)
            {
                var size = _coefficients.Count;
                var nextRow = new int[size + 1];
                nextRow[0] = 1;
                for (int i = 1, prev = size - 1; i < prev; i++)
                    nextRow[i] = _coefficients[prev][i - 1] + _coefficients[prev][i];
                nextRow[size] = 1;
                _coefficients.Add(nextRow);
            }
            return _coefficients[n][k];
        }
    }
}