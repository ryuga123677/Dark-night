using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public float detectionRange = 0.05f;
  
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        agent.destination= player.position;
        if (distanceToPlayer <= detectionRange)
        {
            agent.enabled = false;
            anim.SetBool("iswalking", false);
            
        }
        else
        {
            agent.enabled = true;
            anim.SetBool("iswalking", true);

        }
    }
}
