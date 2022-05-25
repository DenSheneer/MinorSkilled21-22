using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : ConversationPiece
{
    [SerializeField]
    List<string> responses = new List<string>();

    public void chooseResponse(int index)
    {
        if (responses[index] != null)
        {
            text = responses[index];
        }
    }
}
