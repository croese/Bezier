using System;
using System.Linq;
using Bezier;

namespace BezierSvg
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var controlPoints = new[]
            {
                new Point(120, 160),
                new Point(35, 200),
                new Point(220, 260),
                new Point(220, 40)
            };

            //var points = GetWeightedBasis(1000, controlPoints);
            var points = GetDeCasteljau(10000, controlPoints);

            var outlinePath = BuildPath(controlPoints, "gray");
            var curvePath = BuildPath(points, strokeWidth: 2);

            var markers = string.Join(string.Empty, controlPoints.Select(p => BuildMarker(p)));

            var canvasWidth = controlPoints.Max(p => p.X) + 100;
            var canvasHeight = controlPoints.Max(p => p.Y) + 100;
            var svgDocument =
                $"<svg width=\"{canvasWidth}\" height=\"{canvasHeight}\" xmlns=\"http://www.w3.org/2000/svg\">{outlinePath}{markers}{curvePath}</svg>";

            Console.WriteLine(svgDocument);
        }

        private static Point[] GetWeightedBasis(int numberOfPoints, Point[] controlPoints)
        {
            var gen = new WeightedBasisBezierGenerator(new BinomialTable());
            var points = gen.GetCurvePoints(numberOfPoints, controlPoints);
            return points;
        }

        private static Point[] GetDeCasteljau(int numberOfPoints, Point[] controlPoints)
        {
            var gen = new DeCasteljauBezierGenerator();
            var points = gen.GetCurvePoints(numberOfPoints, controlPoints);
            return points;
        }

        private static string BuildMarker(Point point, int circleRadius = 4, int textOffset = 4)
        {
            return
                $@"<circle cx=""{point.X}"" cy=""{point.Y}"" r=""{circleRadius}"" fill=""transparent"" stroke=""black"" stroke-width=""2"" />
<text x=""{point.X + circleRadius + textOffset}"" y=""{point.Y + circleRadius + textOffset}"" font-size=""11"">({point.X}, {point
                    .Y})</text>";
        }

        private static string BuildPath(Point[] points, string strokeColor = "black", int strokeWidth = 1)
        {
            var pathMove = $"M {points[0].X} {points[0].Y}";
            var pathRest = string.Join(" ", points.Skip(1).Select(p => $"L {p.X} {p.Y}"));
            var svgPath =
                $"<path d=\"{pathMove} {pathRest}\" fill=\"transparent\" stroke=\"{strokeColor}\" stroke-width=\"{strokeWidth}\" />";
            return svgPath;
        }
    }
}