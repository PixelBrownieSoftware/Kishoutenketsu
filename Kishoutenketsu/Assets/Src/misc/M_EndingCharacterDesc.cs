using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class M_EndingCharacterDesc : MonoBehaviour
{
    public Text text;
    public TextMeshProUGUI tmpTxt;
    public O_Actor character;
    public CH_Func OnSceneLoad;

    public void OnEnable()
    {
        OnSceneLoad.OnFunctionEvent += ShowThoughts;
    }

    private void OnDisable()
    {
        OnSceneLoad.OnFunctionEvent -= ShowThoughts;
    }


    public void ShowThoughts() {
        string finText = "";
        V_Traits traits = character.perceivedOpinions[0].pTraits;

        string begining = character.name + " thinks that you are ";

        string TraitToString(float value, string posVal, string negVal)
        {
            float absVal = Mathf.Abs(value);
            string adjective = "";

            string trait = "";


            if (value > 0)
            {
                trait = posVal;
            }
            else
            {
                trait = negVal;
            }

            if (absVal < 0.1f)
            {
                return "";
            }
            else if (absVal < 0.2f)
            {
                adjective = "a little bit";
            }
            else if (absVal < 0.3f)
            {
                adjective = "somewhat";
            }
            else if (absVal < 0.5f)
            {
                adjective = "fairly";
            }
            else if (absVal < 0.6f)
            {
                adjective = "quite";
            }
            else if (absVal < 0.75f)
            {
                adjective = "pretty";
            }
            else if (absVal < 0.8f)
            {
                adjective = "very";
            }
            else
            {
                adjective = "extremely";
            }
            return adjective + " " + trait;
        }

        string nasty_nice = TraitToString(traits.nasty_nice, "nice", "nasty");
        string introv_extrov = TraitToString(traits.introv_extrov, "extroverted", "introverted");
        string serious_funny = TraitToString(traits.serious_funny, "funny", "serious");
        string headonic_asethetic = TraitToString(traits.headonic_asethetic, "ascetic", "headonistic");

        List<string> opinions = new List<string>();
        
        if (nasty_nice != "") {
            opinions.Add(nasty_nice);
        }
        if (introv_extrov != "")
        {
            opinions.Add(introv_extrov);
        }
        if (serious_funny != "")
        {
            opinions.Add(serious_funny);
        }
        if (headonic_asethetic != "")
        {
            opinions.Add(headonic_asethetic);
        }
        
        if (opinions.Count == 0)
        {
            finText = character.name + " has no notable opinion of you.";
        }
        else {
            finText = begining;
            int index = 0;
            foreach (var txt in opinions)
            {
                index++;
                if (opinions.Count == 1 || index == 1)
                {
                    finText += txt;
                }
                else if (index == opinions.Count)
                {
                    finText += " and " + txt;
                }
                else
                {
                    finText += ", " + txt;
                }
            }
            finText += ".";
        }
        tmpTxt.text = finText;
    }
}
