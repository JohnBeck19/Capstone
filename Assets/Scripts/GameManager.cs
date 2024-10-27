using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;


    //UI elements
    [SerializeField] GameObject TabPanel;
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text atkSpeedText;
    [SerializeField] TMP_Text HealthRegenText;
    [SerializeField] TMP_Text maxHealthText;
    [SerializeField] TMP_Text DefenseText;
    void Start()
    {
        TabPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabPanel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            TabPanel.SetActive(false);
        }
        UIUpdater();
    }
    void UIUpdater()
    {
        healthBar.value = player.health / player.maxHealth;
        speedText.text = "Speed: " + player.speed;
        atkSpeedText.text = "Attack Speed: " + player.atkSpeed;
        HealthRegenText.text = "Health Regen: " + player.healthRegen;
        maxHealthText.text = "Max Health: " + player.maxHealth;
        DefenseText.text = "Defense: " + player.defense;
    }
}
