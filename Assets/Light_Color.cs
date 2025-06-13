using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;

public class Light_Color : MonoBehaviour
{
    Light myLight;

    public GameObject Light1_Red;
    public GameObject Light1_Blue;
    public GameObject Light1_Green;

    public bool Red_On;
    public bool Blue_On;
    public bool Green_On;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myLight = this.GetComponent<Light>();

        Red_On = false;
        Blue_On = false;
        Green_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Red_Object = Light1_Red.transform.Find("Lever").gameObject;
        GameObject Blue_Object = Light1_Blue.transform.Find("Lever").gameObject;
        GameObject Green_Object = Light1_Green.transform.Find("Lever").gameObject;

        Red_On = Red_Object.GetComponent<XRLever>().value;
        Blue_On = Blue_Object.GetComponent<XRLever>().value;
        Green_On = Green_Object.GetComponent<XRLever>().value;

        
        if (!Red_On && !Blue_On && !Green_On) 
        {
            myLight.color = Color.white;
        }
        else if(Red_On && !Blue_On && !Green_On)
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
