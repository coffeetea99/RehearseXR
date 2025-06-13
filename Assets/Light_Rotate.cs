using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;

public class Light_Rotate : MonoBehaviour
{
    public GameObject Light1_OnOff;
    public GameObject Light1_Rotation;
    public GameObject Light1_Rotation_On;

    public float y_angle;
    public bool Light_On;
    public bool Rotation_On;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        y_angle = 0.0f;

        Light_On = true;
        Rotation_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject OnOff_object = Light1_OnOff.transform.Find("Lever").gameObject;
        GameObject Rotation_On_object = Light1_Rotation_On.transform.Find("Lever").gameObject;
        GameObject Rotation_object = Light1_Rotation.transform.Find("Dial").gameObject;

        Light_On = OnOff_object.GetComponent<XRLever>().value;
        Rotation_On = Rotation_On_object.GetComponent<XRLever>().value;        
        y_angle = 30 + 120 * Rotation_object.GetComponent<XRKnob>().value;

        if (Light_On)
        {
            if (Rotation_On)
            {
                transform.rotation = Quaternion.Euler(60.0f, y_angle, 0.0f);
            }
        }
               
    }
}
