using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] public BossMovement EnemyMovement;
    [SerializeField] GameObject player;
    public GameObject bossHealthBar;
    private float attackTimer = 0;
    [SerializeField] float attackCD = 1.0f;
    [SerializeField] float health = 10.0f;
    [SerializeField] float maxhealth = 100.0f;
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
        
        bossHealthBar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, player.transform.position) <= 2.0f)
        //{

        //}
        if (dashing && animator.GetBool("Death") == false)
        {
            Dash();
            
        } else
        {
            Attack();
        }
        attackTimer -= Time.deltaTime;
        dashCollider.GetComponent<Collider>().enabled = dashing;
        bossHealthBar.GetComponent<Slider>().value = health/maxhealth;
    }
    public void Attack()
    {
        if (animator.GetBool("Death") == false && attackTimer <= 0)
        {
            //animator.SetTrigger("Attack");
            //player.GetComponent<Player>().DamagePlayer(damage);
            // attackTimer = attackCD;
            
            switch (Random.Range(0, 3))
            {
                case 0:
                    animator.SetTrigger("Attack");
                    
                    Debug.Log("Attack1");
                    StartCoroutine(SpawnAttackMidway(0));
                    break;
                case 1:
                    dashing = true;
                    animator.SetBool("Dash", true);
                    Dash();
                    dashDirection = (player.transform.position-transform.position).normalized;
                    dashDirection.y = 0;
                    Debug.Log("Attack2");
                    break;
                case 2:
                    
                    animator.SetTrigger("Smite");
                    Debug.Log("Attack3");
                    StartCoroutine(SpawnAttackMidway(2));
                    break;

            }
            attackTimer = animator.GetCurrentAnimatorStateInfo(0).length+attackCD;
            

        }


    }
    private IEnumerator SpawnAttackMidway(int attack)
    {
        // Get the current animation's state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Assuming default layer

        // Calculate half the animation duration
        float halfAnimationTime = stateInfo.length / 2f;

        // Wait for half the animation duration
        yield return new WaitForSeconds(halfAnimationTime);

        switch (attack)
        {
            case 0:
                Instantiate(attacks[0], firePoint.position, transform.rotation);
                break;
            case 2:
                Instantiate(attacks[1]);
                break;
            default:

                break;
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
                animator.SetBool("Dash", false);
            }
        }
        else
        {
            dashing = false;
            dashTimer = 1;
            animator.SetBool("Dash", false);

        }
    }
    public void death()
    {
        Debug.Log("BOSS KILLED!");
        bossHealthBar.SetActive(false); 
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
