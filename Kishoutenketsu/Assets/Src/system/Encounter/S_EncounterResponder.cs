using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EncounterResponder : MonoBehaviour
{
    public CH_Response optionEvent;
    public CH_Func disableButtonEvent;

    public void OptionSelect(B_Option _resp) {
        optionEvent.RaiseEvent(_resp.resp);
        disableButtonEvent.RaiseEvent();
    }
}
