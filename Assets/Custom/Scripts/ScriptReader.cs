using UnityEngine;
using TMPro;

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

    private float timer = 0f;
    private float beforeTime = 0f;
    private string initialText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mText == null)
        {
            mText = GetComponent<TextMeshProUGUI>();
        }
        initialText = fullScript[0].text + "\n" + fullScript[1].text + "\n" + fullScript[2].text + "\n" + fullScript[3].text;
        mText.text = initialText;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = 0f;
            beforeTime = 0f;
            mText.text = initialText;
        }

        beforeTime = timer;
        timer += Time.deltaTime;
        
        for (int i = 0; i < fullScript.Length; i++)
        {
            if (DidTimePass(fullScript[i].timeSecond))
            {
                string newText = fullScript[i].text;
                if (i + 1 < fullScript.Length)
                {
                    newText += ("\n" + fullScript[i + 1].text);
                }
                if (i + 2 < fullScript.Length)
                {
                    newText += ("\n" + fullScript[i + 2].text);
                }
                if (i + 3 < fullScript.Length)
                {
                    newText += ("\n" + fullScript[i + 3].text);
                }
                mText.text = newText;

                break;
            }
        }
    }

    bool DidTimePass(float referenceTime)
    {
        return beforeTime < referenceTime && referenceTime <= timer;
    }

    // TODO: set time
    private static readonly ScriptLine[] fullScript = new ScriptLine[]{
        new ScriptLine(00, "A와 B 손전등을 켜고 방 안을 뒤진다 <color=red>(핀 조명만 들어와 있다)</color>\n"),
        new ScriptLine(02, "A : 찾았어?"),
        new ScriptLine(04, "B : 아니, 아직."),
        new ScriptLine(06, "A : 큰일 났네."),
        new ScriptLine(08, "B : 어떡하지?\n"),
        new ScriptLine(10, "C가 방 안으로 들어오며 불을 켠다 <color=red>(더 넓은 영역에 조명이 들어온다)</color>\n"),
        new ScriptLine(12, "C : 그냥 불 켜고 찾아."),
        new ScriptLine(14, "B : 미쳤어? 불을 켜면 어떡해!"),
        new ScriptLine(16, "A : 맞아! 누가 보면 어떡하려고!"),
        new ScriptLine(18, "C : 그렇게 시끄럽게 부스럭대는 것보단, 불 켜고 빨리 찾는 게 나아."),
        new ScriptLine(20, "B : 그런가..."),
        new ScriptLine(22, "C : 그러니까 빨리 찾아.\n"),
        new ScriptLine(24, "세 사람이 방을 뒤적거린다\n"),
        new ScriptLine(26, "A : <color=red>(옆 방의 불을 켜며)</color> 여기 있나?"),
        new ScriptLine(28, "B : <color=red>(반대쪽 방의 불을 켜며)</color> 난 이쪽 찾아볼게.\n"),
        new ScriptLine(30, "C : 어? 이거 아냐?"),
        new ScriptLine(32, "B : (다시 방에서 나오며) 아니야. 우리가 찾는 건 빨간색인데 이건 붉은색이잖아."),
        new ScriptLine(34, "C : 뭐가 다른 거야…?\n"),
        new ScriptLine(36, "A : <color=red>(다시 불을 끄고 나오며)</color> 어디 있는 거야 진짜."),
        new ScriptLine(38, "C : 저쪽은 찾아봤어?"),
        new ScriptLine(40, "A : 응. 없어."),
        new ScriptLine(42, "B : 아무리 찾아도 나오질 않아."),
        new ScriptLine(44, "C : 하, 어떡하지. 시간이 없다, 시간이."),
        new ScriptLine(46, "B : 도대체 어디에 놔둔 거야.\n"),
        new ScriptLine(48, "<color=red>갑자기 사이렌이 울리며 붉은 조명이 들어온다</color>\n"),
        new ScriptLine(50, "A : 큰일났다!"),
        new ScriptLine(52, "B : 어쩌지, 아직 못찾았는데."),
        new ScriptLine(54, "C : 지금 그게 중요해? 뛰어!"),
        new ScriptLine(56, "A : 여기만 보고 갈게...!"),
        new ScriptLine(58, "C : 그럴 시간 없다고! 빨리 나와!\n"),
        new ScriptLine(60, "세 사람, 방에서 도망친다"),
        new ScriptLine(62, "<color=red>암전</color>"),
    };
}

/*
\n\n -> \\n\n 으로 일괄 변경
앞쪽에
new ScriptLine(00, "
뒤쪽에
"),
를 붙이기

1.062  1   1.5
1.422  2   2
1.476  3   2
1.188  4   1.5

1.548  5   2
2.754  6   3.5
3.384  7   4
4.482  8   5
0.972  9   1.5
1.674  10  2

1.242  11  1.5
1.782  12  2.5

1.980  13  2.5
4.356  14  5
1.728  15  2

1.980  16  2.5
1.728  17  2
2.142  18  2.5
1.836  19  2.5
3.762  20  4.5
2.034  21  2.5

1.422  22  2
2.322  23  3
2.502  24  3
1.962  25  2.5
2.862  26  3.5
*/
