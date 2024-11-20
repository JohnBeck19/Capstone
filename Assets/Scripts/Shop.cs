using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject SkillTree;
    [SerializeField] Player player;
    [SerializeField] VoidEvent FreezeGameEvent;
    [SerializeField] VoidEvent UnfreezeGameEvent;
    bool inShop = false;
    bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        SkillTree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {     
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.E))
            {
                inShop = !inShop;
                SkillTree.SetActive(inShop);
                if (inShop)
                {
                    FreezeGameEvent.RaiseEvent();
                }
                else 
                {
                    UnfreezeGameEvent.RaiseEvent();
                } 
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        inRange = false;
        SkillTree.SetActive(false);
    }
}
