using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Jobs;

public class TutorialText : MonoBehaviour
{

    public List<int> beatMarkers;
    public List<string> hints;

    public TextMeshPro text;

    public int beat;

    int index = 0;

    int nextBeatMarker;
    int previousBeatMarker = 10000;

    private void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        beat = BeatRecorder.beat;
        if (index < beatMarkers.Count)
        {
            nextBeatMarker = beatMarkers[index];
        }

        if (beat > nextBeatMarker)
        {
            index++;
            text.enabled = true;
            previousBeatMarker = nextBeatMarker;
        }

        if (beat > previousBeatMarker + 8)
        {
            text.enabled = false;
        }

        if (index < hints.Count)
        {
            text.text = hints[index];

        }
    }
}
