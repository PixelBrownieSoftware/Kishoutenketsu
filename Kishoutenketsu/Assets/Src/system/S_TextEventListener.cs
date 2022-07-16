using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class S_TextEventListener : MonoBehaviour
{
    private Text m_text;
    private TextMeshProUGUI m_text2;
    public CH_Text textEvent;

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_text2 = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        textEvent.OnTextEventRaised += TextChange;
    }

    private void OnDisable()
    {
        textEvent.OnTextEventRaised -= TextChange;
    }

    public void TextChange(string txt) {
        if (m_text2 == null)
            m_text.text = txt;
        else
            m_text2.text = txt;
    }
    
}
