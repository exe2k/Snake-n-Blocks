using UnityEngine;

/// <summary>
/// ~  Fps counter for unity  ~
/// Brief : Calculate the FPS and display it on the screen 
/// HowTo : Create empty object at initial scene and attach this script!!!
/// </summary>
public class FPSCounter : MonoBehaviour
{
    private bool isEnabled;

    // for ui.
    private int screenLongSide;
    private Rect boxRect;
    private GUIStyle style = new GUIStyle();

    // for fps calculation.
    private int frameCount;
    private float elapsedTime;
    private double frameRate;

    /// <summary>
    /// Initialization
    /// </summary>
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        UpdateUISize();
    }

    private void Start()
    {
        try
        {
            isEnabled = CONST.DEV_MODE;
        }
        catch
        {
            isEnabled = true;
        }
    }

    private void Update()
    {
        if (!isEnabled) return;

        // FPS calculation
        frameCount++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f)
        {
            frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
            frameRate = Mathf.RoundToInt((float)frameRate);
            frameCount = 0;
            elapsedTime = 0;

            // Update the UI size if the resolution has changed
            if (screenLongSide != Mathf.Max(Screen.width, Screen.height))
            {
                UpdateUISize();
            }
        }
    }

    private void UpdateUISize()
    {

        screenLongSide = Mathf.Max(Screen.width, Screen.height);
        var rectLongSide = screenLongSide / 9;
        boxRect = new Rect(5, Screen.height*.8f, rectLongSide, rectLongSide / 3);
        style.fontSize = (int)(screenLongSide / 36.8);
        style.normal.textColor = Color.cyan;
        style.alignment = TextAnchor.MiddleLeft;
    }

    /// <summary>
    /// Display FPS
    /// </summary>
    private void OnGUI()
    {
        if (!isEnabled) return;

        GUI.Box(boxRect, "");
        GUI.Label(boxRect, " fps: " + frameRate+" ", style);
    }
}