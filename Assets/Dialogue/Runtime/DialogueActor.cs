using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DialogueActor : ScriptableObject
{
    public string ActorName;

    [MenuItem("Visual Novel/Create new actor...")]
    public static void CreateNewActor()
    {
        var newActor = CreateInstance<DialogueActor>();
        EditorUtility.SetDirty(newActor);

        if (!AssetDatabase.IsValidFolder("Assets/Resources/Actors"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Actors");
        }
        AssetDatabase.CreateAsset(newActor, "Assets/Resources/Actors/New actor.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newActor;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(GetInstanceID());
        EditorUtility.SetDirty(this);
        if (!string.IsNullOrEmpty(ActorName))
        {
            AssetDatabase.RenameAsset(path, ActorName);
        }
        EditorUtility.ClearDirty(this);
    }
#endif
}
