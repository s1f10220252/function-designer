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
    public float yMin = -10f;
    public float yMax = 10f;
    public float step = 0.1f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        PlotFunction();
        lineRenderer.enabled = false; // Start hidden
    }

    public void PlotFunction()
    {
        List<Vector3> graphPoints = new List<Vector3>();

        for (float x = xMin; x <= xMax; x += step)
        {
            float y = functionManager.EvaluateFunction(x);
            y = Mathf.Clamp(y, yMin, yMax);
            Vector3 worldPos = new Vector3(x, y, 0);
            graphPoints.Add(worldPos);
        }

        lineRenderer.positionCount = graphPoints.Count;
        lineRenderer.SetPositions(graphPoints.ToArray());
    }

    public void ShowGraph()
    {
        lineRenderer.enabled = true;
    }

    public void HideGraph()
    {
        lineRenderer.enabled = false;
    }
}
