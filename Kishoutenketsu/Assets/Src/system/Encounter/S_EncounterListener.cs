using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_EncounterListener : MonoBehaviour
{
    public CH_Encounter encounterEvent;
    public CH_Func disableMenuListenerEvent;
    public CH_Func disableNextEncButton;
    public CH_Boolean controllerDescription;
    public CH_Encounter nextEncounterEvent;
    // public CH_MapTransfer m_startChannel;
    public S_Globals global;


    public Button[] buttons;
    B_Option[] optionButtons;

    private void Awake()
    {
        optionButtons = new B_Option[buttons.Length];
        int index = 0;
        foreach (var button in buttons) {
            optionButtons[index] = button.GetComponent<B_Option>();
            index++;
        }
    }

    private void OnDisable()
    {
        encounterEvent.OnFunctionEvent -= OnEncounterLoad;
        disableMenuListenerEvent.OnFunctionEvent -= ButtonsDisable;
    }

    private void OnEnable()
    {
        encounterEvent.OnFunctionEvent += OnEncounterLoad;
        disableMenuListenerEvent.OnFunctionEvent += ButtonsDisable;
    }


    
    public void ButtonsDisable()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void OnEncounterLoad(O_Encounter _encounter) {
        int index = 0;
        List<O_Response> respAvailable = global.responses.FindAll(x => x.checkEncounters && x.encounterReq.Contains(_encounter) || !x.checkEncounters);
        List<O_Response> respOptions = new List<O_Response>();

        for (int i = 0; i < 5; i++) {
            O_Response option = respAvailable[Random.Range(0, respAvailable.Count)];
            respOptions.Add(option);
            respAvailable.Remove(option);
        }

        foreach (var button in buttons) {
            if (respOptions.Count <= index) {
                button.gameObject.SetActive(false);
            } else
            {
                button.gameObject.SetActive(true);
                optionButtons[index].resp = respOptions[index];
                button.gameObject.transform.GetChild(0).GetComponent<Text>().text = respOptions[index].name;
            }
            index++;
        }
    }

}
