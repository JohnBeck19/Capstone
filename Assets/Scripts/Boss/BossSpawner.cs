using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject BossHPBar;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!spawned && other.transform.tag == "Player")
        {
            Boss b = Instantiate(boss, spawnPoint.position, Quaternion.identity);
            b.bossHealthBar = BossHPBar;
            spawned = true;
        } 
    }
}
