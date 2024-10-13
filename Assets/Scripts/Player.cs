using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    enum facing { 
        LEFT, RIGHT, FORWARD, BACKWARD, FORWARDRIGHT, FORWARDLEFT, BACKWARDRIGHT, BACKWARDLEFT
    }
    [SerializeField] float speed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator Animator;
    private facing dir = facing.BACKWARD;


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

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) dir = facing.FORWARDRIGHT;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) dir = facing.FORWARDLEFT;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) dir = facing.BACKWARDLEFT;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) dir = facing.BACKWARDRIGHT;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (dir)
            {
                case facing.LEFT:
                    transform.position += Vector3.left * 5;
                    break;
                case facing.RIGHT:
                    transform.position += Vector3.right * 5;
                    break;
                case facing.FORWARD:
                    transform.position += transform.forward * 5;
                    break;
                case facing.BACKWARD:
                    transform.position += -transform.forward * 5;
                    break;
                case facing.FORWARDRIGHT:
                    transform.position += (transform.forward + Vector3.right).normalized *5;
                    break;
                case facing.FORWARDLEFT:
                    transform.position += (transform.forward + Vector3.left).normalized * 5;
                    break;
                case facing.BACKWARDRIGHT:
                    transform.position += (-transform.forward + Vector3.right ).normalized * 5;
                    break;
                case facing.BACKWARDLEFT:
                    transform.position += (-transform.forward + Vector3.left).normalized * 5;
                    break;
            }

        }
    }
}
