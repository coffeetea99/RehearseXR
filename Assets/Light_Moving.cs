using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;

public class Light_Moving : MonoBehaviour
{
    public GameObject Light1_Left;
    public GameObject Light1_Right;

    public bool Left_On;
    public bool Right_On;

    float moving_distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moving_distance = 0.1f;
        Left_On = false; 
        Right_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Left_object = Light1_Left.transform.Find("Lever").gameObject;
        Left_On = Left_object.GetComponent<XRLever>().value;

        GameObject Right_object = Light1_Right.transform.Find("Lever").gameObject;
        Right_On = Right_object.GetComponent<XRLever>().value;

        if (Left_On)
        {
            transform.Translate(Vector3.left * moving_distance * Time.deltaTime);
        }
        if (Right_On) 
        {
            transform.Translate(Vector3.left * (-1 * moving_distance) * Time.deltaTime);
        }
        
    }
}
