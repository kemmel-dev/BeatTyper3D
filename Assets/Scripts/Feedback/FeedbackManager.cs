using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FeedbackManager : MonoBehaviour
{

    public static FeedbackManager current;

    private static AutoTimer timer;
    public static TextMeshPro scoreTextMesh;
    public static TextMeshPro feedbackTextMesh;
    public static TextMeshPro missesTextMesh;

    private static Color defaultColor = Color.white;
    private static Color okayColor = Color.yellow;
    private static Color correctColor = Color.green;
    private static Color wrongColor = Color.red;

    private static float timeCorrect = 0.75f;
    private static float timeWrong = 1f;

    private static int misses = 0;
    private static int maxMisses = 5;

    private static float score = 0;
    private static float scoreAdded = 0;
    private static float multiplier = 1f;
    private static float highestMultiplier = 1f;

    public static int totalBeats = 0;

    private static int streak = 0;
    private static int bestStreak = 0;

    private static AudioSource audioSource;

    public static LineRenderer topLineRenderer;
    public static LineRenderer bottomLineRenderer;

    private static string perfect = "Perfect!";
    private static string good = "Good!";
    private static string okay = "Okay";
    private static string lateText = "Late...";
    private static string wrong = "Oof...";

    // Start is called before the first frame update
    void Start()
    {
        feedbackTextMesh = transform.Find("FeedbackText").GetComponent<TextMeshPro>();
        missesTextMesh = transform.Find("MissesText").GetComponent<TextMeshPro>();
        feedbackTextMesh.enabled = false;
        scoreTextMesh = transform.Find("ScoreText").GetComponent<TextMeshPro>();
        audioSource = GetComponentInChildren<AudioSource>();

        topLineRenderer = GameObject.Find("Line 1").GetComponent<LineRenderer>();
        bottomLineRenderer = GameObject.Find("Line 2").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            if (timer.IsReached(Time.time))
            {
                feedbackTextMesh.enabled = false;
                topLineRenderer.startColor = defaultColor;
                topLineRenderer.endColor = defaultColor;

                bottomLineRenderer.startColor = defaultColor;
                bottomLineRenderer.endColor = defaultColor;
            }
        }
        scoreTextMesh.text = ("Streak: " + streak + "\tScore: " + score.ToString("0") + "\t(+" + scoreAdded.ToString("0") + ")\nBest: " + bestStreak + "\tMultiplier: " + multiplier.ToString("0.00") + "x");
        missesTextMesh.text = "Misses: \t" + misses + "/" + maxMisses;


        if (misses > maxMisses)
        {
            SceneManager.LoadScene("FailScene");
        }
    }

    public static void Hit(float distanceToBeat, bool late)
    {
        float perfectionPoints = (BeatSpawner.beatHitDistance - distanceToBeat) / (BeatSpawner.beatHitDistance) * 100;
        float perfectionReward = 0;

        Color lineColor;


        if (distanceToBeat < BeatSpawner.beatHitDistance)
        {
            feedbackTextMesh.text = perfect;
            feedbackTextMesh.color = correctColor;
            SetLineColors(correctColor);
            perfectionReward = 1f;
        }
        else
        {
            feedbackTextMesh.text = good;
            feedbackTextMesh.color = correctColor;
            SetLineColors(correctColor);
            perfectionReward = 0.75f;
        }

        if (late)
        {
            feedbackTextMesh.text = lateText;
            feedbackTextMesh.color = okayColor;
            SetLineColors(okayColor);
            perfectionReward = 0.25f;
        }

        feedbackTextMesh.enabled = true;
        scoreAdded = (100 + 100 * perfectionReward) * multiplier;
        score += scoreAdded;
        timer = new AutoTimer(Time.time + timeCorrect);



        streak++;
        multiplier += 0.01f;
    }

    private static void SetLineColors(Color32 color)
    {
        topLineRenderer.startColor = color;
        topLineRenderer.endColor = color;

        bottomLineRenderer.startColor = color;
        bottomLineRenderer.endColor = color;
    }

    public static void Miss()
    {
        misses++;
        feedbackTextMesh.text = wrong;
        feedbackTextMesh.color = wrongColor;
        feedbackTextMesh.enabled = true;
        timer = new AutoTimer(Time.time + timeWrong);
        if (streak > bestStreak)
        {
            bestStreak = streak;
        }
        streak = 0;

        topLineRenderer.startColor = wrongColor;
        topLineRenderer.endColor = wrongColor;

        bottomLineRenderer.startColor = wrongColor;
        bottomLineRenderer.endColor = wrongColor;
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.5f);
        audioSource.Play();

        if (multiplier > highestMultiplier)
        {
            highestMultiplier = multiplier;
        }

        multiplier = 1;
    }

    public static void Reset()
    {
        streak = 0;
        score = 0;
        multiplier = 1;
        scoreAdded = 0;
        misses = 0;
    }

    public static void SendScores()
    {
        float maxScore = GetMaxScore();

        if (streak > bestStreak)
        {
            bestStreak = streak;
        }
        if (multiplier > highestMultiplier)
        {
            highestMultiplier = multiplier;
        }
        Scoreboard.SetScores(streak, bestStreak, score, maxScore, highestMultiplier);
    }

    private static float GetMaxScore()
    {
        float score = 0;
        float multiplier = 1;
        for (int i = 0; i < totalBeats; i++)
        {
            score += 200 * multiplier;
            multiplier += 0.01f;
        }
        return score;
    }
}
