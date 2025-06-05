using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScriptUI : MonoBehaviour
{
    public Graphic scriptBackground;
    public Graphic scriptText;

    private bool isVisible = true;

    private InputDevice leftController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setVisibility();

        // Try to get the left controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        if (devices.Count > 0)
        {
            leftController = devices[0];
            Debug.Log("Left controller found: " + leftController.name);
        }
        else
        {
            Debug.LogWarning("Left controller not found.");
        }
    }

    // Update is called once per frame
    // TODO: change key
    void Update()
    {
#if UNITY_EDITOR
        // Editor test: simulate X button press with the 'X' key
        if (Input.GetKeyDown(KeyCode.X))
        {
            isVisible = !isVisible;
            setVisibility();
        }
#else
        if (!leftController.isValid)
            return;

        // Check if the X button (primaryButton on left controller) is pressed
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool xButtonPressed) && xButtonPressed)
        {
            isVisible = !isVisible;
            setVisibility();
        }
#endif
    }

    void setVisibility()
    {
        if (scriptBackground != null)
        {
            Color c = scriptBackground.color;
            if (isVisible)
            {
                c.a = 0.5f;
            }
            else
            {
                c.a = 0f;
            }
            scriptBackground.color = c;
        }

        if (scriptText != null)
        {
            Color c = scriptText.color;
            if (isVisible)
            {
                c.a = 1f;
            }
            else
            {
                c.a = 0f;
            }
            scriptText.color = c;
        }
    }
}
