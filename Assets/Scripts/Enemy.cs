using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void death()
    {
        if (animator.GetBool("Death") == false)
        {
            animator.SetTrigger("DeathTrigger");
            animator.SetBool("Death", true);
            Destroy(gameObject, 3);
        }
    }
}
