using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [Header("控制点")]
    public Transform p0, p1, p2, p3;  // 起点、控制点1、控制点2、终点

    [Range(10, 200)]
    public int segments = 50;        // 曲线分段数（越多越平滑）

    public Color curveColor = Color.red;
    public Color controlLineColor = Color.gray;

    // 在Scene视图中绘制（编辑器模式）
    private void OnDrawGizmos()
    {
        if (p0 == null || p1 == null || p2 == null || p3 == null) return;

        // 绘制控制线
        Gizmos.color = controlLineColor;
        Gizmos.DrawLine(p0.position, p1.position);
        Gizmos.DrawLine(p2.position, p3.position);

        // 绘制贝塞尔曲线
        Gizmos.color = curveColor;
        Vector3 prevPoint = p0.position;

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = CalculateCubicBezierPoint(t, p0.position, p1.position, p2.position, p3.position);
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }
    }

    // 核心算法：三次贝塞尔公式
    public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;           // (1-t)³ * P0
        point += 3 * uu * t * p1;           // 3*(1-t)²*t * P1
        point += 3 * u * tt * p2;           // 3*(1-t)*t² * P2
        point += ttt * p3;                  // t³ * P3

        return point;
    }

    // 获取曲线上某点的切线方向（用于物体朝向）
    public static Vector3 GetTangent(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return 3 * u * u * (p1 - p0) + 6 * u * t * (p2 - p1) + 3 * t * t * (p3 - p2);
    }
}
