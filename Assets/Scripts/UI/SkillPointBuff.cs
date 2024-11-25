using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillPointBuff : MonoBehaviour
{
    public Player player;
    [SerializeField] public string type;
    [SerializeField] public float increaseAmount = 5f;

    public abstract void Buff();

}
