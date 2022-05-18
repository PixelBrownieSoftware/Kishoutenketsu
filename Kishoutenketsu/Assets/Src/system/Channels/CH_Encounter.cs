using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "System/Encounter Channel")]
public class CH_Encounter : SO_ChannelDefaut
{
    public UnityAction<O_Encounter> OnFunctionEvent;
    public void RaiseEvent(O_Encounter enc)
    {
        if (OnFunctionEvent != null)
            OnFunctionEvent.Invoke(enc);
    }
}
