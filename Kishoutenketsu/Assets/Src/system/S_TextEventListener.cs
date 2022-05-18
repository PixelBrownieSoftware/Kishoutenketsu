using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class S_TextEventListener : MonoBehaviour
{
    private Text m_text;
    public CH_Text textEvent;

    private void Awake()
    {
        m_text = GetComponent<Text>();
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
        m_text.text = txt;
    }
    
}
