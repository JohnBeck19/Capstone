using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] Transform spawnPoint;
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
            Instantiate(boss, spawnPoint.position, Quaternion.identity);
            spawned = true;
        } 
    }
}
