using UnityEngine;

public class ShadowDetector : MonoBehaviour
{
    public Light sun; 


    void Update()
    {
        if (IsInShadow(transform.position))
        {
            Debug.Log("is in shadow");
        }
        else
        {
            Debug.Log("is in the light");
        }
    }

    bool IsInShadow(Vector3 position)
    {
        // Calculate the direction from the object to the light source
        Vector3 lightDirection = -sun.transform.forward;

        // Cast a ray from the object in the direction of the light
        Ray ray = new Ray(position, lightDirection);
        Debug.DrawRay(position, lightDirection*100,Color.yellow);
        RaycastHit hit;

        // Check if the ray hits something before reaching light returns true if something is hit
        if (Physics.Raycast(ray, out hit))
        {
            return true;
        }

        return false;
    }
}
