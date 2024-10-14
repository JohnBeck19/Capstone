using UnityEditor.SearchService;
using UnityEngine;

public class ShadowDetector : MonoBehaviour
{
    public Light sun;
    private Light[] lights;
    void Start()
    {
        lights = FindObjectsOfType<Light>();
    }
    void Update()
    {

        if (isInSunlight(transform.position) && isInLight(transform.position))
        {
            Debug.Log("is in shadow");
        }
        else
        {
            Debug.Log("is in the light");
        }
    }

    bool isInSunlight(Vector3 position)
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

    bool isInLight(Vector3 position)
    {
        foreach (Light light in lights)
        {
            if (light == sun) continue;
            if (Mathf.Abs(Vector3.Distance(light.transform.position, position)) <= light.range)
            {
                Vector3 lightDirection = (light.transform.position - position).normalized;
                Ray ray = new Ray(position, lightDirection);
                Debug.DrawRay(position, lightDirection * 100, Color.magenta);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Vector3.Distance(light.transform.position, position)))
                {
                    return true;
                }
                else { return false; }
            }
        }
        return true;   
    
    }
}
