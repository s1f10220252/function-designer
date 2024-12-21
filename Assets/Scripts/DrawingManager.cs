using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    public Camera mainCamera;
    public RectTransform drawingArea;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsPointerOverDrawingArea())
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            if (points.Count == 0 || Vector3.Distance(worldPos, points[points.Count - 1]) > 0.01f)
            {
                points.Add(worldPos);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, worldPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }

    bool IsPointerOverDrawingArea()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea, Input.mousePosition, mainCamera, out localPoint);
        return drawingArea.rect.Contains(localPoint);
    }

    public List<Vector3> GetDrawnPoints()
    {
        return points;
    }

    public void ClearDrawing()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    }
}
