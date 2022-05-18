using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Encounters/Actor")]
public class O_Actor : ScriptableObject
{
    public V_Traits traits;
    public List<pO_Actor> perceivedOpinions = new List<pO_Actor>();
    public bool male_female;    //Sex is female if true

    private void OnDisable()
    {
        if (perceivedOpinions.Count == 0)
            return;
        perceivedOpinions[0].pTraits.headonic_asethetic = 0;   
        perceivedOpinions[0].pTraits.serious_funny = 0;   
        perceivedOpinions[0].pTraits.nasty_nice = 0;   
        perceivedOpinions[0].pTraits.introv_extrov = 0;
    }

    public void pTraitsAdd(V_Traits traitsAdd) {
        perceivedOpinions[0].pTraits += traitsAdd;
    }

    public V_Traits this[O_Actor act]
    {
        get => perceivedOpinions.Find(x => act == x.target).pTraits;
    }
    [System.Serializable]
    public class pO_Actor {

        public O_Actor target;
        public V_Traits pTraits;
    }
}
