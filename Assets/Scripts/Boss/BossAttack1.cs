using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float lifespan = 5.0f;
    [SerializeField] float bulletSpeed = 15.0f;
    [SerializeField] Player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

        transform.LookAt(player.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed *Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.DamagePlayer(50);
            Destroy(gameObject);
        }
        

    }
}
