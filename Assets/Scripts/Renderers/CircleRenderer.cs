using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class CircleRenderer : MonoBehaviour
{

    private LineRenderer lineRenderer;

    public int resolution = 50;
    public float radius = 5;

    public float width = .25f;

    private float angleStep;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution + 2;
        angleStep = (360 / resolution);
        DrawCircle();
        SetWidth(width);
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle();
    }

    public void SetColor(Color32 color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    public void SetWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    public void Show()
    {
        lineRenderer.enabled = true;
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }

    private void DrawCircle()
    {
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            float angle = angleStep * i;

            lineRenderer.SetPosition(i, transform.position + radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)));
        }
        lineRenderer.SetPosition(lineRenderer.positionCount - 2, lineRenderer.GetPosition(0));
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, lineRenderer.GetPosition(1));
    }
}
