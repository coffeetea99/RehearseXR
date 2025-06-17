using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.XR;
using UnityEngine.XR.Content;
using UnityEngine.XR.Content.Interaction;


public class Light_On_Off : MonoBehaviour
{
    Light myLight; //light ������Ʈ�� ��� ����
    public float slider_Value; // slider�� ��ġ�� ��� ����
    public int slider_Value_int; // slider�� ��ġ�� ������ ��ȯ, log(intensity) ��, 0 ~ 30

    public bool Light_On;

    public GameObject Light1_Intensiety;
    // public GameObject Light1_OnOff;
    public GameObject Light_Button;

    void Start()
    {
        myLight = this.GetComponent<Light>(); //������Ʈ�� ���� light ������Ʈ�� ������.
        myLight.intensity = 500;
        slider_Value = 0.0f;
        slider_Value_int = 0;

        Light_On = true;
    }

    void Update()
    {
        /*
         // Ű����θ� ����
         // I : �������� ����, O : ���� on/off, P : ���� ���� ����
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(myLight.intensity <= 0)
            {
                myLight.intensity = 500;
            }
            else
            {
                myLight.intensity = 0;
            }

        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (myLight.intensity <= 0)
            {
                myLight.intensity = 8;
            }
            else
            {
                myLight.intensity = (int)((float)myLight.intensity * 1.23);
            }


            if (myLight.intensity > 500)
            {
                myLight.intensity = 500;
            }

        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if(myLight.intensity > 0)
            {
                myLight.intensity = (int)((float)myLight.intensity / 1.23);


                if (myLight.intensity < 8)
                {
                    myLight.intensity = 0;
                }
            }
            
        }   
         */

        /*
        // Ű����θ� ����, slider_Value_int Ȱ��
        if (Input.GetKeyDown(KeyCode.I))
        {
            slider_Value_int--;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            slider_Value_int++;
        }

        if (slider_Value_int < 0)
        {
            slider_Value_int = 0;
        }
        else if(slider_Value_int > 30)
        {
            slider_Value_int = 30;
        }
         */

        GameObject child_object = Light1_Intensiety.transform.Find("Slider").gameObject;
        slider_Value = child_object.GetComponent<XRSlider>().value;
        slider_Value_int = (int)(slider_Value * 30);

        // GameObject OnOff_object = Light1_OnOff.transform.Find("Lever").gameObject;
        Light_On = (Light_Button.transform.localPosition.y != 0);

        if (Light_On)
        {
            if (slider_Value_int != 0)
            {
                double counter = 1.0f;
                for (int i = 0; i < slider_Value_int; i++)
                {
                    counter = counter * 1.23;
                }
                myLight.intensity = (int)counter;
            }
            else if (slider_Value_int == 30)
            {
                myLight.intensity = 500;
            }
            else
            {
                myLight.intensity = 0;
            }

        }       
    }
}