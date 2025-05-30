using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Light_On_Off : MonoBehaviour
{
    Light myLight; //light 컴포넌트를 담는 변수

    void Start()
    {
        myLight = this.GetComponent<Light>(); //오브젝트가 가진 light 컴포넌트를 가져옴.
        myLight.intensity = 500;
    }

    void Update()
    {
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
    }
}