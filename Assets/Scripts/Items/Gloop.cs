using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gloop : Item
{
    public override void Use(Player player)
    {
        if (player.currentAttack && player.currentAttack.hit && player.currentAttack.enemy)
        {
            player.currentAttack.enemy.GetComponentInParent<NavMeshAgent>().speed = 1;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        Name = "Gloop";
        Description = "Slows enemies on hit.";
        Icon = GetComponentInParent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
