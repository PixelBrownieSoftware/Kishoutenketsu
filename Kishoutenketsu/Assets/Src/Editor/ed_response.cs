using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(O_Response))]
public class ed_response : Editor
{
    int tab = 0;
    O_Response data = null;

    public void DrawSlider(ref float val,string lowVal, string highVal)
    {
        EditorGUILayout.LabelField(lowVal + " " + highVal);
        val = EditorGUILayout.Slider(val, -0.99f, 0.99f);
    }

    public override void OnInspectorGUI()
    {
        data = (O_Response)target;
        if (data != null)
        {
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            tab = GUILayout.Toolbar(tab, new string[] { "Response", "Raw data" });
            switch (tab)
            {
                case 0:
                    if (data.reactions != null)
                    {
                        foreach (var reaction in data.reactions)
                        {
                            EditorGUILayout.Separator();
                            EditorGUILayout.LabelField("Response");
                            reaction.responseDialogue = EditorGUILayout.TextArea(reaction.responseDialogue);
                            EditorGUILayout.BeginHorizontal();
                            reaction.Val1.inverseVal = EditorGUILayout.Toggle(reaction.Val1.inverseVal);
                            reaction.Val1.trait = (PERSONALITY_TRAITS)EditorGUILayout.EnumPopup(reaction.Val1.trait);
                            EditorGUILayout.Separator();
                            reaction.Val2.inverseVal = EditorGUILayout.Toggle(reaction.Val2.inverseVal);
                            reaction.Val2.trait = (PERSONALITY_TRAITS)EditorGUILayout.EnumPopup(reaction.Val2.trait);
                            EditorGUILayout.EndHorizontal();
                            reaction.weight = EditorGUILayout.Slider(reaction.weight, -0.99f, 0.99f);
                            EditorGUILayout.Space();
                            DrawSlider(ref reaction.traitsChange.nasty_nice, "Nasty", "Nice");
                            DrawSlider(ref reaction.traitsChange.introv_extrov, "Introverted", "Extroverted");
                            DrawSlider(ref reaction.traitsChange.serious_funny, "Serious", "Funny");
                            DrawSlider(ref reaction.traitsChange.headonic_asethetic, "Hedonistic", "Ascetic");
                            EditorGUILayout.Separator();
                        }
                    }
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
