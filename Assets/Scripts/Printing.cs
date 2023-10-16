using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Printing : MonoBehaviour
{
    public Font font;
    public string text;
    TextGenerator tg;
    TextGenerationSettings tg_Setting;

    private void Start()
    {
        tg = new TextGenerator();
        tg_Setting = new TextGenerationSettings();
        tg_Setting.font = font;
        tg_Setting.textAnchor = TextAnchor.MiddleCenter;
        tg_Setting.color = Color.red;
        tg_Setting.generationExtents = new Vector2(500.0f, 200.0f);
        tg_Setting.pivot = Vector2.zero;
        tg_Setting.richText = true;
        tg_Setting.fontSize = 32;
        tg_Setting.fontStyle = FontStyle.Normal;
        tg_Setting.verticalOverflow = VerticalWrapMode.Overflow;
        tg.Populate(text, tg_Setting);
        Debug.Log(tg.vertexCount);
    }

    private void Update()
    {

    }
}
