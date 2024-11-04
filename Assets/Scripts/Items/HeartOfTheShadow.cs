using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Increases max hp per enemy kill think ror2 infusion
public class HeartOfTheShadow : Item
{
    Slash last = null;
    float currentIncrease = 0;
    public override void Use(Player player)
    {
        if (currentIncrease < 100)
        {
            if (player.currentAttack)
            {
                if (player.currentAttack.hit == true && player.currentAttack != last)
                {
                    player.maxHealth += 5;
                    currentIncrease += 5;
                    last = player.currentAttack;
                }
            }
        }
        return;
    }


    // Start is called before the first frame update
    void Start()
    {
        Name = "Heart Of The Shadow";
        Description = "Gain Max HP on enemy kill, up to 100.";
        Icon = GetComponentInParent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
