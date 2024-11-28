using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{

    enum facing { 
        LEFT, RIGHT, FORWARD, BACKWARD, FORWARDRIGHT, FORWARDLEFT, BACKWARDRIGHT, BACKWARDLEFT
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator Animator;
    [SerializeField] LayerMask groundLayerMask;

    private facing dir = facing.BACKWARD;

    [SerializeField] GameObject[] attacks;

    [Header("Dash")]
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCD = 0f;
    private float attackTimer = 0f;
    private float attackCD = 0.5f;
    [SerializeField] float dashDistance = 20f;
    [SerializeField] float dashCooldown = 2.0f;

    //attack
    [Header("Attack")]
    private Vector3 attackDirection;
    public Slash currentAttack;
    public Slash lastAttack;

    //stats
    [Header("Stats")]
    [SerializeField] public float health = 100f;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float healthRegen = 0.2f;
    [SerializeField] public float defense = 10.0f;
    [SerializeField] public float atkDamage = 5.0f;
    [SerializeField] public float atkSpeed = 20.0f;
    [SerializeField] public float speed = 1f;
    [SerializeField] VoidEvent inGameEvent;
    [SerializeField] VoidEvent playerDeadEvent;
    [SerializeField] public int souls = 0;
    public bool active = false;
    public bool inLight = false;
    [Header("Audio")]
    [SerializeField] AudioSource walksound;
    [SerializeField] AudioSource attackAudioSource;
    [SerializeField] AudioClip attackSound;

    public List<Item> items = new List<Item>();
  
    private void Start()
    {
        inGameEvent.Subscribe(inGame);
    }
    private void inGame()
    {
        active = true;

    }
    // Update is called once per frame
    void Update()
    {
        walksound.volume = 0;
        if (active) {
            //movement
            Move();

            //dash input
            if (Input.GetKeyDown(KeyCode.Space) || isDashing)
            {
                if (dashCD >= 0)
                {
                    ;
                }
                else if (isDashing)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dashDirection, out hit, dashDistance))
                    {
                        dash(dashDirection * Time.deltaTime * hit.distance);
                    }
                    else
                    {
                        dash(dashDirection * Time.deltaTime * dashDistance);
                    }
                    dashTimer -= Time.deltaTime;
                    if (dashTimer < 0)
                    {
                        isDashing = false;
                        dashCD = dashCooldown;
                    }
                }
                else
                {
                    isDashing = true;
                    setDashDirection();
                    dashTimer = .15f;


                }
            }

            //ATTACK
            if (Input.GetMouseButtonDown(0))
            {
                if (attackTimer <= 0)
                {
                    attackTimer = attackCD - atkSpeed / 100.0f;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit cursor, 1000, groundLayerMask))
                    {
                        attackDirection = (cursor.point - transform.position).normalized * 4f;
                        attackDirection.y = transform.position.y - .75f;
                        GameObject g = Instantiate(attacks[Random.Range(0, attacks.Length)], transform.position + attackDirection, Quaternion.LookRotation(attackDirection));
                        attackAudioSource.PlayOneShot(attackSound);
                        Debug.DrawRay(transform.position, attackDirection, Color.green);
                        currentAttack = g.GetComponentInChildren<Slash>();
                        currentAttack.player = this;

                    }
                }
            }
            //souls per kill
            if (currentAttack)
            {
                if (currentAttack.kill && currentAttack != lastAttack)
                {
                    souls += 5;
                    lastAttack = currentAttack;

                }
            }

            //items
            foreach (Item item in items)
            {
                item.Use(this);
            }

            //timers
            if (attackTimer >= 0)
            {
                attackTimer -= Time.deltaTime;
            }
            if (dashCD >= 0)
            {
                dashCD -= Time.deltaTime;
            }

        }
    }

    public void Equip(Item item)
    {
        items.Add(item);
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            walksound.volume = 1;
            transform.position += transform.forward * speed * Time.deltaTime;
            if (!Input.GetKey(KeyCode.S))
            {
               
                dir = facing.FORWARD;
                Animator.SetTrigger("BackIdle");
            }

        }
        if (Input.GetKey(KeyCode.A))
        {
            walksound.volume = 1;
            transform.position += transform.right * -speed * Time.deltaTime;
            spriteRenderer.flipX = false;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                dir = facing.LEFT;
                Animator.SetTrigger("SideIdle");
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            walksound.volume = 1;
            transform.position += transform.forward * -speed * Time.deltaTime;
            Animator.SetTrigger("FrontIdle");
            dir = facing.BACKWARD;
        }
        if (Input.GetKey(KeyCode.D))
        {
            walksound.volume = 1;
            transform.position += transform.right * speed * Time.deltaTime;
            spriteRenderer.flipX = true;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                dir = facing.RIGHT;
                Animator.SetTrigger("SideIdle");
            }
        }
        //set direction
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) dir = facing.FORWARDRIGHT;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) dir = facing.FORWARDLEFT;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) dir = facing.BACKWARDLEFT;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) dir = facing.BACKWARDRIGHT;

    }
    //Receive damage
    public float DamagePlayer(float damage)
    {

        health = health - Mathf.Max(0, damage - defense);
        if (health < 0) playerDeadEvent.RaiseEvent();
        return health;
    }
    public float DamagePlayerNoDefense(float damage)
    {
        health = health - damage;
        if (health < 0) playerDeadEvent.RaiseEvent();
        return health;
    }
    public float HealPlayer(float heal)
    {
        health = Mathf.Min(maxHealth, health + heal);
        return health;
    }
    //dash method
    void dash(Vector3 DashDistance)
    { 
        transform.position += DashDistance;
    }


    //set dash direction
    private void setDashDirection()
    {
        switch (dir)
        {
            case facing.LEFT:
                dashDirection = Vector3.left;

                Animator.SetTrigger("Dash");
                break;
            case facing.RIGHT:
                dashDirection = Vector3.right;

                Animator.SetTrigger("Dash");

                break;
            case facing.FORWARD:
                dashDirection = transform.forward;
                Animator.SetTrigger("Dashf");
                break;
            case facing.BACKWARD:
                dashDirection = -transform.forward;
                Animator.SetTrigger("Dashb");
                break;
            case facing.FORWARDRIGHT:
                dashDirection = (transform.forward + Vector3.right).normalized;
                Animator.SetTrigger("Dashfs");

                break;
            case facing.FORWARDLEFT:
                dashDirection = (transform.forward + Vector3.left).normalized;
                Animator.SetTrigger("Dashfs");

                break;
            case facing.BACKWARDRIGHT:
                dashDirection = (-transform.forward + Vector3.right).normalized;
                Animator.SetTrigger("Dash");
                break;
            case facing.BACKWARDLEFT:
                dashDirection = (-transform.forward + Vector3.left).normalized;
                Animator.SetTrigger("Dash");
                break;
        }
    }
}
