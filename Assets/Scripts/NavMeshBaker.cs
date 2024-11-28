using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    // Reference to the NavMeshSurface component
    public NavMeshSurface navMeshSurface;
    [SerializeField] VoidEvent gameStartEvent;

    void Start()
    {
        gameStartEvent.Subscribe(onStartGame);
    }
    private void onStartGame()
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
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    RebuildNavMesh(); // Rebuild the NavMesh during gameplay
        //}
    }

    public IEnumerator CreateNavMesh()
    {
        RebuildNavMesh();
        yield return new WaitForSeconds(0.001f);
        

    }

}
