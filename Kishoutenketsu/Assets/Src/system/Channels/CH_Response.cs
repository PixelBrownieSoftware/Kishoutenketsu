using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "System/Response Channel")]
public class CH_Response : SO_ChannelDefaut
{
    public UnityAction<O_Response> OnRespEventRaised;
    public void RaiseEvent(O_Response _resp)
    {
        if (OnRespEventRaised != null)
            OnRespEventRaised.Invoke(_resp);
    }
}

