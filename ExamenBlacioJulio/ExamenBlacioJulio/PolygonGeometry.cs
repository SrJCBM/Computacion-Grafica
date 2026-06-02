using System;
using System.Drawing;

namespace ExamenBlacioJulio
{
    internal static class PolygonGeometry
    {
        public static PointF[] CreateRegularPolygon(PointF center, float radius, int sides, float startAngleDegrees = -90f)
        {
            if (sides < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(sides));
            }

            var points = new PointF[sides];
            float step = 360f / sides;

            for (int i = 0; i < sides; i++)
            {
                points[i] = PointOnCircle(center, radius, startAngleDegrees + (step * i));
            }

            return points;
        }

        public static PointF[] CreateStarPolygon(PointF center, float outerRadius, float innerRadius, int points, float startAngleDegrees = -90f)
        {
            if (points < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            var vertices = new PointF[points * 2];
            float step = 360f / points;

            for (int i = 0; i < points; i++)
            {
                float outerAngle = startAngleDegrees + (step * i);
                float innerAngle = outerAngle + (step / 2f);

                vertices[i * 2] = PointOnCircle(center, outerRadius, outerAngle);
                vertices[i * 2 + 1] = PointOnCircle(center, innerRadius, innerAngle);
            }

            return vertices;
        }

        public static PointF PointOnCircle(PointF center, float radius, float angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;
            return new PointF(
                center.X + (float)(Math.Cos(angleRadians) * radius),
                center.Y + (float)(Math.Sin(angleRadians) * radius));
        }
    }
}
