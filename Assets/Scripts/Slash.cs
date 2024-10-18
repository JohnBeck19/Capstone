using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] AnimationClip Animation;
    // Start is called before the first frame update
    void Start()
    {
      
        Destroy(gameObject, Animation.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
