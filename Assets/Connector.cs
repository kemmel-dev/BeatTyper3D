using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    private static LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Disable();
    }

    public static void Connect(Transform a, Transform b)
    {
        lineRenderer.SetPosition(0, a.position);
        lineRenderer.SetPosition(1, b.position);
    }

    public static void ConnectPositions(Vector3 a, Vector3 b)
    {
        lineRenderer.SetPosition(0, a);
        lineRenderer.SetPosition(1, b);
    }

    public static void Enable()
    {
        lineRenderer.enabled = true;
    }

    public static void Disable()
    {
        lineRenderer.enabled = false;
    }
}
