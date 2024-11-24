using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class spawnFloor : MonoBehaviour
{
    [SerializeField] GameObject[] floors;
    void Start()
    {
        for (int y = -15; y < 15; y++)
        {
            for (int x = -15; x < 15; x++)
            {
                GameObject spawnedTile = Instantiate(floors[0 + Random.Range(0, 4)], new Vector3(x * 5f + transform.position.x, Random.Range(-0.1f, 0.1f), y * 5f + transform.position.z), Quaternion.Euler(0, 90 * Random.Range(0, 2), 0), transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
