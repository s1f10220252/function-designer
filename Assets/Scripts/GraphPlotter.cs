using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPlotter : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public FunctionManager functionManager;
    public RectTransform drawingArea;
    public Camera mainCamera;
    public float xMin = -10f;
    public float xMax = 10f;
    public float step = 0.1f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        PlotFunction();
        lineRenderer.enabled = false; // Start hidden

        // Debugging: Initial plot
        Debug.Log("GraphPlotter Start: Plotting initial function.");
    }

    public void PlotFunction()
    {
        if (functionManager == null)
        {
            Debug.LogError("FunctionManager reference is missing in GraphPlotter.");
            return;
        }

        List<Vector3> graphPoints = new List<Vector3>();

        for (float x = xMin; x <= xMax; x += step)
        {
            float y = functionManager.EvaluateFunction(x);
            Vector3 worldPos = new Vector3(x, y, 0);
            graphPoints.Add(worldPos);
        }

        lineRenderer.positionCount = graphPoints.Count;
        lineRenderer.SetPositions(graphPoints.ToArray());

        // Debugging: Function plotted
        Debug.Log("GraphPlotter: Function plotted - " + functionManager.GetCurrentFunctionString());
    }

    public void ShowGraph()
    {
        PlotFunction(); // Ensure PlotFunction is called each time
        lineRenderer.enabled = true;
        Debug.Log("GraphPlotter: Graph shown.");
    }

    public void HideGraph()
    {
        lineRenderer.enabled = false;
        Debug.Log("GraphPlotter: Graph hidden.");
    }
}
