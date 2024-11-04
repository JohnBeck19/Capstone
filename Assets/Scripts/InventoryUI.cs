using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject ItemUIBase;
    [SerializeField] GameObject Description;
    [SerializeField] TMP_Text DescriptionText;
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
                created.GetComponentInChildren<Button>().onClick.AddListener(() => onInvItemPressed(created));
                created.GetComponentInChildren<TMP_Text>().text = item.Name;
                created.GetComponentsInChildren<Image>()[1].sprite = item.Icon;
                createdItems.Add(item);
            }
        }
    }
    public void onInvItemPressed(GameObject a)
    {
        foreach (Item item in player.items) 
        {
            
            if (a.GetComponentInChildren<TMP_Text>().text == item.Name)
            {
                Description.SetActive(true);
                DescriptionText.text = item.Description;
            }
        }


    }
}
