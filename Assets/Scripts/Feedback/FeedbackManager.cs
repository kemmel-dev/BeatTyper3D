using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackManager : MonoBehaviour
{

    public static FeedbackManager current;

    private static AutoTimer timer;
    private static TextMeshPro textMeshPro;

    private static Color defaultColor = Color.white;
    private static Color correctColor = Color.green;
    private static Color wrongColor = Color.red;

    private static float timeCorrect = 0.5f;
    private static float timeWrong = 1f;

    private static int streak = 0;
    private static int best = 0;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = ("Streak: " + streak);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            if (timer.IsReached(Time.time))
            {
                textMeshPro.color = defaultColor;
            }
        }
        textMeshPro.text = ("Streak: " + streak + "\nBest: " + best);
    }

    public static void Hit()
    {

        timer = new AutoTimer(Time.time + timeCorrect);
        textMeshPro.color = correctColor;
        streak++;
    }

    public static void Miss()
    {

        timer = new AutoTimer(Time.time + timeWrong);
        textMeshPro.color = wrongColor;
        if (streak > best)
        {
            best = streak;
        }
        streak = 0;
    }
}
