using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] VoidEvent FreezeGameEvent;
    [SerializeField] GameObject DialogueUI;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] string CharacterName;
    [SerializeField] List<string> messages = new List<string>();


    private void OnTriggerEnter(Collider other)
    {
        if (DialogueUI != null)
        {
            DialogueUI.SetActive(true);
            FreezeGameEvent.RaiseEvent();
            dialogueManager.characterNameText = CharacterName;
            dialogueManager.messages = messages;
        }
        Destroy(this);
    }
    
}
