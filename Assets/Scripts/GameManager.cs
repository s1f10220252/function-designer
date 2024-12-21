using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public FunctionManager functionManager;
    public GraphPlotter graphPlotter;
    public DrawingManager drawingManager;
    public ScoringManager scoringManager;
    public Button submitButton;
    public Button nextButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI functionText;

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
        nextButton.onClick.AddListener(OnNext);
        nextButton.gameObject.SetActive(false);
    }

    void OnSubmit()
    {
        drawingManager.GetComponent<LineRenderer>().enabled = false; // Prevent further drawing
        graphPlotter.ShowGraph();
        scoringManager.CalculateScore();
        nextButton.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(false);
    }

    void OnNext()
    {
        // Reset for next round
        drawingManager.ClearDrawing();
        graphPlotter.HideGraph();
        scoringManager.ResetScore();
        functionManager.GenerateNewFunction();
        submitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }
}
