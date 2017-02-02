using System;
using System.Linq;

namespace Bezier
{
    public class WeightedBasisBezierGenerator
    {
        private readonly BinomialTable _coefficientTable;

        public WeightedBasisBezierGenerator(BinomialTable coefficientTable)
        {
            _coefficientTable = coefficientTable;
        }

        public Point[] GetCurvePoints(int numberOfPoints, Point[] controlPoints)
        {
            var order = controlPoints.Length - 1;
            var xWeights = controlPoints.Select(p => p.X).ToArray();
            var yWeights = controlPoints.Select(p => p.Y).ToArray();
            var result = new Point[numberOfPoints];
            double t = 0;
            var deltaT = 1.0 / numberOfPoints;
            for (var i = 0; i < result.Length; i++)
            {
                var x = Bezier(order, t, xWeights);
                var y = Bezier(order, t, yWeights);
                result[i] = new Point(x, y);
                t += deltaT;
            }

            return result;
        }

        private int Bezier(int n, double t, int[] weights)
        {
            switch (n)
            {
                case 2:
                    return Bezier2(t, weights);
                case 3:
                    return Bezier3(t, weights);
            }

            var sum = 0;
            for (var i = 0; i < n; i++)
                sum += (int) (weights[i] * _coefficientTable.Lookup(n, i) * Math.Pow(1 - t, n - i) * Math.Pow(t, i));
            return sum;
        }

        private int Bezier2(double t, int[] weights)
        {
            var t2 = t * t;
            var mt = 1 - t;
            var mt2 = mt * mt;
            return (int) (weights[0] * mt2 + weights[1] * 2 * mt * t + weights[2] * t2);
        }

        private int Bezier3(double t, int[] weights)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            var mt = 1 - t;
            var mt2 = mt * mt;
            var mt3 = mt2 * mt;
            return (int) (weights[0] * mt3 + weights[1] * 3 * mt2 * t + weights[2] * 3 * mt * t2 + weights[3] * t3);
        }
    }
}