using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    GameObject multipleChoicePanel;

    [SerializeField]
    TextMeshProUGUI text_a, text_b;

    [SerializeField]
    Button button_a, button_b;

    [SerializeField]
    TextMeshProUGUI actorName, dialogueText;

    [SerializeField]
    DialogueContainer dialogueObject;

    DialogueNodeData currentNode = null;
    bool waitingForInput = false;

    public void Start()
    {
        if (dialogueObject != null)
        {
            loadDialogue(dialogueObject.DialogueNodeData[0]);
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!waitingForInput)
            {
                findNextNode();
            }
        }
    }

    private void loadDialogue(DialogueNodeData node)
    {
        if (node != null)
        {
            currentNode = node;
            updateViewModel(currentNode);
        }
    }
    private void findNextNode()
    {
        var connections = dialogueObject.NodeLinks.Where(x => x.BaseNodeGuid == currentNode.Guid).ToList();

        if (connections.Count > 1)
        {
            List<DialogueNodeData> nodeChoices = new List<DialogueNodeData>();
            foreach (var connection in connections)
            {
                //nodeChoices = dialogueObject.DialogueNodeData.Where(x => x.Guid == connection.TargetNodeGuid).ToList();

                foreach (var node in dialogueObject.DialogueNodeData)
                {
                    //Debug.Log("comparing " + node.Guid + " to: " + connection.TargetNodeGuid);
                    if (node.Guid == connection.TargetNodeGuid)
                    {
                        nodeChoices.Add(node);
                    }
                }
            }
            showMultipleChoiceWindow(nodeChoices);
        }
        else
        {
            //  to do: clean this up.
            foreach (var connection in connections)
            {
                foreach (var node in dialogueObject.DialogueNodeData)
                {
                    if (node.Guid == connection.TargetNodeGuid)
                    {
                        updateViewModel(node);
                        break;
                    }
                }
            }
        }
    }
    private void showMultipleChoiceWindow(List<DialogueNodeData> options)
    {
        if (multipleChoicePanel != null)
        {
            //Debug.Log(options.Count + " choices found");
            waitingForInput = true;
            multipleChoicePanel.SetActive(true);

            text_a.text = options[0].DialogueText;
            text_b.text = options[1].DialogueText;

            button_a.onClick.AddListener(() => loadDialogue(options[0]));
            button_a.onClick.AddListener(() => multipleChoicePanel.SetActive(false));
            button_a.onClick.AddListener(() => waitingForInput = false);

            button_b.onClick.AddListener(() => loadDialogue(options[1]));
            button_b.onClick.AddListener(() => multipleChoicePanel.SetActive(false));
            button_b.onClick.AddListener(() => waitingForInput = false);
        }
    }
    private void testThing()
    {
        Debug.Log("option 1");
    }
    private void updateViewModel(DialogueNodeData node)
    {
        if (actorName != null)
        {
            if (node.Actor != null)
            {
                actorName.text = node.Actor.name;
            }
        }
        if (dialogueText != null)
        {
            dialogueText.text = node.DialogueText;
        }
    }
}
