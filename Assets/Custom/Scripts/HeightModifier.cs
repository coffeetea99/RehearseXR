using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HeightModifier : MonoBehaviour
{
    
    private InputDevice rightController;

    public Transform cameraTransform;
    public float speed = 0.03f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        // Find the left-hand controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
            Debug.Log("Right controller found: " + rightController.name);
        }
        else
        {
            Debug.LogWarning("Right controller not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightController.isValid)
            return;

        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed) && aButtonPressed)
        {
            if (cameraTransform != null)
            {
                Vector3 pos = cameraTransform.localPosition;
                pos.y -= speed;
                cameraTransform.localPosition = pos;
            }
        }

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bButtonPressed) && bButtonPressed)
        {
            if (cameraTransform != null)
            {
                Vector3 pos = cameraTransform.localPosition;
                pos.y += speed;
                cameraTransform.localPosition = pos;
            }
        }
    }
}
