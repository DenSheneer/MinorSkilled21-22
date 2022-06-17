using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    SceneEvent sceneEvent;

    [SerializeField]
    Object nextScene = null;

    public void Start()
    {

        if (sceneEvent != null)
        {
            sceneEvent.onEnd += onEnd;
        }
    }
    public void TryProgress()
    {
        sceneEvent.Progress();
    }
    private void onEnd()
    {
        if (nextScene != null)
        {
            if (nextScene != null)
            {
                SceneManager.LoadScene(nextScene.name);
            }
        }
    }
}
