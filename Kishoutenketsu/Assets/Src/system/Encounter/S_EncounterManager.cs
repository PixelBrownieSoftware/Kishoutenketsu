using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


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

    public CH_Sprite BGSpriteChannel;
    public CH_Sprite CHSpriteChannel;

    public CH_MapTransfer loadEndingEvent;
    public CH_Func sceneloadEvent;
    public CH_Func encounterLoadEvent;

    public CH_Response optionEvent;
    public int currentDay;
    public R_Int dayLength;

    public void OptionSelect(O_Response eventOption) {
        print(eventOption.name);
        float hInclinationValue = float.MinValue;
        O_Response.O_Reactions appropriateReaction = new O_Response.O_Reactions();
        foreach (var reaction in eventOption.reactionsList)
        {
            if (reaction.encounterReq.Count > 0) {
                if (reaction.qualify_disqualify)
                {
                    if (!reaction.encounterReq.Contains(currentEncounter))
                    {
                        continue;
                    }
                }
                else
                {
                    if (reaction.encounterReq.Contains(currentEncounter))
                    {
                        continue;
                    }
                }
            }

            if (reaction.interests.Count > 0) {
                bool gotcha = false;
                foreach (var interest in reaction.interests) {
                    if (!global.currentActor.interests.Contains(interest)) {
                        gotcha = true;
                        break;
                    }
                }
                if (gotcha)
                    continue;
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
        descriptionChannel.RaiseEvent(ParseDialogue(appropriateReaction.responseDialogue));
        nextSceneButton.gameObject.SetActive(true);
    }

    public IEnumerator LoadEncounter()
    {
        nextSceneButton.gameObject.SetActive(false);
        m_FadeChannel.Fade(Color.black);
        yield return new WaitForSeconds(0.75f);
        currentDay++;
        if (currentDay < dayLength.integer)
        {
            LoadEncounter(global.SetEncounters());
            dayDescChannel.RaiseEvent("Day " + currentDay + "/" + dayLength.integer);
            yield return new WaitForSeconds(0.35f);
            m_FadeChannel.Fade(Color.clear);
        }
        else {
            loadEndingEvent.RaiseEvent("Ending");
        }
    }

    public void InitilizeEncounter() {
        global.Initialise();
        global.SetRandomActor();
        StartCoroutine(LoadEncounter());
    }

    public void NextEncounter()
    {
        global.SetRandomActor();
        StartCoroutine(LoadEncounter());
    }

    public string ParseDialogue(string modText)
    {
        string[] words = modText.Split(' ');

        string pronoun = "";
        string pronoun2 = "";
        string pronounPosses = "";

        string modStr = "";
        string lastWord = " ";
        foreach (string word in words)
        {
            if (lastWord != "")
            {
                if (global.currentActor.male_female)
                {
                    if (lastWord[lastWord.Length - 1] == '.')
                    {
                        pronoun = "Her";
                        pronoun2 = "She";
                        pronounPosses = "Her";
                    }
                    else
                    {
                        pronoun = "her";
                        pronoun2 = "she";
                        pronounPosses = "her";
                    }
                }
                else
                {
                    if (lastWord[lastWord.Length - 1] == '.')
                    {
                        pronoun = "Him";
                        pronoun2 = "He";
                        pronounPosses = "His";
                    }
                    else
                    {
                        pronoun = "him";
                        pronoun2 = "he";
                        pronounPosses = "his";
                    }
                }
            }

            string wordCheck = word;
            if (wordCheck.Contains('.'))
            {
                switch (wordCheck)
                {
                    case "<AN>":
                    case "<APrP>":
                    case "<APr1>":
                    case "<APr2>":
                        wordCheck = word.Remove('.');
                        break;
                }

            }
            switch (wordCheck)
            {
                case "<AN>":
                    modStr += global.currentActor.name;
                    break;
                case "<APrP>":
                    modStr += pronounPosses;
                    break;
                case "<APr1>":
                    modStr += pronoun;
                    break;
                case "<APr2>":
                    modStr += pronoun2;
                    break;
                default:
                    modStr += word;
                    break;
            }
            modStr += " ";
            lastWord = word;
        }
        return modStr;
    }

    public void LoadEncounter(O_Encounter encounter)
    {
        currentEncounter = encounter;
        
        descriptionEnabler.RaiseEvent(true);
        descriptionChannel.RaiseEvent(ParseDialogue(currentEncounter.description));
        BGSpriteChannel.RaiseEvent(encounter.background);
        CHSpriteChannel.RaiseEvent(global.currentActor.characterSprite);
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
