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
        // drawingManager.GetComponent<LineRenderer>().enabled = false;
        graphPlotter.ShowGraph();
        scoringManager.CalculateScore();
        nextButton.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(false);

        Debug.Log("OnSubmit called: Graph displayed and score calculated.");
    }

    void OnNext()
    {
        // Reset for next round
        drawingManager.ClearDrawing();
        // drawingManager.GetComponent<LineRenderer>().enabled = true;
        graphPlotter.HideGraph();
        scoringManager.ResetScore();
        functionManager.GenerateNewFunction();
        submitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);

        // Debugging: Confirm OnNext was called
        Debug.Log("OnNext called: New function generated.");
    }
}
