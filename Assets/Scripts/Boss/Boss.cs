using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] public BossMovement EnemyMovement;
    [SerializeField] GameObject player;
    private float attackTimer = 0;
    [SerializeField] float attackCD = 1.0f;
    [SerializeField] float health = 10.0f;
    [SerializeField] GameObject[] attacks;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject dashCollider;

    bool dashing = false;
    Vector3 dashDirection;
    [SerializeField] float dashDistance = 15;
    private float dashTimer = 1;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, player.transform.position) <= 2.0f)
        //{

        //}
        if (dashing)
        {
                Dash();
        } else
        {
            Attack();
        }
        attackTimer -= Time.deltaTime;
        dashCollider.GetComponent<Collider>().enabled = dashing;
    }
    public void Attack()
    {
        if (animator.GetBool("Death") == false && attackTimer <= 0)
        {
            //animator.SetTrigger("Attack");
            //player.GetComponent<Player>().DamagePlayer(damage);
            attackTimer = attackCD;
            
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(attacks[0], firePoint.position, transform.rotation);
                    Debug.Log("Attack1");
                    break;
                case 1:
                    dashing = true;
                    Dash();
                    dashDirection = (player.transform.position-transform.position).normalized;
                    dashDirection.y = 0;
                    Debug.Log("Attack2");
                    break;
                case 2:
                    Instantiate(attacks[1]);
                    Debug.Log("Attack3");
                    break;

            }
        }


    }

    public void Dash()
    {
        if (dashTimer > 0)
        {
            transform.position += dashDirection * Time.deltaTime * dashDistance;
            dashTimer -= Time.deltaTime;
            if (Vector3.Distance(transform.position,player.transform.position) < 1.0f)
            {
                dashTimer = 0;
                dashing = false;
            }
        }
        else
        {
            dashing = false;
            dashTimer = 1;
            
        }
    }
    public void death()
    {
        Debug.Log("BOSS KILLED!");
        Destroy(gameObject, 3);
        //if (animator.GetBool("Death") == false)
        //{
        //    animator.SetTrigger("DeathTrigger");
        //    animator.SetBool("Death", true);
        //    EnemyMovement.stop();
        //    Destroy(gameObject, 3);
        //}
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
