using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    public NoteLine noteLine;
    public FeedbackManager feedbackManager;

    public float fallSpeed; 

    public void Start()
    {
        noteLine = GetComponent<NoteLine>();
        meshRenderer = GetComponent<MeshRenderer>();

        Color32 color = meshRenderer.material.color;
        color.a = 50;
        meshRenderer.material.color = color;
    }

    public void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.y -= fallSpeed;
        transform.position = position;
    }

    public void Miss()
    {
        Destroy(this.gameObject);
        FeedbackManager.Miss();
    }

    public void Hit()
    {
        Destroy(this.gameObject);
        FeedbackManager.Hit();
    }

    public void MakeActive()
    {
        Color32 color = meshRenderer.material.color;
        color.a = 255;
        meshRenderer.material.color = color;
    }
}
