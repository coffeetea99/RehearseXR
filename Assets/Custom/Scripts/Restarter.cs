using UnityEngine;
using UnityEngine.UI;

public class Restarter : MonoBehaviour
{
    public RawImage blackScreen;
    public float blackTime = 0;
    public float brightSpeed = 1;

    private float alpha = 0f;
    private float time = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (blackScreen == null)
        {
            blackScreen = GetComponent<RawImage>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))    // TODO: change key
        {
            alpha = 1;
            time = 0;
            blackScreen.color = new Color(0f, 0f, 0f, alpha);
        }

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
