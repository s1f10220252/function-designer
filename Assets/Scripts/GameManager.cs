using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public FunctionManager functionManager;
    public GraphPlotter graphPlotter;
    public DrawingManager drawingManager;
    public ScoringManager scoringManager;

    [Header("UI Elements")]
    public Button submitButton;
    public Button nextButton;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI functionText;

    [Header("Game Settings")]
    public int maxRounds = 3;

    private int currentRound = 0;
    private float totalScore = 0f;

    void Start()
    {
        // Assign button listeners
        submitButton.onClick.AddListener(OnSubmit);
        nextButton.onClick.AddListener(OnNext);
        restartButton.onClick.AddListener(OnRestart);

        // Initialize UI
        nextButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        // Initialize game state
        totalScore = 0f;
        currentRound = 0;
        scoringManager.ResetScore();
        functionManager.GenerateNewFunction();
    }

    void OnSubmit()
    {
        // Show the correct graph and calculate the score
        graphPlotter.ShowGraph();
        scoringManager.CalculateScore();

        // Get the current round score from ScoringManager
        float currentScore = scoringManager.GetCurrentScore();
        totalScore += currentScore;
        currentRound++;

        // Update the round score display
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + Mathf.RoundToInt(currentScore).ToString();

        if (currentRound >= maxRounds)
        {
            // After max rounds, display total score and show Restart button
            totalScoreText.gameObject.SetActive(true);
            totalScoreText.text = "Total Score: " + Mathf.RoundToInt(totalScore).ToString();

            restartButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            // Otherwise, show Next button for the next round
            nextButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
        }

        Debug.Log("OnSubmit called: Graph displayed and score calculated.");
    }

    void OnNext()
    {
        // Prepare for the next round
        drawingManager.ClearDrawing();
        graphPlotter.HideGraph();
        scoringManager.ResetScore();
        functionManager.GenerateNewFunction();

        // Reset UI elements for the next round
        submitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        // Debugging: Confirm OnNext was called
        Debug.Log("OnNext called: New function generated.");
    }

    void OnRestart()
    {
        // Reset all game variables and UI elements
        drawingManager.ClearDrawing();
        graphPlotter.HideGraph();
        scoringManager.ResetScore();
        functionManager.GenerateNewFunction();

        submitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        totalScore = 0f;
        currentRound = 0;

        // Debugging: Confirm OnRestart was called
        Debug.Log("OnRestart called: Game reset.");
    }
}
