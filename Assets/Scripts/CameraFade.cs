using System.Linq;
using UnityEngine;

public class CameraObstructionFader : MonoBehaviour
{
    public Transform player; // The player's transform
    public int layerMask;
    private Material[] originalMaterials;
    private Renderer[] obstructionRenderers;
    [SerializeField] [Range(0f,1.0f)] float transparency;
    private float fade = 1.0f;
    private bool isFading = false;
    [SerializeField] float fadeSpeed = 1.0f;

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;


        if (obstructionRenderers != null)
        {
            for (int i = 0; i < obstructionRenderers.Length; i++)
            {
                if (obstructionRenderers[i] != null)
                {
                    SetObjectTransparent(obstructionRenderers[i],1.0f);
                   
                }
            }
        }


        RaycastHit[] hits1 = Physics.RaycastAll(transform.position, directionToPlayer, directionToPlayer.magnitude, layerMask);
        RaycastHit[] hits2 = Physics.RaycastAll(transform.position, Vector3.down, 100, layerMask);

        Debug.DrawLine(transform.position, player.position, Color.green);
        Debug.DrawRay(transform.position, Vector3.down*50, Color.blue);

        RaycastHit[] combinedHits = new RaycastHit[hits1.Length + hits2.Length];
        hits1.CopyTo(combinedHits, 0);
        hits2.CopyTo(combinedHits, hits1.Length);

        processHits(combinedHits);
        //if (hits2.Length > 0) processHits(hits2);
    }
    private void processHits(RaycastHit[] hits)
    {
        if (hits.Length > 0)
        {
            obstructionRenderers = new Renderer[hits.Length];
            originalMaterials = new Material[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                Renderer renderer = hits[i].collider.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    obstructionRenderers[i] = renderer;
                    originalMaterials[i] = renderer.material;
                    isFading = true;
                    SetObjectTransparent(renderer, fade);
                }
            }

            if (isFading == true)
            {
                fade -= Time.deltaTime * fadeSpeed;
                if (fade <= transparency) fade = transparency;
                for (int i = 0; i < obstructionRenderers.Length; i++)
                {
                    if (obstructionRenderers[i] != null)
                    {
                        SetObjectTransparent(obstructionRenderers[i], fade);
                    }
                }
            }
        }
        else
        {
            fade = 1.0f;
            isFading = false;
        }
    }
    // Function to set the object to transparent using the custom color shader
    private void SetObjectTransparent(Renderer renderer, float transparency)
    {
        // Assuming the object uses a material with the custom color shader
        Material material = renderer.material;
        material.SetFloat("_Transparency", transparency); // Set alpha to 30% transparent
    }



}
