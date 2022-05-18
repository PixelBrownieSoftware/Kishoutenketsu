using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(O_Encounter))]
public class ed_encounter : Editor
{
    int tab = 0;
    O_Encounter data = null;
    
    public void DrawNumberLines(ref float minVal, ref float maxVal, ref bool _enabled, string lowVal, string highVal) {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(lowVal + " " + highVal);
        _enabled = EditorGUILayout.Toggle(_enabled);
        if (_enabled)
        {
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField(minVal.ToString("0.00") + " to " + maxVal.ToString("0.00"));
            EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, -0.99f, 0.99f);
        }
        else
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    public override void OnInspectorGUI()
    {
        data = (O_Encounter)target;
        if (data != null)
        {
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            tab = GUILayout.Toolbar(tab, new string[] { "Encounter", "Raw data"});
            switch (tab)
            {
                case 0:
                    EditorGUILayout.LabelField("Description");
                    data.description = EditorGUILayout.TextArea(data.description);
                    EditorGUILayout.LabelField("Conditions");
                    DrawNumberLines(
                        ref data.minConditions.nasty_nice,
                        ref data.maxConditions.nasty_nice,
                        ref data.useNN,
                        "Nasty",
                        "Nice");
                    DrawNumberLines(
                        ref data.minConditions.introv_extrov,
                        ref data.maxConditions.introv_extrov,
                        ref data.useIE,
                        "Introverted",
                        "Extroverted");
                    DrawNumberLines(
                        ref data.minConditions.serious_funny,
                        ref data.maxConditions.serious_funny,
                        ref data.useSF,
                        "Serious",
                        "Funny");
                    DrawNumberLines(
                        ref data.minConditions.headonic_asethetic,
                        ref data.maxConditions.headonic_asethetic, 
                        ref data.useHA,
                        "Hedonistic",
                        "Ascetic");
                    
                    DrawNumberLines(
                        ref data.pMinConditions.nasty_nice,
                        ref data.pMaxConditions.nasty_nice,
                        ref data.usePNN,
                        "pNasty",
                        "pNice");
                    DrawNumberLines(
                        ref data.pMinConditions.introv_extrov,
                        ref data.pMaxConditions.introv_extrov,
                        ref data.usePIE,
                        "pIntroverted",
                        "pExtroverted");
                    DrawNumberLines(
                        ref data.pMinConditions.serious_funny,
                        ref data.pMaxConditions.serious_funny,
                        ref data.usePSF,
                        "pSerious",
                        "pFunny");
                    DrawNumberLines(
                        ref data.pMinConditions.headonic_asethetic,
                        ref data.pMaxConditions.headonic_asethetic,
                        ref data.usePHA,
                        "pHedonistic",
                        "pAscetic");
                    break;

                case 1:
                    base.OnInspectorGUI();
                    break;
            }


            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
    }
}