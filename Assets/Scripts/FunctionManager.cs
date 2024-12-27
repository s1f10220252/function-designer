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
        // Increased number of functions for more variety
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
                currentFunction = (x) => x * x; // y = x^2
                currentFunctionString = "y = x^2";
                break;
            case 3:
                currentFunction = (x) => Mathf.Exp(x); // y = e^x
                currentFunctionString = "y = e^x";
                break;
            case 4:
                currentFunction = (x) => Mathf.Log(x + 10); // y = ln(x + 10)
                currentFunctionString = "y = ln(x + 10)";
                break;
            case 5:
                currentFunction = (x) => 2 * x + 3; // Linear function: y = 2x + 3
                currentFunctionString = "y = 2x + 3";
                break;
            case 6:
                currentFunction = (x) => Mathf.Abs(x); // y = |x|
                currentFunctionString = "y = |x|";
                break;
            case 7:
                currentFunction = (x) => 1 / (x + 1); // y = 1/(x + 1), avoiding division by zero
                currentFunctionString = "y = 1/(x + 1)";
                break;
            case 8:
                currentFunction = (x) => Mathf.Sqrt(Mathf.Abs(x)); // y = sqrt(|x|)
                currentFunctionString = "y = sqrt(|x|)";
                break;
            case 9:
                currentFunction = (x) => Mathf.Tan(x); // y = tan(x), note: handle appropriately in practice
                currentFunctionString = "y = tan(x)";
                break;
            default:
                currentFunction = (x) => 0f; // Default to y = 0
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
