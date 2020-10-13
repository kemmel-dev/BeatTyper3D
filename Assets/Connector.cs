using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Connector : MonoBehaviour
{

    public Color32 startColorOne, endColorOne, startColorTwo, endColorTwo;

    private static Vector3 thisPos;

    private static LineRenderer lrOne;
    private static LineRenderer lrTwo;

    private void Start()
    {
        lrOne = transform.Find("LR One").GetComponent<LineRenderer>();
        lrTwo = transform.Find("LR Two").GetComponent<LineRenderer>();
        thisPos = transform.position;
        lrOne.startColor = startColorOne;
        lrOne.endColor = endColorOne;
        lrTwo.startColor = startColorTwo;
        lrTwo.endColor = endColorTwo;

        Disable();
    }

    public static void Connect(Transform a, Transform b, Transform c)
    {
        lrOne.SetPosition(0, a.position);
        lrOne.SetPosition(1, b.position);
        lrTwo.SetPosition(0, b.position);
        lrTwo.SetPosition(1, c.position);
    }

    public static void ConnectPositions(Vector3 a, Vector3 b, Vector3 c)
    {
        lrOne.SetPosition(0, thisPos + a);
        lrOne.SetPosition(1, thisPos + b);
        lrTwo.SetPosition(0, thisPos + b);
        lrTwo.SetPosition(1, thisPos + c);
    }

    public static void Enable()
    {
        lrOne.enabled = true;
        lrTwo.enabled = true;
    }

    public static void Disable()
    {
        lrOne.enabled = false;
        lrTwo.enabled = false;
    }
}
