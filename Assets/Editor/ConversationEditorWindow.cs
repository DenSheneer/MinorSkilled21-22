using UnityEditor;
using UnityEngine;

public class ConversationEditorWindow : ExtendedEditorWindow
{
    public static void Open(ConversationData data)
    {
        ConversationEditorWindow window = GetWindow<ConversationEditorWindow>("Conversation Editor");
        window.serializedObject = new SerializedObject(data);
    }
    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("lines");
        if (currentProperty != null)
        {
            DrawProperties(currentProperty, true);
        }
        else
        {
            Debug.Log("was null");
            Debug.Log("was null. " + currentProperty.ToString());
        }
    }
}

