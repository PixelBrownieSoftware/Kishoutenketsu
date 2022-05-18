using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "System/Global")]
public class S_Globals : ScriptableObject
{
    public O_Actor Player;
    public O_Actor currentActor;
    public List<O_Actor> others = new List<O_Actor>();

    public List<O_Encounter> encounters = new List<O_Encounter>();
    public List<O_Response> responses = new List<O_Response>();

    public void SetRandomActor() {
        currentActor = others[Random.Range(0, others.Count)];
    }

    public O_Encounter SetEncounters() {
        List<O_Encounter> accessibleEncounters = new List<O_Encounter>();
        foreach (var enc in encounters) {
            bool checkIfCondFufilled(float num, float numMin, float numMax) {
                if (num >= numMin && num <= numMax)
                {
                    return true;
                }
                return false;
            }


            if (enc.useHA) {
                if (!checkIfCondFufilled(currentActor.traits.headonic_asethetic, enc.minConditions.headonic_asethetic, enc.maxConditions.headonic_asethetic))
                    continue;
            }
            if (enc.useIE)
            {
                if (!checkIfCondFufilled(currentActor.traits.introv_extrov, enc.minConditions.introv_extrov, enc.maxConditions.introv_extrov))
                    continue;
            }
            if (enc.useNN)
            {
                if (!checkIfCondFufilled(currentActor.traits.nasty_nice, enc.minConditions.nasty_nice, enc.maxConditions.nasty_nice))
                    continue;
            }
            if (enc.useSF)
            {
                if (!checkIfCondFufilled(currentActor.traits.serious_funny, enc.minConditions.serious_funny, enc.maxConditions.serious_funny))
                    continue;
            }

            if (enc.useHA)
            {
                if (!checkIfCondFufilled(currentActor.perceivedOpinions[0].pTraits.headonic_asethetic, enc.pMinConditions.headonic_asethetic, enc.pMaxConditions.headonic_asethetic))
                    continue;
            }
            if (enc.useIE)
            {
                if (!checkIfCondFufilled(currentActor.perceivedOpinions[0].pTraits.introv_extrov, enc.pMinConditions.introv_extrov, enc.pMaxConditions.introv_extrov))
                    continue;
            }
            if (enc.useNN)
            {
                if (!checkIfCondFufilled(currentActor.perceivedOpinions[0].pTraits.nasty_nice, enc.pMinConditions.nasty_nice, enc.pMaxConditions.nasty_nice))
                    continue;
            }
            if (enc.useSF)
            {
                if (!checkIfCondFufilled(currentActor.perceivedOpinions[0].pTraits.serious_funny, enc.pMinConditions.serious_funny, enc.pMaxConditions.serious_funny))
                    continue;
            }
            accessibleEncounters.Add(enc);
        }
        return accessibleEncounters[Random.Range(0, accessibleEncounters.Count)];
    }
}
