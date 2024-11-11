using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    private Vector3 target; // Target point to move towards
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    //[SerializeField] float radius = 10f; // Radius within which to find a random point
    void Start()
    {
        // Get the NavMeshAgent component attached to the GameObject
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target);
        
    }

    void Update()
    {

    }
    public void stop()
    {
        agent.isStopped = true;
    }

}

