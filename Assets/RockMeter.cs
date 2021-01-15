using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{

    public Transform startPoint, endPoint;
    private float distance;
    private LineRenderer lineRenderer;

    public Color32 wrongColor;
    public Color32 rightColor;

    public float rockPercentage;
    public float progressionWeight;
    public float errorWeight;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector2.Distance(startPoint.position, endPoint.position);
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void IncrementRockMeter()
    {
        rockPercentage = Mathf.Clamp(rockPercentage += progressionWeight, 0, 1);
    }

    public void DecreaseRockMeter()
    {
        rockPercentage = Mathf.Clamp(rockPercentage -= errorWeight, 0, 1);
        if (rockPercentage < 0.01f)
        {
            rockPercentage = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        Vector3 currentEndPoint = startPoint.position;
        currentEndPoint.x += distance * rockPercentage;
        lineRenderer.SetPosition(1, currentEndPoint);
        lineRenderer.startColor = Color32.Lerp(wrongColor, rightColor, rockPercentage);
        lineRenderer.endColor = Color32.Lerp(wrongColor, rightColor, rockPercentage);
    }
}
