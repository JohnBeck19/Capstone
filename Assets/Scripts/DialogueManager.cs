using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public GameObject dialogue;
    [SerializeField] public TMP_Text characterName;
    [SerializeField] public string characterNameText;
    [SerializeField] public TMP_Text message;
    [SerializeField] public List<string> messages;
    [SerializeField] VoidEvent UnfreezeGameEvent;
    private int msgCount = 0;
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        characterName.text = characterNameText;
        if (messages.Count > msgCount)
        {
            if (Input.GetMouseButtonDown(0) || msgCount == 0)
            {
                message.text = messages[msgCount];
                msgCount++;

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogue.SetActive(false);
                UnfreezeGameEvent.RaiseEvent();
            }
        }
    }
}
