using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    GameObject multipleChoicePanel;

    [SerializeField]
    TextMeshProUGUI text_a, text_b;

    [SerializeField]
    Button button_a, button_b;

    [SerializeField]
    TextMeshProUGUI actorNameObject, dialogueTextObject;

    [SerializeField]
    DialogueContainer dialogueObject;

    private Dictionary<string, DialogueNodeData> nodeCache;

    bool waitingForInput = false;
    DialogueNodeData currentNode = null;
    DialogueNodeData nextNode = null;

    public void Start()
    {
        if (dialogueObject != null)
        {
            if (dialogueObject.DialogueNodeData.Count > 0)
            {
                generateCache();
                loadDialogue(dialogueObject.DialogueNodeData[0]);
            }
        }
        else
        {
            Debug.Log("DialogueObject was null! Check the reference in the inspector.");
        }
    }

    private void generateCache()
    {
        nodeCache = new Dictionary<string, DialogueNodeData>();
        foreach (var node in dialogueObject.DialogueNodeData)
        {
            nodeCache.Add(node.Guid, node);
        }
    }

    public void Update()
    {
        if (!waitingForInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                loadDialogue(nextNode);
            }
        }
    }

    private void loadDialogue(DialogueNodeData node)
    {
        if (node != null)
        {
            currentNode = node;
            showNode(currentNode);

            var next = findNextNode();
            if (next.Count > 0)
            {
                if (next.Count == 1)
                {
                    nextNode = next[0];
                }
                else
                {
                    waitingForInput = true;
                    showChoices(next);
                }
            }
            else
            {
                nextNode = null;
                Debug.Log("End of the graph reached.");
            }
        }
    }
    private List<DialogueNodeData> findNextNode()
    {
        var connections = dialogueObject.NodeLinks.Where(x => x.BaseNodeGuid == currentNode.Guid).ToList();
        List<DialogueNodeData> nextNodes = new List<DialogueNodeData>();

        if (connections.Count > 0)
        {
            foreach (var connection in connections)
            {
                if (nodeCache.ContainsKey(connection.TargetNodeGuid))
                {
                    nextNodes.Add(nodeCache[connection.TargetNodeGuid]);
                }
            }
        }
        return nextNodes;
    }
    private void showChoices(List<DialogueNodeData> options)
    {
        if (multipleChoicePanel != null)
        {
            if (options.Count > 1)
            {
                multipleChoicePanel.SetActive(true);
                if (options.Count > 2)
                {
                    Debug.Log("Warning! This dialogue graph had >2 options for this branch.\n " +
                        "       This game was meant to have a max of 2. The remaining options will not be displayed.");
                }

                text_a.text = options[0].DialogueText;
                text_b.text = options[1].DialogueText;

                button_a.onClick.AddListener(() => loadDialogue(options[0]));
                button_a.onClick.AddListener(() => resetMultipleChoicePanel());

                button_b.onClick.AddListener(() => loadDialogue(options[1]));
                button_b.onClick.AddListener(() => resetMultipleChoicePanel());
            }
            else
            {
                Debug.Log("Warning! Trying to display 1 choice as a multiple choice branch");
            }
        }
    }
    void resetMultipleChoicePanel()
    {
        waitingForInput = false;
        multipleChoicePanel.SetActive(false);
        button_a.onClick.RemoveAllListeners();
        button_b.onClick.RemoveAllListeners();
    }
    private void showNode(DialogueNodeData node)
    {
        if (node != null)
        {
            if (actorNameObject != null)
            {
                if (node.Actor != null)
                    actorNameObject.text = node.Actor.name;
                else
                    actorNameObject.text = string.Empty;
            }
            if (dialogueTextObject != null)
                dialogueTextObject.text = node.DialogueText;
        }
    }
}
