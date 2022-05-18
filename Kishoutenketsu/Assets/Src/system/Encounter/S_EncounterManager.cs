using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum GAME_LENGTH
{
    SHORT,
    MEDIUM,
    LONG
}

public class S_EncounterManager : MonoBehaviour
{
    public O_Encounter currentEncounter;
    public CH_Text descriptionChannel;
    public CH_Text dayDescChannel;
    public CH_Encounter encounterChannel;
    public CH_Boolean descriptionEnabler;
    public S_Globals global;
    public CH_Fade m_FadeChannel;
    public Button nextSceneButton;

    public CH_MapTransfer loadEndingEvent;
    public CH_Func sceneloadEvent;
    public CH_Func encounterLoadEvent;

    public CH_Response optionEvent;
    public int currentDay;
    public GAME_LENGTH game_length;

    public void OptionSelect(O_Response eventOption) {
        

        print(eventOption.name);
        float hInclinationValue = float.MinValue;
        O_Response.O_Reactions appropriateReaction = new O_Response.O_Reactions();
        foreach (var reaction in eventOption.reactions)
        {
            if (reaction.requireEncounter) {
                if (!reaction.encounterReq.Contains(currentEncounter)) {
                    continue;
                }
            }

            float var1 = 0;
            float var2 = 0;

            float getTrait(O_Response.O_Reactions_Cond trait)
            {
                float trt = 0;
                switch (trait.trait)
                {
                    case PERSONALITY_TRAITS.HEADONIC_AESETIC:
                        trt = global.currentActor.traits.headonic_asethetic;
                        break;
                    case PERSONALITY_TRAITS.INTROV_EXTROV:
                        trt = global.currentActor.traits.introv_extrov;
                        break;
                    case PERSONALITY_TRAITS.NASTY_NICE:
                        trt = global.currentActor.traits.nasty_nice;
                        break;
                    case PERSONALITY_TRAITS.SERIOUS_FUNNY:
                        trt = global.currentActor.traits.serious_funny;
                        break;

                    case PERSONALITY_TRAITS.pHEADONIC_AESETIC:
                        trt = global.currentActor[global.Player].headonic_asethetic;
                        break;
                    case PERSONALITY_TRAITS.pINTROV_EXTROV:
                        trt = global.currentActor[global.Player].introv_extrov;
                        break;
                    case PERSONALITY_TRAITS.pNASTY_NICE:
                        trt = global.currentActor[global.Player].nasty_nice;
                        break;
                    case PERSONALITY_TRAITS.pSERIOUS_FUNNY:
                        trt = global.currentActor[global.Player].serious_funny;
                        break;
                }
                if (trait.inverseVal)
                    return -trt;
                else
                    return trt;
            }
            var1 = getTrait(reaction.Val1);
            var2 = getTrait(reaction.Val2);

            float incl = S_BMaths.Blend(var1,var2, reaction.weight);
            print("Action: " + reaction.responseDialogue + ", Inclination: " + incl);
            if (incl > hInclinationValue) {
                appropriateReaction = reaction;
                hInclinationValue = incl;
            }
        }
        print(appropriateReaction.responseDialogue);
        global.currentActor.pTraitsAdd(appropriateReaction.traitsChange);
        descriptionChannel.RaiseEvent(appropriateReaction.responseDialogue);
        nextSceneButton.gameObject.SetActive(true);
    }

    public IEnumerator LoadEncounter()
    {
        nextSceneButton.gameObject.SetActive(false);
        m_FadeChannel.Fade(Color.black);
        yield return new WaitForSeconds(0.75f);
        currentDay++;
        int dayLength = 0;
        switch (game_length) {
            default:
                dayLength = 28;
                break;
        }
        if (currentDay < dayLength)
        {
            LoadEncounter(global.SetEncounters());
            dayDescChannel.RaiseEvent("Day " + currentDay);
            yield return new WaitForSeconds(0.35f);
            m_FadeChannel.Fade(Color.clear);
        }
        else {
            loadEndingEvent.RaiseEvent("Ending");
        }
    }

    public void InitilizeEncounter() {
        StartCoroutine(LoadEncounter());
    }

    public void NextEncounter()
    {
        StartCoroutine(LoadEncounter());
    }

    public void LoadEncounter(O_Encounter encounter)
    {
        currentEncounter = encounter;
        global.SetRandomActor();

        string modText = currentEncounter.description;
        string pronoun = "";
        string pronoun2 = "";
        string pronounPosses = "";
        if (global.currentActor.male_female)
        {
            pronoun = "her";
            pronoun2 = "she";
            pronounPosses = "her";
        }
        else {
            pronoun = "him";
            pronoun2 = "he";
            pronounPosses = "his";
        }

        modText = modText.Replace("(APrP)", pronounPosses);
        modText = modText.Replace("(APr1)", pronoun);
        modText = modText.Replace("(APr2)", pronoun2);
        modText = modText.Replace("(AN)", global.currentActor.name);

        descriptionEnabler.RaiseEvent(true);
        descriptionChannel.RaiseEvent(modText);
        encounterChannel.RaiseEvent(currentEncounter);
    }

    private void OnDisable()
    {
        optionEvent.OnRespEventRaised -= OptionSelect;
        sceneloadEvent.OnFunctionEvent -= InitilizeEncounter;
    }

    private void OnEnable()
    {
        optionEvent.OnRespEventRaised += OptionSelect;
        sceneloadEvent.OnFunctionEvent += InitilizeEncounter;
    }
}
