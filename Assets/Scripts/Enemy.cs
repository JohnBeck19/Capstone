using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] public EnemyMovement EnemyMovement;
    [SerializeField] GameObject player;
    private float attackTimer = 0;
    [SerializeField] float attackCD = 1.0f;
    [SerializeField] float damage = 30.0f;
    [SerializeField] float health = 10.0f;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2.0f)
        {
            Attack();
        }
        attackTimer -= Time.deltaTime;  
    }
    public void Attack()
    {
        if (animator.GetBool("Death") == false && attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
            player.GetComponent<Player>().DamagePlayer(30.0f);
            attackTimer = attackCD;
        }
    }
    public void death()
    {
        if (animator.GetBool("Death") == false)
        {
            animator.SetTrigger("DeathTrigger");
            animator.SetBool("Death", true);
            EnemyMovement.stop();
            Destroy(gameObject, 3);
        }
    }
    public bool Damage(float damage)
    {   
        //already dead
        if (health <= 0)
        {
            return false;
        }

        health -= damage;

        //return true if this is killing blow.
        if (health <= 0)
        {
            death();
            return true;
        }
        return false;

    }
}
