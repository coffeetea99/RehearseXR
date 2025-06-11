using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;

public class Light_Rotate : MonoBehaviour
{
    public GameObject Light1_Rotation;
    public float y_angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        y_angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Rotation_object = Light1_Rotation.transform.Find("Dial").gameObject;
        y_angle = 30 + 120 * Rotation_object.GetComponent<XRKnob>().value;

        transform.rotation = Quaternion.Euler(60.0f, y_angle, 0.0f);
    }
}
