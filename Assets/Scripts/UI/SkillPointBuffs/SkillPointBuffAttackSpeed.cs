using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointBuffAttackSpeed: SkillPointBuff
{
    
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        type = "Attack Speed";
        increaseAmount = 10;
    }
    public override void Buff()
    {
        player.atkSpeed += increaseAmount;
    }

}
