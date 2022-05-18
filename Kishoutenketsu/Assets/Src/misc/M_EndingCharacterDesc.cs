using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class M_EndingCharacterDesc : MonoBehaviour
{
    public Text text;
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
        string headonic_asethetic = TraitToString(traits.headonic_asethetic, "asethetic", "headonistic");

        if (nasty_nice == "" &&
            introv_extrov == "" &&
            serious_funny == "" &&
            headonic_asethetic == "")
        {
            text.text = character.name + " has no notable opinion of you.";
        }
        else {
            text.text = begining;
            if (nasty_nice != "") {
                text.text += nasty_nice;
            }
            if (introv_extrov != "")
            {
                text.text += ", " + introv_extrov;
            }
            if (serious_funny != "")
            {
                text.text += ", " + serious_funny;
            }
            if (headonic_asethetic != "")
            {
                text.text += ", " + headonic_asethetic;
            }
            text.text += ".";
        }

    }
}
