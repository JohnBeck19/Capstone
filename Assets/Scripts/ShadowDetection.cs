using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShadowDetector : MonoBehaviour
{
    public Light[] sun;
    [SerializeField] Volume volume;
    private Vignette vignette;
    private float vignetteIntensity;
    private Color vignetteColor;
    [SerializeField] Color sunColor;
    private Light[] lights;
    
    void Start()
    {
        sun = new Light[1];
        lights = FindObjectsOfType<Light>();
        for (int i = 0; i < lights.Length-1; i++)
        {
            if (lights[i].type == LightType.Directional)
            {
                sun[i] = lights[i];
            }
        }
        volume.profile.TryGet<Vignette>(out vignette);
    }
    void Update()
    {

        if (isInSunlight(sun[0], transform.position)  && isNotInLight(transform.position))
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
            
            
        }
        else
        {
            Debug.Log("is in the light");
            
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
        vignetteIntensity = 0.75f;
        return false;
    }

    bool isNotInLight(Vector3 position)
    {
        foreach (Light light in lights)
        {
            if (light == sun[0]) continue;
            if (Mathf.Abs(Vector3.Distance(light.transform.position, position)) <= light.range)
            {
                float lightDistance = Vector3.Distance(light.transform.position, position);
                Vector3 lightDirection = (light.transform.position - position).normalized;
                Ray ray = new Ray(position, lightDirection);
                Debug.DrawRay(position, lightDirection * 100, Color.magenta);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, lightDistance))
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
