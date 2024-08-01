using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public float detectionRange = 1f;

    void Start()
    {
        // Get the NavMeshAgent component from the current GameObject
        agent = GetComponent<NavMeshAgent>();

        Transform ghostTransform = transform.Find("Ghostface");
        if (ghostTransform != null)
        {
            anim = ghostTransform.GetComponent<Animator>();
            if (anim == null)
            {
                Debug.LogWarning("Animator component not found on 'Ghost'.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject 'Ghost' not found.");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        agent.destination = player.position;

        if (distanceToPlayer <= detectionRange)
        {
            agent.enabled = false;
            if (anim != null)
            {
                anim.SetBool("iswalking", false);
            }
        }
        else
        {
            agent.enabled = true;
            if (anim != null)
            {
                anim.SetBool("iswalking", true);
            }
        }
    }
}
