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

    private float currentRadius;

    private Transform target;
    private CircleRenderer circleRenderer;

    public Color32 wrong, ok, good;
    private float okRadius;

    public byte alphaActive, alphaInactive;
    private byte currentAlpha;

    // Start is called before the first frame update
    void Start()
    {
        circleRenderer = GetComponent<CircleRenderer>();
        Color color = wrong;
        currentAlpha = alphaInactive;
        color.a = currentAlpha;
        circleRenderer.SetColor(color);
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

        Color32 color = GetColor(currentRadius);
        color.a = currentAlpha;
        circleRenderer.SetColor(color);
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

    public void SetActive()
    {
        currentAlpha = alphaActive;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
