using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Restarter : MonoBehaviour
{
    public RawImage blackScreen;
    public float blackTime = 0;
    public float brightSpeed = 1;

    private float alpha = 0f;
    private float time = 0f;
    
    private InputDevice leftController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (blackScreen == null)
        {
            blackScreen = GetComponent<RawImage>();
        }
        
        // Find the left-hand controller
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
    void Update()
    {
    #if UNITY_EDITOR
        // Debug key for simulating Y button press in Editor
        if (Input.GetKeyDown(KeyCode.Y))
        {
            alpha = 1;
            time = 0;
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
        }
    #else
        if (!leftController.isValid)
            return;

        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool yButtonPressed) && yButtonPressed)
        {
            alpha = 1;
            time = 0;
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
        }
    #endif

        if (alpha > 0)
        {
            time += Time.deltaTime;
            if (time > blackTime)
            {
                alpha = Mathf.Max(alpha - Time.deltaTime * brightSpeed, 0);
                blackScreen.color = new Color(0f, 0f, 0f, alpha);
            }
            
        }
    }
}
