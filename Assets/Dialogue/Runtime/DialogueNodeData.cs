using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNodeData
{
    public string Guid;
    public string DialogueText;
    public DialogueActor Actor;
    public bool FirstNode;
    public Vector2 Position;

}
