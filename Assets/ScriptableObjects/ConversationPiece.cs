using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class ConversationPiece
{
    [SerializeField]
    StoryCharacter character;

    [TextArea(5, 20)][SerializeField]
    protected string text;
}
