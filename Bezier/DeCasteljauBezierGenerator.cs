namespace Bezier
{
    public class DeCasteljauBezierGenerator
    {
        public Point[] GetCurvePoints(int numberOfPoints, Point[] controlPoints)
        {
            var result = new Point[numberOfPoints];
            double t = 0;
            var deltaT = 1.0 / numberOfPoints;
            for (var i = 0; i < numberOfPoints; i++)
            {
                result[i] = GeneratePoint(t, controlPoints);
                t += deltaT;
            }

            return result;
        }

        private Point GeneratePoint(double t, Point[] points)
        {
            if (points.Length == 1)
                return points[0];

            var newPoints = new Point[points.Length - 1];
            for (var i = 0; i < newPoints.Length; i++)
            {
                var x = (1 - t) * points[i].X + t * points[i + 1].X;
                var y = (1 - t) * points[i].Y + t * points[i + 1].Y;
                newPoints[i] = new Point((int) x, (int) y);
            }
            return GeneratePoint(t, newPoints);
        }
    }
}