using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float lifespan = 5.0f;
    [SerializeField] float bulletSpeed = 15.0f;
    [SerializeField] Player player;
    [SerializeField] Vector3 playerpos;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

        playerpos = player.transform.position + -transform.up;
        transform.position = player.transform.position+Vector3.up*20;
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += -transform.up * bulletSpeed *Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, playerpos, Time.deltaTime * bulletSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.DamagePlayer(50);
            
        }
        Destroy(gameObject);

    }
}
