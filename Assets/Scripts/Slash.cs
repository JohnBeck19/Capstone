using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] AnimationClip Animation;
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation =  Quaternion.Euler(0, transform.eulerAngles.y + Random.Range(-30f, 30f), 0);

        Destroy(gameObject, Animation.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(other.gameObject.ToString());
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().death();
            hit = true;
        } 
    }

}
