using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;

public class Light_Color : MonoBehaviour
{
    Light myLight;

    public GameObject Light1_OnOff;
    public GameObject Light1_Red;
    public GameObject Light1_Blue;
    public GameObject Light1_Green;

    public bool Light_On;
    public bool Red_On;
    public bool Blue_On;
    public bool Green_On;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myLight = this.GetComponent<Light>();

        Light_On = true;
        Red_On = false;
        Blue_On = false;
        Green_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject OnOff_object = Light1_OnOff.transform.Find("Lever").gameObject;
        GameObject Red_Object = Light1_Red.transform.Find("Lever").gameObject;
        GameObject Blue_Object = Light1_Blue.transform.Find("Lever").gameObject;
        GameObject Green_Object = Light1_Green.transform.Find("Lever").gameObject;

        Light_On = OnOff_object.GetComponent<XRLever>().value;
        Red_On = Red_Object.GetComponent<XRLever>().value;
        Blue_On = Blue_Object.GetComponent<XRLever>().value;
        Green_On = Green_Object.GetComponent<XRLever>().value;

        if(Light_On)
        {
            if (!Red_On && !Blue_On && !Green_On)
            {
                myLight.color = Color.white;
            }
            else if (Red_On && !Blue_On && !Green_On)
            {
                myLight.color = Color.red;
            }
            else if (!Red_On && Blue_On && !Green_On)
            {
                myLight.color = Color.blue;
            }
            else if (!Red_On && !Blue_On && Green_On)
            {
                myLight.color = Color.green;
            }
            else if (Red_On && Blue_On && !Green_On)
            {
                myLight.color = Color.magenta;
            }
            else if (Red_On && !Blue_On && Green_On)
            {
                myLight.color = Color.yellow;
            }
            else if (!Red_On && Blue_On && Green_On)
            {
                myLight.color = Color.cyan;
            }
            else if (Red_On && Blue_On && Green_On)
            {
                myLight.color = Color.black;
            }
        }  

    }
}
