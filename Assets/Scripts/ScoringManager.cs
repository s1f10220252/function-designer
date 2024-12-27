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
    public float distanceThreshold = 1f; // Maximum distance considered

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

        foreach (Vector3 point in drawnPoints)
        {
            float yExpected = functionManager.EvaluateFunction(point.x);
            float distance = Mathf.Abs(point.y - yExpected);
            if (distance <= distanceThreshold)
            {
                score += individualScore * (1 - (distance / distanceThreshold));
            }
            Debug.LogFormat("x:{0} y:{1} dis:{2}", point.x, point.y, distance);
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
