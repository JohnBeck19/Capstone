using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            if (!Input.GetKey(KeyCode.S))
            {
                Animator.SetTrigger("BackIdle");
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
            spriteRenderer.flipX = false;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) )
            { 
                Animator.SetTrigger("SideIdle");
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -speed * Time.deltaTime;
            Animator.SetTrigger("FrontIdle");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
            spriteRenderer.flipX = true;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                Animator.SetTrigger("SideIdle");
            }
        }
    }
}
