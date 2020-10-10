using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCircle : MonoBehaviour
{

    private float shrinkSpeed;

    public float minRadius, maxRadius;
    private float deltaRadius, halfRadius;

    public static float maxDistanceToNote = 4f;
    public float currentDistanceToNote;

    private float currentRadius;

    private Transform target;
    private CircleRenderer circleRenderer;

    public Color32 wrong, ok, good;
    private float okRadius;

    // Start is called before the first frame update
    void Start()
    {
        circleRenderer = GetComponent<CircleRenderer>();
        circleRenderer.SetColor(wrong);
        deltaRadius = maxRadius - minRadius;
        halfRadius = deltaRadius / 2;
        currentRadius = maxRadius;
        okRadius = minRadius + deltaRadius / 2;
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistanceToNote = Vector3.Distance(transform.position, target.position);
        currentRadius = minRadius + (currentDistanceToNote / maxDistanceToNote) * deltaRadius;
        circleRenderer.radius = currentRadius;
        circleRenderer.SetColor(GetColor(currentRadius));
    }

    private Color32 GetColor(float radius)
    {
        float colorRatio;
        if (radius < okRadius)
        {
            colorRatio = (radius - minRadius) / halfRadius;
            return Color32.Lerp(good, ok, colorRatio);
        }
        colorRatio = (radius - okRadius) / halfRadius;
        return Color32.Lerp(ok, wrong, colorRatio);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
