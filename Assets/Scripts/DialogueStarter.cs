using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] VoidEvent FreezeGameEvent;
    [SerializeField] GameObject DialogueUI;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] string CharacterName;
    [SerializeField] Sprite characterImage;
    [SerializeField] List<string> messages = new List<string>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DialogueUI != null)
            {
                DialogueUI.SetActive(true);
                FreezeGameEvent.RaiseEvent();
                dialogueManager.characterNameText = CharacterName;
                dialogueManager.messages = messages;
                dialogueManager.characterImage.sprite = characterImage;
            }
            Destroy(this);
        }

    }
    
}
