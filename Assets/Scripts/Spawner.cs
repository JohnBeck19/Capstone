using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] NavMeshBaker NavMeshBaker;
    [SerializeField] float timeBetweenSpawn = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }


    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        StartCoroutine(SpawnTimer());
        Spawn();
    }
    void Spawn()
    {
        Vector3 Spawnpoint = GetRandomPointOnNavMesh();
        //Debug.Log(Spawnpoint.ToString());
        Instantiate(enemies[0], Spawnpoint, Quaternion.identity);
    }
    Vector3 GetRandomPointOnNavMesh()
    {
        // Generate a random point within a sphere of a specified radius
        Vector3 randomPoint = new Vector3(Random.Range(0, 1000), 2, Random.Range(0, 1000));
        randomPoint.y = 2;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas))
        {
            // Debug.Log(hit.position.ToString());
            return new Vector3(hit.position.x, 1.8f, hit.position.z); // Return the valid NavMesh point
        }
        return GetRandomPointOnNavMesh();
    }
}
