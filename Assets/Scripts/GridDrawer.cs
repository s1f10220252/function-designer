using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class GridDrawer : MonoBehaviour
{
    // **Public Variables:**

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

        // Set the orthographic size based on the desired horizontal grid count and aspect ratio
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

        // **2. Calculate Grid Boundaries:**
        // The DrawingArea is expected to have a size of (desiredHorizontalGridCount * gridSquareSize, desiredVerticalGridCount * gridSquareSize)
        // Assuming a square aspect for DrawingArea. Adjust if different.
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        minX = -width / 2f;
        maxX = width / 2f;
        minY = -height / 2f;
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

            // Tick mark starts at (maxX, y) and extends left by tickLength
            hTickLR.SetPosition(0, new Vector3(maxX, y, 0));
            hTickLR.SetPosition(1, new Vector3(maxX - tickLength, y, 0));
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

            // Tick mark starts at (x, maxY) and extends down by tickLength
            vTickLR.SetPosition(0, new Vector3(x, maxY, 0));
            vTickLR.SetPosition(1, new Vector3(x, maxY - tickLength, 0));
        }

        // **5. Ensure Specific World Coordinates Align Correctly:**
        // For example, placing an object at (-10, 0) should align perfectly with the grid
        // This is inherently handled by ensuring gridSquareSize is consistent and the camera is adjusted accordingly
    }
}
