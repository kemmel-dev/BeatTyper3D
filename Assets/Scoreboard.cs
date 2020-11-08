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

    private static Color textColor = Color.white;

    private static string grade = "";

    private void Start()
    {
        textMesh.text = string.Format(grade + "\n\nLast streak: {0}\t\t\tBest streak: {1}\nScore: {2}\t\t\tHighest Multiplier: {3}x", streak, bestStreak, score.ToString("0"), highestMultiplier.ToString("0.00"));
    }

    private void Update()
    {
        textMesh.color = textColor;
    }

    public static void SetScores(int _streak, int _bestStreak, float _score, float maxScore, float _highestMultiplier)
    {
        streak = _streak;
        bestStreak = _bestStreak;
        score = _score;
        highestMultiplier = _highestMultiplier;

        if (score < maxScore * 0.5f)
        {
            grade = "Oof... Try again, you'll get there! (<50% of max score)";
            textColor = Color.red;
        }
        else if (score < maxScore * 0.75f)
        {
            grade = "That's pretty good, but you can do better! (50-75% of max score)";
            textColor = Color.yellow;
        }
        else if (score < maxScore * 0.85f)
        {
            grade = "Getting near the top, good job! (75-85% of max score)";
            textColor = Color.green;
        }
        else if (score < maxScore * 0.95f)
        {
            grade = "Impressive!! But not yet perfect... (85-95% of max score!)";
            textColor = Color.green;
        }
        else if (score < maxScore * 1f)
        {
            grade = "That was VERY close to perfection!!! (95-99% of max score!!)";
            textColor = Color.green;
        }
        else
        {
            grade = "ABSOLUTE PERFECTION!!! (100% of max score!!!)";
            textColor = Color.green;
        }

    }
}
