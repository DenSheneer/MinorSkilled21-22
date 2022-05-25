using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceID, int line)
    {
        ConversationData obj = EditorUtility.InstanceIDToObject(instanceID) as ConversationData;
        if (obj != null)
        {
            ConversationEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}


//[CustomEditor(typeof(ConversationData))]
//public class ConversationCustomEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        if (GUILayout.Button("Open Editor"))
//            ConversationEditorWindow.Open((ConversationData)target);
//    }
//}
