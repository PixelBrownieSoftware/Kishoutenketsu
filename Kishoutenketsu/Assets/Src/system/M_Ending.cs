using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Ending : MonoBehaviour
{
    public CH_MapTransfer loadScene;
    public S_Globals global;
    public CH_Func disableText;
    public CH_Func disableButton;
    public CH_Boolean disablMenu;
    public CH_Func onLoadScene;

    public void OnSceneLoad() {
        disableText.RaiseEvent();
        disableButton.RaiseEvent();
        disablMenu.RaiseEvent(false);
    }

    private void OnDisable()
    {
        onLoadScene.OnFunctionEvent -= OnSceneLoad;
    }

    private void OnEnable()
    {
        onLoadScene.OnFunctionEvent += OnSceneLoad;    
    }

    public void Ending() {
        loadScene.RaiseEvent("MainMenu");
    }
}
