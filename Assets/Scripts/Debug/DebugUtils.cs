using UnityEngine;
using System.Collections;

namespace BezierUtils {

    public class DebugUtils {

        public static void DrawCircle(Vector3 center, float radius) {
            DebugUtils.DrawCircle(center, radius, Color.white, 0);
        }

        public static void DrawCircle(Vector3 center, float radius, Color color, float duration) {
            if (radius <= 0)
                return;

            float theta = 0f;
            float step = 2 * Mathf.PI / 60;

            Vector2 nextLinePoint = Vector2.zero;
            nextLinePoint.x = center.x + radius * Mathf.Cos(theta);
            nextLinePoint.y = center.y + radius * Mathf.Sin(theta);
            Vector2 previousLinePoint = nextLinePoint;

            for (theta = step; theta < 2 * Mathf.PI; theta += step) {
                nextLinePoint.x = center.x + radius * Mathf.Cos(theta);
                nextLinePoint.y = center.y + radius * Mathf.Sin(theta);

                Debug.DrawLine(previousLinePoint, nextLinePoint, color, duration);

                previousLinePoint = nextLinePoint;
            }
        }

        public static void DrawProjectedLine(Vector3 l0p0, Vector3 l0p1, Vector3 l1p0, Vector3 l1p1) {
            Debug.DrawLine(l0p0, l0p1);
            Debug.DrawLine(l1p0, l1p1);

            Vector3 intersectPoint;

            if (DebugUtils.SegmentIntersects(l0p0, l0p1, l1p0, l1p1, out intersectPoint)) {
                Vector3 average = AverageVector(l0p0, l0p1, l1p0, l1p1);

                // Outside Normal Line
                Debug.DrawLine(intersectPoint, intersectPoint - average.normalized * 5f, Color.red);
                // Inside Normal Line
                Debug.DrawLine(intersectPoint, intersectPoint + average.normalized * 5f, Color.gray);
            }
        }

        public static Vector3 AverageVector(Vector3 l0p0, Vector3 l0p1, Vector3 l1p0, Vector3 l1p1) {
            return (((l1p0.normalized + l0p0.normalized) * 0.5f) - ((l1p1.normalized + l0p1.normalized) * 0.5f) / 2);
        }

        public static bool SegmentIntersects(Vector3 l0p0, Vector3 l0p1, Vector3 l1p0, Vector3 l1p1, out Vector3 intersectPoint) {
            intersectPoint = Vector3.zero;

            float
                x1 = l0p0.x,
                x2 = l0p1.x,
                x3 = l1p0.x,
                x4 = l1p1.x;
            float
                y1 = l0p0.y,
                y2 = l0p1.y,
                y3 = l1p0.y,
                y4 = l1p1.y;

            float d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (d == 0) return false;

            float xi = ((x3 - x4) * (x1 * y2 - y1 * x2) - (x1 - x2) * (x3 * y4 - y3 * x4)) / d;
            float yi = ((y3 - y4) * (x1 * y2 - y1 * x2) - (y1 - y2) * (x3 * y4 - y3 * x4)) / d;

            if (xi <= Mathf.Min(x1, x2) || xi > Mathf.Max(x1, x2)) return false;
            if (xi <= Mathf.Min(x3, x4) || xi > Mathf.Max(x3, x4)) return false;

            intersectPoint.x = xi;
            intersectPoint.y = yi;

            return true;
        }

        public static bool IsZero(float d) {
            return Mathf.Abs(d) < Mathf.Epsilon;
        }

        public static float Cross(Vector3 a, Vector3 b) {
            return a.x * b.y - a.y * b.x;
        }

        public static float Multiply(Vector3 a, Vector3 b) {
            return a.x * b.x + a.y * b.y;
        }
    }
}