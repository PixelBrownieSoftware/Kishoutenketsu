using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Start : MonoBehaviour
{
    [Header("Location")]
    public string m_locationToLoad;

    [Header("Channel to broadcast on")]
    public CH_MapTransfer m_startChannel;

    [Header("Channel disable button and description")]
    public CH_Func disableButton;
    public CH_Boolean NextEncButton;
    public CH_Boolean Description;

    [Header("On scene load listener")]
    public CH_Func loadListener;

    public R_Int daysNum;

    private void OnEnable()
    {
        loadListener.OnFunctionEvent += DisableGame;
    }

    public void DisableGame() {
        disableButton.RaiseEvent();
        NextEncButton.RaiseEvent(false);
        Description.RaiseEvent(false);
    }

    private void OnDisable()
    {
        loadListener.OnFunctionEvent -= DisableGame;
    }

    public void StartGame(int daysNum) {
        this.daysNum.integer = daysNum;
        m_startChannel.OnMapTransferEvent.Invoke(m_locationToLoad);
    }
}
