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
    public float maxDistance = 5f; // Maximum possible distance based on y-axis range (-5 to 5)

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
        int totalPoints = drawnPoints.Count;
        int pointsWithinThreshold = 0;

        for (int i = 0; i < drawnPoints.Count; i++)
        {
            Vector3 point = drawnPoints[i];
            float yExpected = functionManager.EvaluateFunction(point.x);
            float distance = Mathf.Abs(point.y - yExpected);

            if (distance <= maxDistance)
            {
                // Scale the score contribution based on how close the point is to the expected value
                float contribution = individualScore * (1 - (distance / maxDistance));
                score += contribution;

                if (distance <= distanceThreshold)
                {
                    pointsWithinThreshold++;
                }
            }
            else
            {
                // If the distance exceeds the maximum, no contribution is added
                // Optionally, you can impose a penalty here if desired
            }

            Debug.LogFormat("x:{0} y:{1} dis:{2}", point.x, point.y, distance);
        }

        // Optional: Apply a smaller penalty if fewer points are within the threshold
        float thresholdRatio = (float)pointsWithinThreshold / totalPoints;
        if (thresholdRatio < 0.9f)
        {
            score *= 0.9f; // 10% penalty if fewer than 90% of points are precise
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
