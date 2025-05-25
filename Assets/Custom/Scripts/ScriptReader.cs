using UnityEngine;
using TMPro;
using System;
using System.Linq;

struct ScriptLine
{
    public int timeSecond;
    public string text;

    public ScriptLine(int _timeSecond, string _text)
    {
        timeSecond = _timeSecond;
        text = _text;
    }
}

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
        mText.text = String.Join("\n", fullScript.Select(line => line.text));
    }

    private static readonly ScriptLine[] fullScript = new ScriptLine[]{
        new ScriptLine(1, "A : 찾았어?"),
        new ScriptLine(2, "B : 아니, 아직."),
        new ScriptLine(3, "A : 큰일 났네."),
        new ScriptLine(4, "B : 어떡하지?\n")
    };
}
