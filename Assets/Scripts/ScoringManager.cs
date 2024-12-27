using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public DrawingManager drawingManager;
    public GraphPlotter graphPlotter;
    public FunctionManager functionManager;
    public TextMeshProUGUI scoreText;

    public float maxScore = 100f;
    public float distanceThreshold = 0.5f; // Reduced for more precision

    private float currentScore = 0f;

    public void CalculateScore()
    {
        List<Vector3> drawnPoints = drawingManager.GetDrawnPoints();
        if (drawnPoints.Count == 0)
        {
            currentScore = 0f;
            UpdateScoreText();
            return;
        }

        float individualScore = maxScore / drawnPoints.Count;
        float score = 0f;
        int pointsWithinThreshold = 0;

        for (int i = 0; i < drawnPoints.Count; i++)
        {
            Vector3 point = drawnPoints[i];
            float yExpected = functionManager.EvaluateFunction(point.x);
            float distance = Mathf.Abs(point.y - yExpected);

            // Reward only points very close to the expected value
            if (distance <= distanceThreshold)
            {
                score += individualScore * (1 - (distance / distanceThreshold));
                pointsWithinThreshold++;
            }

            Debug.LogFormat("x:{0} y:{1} dis:{2}", point.x, point.y, distance);
        }

        // Further reduce score if not enough points are within threshold
        if (pointsWithinThreshold < drawnPoints.Count * 0.8)
        {
            score *= 0.8f; // 20% penalty if fewer than 80% of points are precise
        }

        currentScore = Mathf.Clamp(score, 0, maxScore);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(currentScore).ToString();
    }

    public void ResetScore()
    {
        currentScore = 0f;
        UpdateScoreText();
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }
}
