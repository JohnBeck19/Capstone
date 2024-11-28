using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject ItemUIBase;
    [SerializeField] GameObject Description;
    [SerializeField] TMP_Text DescriptionText;
    [SerializeField] AudioSource gameAudioSource;
    [SerializeField] AudioClip UIClick;
    [SerializeField] AudioClip UIHover;
    private List<Item> createdItems;
    private GameObject created;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();

        createdItems = new List<Item>();
        Description.SetActive(false);
    }
    void Update()
    {
        foreach (Item item in player.items)
        {
            if (!createdItems.Contains(item))
            {
                created = Instantiate(ItemUIBase, this.transform);
                Item cItem = item;
                created.GetComponentInChildren<Button>().onClick.AddListener(() => onInvItemPressed(cItem));
                created.GetComponentInChildren<TMP_Text>().text = item.Name;
                created.GetComponentsInChildren<Image>()[1].sprite = item.Icon;
                EventTrigger eventTrigger = created.GetComponentsInChildren<EventTrigger>()[0];
                eventTrigger.AddListener(EventTriggerType.PointerEnter, (data) => onHover());
                createdItems.Add(item);
            }
        }
    }

    private void onHover()
    {
        gameAudioSource.PlayOneShot(UIHover);
        
    }

    public void onInvItemPressed(Item item)
    {
        Description.SetActive(true);
        gameAudioSource.PlayOneShot(UIClick);
        DescriptionText.text = item.Description;
    }

}
