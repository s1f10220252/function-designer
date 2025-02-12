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

    private int GetNonZero(int min, int max)
    {
        int val = 0;
        while (val == 0)
        {
            val = UnityEngine.Random.Range(min, max);
        }
        return val;
    }

    void Start()
    {
        GenerateNewFunction();
    }

    public void GenerateNewFunction()
    {
        // We have eight equally likely cases (0 to 7):
        int choice = UnityEngine.Random.Range(0, 8);

        switch (choice)
        {
            case 0:
                {
                    // Linear function: y = a·x + b
                    int a = GetNonZero(-3, 4); // Random nonzero between -3 and 3.
                    int b = UnityEngine.Random.Range(-5, 6); // between -5 and 5.
                    currentFunction = (x) => a * x + b;
                    string aStr = (a == 1) ? "" : (a == -1 ? "-" : a.ToString());
                    currentFunctionString = "y = " + aStr + "x";
                    if (b > 0)
                        currentFunctionString += " + " + b;
                    else if (b < 0)
                        currentFunctionString += " - " + Math.Abs(b);
                    break;
                }
            case 1:
                {
                    // Quadratic function: y = a·x² + b·x + c
                    int a = GetNonZero(-2, 3); // small range for scaling control.
                    int b = UnityEngine.Random.Range(-3, 4);
                    int c = UnityEngine.Random.Range(-5, 6);
                    currentFunction = (x) => a * x * x + b * x + c;
                    string aStr = (Math.Abs(a) == 1) ? (a < 0 ? "-" : "") : a.ToString();
                    currentFunctionString = "y = " + aStr + "x^2";
                    if (b != 0)
                        currentFunctionString += (b > 0 ? " + " : " - ") + (Math.Abs(b) == 1 ? "x" : Math.Abs(b) + "x");
                    if (c != 0)
                        currentFunctionString += (c > 0 ? " + " : " - ") + Math.Abs(c);
                    break;
                }
            case 2:
                {
                    // Absolute value function: y = A·|x| + B
                    int A = GetNonZero(-3, 4); // Avoid 0.
                    int B = UnityEngine.Random.Range(-5, 6);
                    currentFunction = (x) => A * Mathf.Abs(x) + B;
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    currentFunctionString = "y = " + AStr + "|x|";
                    if (B > 0)
                        currentFunctionString += " + " + B;
                    else if (B < 0)
                        currentFunctionString += " - " + Math.Abs(B);
                    break;
                }
            case 3:
                {
                    // Cubic monomial: y = A·x^3
                    int A = GetNonZero(-2, 3);
                    currentFunction = (x) => A * x * x * x;
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    currentFunctionString = "y = " + AStr + "x^3";
                    break;
                }
            case 4:
                {
                    // Exponential function: y = A·exp(B·x)
                    int A = GetNonZero(-2, 3);
                    int B = GetNonZero(-2, 3);
                    currentFunction = (x) => A * Mathf.Exp(B * x);
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    string signB = (B >= 0) ? "" : "-";
                    currentFunctionString = "y = " + AStr + "e^(" + signB + Math.Abs(B) + "x)";
                    break;
                }
            case 5:
                {
                    // Sine function: y = A·sin(x)
                    int A = GetNonZero(-3, 4);
                    currentFunction = (x) => A * Mathf.Sin(x);
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    currentFunctionString = "y = " + AStr + "sin(x)";
                    break;
                }
            case 6:
                {
                    // Cosine function: y = A·cos(x)
                    int A = GetNonZero(-3, 4);
                    currentFunction = (x) => A * Mathf.Cos(x);
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    currentFunctionString = "y = " + AStr + "cos(x)";
                    break;
                }
            case 7:
                {
                    // Constant function: y = C
                    int C = UnityEngine.Random.Range(-5, 6);
                    currentFunction = (x) => C;
                    currentFunctionString = "y = " + C;
                    break;
                }
            default:
                {
                    currentFunction = (x) => 0;
                    currentFunctionString = "y = 0";
                    break;
                }
        }

        functionText.text = currentFunctionString;
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
