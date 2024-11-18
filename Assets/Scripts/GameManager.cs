using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    enum GameState { 
        TITLE, STARTGAME, INGAME, PAUSE, GAMEOVER, SHOP
    }
    GameState gameState = GameState.TITLE;
    // Start is called before the first frame update
    [SerializeField] Player player;

    //events
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] VoidEvent inGameEvent;
    [SerializeField] VoidEvent PlayerDeadEvent;


    //UI elements
    [SerializeField] GameObject TabPanel;
    [SerializeField] GameObject TitlePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text atkSpeedText;
    [SerializeField] TMP_Text atkDamageText;
    [SerializeField] TMP_Text HealthRegenText;
    [SerializeField] TMP_Text maxHealthText;
    [SerializeField] TMP_Text DefenseText;
    [SerializeField] TMP_Text SoulsText;
    [SerializeField] TMP_Text SoulsText2;
    void Start()
    {
        TabPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PlayerDeadEvent.Subscribe(onPlayerDead);
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
                    DescriptionPanel.SetActive(false);
                    
                }
                break;
            case GameState.PAUSE:
                break;
            case GameState.GAMEOVER:
                break;
            case GameState.SHOP:
                
                break;

        }

        UIUpdater();
    }
    void UIUpdater()
    {
        healthBar.value = player.health / player.maxHealth;
        speedText.text = "Speed: " + player.speed;
        atkSpeedText.text = "Attack Speed: " + player.atkSpeed;
        atkDamageText.text = "Attack Damage: " + player.atkDamage;
        HealthRegenText.text = "Health Regen: " + player.healthRegen;
        maxHealthText.text = "Max Health: " + player.maxHealth;
        DefenseText.text = "Defense: " + player.defense;
        SoulsText.text = ""+player.souls;
        SoulsText2.text = ""+player.souls;
    }

    public void onPressPlay()
    { 
        TitlePanel.SetActive(false);
        gameState = GameState.STARTGAME;
        gameStartEvent.RaiseEvent();
    }
    private IEnumerator LoadGame()
    {
        
        inGameEvent.RaiseEvent();
        yield return new WaitForSeconds(1f);
    }

    private void onPlayerDead()
    {
        gameState = GameState.GAMEOVER;
        GameOverPanel.SetActive(true);
    }
}
