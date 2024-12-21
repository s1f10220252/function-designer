using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FunctionManager : MonoBehaviour
{
    public TextMeshProUGUI functionText;
    private Func<float, float> currentFunction;
    private string currentFunctionString;

    void Start()
    {
        GenerateNewFunction();
    }

    public void GenerateNewFunction()
    {
        // Example: Randomly choose between a few functions
        int choice = UnityEngine.Random.Range(0, 3);
        switch (choice)
        {
            case 0:
                currentFunction = Mathf.Sin;
                currentFunctionString = "y = sin(x)";
                break;
            case 1:
                currentFunction = Mathf.Cos;
                currentFunctionString = "y = cos(x)";
                break;
            case 2:
                currentFunction = (x) => x * x; // y = x^2
                currentFunctionString = "y = x^2";
                break;
                // Add more functions as needed
        }
        functionText.text = "Function: " + currentFunctionString;
    }

    public float EvaluateFunction(float x)
    {
        return currentFunction(x);
    }

    public string GetCurrentFunctionString()
    {
        return currentFunctionString;
    }
}
