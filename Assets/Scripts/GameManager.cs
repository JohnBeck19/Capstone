using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    enum GameState { 
        TITLE, STARTGAME, INGAME, PAUSE, END
    }
    GameState gameState = GameState.TITLE;
    // Start is called before the first frame update
    [SerializeField] Player player;

    //events
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] VoidEvent inGameEvent;


    //UI elements
    [SerializeField] GameObject TabPanel;
    [SerializeField] GameObject TitlePanel;
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
        switch (gameState)
        {
            case GameState.TITLE:

                break;
            case GameState.STARTGAME:
                StartCoroutine(LoadGame());
                gameState = GameState.INGAME;
                break;
            case GameState.INGAME:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    TabPanel.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Tab))
                {
                    TabPanel.SetActive(false);
                }
                break;
            case GameState.PAUSE:
                break;
            case GameState.END:
                break;
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

    public void onPressPlay()
    { 
        TitlePanel.SetActive(false);
        gameState = GameState.STARTGAME;
        gameStartEvent.RaiseEvent();
    }
    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        inGameEvent.RaiseEvent();
    }
}
