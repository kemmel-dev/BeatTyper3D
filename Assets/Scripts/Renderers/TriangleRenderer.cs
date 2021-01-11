using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TriangleRenderer : MonoBehaviour
{

    private LineRenderer lineRenderer;

    public float radius = 5;
    public float width = .25f;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
        DrawTriangle();
        SetWidth(width);
    }

    // Update is called once per frame
    void Update()
    {
        DrawTriangle();
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

    private void DrawTriangle()
    {
        Vector3 bottomCorner = new Vector3(transform.position.x, transform.position.y, transform.position.z - radius / 2 - radius / 5);
        Vector3 rightCorner = new Vector3(transform.position.x + radius / 2, transform.position.y, transform.position.z + radius / 2 - radius / 5);
        Vector3 leftCorner = new Vector3(transform.position.x - radius / 2, transform.position.y, transform.position.z + radius / 2 - radius / 5);
        lineRenderer.SetPosition(0, bottomCorner);
        lineRenderer.SetPosition(1, rightCorner);
        lineRenderer.SetPosition(2, leftCorner);
        lineRenderer.SetPosition(3, bottomCorner);
    }
}
