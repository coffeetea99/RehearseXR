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
    Light myLight; //light 컴포넌트를 담는 변수
    public float slider_Value; // slider의 위치를 담는 변수
    public int slider_Value_int; // slider의 위치를 정수로 변환, log(intensity) 값, 0 ~ 30

    public bool lever_on;

    public GameObject Light1_Intensiety;
    public GameObject Light1_OnOff;

    void Start()
    {
        myLight = this.GetComponent<Light>(); //오브젝트가 가진 light 컴포넌트를 가져옴.
        myLight.intensity = 500;
        slider_Value = 0.0f;
        slider_Value_int = 0;
    }

    void Update()
    {
        /*
         // 키보드로만 조작
         // I : 조명세기 증가, O : 조명 on/off, P : 조명 세기 감소
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
        // 키보드로만 조작, slider_Value_int 활용
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

        GameObject child_object2 = Light1_OnOff.transform.Find("Lever").gameObject;
        lever_on = child_object2.GetComponent<XRLever>().value;

        if (lever_on)
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
        else
        {
            myLight.intensity = 0;
        }

       
    }
}