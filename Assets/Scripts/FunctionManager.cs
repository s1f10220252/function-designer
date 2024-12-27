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
        // Simplified and increased variety of single-stroke functions
        int choice = UnityEngine.Random.Range(0, 10);
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
                currentFunction = (x) => x * x; // Parabola : y = x^2
                currentFunctionString = "y = x^2";
                break;
            case 3:
                currentFunction = (x) => Mathf.Exp(x); // Exponential : y = e^x
                currentFunctionString = "y = e^x";
                break;
            case 4:
                currentFunction = (x) => Mathf.Log(x + 10); // Logarithmic : y = ln(x + 10)
                currentFunctionString = "y = ln(x + 10)";
                break;
            case 5:
                currentFunction = (x) => 2 * x + 3; // Linear : y = 2x + 3
                currentFunctionString = "y = 2x + 3";
                break;
            case 6:
                currentFunction = (x) => Mathf.Abs(x); // Absolute value : y = |x|
                currentFunctionString = "y = |x|";
                break;
            case 7:
                currentFunction = (x) => x * x * x; // Cubic : y = x^3
                currentFunctionString = "y = x^3";
                break;
            case 8:
                currentFunction = (x) => Mathf.Sqrt(x + 10); // Square root (shifted) : y = sqrt(x + 10)
                currentFunctionString = "y = sqrt(x + 10)";
                break;
            case 9:
                currentFunction = (x) => -x; // Inverse linear : y = -x
                currentFunctionString = "y = -x";
                break;
            default:
                currentFunction = (x) => 0f; // Default : y = 0
                currentFunctionString = "y = 0";
                break;
        }
        functionText.text = currentFunctionString;

        // Debugging: Log the new function
        Debug.Log("New Function Generated: " + currentFunctionString);
    }

    public float EvaluateFunction(float x)
    {
        if (currentFunction != null)
        {
            return currentFunction(x);
        }
        else
        {
            Debug.LogError("currentFunction is not assigned.");
            return 0f;
        }
    }

    public string GetCurrentFunctionString()
    {
        return currentFunctionString;
    }
}
