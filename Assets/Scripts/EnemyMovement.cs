using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 target; // Target point to move towards
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    //[SerializeField] float radius = 10f; // Radius within which to find a random point
    void Start()
    {
        // Get the NavMeshAgent component attached to the GameObject
        agent = GetComponent<NavMeshAgent>();
        target = GetRandomPointOnNavMesh();
        agent.SetDestination(target);
        
    }

    void Update()
    {
        // Optional: Check if the AI has reached the target
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            target = GetRandomPointOnNavMesh();
            agent.SetDestination(target);
            Debug.Log("Reached target!");
        }
    }
    public void stop()
    {
        agent.isStopped = true;
    }
    Vector3 GetRandomPointOnNavMesh()
    {
        // Generate a random point within a sphere of a specified radius
        Vector3 randomPoint = new Vector3(Random.Range(0, 1000), 2, Random.Range(0, 1000));
        randomPoint.y = transform.position.y;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas))
        {

            return new Vector3(hit.position.x, 0, hit.position.z); // Return the valid NavMesh point
        }
        return GetRandomPointOnNavMesh();
    }
}

