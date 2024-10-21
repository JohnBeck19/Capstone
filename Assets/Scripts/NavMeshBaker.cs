using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    // Reference to the NavMeshSurface component
    public NavMeshSurface navMeshSurface;

    void Start()
    {

        StartCoroutine(CreateNavMesh());
    }

    public void BakeNavMesh()
    {
        // This will clear the old NavMesh and bake a new one
        navMeshSurface.BuildNavMesh();
        Debug.Log("NavMesh baked at runtime.");
    }

    // Call this method if you need to rebake the NavMesh dynamically (e.g., after terrain changes)
    public void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
        Debug.Log("NavMesh rebuilt.");
    }
    void Update()
    {
        // Example of triggering a bake dynamically (e.g., when pressing spacebar)
        if (Input.GetKeyDown(KeyCode.B))
        {
            RebuildNavMesh(); // Rebuild the NavMesh during gameplay
        }
    }

    public IEnumerator CreateNavMesh()
    {
        yield return new WaitForSeconds(1f);
        RebuildNavMesh();

    }

}
