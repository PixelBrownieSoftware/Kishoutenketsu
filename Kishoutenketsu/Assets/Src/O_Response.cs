using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Encounters/Responses")]
public class O_Response : ScriptableObject
{
    public bool qualify_disqualify = true;
    public List<O_Encounter> encounterReq;
    //public O_Reactions[] reactions;
    public List<O_Reactions> reactionsList = new List<O_Reactions>();

    [System.Serializable]
    public class O_Reactions_Cond {
        public bool inverseVal;
        public PERSONALITY_TRAITS trait;
    }
    [System.Serializable]
    public class O_Reactions
    {
        public List<O_Encounter> encounterReq = new List<O_Encounter>();
        public bool qualify_disqualify = true;
        public string responseDialogue;
        public V_Traits traitsChange;
        public O_Reactions_Cond Val1;
        public O_Reactions_Cond Val2;
        public float weight;
        public List<O_Interest> interests = new List<O_Interest>();
    }
}
