using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Encounters/Encounter")]
public class O_Encounter : ScriptableObject
{
    public string sceneName;
    public V_Traits minConditions;
    public V_Traits maxConditions;

    public V_Traits pMinConditions;
    public V_Traits pMaxConditions;

    public bool useNN;
    public bool useHA;
    public bool useIE;
    public bool useSF;

    public bool usePNN;
    public bool usePHA;
    public bool usePIE;
    public bool usePSF;

    public string description;
    public Sprite background;
    public List<O_Interest> interestReq;

    [System.Serializable]
    public struct O_Option {
        public string name;
        public O_Response responses;
    }
}
