using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierLineRenderer : MonoBehaviour
{
    Vector3[] Positions = new Vector3[4];
    public int segmentCount = 50;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        DrawCurve();
    }

    public void Update()
    {
        DrawCurve();
    }

    public void SetPosition(int index, Vector2 position)
    {
        if (index < 0 || index >= Positions.Length)
        {
            Debug.LogError("Index out of range");
            return;
        }
        Positions[index] = position;
    }

    void DrawCurve()
    {
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector2 position = BezierCurve.CalculateCubicBezierPoint(
                t,
                Positions[0],
                Positions[1],
                Positions[2],
                Positions[3]
            );
            lineRenderer.SetPosition(i, position);
        }
    }

    void OnDisable()
    {
        if (lineRenderer)
            lineRenderer.enabled = false;
    }

    void OnEnable()
    {
        if (lineRenderer)
            lineRenderer.enabled = true;
    }
}
