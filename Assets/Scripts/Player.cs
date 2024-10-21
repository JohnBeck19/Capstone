using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{

    enum facing { 
        LEFT, RIGHT, FORWARD, BACKWARD, FORWARDRIGHT, FORWARDLEFT, BACKWARDRIGHT, BACKWARDLEFT
    }
    [SerializeField] float speed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator Animator;
    [SerializeField] LayerMask groundLayerMask;
    private facing dir = facing.BACKWARD;

    [SerializeField] GameObject attack;

    //dash 
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCD = 0f;
    [SerializeField] float dashDistance = 20f;
    [SerializeField] float dashCooldown = 2.0f;

    //attack
    private Vector3 attackDirection;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
            transform.position += transform.forward * speed * Time.deltaTime;
            if (!Input.GetKey(KeyCode.S))
            {
                dir = facing.FORWARD;
                Animator.SetTrigger("BackIdle");
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
            spriteRenderer.flipX = false;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) )
            { 
                dir = facing.LEFT;
                Animator.SetTrigger("SideIdle");
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -speed * Time.deltaTime;
            Animator.SetTrigger("FrontIdle");
            dir = facing.BACKWARD;
        }
        if (Input.GetKey(KeyCode.D))
        {
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



        //dash input
        if (Input.GetKeyDown(KeyCode.Space) || isDashing)
        {
            if (dashCD >= 0)
            {
                ;
            }
            else if (isDashing)
            {
                dash(dashDirection * Time.deltaTime * dashDistance);
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
        if (dashCD >= 0)
        {
            dashCD -= Time.deltaTime;
        }



        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attack");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit cursor, 1000,groundLayerMask))
            {
                attackDirection =  (cursor.point - transform.position).normalized * 4f;
                attackDirection.y = transform.position.y-.75f;
            }

            
            Instantiate(attack, transform.position + attackDirection, Quaternion.LookRotation(attackDirection));
            Debug.DrawRay(transform.position, attackDirection, Color.green);



        }

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
