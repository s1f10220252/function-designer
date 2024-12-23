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

    private float totalScore = 0f;

    public void CalculateScore()
    {
        List<Vector3> drawnPoints = drawingManager.GetDrawnPoints();
        if (drawnPoints.Count == 0)
        {
            totalScore = 0f;
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
            Debug.LogFormat("x:{0} y:{1} dis:{2}",point.x,point.y,distance);
        }

        totalScore = Mathf.Clamp(score, 0, maxScore);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(totalScore).ToString();
    }

    public void ResetScore()
    {
        totalScore = 0f;
        UpdateScoreText();
    }
}
