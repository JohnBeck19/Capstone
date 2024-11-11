using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErebusCloak : Item
{
    public override void Use(Player player)
    {
        if (player.inLight == false)
        {
            player.HealPlayer(player.healthRegen);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Name = "Erebus' Cloak";
        Description = "Regenerate health while inside of shadows.";
        Icon = GetComponentInParent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
