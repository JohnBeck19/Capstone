using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointBuffAttack : SkillPointBuff
{
    
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        type = "Attack";
        increaseAmount = 5;
        Debug.Log(player.ToString());
    }
    public override void Buff()
    {
        player.atkDamage += increaseAmount;
    }

}
