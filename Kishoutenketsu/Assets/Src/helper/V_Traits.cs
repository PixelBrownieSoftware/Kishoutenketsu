using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class V_Traits
{
    public static V_Traits operator+ (V_Traits a, V_Traits b) {
        V_Traits trait = new V_Traits();
        trait.headonic_asethetic = S_BMaths.BSum(a.headonic_asethetic, b.headonic_asethetic);
        trait.introv_extrov = S_BMaths.BSum(a.introv_extrov, b.introv_extrov);
        trait.nasty_nice = S_BMaths.BSum(a.nasty_nice, b.nasty_nice);
        trait.serious_funny = S_BMaths.BSum(a.serious_funny, b.serious_funny);
        return trait;
    }

    public float nasty_nice;
    public float introv_extrov;
    public float serious_funny;
    public float headonic_asethetic;
}

public enum PERSONALITY_TRAITS {
    NASTY_NICE,
    INTROV_EXTROV,
    SERIOUS_FUNNY,
    HEADONIC_AESETIC,

    pNASTY_NICE,
    pINTROV_EXTROV,
    pSERIOUS_FUNNY,
    pHEADONIC_AESETIC,
}