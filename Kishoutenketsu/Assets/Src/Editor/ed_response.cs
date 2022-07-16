using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public static class EditorGUILayoutUtility
{
    public static readonly Color DEFAULT_COLOR = new Color(0f, 0f, 0f, 0.3f);
    public static readonly Vector2 DEFAULT_LINE_MARGIN = new Vector2(2f, 2f);

    public const float DEFAULT_LINE_HEIGHT = 1f;

    public static void HorizontalLine(Color color, float height, Vector2 margin)
    {
        GUILayout.Space(margin.x);

        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, height), color);

        GUILayout.Space(margin.y);
    }
    public static void HorizontalLine(Color color, float height) => EditorGUILayoutUtility.HorizontalLine(color, height, DEFAULT_LINE_MARGIN);
    public static void HorizontalLine(Color color, Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(color, DEFAULT_LINE_HEIGHT, margin);
    public static void HorizontalLine(float height, Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, height, margin);

    public static void HorizontalLine(Color color) => EditorGUILayoutUtility.HorizontalLine(color, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);
    public static void HorizontalLine(float height) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, height, DEFAULT_LINE_MARGIN);
    public static void HorizontalLine(Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, margin);

    public static void HorizontalLine() => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);

#if UNITY_EDITOR
#endif
}
[CanEditMultipleObjects]
[CustomEditor(typeof(O_Response))]
public class ed_response : Editor
{
    int tab = 0;
    O_Response data = null;
    bool[] itemsDropDown = null;
    bool[] interestDropDown = null;
    bool[] encounterDropDown = null;

    public void DrawSlider(ref float val,string lowVal, string highVal)
    {
        EditorGUILayout.LabelField(lowVal + " " + highVal);
        val = EditorGUILayout.Slider(val, -0.99f, 0.99f);
    }

    public void CreateDropDowns()
    {
        if (itemsDropDown != null)
        {
            bool[] boolsItem = itemsDropDown;
            bool[] boolsInterest = interestDropDown;
            bool[] boolsEncounter = encounterDropDown;
            itemsDropDown = new bool[data.reactionsList.Count];
            System.Array.Copy(boolsItem, itemsDropDown, itemsDropDown.Length);
            interestDropDown = new bool[data.reactionsList.Count];
            System.Array.Copy(boolsInterest, interestDropDown, interestDropDown.Length);
            encounterDropDown = new bool[data.reactionsList.Count];
            System.Array.Copy(boolsEncounter, encounterDropDown, encounterDropDown.Length);
        }
        else
        {
            itemsDropDown = new bool[data.reactionsList.Count];
            interestDropDown = new bool[data.reactionsList.Count];
            encounterDropDown = new bool[data.reactionsList.Count];
        }
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
            
            /*
            if (GUILayout.Button("Convert"))
            {
                if (data.reactionsList.Count == 0)
                {
                    data.reactionsList.AddRange(data.reactions);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            */

            tab = GUILayout.Toolbar(tab, new string[] { "Response", "Raw data" });
            switch (tab)
            {
                case 0:
                    if (data.reactionsList != null)
                    {
                        CreateDropDowns();
                        int index = 0;
                        foreach (var reaction in data.reactionsList)
                        {

                            EditorGUILayout.BeginHorizontal();
                            itemsDropDown[index] = EditorGUILayout.Toggle("",itemsDropDown[index], GUILayout.MinWidth(0.5f), GUILayout.MaxWidth(40.00f));
                            EditorGUILayout.LabelField(reaction.responseDialogue, GUILayout.MinWidth(0.1f), GUILayout.MaxWidth(1900.00f));
                            EditorGUILayout.EndHorizontal();
                            if (itemsDropDown[index])
                            {
                                EditorGUILayout.BeginVertical();
                                EditorGUILayoutUtility.HorizontalLine(Color.grey, 1f);
                                EditorGUILayout.EndVertical();

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
                                EditorGUILayout.Space();

                                EditorGUILayout.BeginHorizontal();
                                interestDropDown[index] = EditorGUILayout.Toggle("", interestDropDown[index], GUILayout.MinWidth(0.5f), GUILayout.MaxWidth(40.00f));
                                EditorGUILayout.LabelField("Interests", GUILayout.MinWidth(0.1f), GUILayout.MaxWidth(1900.00f));
                                EditorGUILayout.EndHorizontal();
                                if (interestDropDown[index])
                                {
                                    for (int i = 0; i < reaction.interests.Count; i++)
                                    {
                                        EditorGUILayout.BeginHorizontal();
                                        reaction.interests[i] = (O_Interest)EditorGUILayout.ObjectField("", reaction.interests[i], typeof(O_Interest), false) as O_Interest;
                                        if (GUILayout.Button("-"))
                                        {
                                            reaction.interests.RemoveAt(i);
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }
                                    if (GUILayout.Button("+ Interest"))
                                    {
                                        reaction.interests.Add(ScriptableObject.CreateInstance<O_Interest>());
                                    }
                                }
                                EditorGUILayout.BeginHorizontal();
                                encounterDropDown[index] = EditorGUILayout.Toggle("", encounterDropDown[index], GUILayout.MinWidth(0.5f), GUILayout.MaxWidth(40.00f));
                                EditorGUILayout.LabelField("Encounters", GUILayout.MinWidth(0.1f), GUILayout.MaxWidth(1900.00f));
                                EditorGUILayout.EndHorizontal();
                                if (encounterDropDown[index])
                                {
                                    reaction.qualify_disqualify = EditorGUILayout.Toggle("Qualify?", reaction.qualify_disqualify, GUILayout.MinWidth(0.5f), GUILayout.MaxWidth(40.00f));
                                    for (int i = 0; i < reaction.encounterReq.Count; i++)
                                    {
                                        EditorGUILayout.BeginHorizontal();
                                        reaction.encounterReq[i] = (O_Encounter)EditorGUILayout.ObjectField("", reaction.encounterReq[i], typeof(O_Encounter), false) as O_Encounter;
                                        
                                        if (GUILayout.Button("-")) {
                                            reaction.encounterReq.RemoveAt(i);
                                        }
                                            EditorGUILayout.EndHorizontal();
                                    }
                                    if (GUILayout.Button("+ Encounter requirement"))
                                    {
                                        reaction.encounterReq.Add(ScriptableObject.CreateInstance<O_Encounter>());
                                    }
                                }
                                EditorGUILayout.BeginVertical();
                                EditorGUILayoutUtility.HorizontalLine(Color.grey, 1f);
                                EditorGUILayout.EndVertical();
                                EditorGUILayout.Separator();
                            }
                           index++;
                        }
                    }
                    if (GUILayout.Button("+")) {
                        data.reactionsList.Add(new O_Response.O_Reactions());
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
