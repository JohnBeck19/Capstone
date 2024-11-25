using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShadowDetector : MonoBehaviour
{
    public Light sun;
    [SerializeField] Volume volume;
    private Vignette vignette;
    private float vignetteIntensity;
    private Color vignetteColor;
    [SerializeField] Color sunColor;
    private Light[] lights;
    [SerializeField] Player player;
    
    void Start()
    {
        
        lights = FindObjectsOfType<Light>();
        volume.profile.TryGet<Vignette>(out vignette);
    }
    void Update()
    {



        if (player.active) { 
            if (isInSunlight(sun, transform.position) && isNotInLight(transform.position))
            {
                Debug.Log("is in shadow");
                if (vignette.intensity.value > 0.0f)
                {
                    vignette.intensity.value -= Time.deltaTime;
                }
                else
                {
                    vignette.intensity.value = 0.0f;
                }
                player.inLight = false;
            }
            else
            {
                Debug.Log("is in the light");
                player.inLight = true;
                player.DamagePlayerNoDefense(0.1f);
                vignette.color.value = vignetteColor;
                if (Mathf.Abs(vignette.intensity.value - vignetteIntensity) < 0.05f)
                {
                    vignette.intensity.value = vignetteIntensity;
                }
                else if (vignette.intensity.value > vignetteIntensity)
                {
                    vignette.intensity.value -= Time.deltaTime;
                }
                else if (vignette.intensity.value < vignetteIntensity)
                {
                    vignette.intensity.value += Time.deltaTime;
                }

            }
        }
    }

    bool isInSunlight(Light s, Vector3 position)
    {
        // Calculate the direction from the object to the light source
        Vector3 lightDirection = -s.transform.forward;
        
        // Cast a ray from the object in the direction of the light
        Ray ray = new Ray(position, lightDirection);
        Debug.DrawRay(position, lightDirection*100,Color.yellow);
        RaycastHit hit;

        // Check if the ray hits something before reaching light returns true if something is hit
        if (Physics.Raycast(ray, out hit))
        {
            return true;
        }
        vignetteColor = sunColor;
        vignetteIntensity = 0.5f;
        return false;
    }

    bool isNotInLight(Vector3 position)
    {
        LayerMask layerMask = ~LayerMask.GetMask("Enemy");
        lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light == sun) continue;
            if (Mathf.Abs(Vector3.Distance(light.transform.position, position)) <= light.range)
            {
                float lightDistance = Vector3.Distance(light.transform.position, position);
                Vector3 lightDirection = (light.transform.position - position).normalized;
                Ray ray = new Ray(position, lightDirection);
                Debug.DrawRay(position, lightDirection * 100, Color.magenta);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit, lightDistance,layerMask))
                {
                    return true;
                }
                else {
                    vignetteColor = light.color;
                    vignetteIntensity = 1/lightDistance +0.2f;
                    return false; }
            }
        }
        return true;   
    
    }
}
