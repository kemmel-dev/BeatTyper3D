using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCircle : MonoBehaviour
{
    private float shrinkSpeed;

    public float minRadius, maxRadius;
    private float deltaRadius, thirdRadius;

    public static float maxDistanceToNote = 4f;

    public float currentRadius;

    private Transform target;
    private CircleRenderer circleRenderer;
    private TriangleRenderer triangleRenderer;

    public Color32 wrong, ok, good;
    private float okRadius;

    public byte alphaActive, alphaInactive;
    private byte currentAlpha;

    private bool late = false;

    public bool holdable = false;

    // Start is called before the first frame update
    void Start()
    {
        circleRenderer = transform.Find("CircleLine").GetComponent<CircleRenderer>();
        triangleRenderer = transform.Find("TriangleLine").GetComponent<TriangleRenderer>();
        Color32 color = wrong;
        currentAlpha = alphaInactive;
        color.a = currentAlpha;
        circleRenderer.SetColor(color);
        deltaRadius = maxRadius - minRadius;
        thirdRadius = deltaRadius / 4;
        currentRadius = maxRadius;
        okRadius = minRadius + thirdRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdable)
        {
            circleRenderer.Hide();
            if (late)
            {
                triangleRenderer.SetColor(wrong);
                triangleRenderer.radius = minRadius;
            }
            else
            {
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
                float currentDistanceToNote = Vector3.Distance(transform.position, targetPos);
                currentRadius = minRadius + (currentDistanceToNote / maxDistanceToNote) * deltaRadius;
                triangleRenderer.radius = currentRadius * 2.857f;
                Color32 color = GetColor(currentRadius);
                color.a = currentAlpha;
                triangleRenderer.SetColor(color);
            }
            return;
        }
        triangleRenderer.Hide();
        if (late)
        {
            circleRenderer.SetColor(wrong);
            circleRenderer.radius = minRadius;
        }
        else
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
            float currentDistanceToNote = Vector3.Distance(transform.position, targetPos);
            currentRadius = minRadius + (currentDistanceToNote / maxDistanceToNote) * deltaRadius;
            circleRenderer.radius = currentRadius;
            Color32 color = GetColor(currentRadius);
            color.a = currentAlpha;
            circleRenderer.SetColor(color);
        }

    }

    private Color32 GetColor(float radius)
    {
        float colorRatio;
        if (radius < okRadius)
        {
            colorRatio = (radius - minRadius) / thirdRadius;
            return Color32.Lerp(good, ok, colorRatio);
        }
        colorRatio = (radius - okRadius) / thirdRadius;
        return Color32.Lerp(ok, wrong, colorRatio);
    }

    public void MarkLate()
    {
        late = true;
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
