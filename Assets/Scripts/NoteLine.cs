using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NoteLine : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private LayerMask keyTileLayerMask;

    private Vector3 startPoint;
    private Vector3 endPoint;

    // Start is called before the first frame update
    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        keyTileLayerMask = LayerMask.GetMask("KeyTileLayer");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        lineRenderer.SetPosition(0, currentPos);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, keyTileLayerMask))
        {
            KeyTile keyTile = hit.collider.gameObject.GetComponent<KeyTile>();
            if (keyTile)
            {
                keyTile.MakeActive();
            }
            endPoint = hit.point;
            lineRenderer.SetPosition(1, endPoint);
        }
        AlterOpacity(currentPos.y);
    }

    public void AlterOpacity(float height)
    {
        float percentagePathTravelled = 1 - (height / 100f);
        Color32 color = lineRenderer.startColor;
        lineRenderer.startColor = new Color32(color.r, color.g, color.b, (byte)(percentagePathTravelled * 255)); 
    }

    public void SetColor(Color32 color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    public void SetWidth(float width)
    {
        lineRenderer.startWidth = 0.125f;
        lineRenderer.endWidth = width;
    }
}
