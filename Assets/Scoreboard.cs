using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{

    public TextMeshProUGUI textMesh;

    private static int streak = 0;
    private static int bestStreak = 1;
    private static float score = 2;
    private static float highestMultiplier = 3;

    private void Start()
    {

        textMesh.text = string.Format("Here's how you did:\n\nLast streak: {0}\t\t\tBest streak: {1}\nScore: {2}\t\t\tHighest Multiplier: {3}x", streak, bestStreak, score.ToString("0"), highestMultiplier.ToString("0.00"));
    }

    public static void SetScores(int _streak, int _bestStreak, float _score, float _highestMultiplier)
    {
        streak = _streak;
        bestStreak = _bestStreak;
        score = _score;
        highestMultiplier = _highestMultiplier;
    }

}
