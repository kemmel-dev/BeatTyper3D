using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    private static List<Beat> beats = new List<Beat>();

    private void Start()
    {
    }

    private void Update()
    {
        Beat nextBeat = GetBeat(0);
        Beat secondBeat = GetBeat(1);
        Beat thirdBeat = GetBeat(2);

        if (nextBeat)
        {
            if (nextBeat.GetKeyTile())
            {
                nextBeat.GetKeyTile().Show();
            }
            if (nextBeat.transform.position.y <=  - BeatSpawner.beatHitDistance / 2)
            {
                MissBeat(true);
                return;
            }
            if (secondBeat)
            {
                if (secondBeat.GetKeyTile())
                {
                    secondBeat.GetKeyTile().Show();
                }
                if (thirdBeat)
                {
                    if (thirdBeat.GetKeyTile())
                    {
                        thirdBeat.GetKeyTile().Show();
                    }
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


    public static void HitBeat(float distanceToBeat)
    {
        Beat nextBeat = GetNextBeat();
        if (nextBeat.holdable)
        {
            HoldableBeatsManager.HoldDown(nextBeat.GetKeyTile().keyCode, nextBeat.holdDuration);
        }
        nextBeat.GetKeyTile().Hide();
        bool late = nextBeat.transform.position.y < - BeatSpawner.beatHitDistance / 4;

        Destroy(nextBeat.GetBeatCircle().gameObject);
        Destroy(nextBeat.gameObject);

        FeedbackManager.Hit(distanceToBeat, late);
        RemoveFirstBeat();
    }

    public static void MissBeat(bool beatPresent)
    {
        if (beatPresent)
        {
            if (BeatManager.beats.Count > 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    RemoveFirstBeat();
                }
            }
            else
            {
                RemoveFirstBeat();
            }
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
        Beat nextBeat = GetNextBeat();
        if (nextBeat)
        {
            if (nextBeat.GetKeyTile())
            {
                nextBeat.GetKeyTile().Hide();
            }
            if (nextBeat.GetBeatCircle())
            {
                Destroy(nextBeat.GetBeatCircle().gameObject);

            }
            Destroy(nextBeat.gameObject);
        }
        beats.RemoveAt(0);
    }

    public static void Reset()
    {
        beats = new List<Beat>();
    }
}
