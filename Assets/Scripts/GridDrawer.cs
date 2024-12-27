using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class GridDrawer : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("Number of grid squares horizontally.")]
    public int desiredHorizontalGridCount = 20; // For example, 20 squares horizontally

    [Tooltip("Size of each grid square in local units.")]
    public float gridSquareSize = 1f; // Each grid square is 1 unit

    [Header("Line Settings")]
    [Tooltip("Width of the grid lines.")]
    public float lineWidth = 0.05f;

    [Tooltip("Length of the tick marks.")]
    public float tickLength = 0.2f;

    [Header("Materials")]
    public Material gridLineMaterial;
    public Material tickMaterial;

    // **Private Variables:**

    private RectTransform rectTransform;
    private Camera mainCamera;
    private float screenRatio;
    private float orthoSize;
    private float minX, maxX, minY, maxY;
    private int previousScreenWidth;
    private int previousScreenHeight;

    void Awake()
    {
        // Initialize references
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a camera tagged as MainCamera.");
        }

        // Store initial screen dimensions
        previousScreenWidth = Screen.width;
        previousScreenHeight = Screen.height;
    }

    void Start()
    {
        AdjustCamera();
        DrawGrid();
    }

    void Update()
    {
        // Check if the screen size has changed to redraw the grid
        if (Screen.width != previousScreenWidth || Screen.height != previousScreenHeight)
        {
            previousScreenWidth = Screen.width;
            previousScreenHeight = Screen.height;
            AdjustCamera();
            DrawGrid();
        }
    }

    /// <summary>
    /// Adjusts the camera's orthographic size to maintain a consistent number of horizontal grid squares.
    /// </summary>
    void AdjustCamera()
    {
        if (mainCamera == null) return;

        // Calculate the screen ratio (width / height)
        screenRatio = (float)Screen.width / Screen.height;

        // Set the orthographic size based on the desired number of horizontal squares and aspect ratio
        // Orthographic Size defines the half-height of the camera's view
        // Total horizontal view = orthographicSize * 2 * aspectRatio
        // We set: totalHorizontalView = desiredHorizontalGridCount * gridSquareSize
        float totalHorizontalView = desiredHorizontalGridCount * gridSquareSize;
        orthoSize = (totalHorizontalView / screenRatio) / 2f;

        mainCamera.orthographicSize = orthoSize;
    }

    /// <summary>
    /// Draws the grid lines and tick marks based on the current camera settings and DrawingArea size.
    /// </summary>
    void DrawGrid()
    {
        // **1. Clear Existing Grid Lines and Ticks:**
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // **2. Calculate Grid Boundaries Based on DrawingArea Size:**
        // Assume the RectTransform size is in units matching the gridSquareSize
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        // Calculate min and max based on RectTransform size and grid square size
        minX = -(width / 2f);
        maxX = width / 2f;
        minY = -(height / 2f);
        maxY = height / 2f;

        // **3. Draw Horizontal Lines:**
        for (float y = minY; y <= maxY; y += gridSquareSize)
        {
            // Create a new GameObject for each horizontal line
            GameObject hLine = new GameObject("H_Line_" + y);
            hLine.transform.parent = this.transform;

            // Add LineRenderer component
            LineRenderer lr = hLine.AddComponent<LineRenderer>();
            lr.material = gridLineMaterial;
            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.positionCount = 2;
            lr.useWorldSpace = false; // Use local space

            // Set positions in local coordinates
            lr.SetPosition(0, new Vector3(minX, y, 0));
            lr.SetPosition(1, new Vector3(maxX, y, 0));
            lr.sortingOrder = 0;

            // **Add Tick Marks to Horizontal Lines:**
            // Tick marks are added at the right end of each horizontal line
            GameObject hTick = new GameObject("Tick_H_" + y);
            hTick.transform.parent = hLine.transform;

            LineRenderer hTickLR = hTick.AddComponent<LineRenderer>();
            hTickLR.material = tickMaterial;
            hTickLR.startColor = Color.white;
            hTickLR.endColor = Color.white;
            hTickLR.startWidth = lineWidth;
            hTickLR.endWidth = lineWidth;
            hTickLR.positionCount = 2;
            hTickLR.useWorldSpace = false;

            // Tick mark starts at (maxX, y, 0) and extends left by tickLength
            hTickLR.SetPosition(0, new Vector3(maxX, y, 0));
            hTickLR.SetPosition(1, new Vector3(maxX - tickLength, y, 0));
            hTickLR.sortingOrder = 1;
        }

        // **4. Draw Vertical Lines:**
        for (float x = minX; x <= maxX; x += gridSquareSize)
        {
            // Create a new GameObject for each vertical line
            GameObject vLine = new GameObject("V_Line_" + x);
            vLine.transform.parent = this.transform;

            // Add LineRenderer component
            LineRenderer vr = vLine.AddComponent<LineRenderer>();
            vr.material = gridLineMaterial;
            vr.startColor = Color.white;
            vr.endColor = Color.white;
            vr.startWidth = lineWidth;
            vr.endWidth = lineWidth;
            vr.positionCount = 2;
            vr.useWorldSpace = false; // Use local space

            // Set positions in local coordinates
            vr.SetPosition(0, new Vector3(x, minY, 0));
            vr.SetPosition(1, new Vector3(x, maxY, 0));
            vr.sortingOrder = 0;

            // **Add Tick Marks to Vertical Lines:**
            // Tick marks are added at the top end of each vertical line
            GameObject vTick = new GameObject("Tick_V_" + x);
            vTick.transform.parent = vLine.transform;

            LineRenderer vTickLR = vTick.AddComponent<LineRenderer>();
            vTickLR.material = tickMaterial;
            vTickLR.startColor = Color.white;
            vTickLR.endColor = Color.white;
            vTickLR.startWidth = lineWidth;
            vTickLR.endWidth = lineWidth;
            vTickLR.positionCount = 2;
            vTickLR.useWorldSpace = false;

            // Tick mark starts at (x, maxY, 0) and extends down by tickLength
            vTickLR.SetPosition(0, new Vector3(x, maxY, 0));
            vTickLR.SetPosition(1, new Vector3(x, maxY - tickLength, 0));
            vTickLR.sortingOrder = 1;
        }

        // **5. Optional: Draw Origin Axis Differently**
        // For better visibility, you can set the origin axes to a different color or width
        DrawOriginAxes();
    }

    /// <summary>
    /// Draws the X and Y axes with a distinct appearance.
    /// </summary>
    void DrawOriginAxes()
    {
        // **a. Horizontal Axis (Y=0)**
        GameObject hAxis = new GameObject("H_Axis_0");
        hAxis.transform.parent = this.transform;

        LineRenderer lr = hAxis.AddComponent<LineRenderer>();
        lr.material = gridLineMaterial;
        lr.startColor = Color.red; // Different color for axes
        lr.endColor = Color.red;
        lr.startWidth = lineWidth * 2; // Thicker line for axes
        lr.endWidth = lineWidth * 2;
        lr.positionCount = 2;
        lr.useWorldSpace = false;

        lr.SetPosition(0, new Vector3(minX, 0, 0));
        lr.SetPosition(1, new Vector3(maxX, 0, 0));
        lr.sortingOrder = 2;

        // **b. Vertical Axis (X=0)**
        GameObject vAxis = new GameObject("V_Axis_0");
        vAxis.transform.parent = this.transform;

        LineRenderer vr = vAxis.AddComponent<LineRenderer>();
        vr.material = gridLineMaterial;
        vr.startColor = Color.red; // Different color for axes
        vr.endColor = Color.red;
        vr.startWidth = lineWidth * 2; // Thicker line for axes
        vr.endWidth = lineWidth * 2;
        vr.positionCount = 2;
        vr.useWorldSpace = false;

        vr.SetPosition(0, new Vector3(0, minY, 0));
        vr.SetPosition(1, new Vector3(0, maxY, 0));
        vr.sortingOrder = 2;
    }
}
