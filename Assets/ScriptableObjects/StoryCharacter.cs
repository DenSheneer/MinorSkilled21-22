using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "StoryCharacter", menuName = "Visual novel/Create new story character", order = 1)]
public class StoryCharacter : ScriptableObject
{
    [SerializeField]
    string characterName;
}
