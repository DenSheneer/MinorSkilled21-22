using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Visual novel/Create new conversation", order = 1)]
[System.Serializable]
public class ConversationData : ScriptableObject
{
    [SerializeField]
    List<ConversationPiece> lines = new List<ConversationPiece>();
}
