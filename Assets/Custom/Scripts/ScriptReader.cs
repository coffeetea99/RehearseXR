using UnityEngine;
using TMPro;
using System;

public class ScriptReader : MonoBehaviour
{
    public TextMeshProUGUI mText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mText == null)
        {
            mText = GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        mText.text = String.Join("\n", new string[] { "A : 찾았어?", "B : 아니, 아직.", "A : 큰일 났네.", "B : 어떡하지?\n" });
    }
}
