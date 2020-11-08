using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableBeatsManager : MonoBehaviour
{

    public Transform startPoint, endPoint;

    private static float startTime, endTime, holdDuration;

    private static bool started;

    private static KeyCode keyCode;

    private static LineRenderer holdDownLine;

    private static float distanceBetweenPoints;

    private void Start()
    {
        holdDownLine = GetComponent<LineRenderer>();
        holdDownLine.enabled = false;
        distanceBetweenPoints = Vector3.Distance(startPoint.position, endPoint.position);
    }

    public static void HoldDown(KeyCode _keyCode, float _holdDuration)
    {
        started = true;
        startTime = Time.time;
        holdDuration = _holdDuration;
        endTime = Time.time + holdDuration;
        keyCode = _keyCode;
    }

    public void Update()
    {
        if (started)
        {
            if (Time.time > endTime)
            {
                FeedbackManager.HitHeldDownNote(holdDuration);
                holdDownLine.enabled = false;
                started = false;
                return;
            }
            if (Input.GetKey(keyCode))
            {
                holdDownLine.enabled = true;
                holdDownLine.SetPosition(0, startPoint.position);
                float percentageHeldDown = (Time.time - startTime) / holdDuration;
                Vector3 endPos = new Vector3(startPoint.position.x + distanceBetweenPoints * (1 - percentageHeldDown), startPoint.position.y, startPoint.position.z);
                holdDownLine.SetPosition(1, endPos);
            }
            else
            {
                FeedbackManager.Miss();
                holdDownLine.enabled = false;
                started = false;
            }
        }
    }
}
