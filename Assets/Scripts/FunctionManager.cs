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
        // We'll have eight equally likely cases (0 to 7):
        int choice = UnityEngine.Random.Range(0, 8);

        switch (choice)
        {
            case 0:
                {
                    // Linear function: y = a·x + b
                    int a = GetNonZero(-3, 4); // [-3, 3] but not 0
                    int b = UnityEngine.Random.Range(-5, 6); // [-5, 5]
                    currentFunction = (x) => a * x + b;
                    // Build display string:
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
                    int a = GetNonZero(-2, 3); // small range to keep scaling moderate
                    int b = UnityEngine.Random.Range(-3, 4);
                    int c = UnityEngine.Random.Range(-5, 6);
                    currentFunction = (x) => a * x * x + b * x + c;
                    // Build display string:
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
                    // Radical function: choose randomly between a half–power and a cube–root variant.
                    int radOption = UnityEngine.Random.Range(0, 2);
                    int A = GetNonZero(-2, 3); // coefficient A
                    int B = UnityEngine.Random.Range(-3, 4); // intercept B

                    if (radOption == 0)
                    {
                        // Half–power: y = A·(sign(x)·sqrt(|x|)) + B.
                        currentFunction = (x) => A * (x < 0 ? -Mathf.Sqrt(-x) : Mathf.Sqrt(x)) + B;
                        currentFunctionString = "y = " + ((A == 1) ? "" : (A == -1 ? "-" : A.ToString()))
                                                  + "x^(1/2)";
                    }
                    else
                    {
                        // Cube–root: y = A·(if x >= 0 then x^(1/3) else -(|x|)^(1/3)) + B.
                        currentFunction = (x) => A * (x >= 0 ? Mathf.Pow(x, 1f / 3f) : -Mathf.Pow(-x, 1f / 3f)) + B;
                        currentFunctionString = "y = " + ((A == 1) ? "" : (A == -1 ? "-" : A.ToString()))
                                                  + "x^(1/3)";
                    }
                    // Append intercept if needed:
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
                    // Logarithmic function (modified to be continuous):
                    // Define y = A · ln(|x| + 1). This is continuous over all x.
                    int A = GetNonZero(-2, 3);
                    currentFunction = (x) => A * Mathf.Log(Mathf.Abs(x) + 1);
                    string AStr = (A == 1) ? "" : (A == -1 ? "-" : A.ToString());
                    currentFunctionString = "y = " + AStr + "ln(|x| + 1)";
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
