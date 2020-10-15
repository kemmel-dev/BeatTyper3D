using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    private static List<Beat> beats = new List<Beat>();

    private void Update()
    {
        Beat nextBeat = GetBeat(0);
        Beat secondBeat = GetBeat(1);
        Beat thirdBeat = GetBeat(2);

        if (nextBeat)
        {
            if (nextBeat.transform.position.y <= 0f)
            {
                MissBeat(true);
                return;
            }
            if (secondBeat)
            {
                if (thirdBeat)
                {
                    Connector.Connect(nextBeat, secondBeat, thirdBeat);
                    return;
                }
                Connector.Disable(2);
                Connector.Connect(nextBeat, secondBeat);
                return;
            }
            Connector.Disable(1);
            return;
        }
    }

    public static void AddBeat(Beat beat)
    {
        beats.Add(beat);
    }

    public static int BeatsLeft()
    {
        return beats.Count;
    }


    public static void HitBeat()
    {
        Beat nextBeat = GetNextBeat();
        Destroy(nextBeat.GetBeatCircle().gameObject);
        Destroy(nextBeat.gameObject);

        FeedbackManager.Hit();
        RemoveFirstBeat();
    }

    public static void MissBeat(bool beatPresent)
    {
        if (beatPresent)
        {
            Beat nextBeat = GetNextBeat();
            Destroy(nextBeat.GetBeatCircle().gameObject);
            Destroy(nextBeat.gameObject);
            RemoveFirstBeat();
        }
        FeedbackManager.Miss();
    }

    public static Beat GetBeat(int index)
    {
        if (index < 0)
        {
            throw new ArgumentException("Error: Tried to access the beats with a negative index.");
        }
        if (index < beats.Count)
        {
            return beats[index];
        }
        return null;
    }

    public static Beat GetNextBeat()
    {
        if (beats.Count > 0)
        {
            return beats[0];
        }
        return null;
    }

    public static void RemoveFirstBeat()
    {
        beats.RemoveAt(0);
    }
}
