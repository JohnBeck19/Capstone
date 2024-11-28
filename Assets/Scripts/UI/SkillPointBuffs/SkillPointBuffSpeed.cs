using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointBuffSpeed: SkillPointBuff
{
    
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        type = "Speed";
        increaseAmount = 5;
    }
    public override void Buff()
    {
        player.speed += increaseAmount;
    }

}
