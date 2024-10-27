using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;

    //UI elements
    [SerializeField] Slider healthBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdater();
    }
    void UIUpdater()
    {
        healthBar.value = player.health / player.maxHealth;
    
    }
}
