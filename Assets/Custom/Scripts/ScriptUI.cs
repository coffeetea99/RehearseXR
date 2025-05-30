using UnityEngine;
using UnityEngine.UI;

public class ScriptUI : MonoBehaviour
{
    public Graphic scriptBackground;
    public Graphic scriptText;

    private bool isVisible = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setVisibility();
    }

    // Update is called once per frame
    // TODO: change key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isVisible = !isVisible;
            setVisibility();
        }
    }

    void setVisibility()
    {
        if (scriptBackground != null)
        {
            Color c = scriptBackground.color;
            if (isVisible)
            {
                c.a = 0.5f;
            }
            else
            {
                c.a = 0f;
            }
            scriptBackground.color = c;
        }

        if (scriptText != null)
        {
            Color c = scriptText.color;
            if (isVisible)
            {
                c.a = 1f;
            }
            else
            {
                c.a = 0f;
            }
            scriptText.color = c;
        }
    }
}
