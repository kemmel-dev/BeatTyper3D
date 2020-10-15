using System;
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

    public static void Connect(Beat a, Beat b)
    {
        lrOne.SetPosition(0, a.GetKeyTilePosition());
        lrOne.SetPosition(1, b.GetKeyTilePosition());
    }

    public static void Connect(Beat a, Beat b, Beat c)
    {
        lrOne.SetPosition(0, a.GetKeyTilePosition());
        lrOne.SetPosition(1, b.GetKeyTilePosition());
        lrTwo.SetPosition(0, b.GetKeyTilePosition());
        lrTwo.SetPosition(1, c.GetKeyTilePosition());
    }

    public static void Enable()
    {
        lrOne.enabled = true;
        lrTwo.enabled = true;
    }

    public static void Enable(int lineRendererNumber)
    {
        switch (lineRendererNumber)
        {
            case 1:
                lrOne.enabled = true;
                break;
            case 2:
                lrTwo.enabled = true;
                break;
            default:
                throw new ArgumentException("Error: Specify which line renderer to disable (1 or 2)");

        }
    }

    public static void Disable()
    {
        lrOne.enabled = false;
        lrTwo.enabled = false;
    }

    public static void Disable(int lineRendererNumber)
    {
        switch(lineRendererNumber)
        {
            case 1:
                lrOne.enabled = false;
                break;
            case 2:
                lrTwo.enabled = false;
                break;
            default:
                throw new ArgumentException("Error: Specify which line renderer to disable (1 or 2)");

        }
    }

}
