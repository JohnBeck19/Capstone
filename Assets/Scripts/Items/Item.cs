using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }
    public abstract void Use(Player player);


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            other.GetComponent<Player>().Equip(this);
            this.GetComponentInParent<SpriteRenderer>().enabled = false;
            this.GetComponentInParent<SphereCollider>().enabled = false;
        }
    }
}
