using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; // Radius of the area the agent moves within
    public float detectionRange = 10f;
    public float insideattackrange =4f;
    private Animator anim;
    public Transform player;
    public Transform centrePoint; // Centre of the area for random movement
    private ParticleSystem particlesys;
    private AudioSource audiosource;
    private EnemyFire enemyfire;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyfire= GetComponent<EnemyFire>();
        audiosource= GetComponent<AudioSource>();
        Transform ghostTransform = transform.Find("Ghostface");
        if (ghostTransform != null)
        {
            anim = ghostTransform.GetComponent<Animator>();
            if (anim == null)
            {
                Debug.LogWarning("Animator component not found on 'Ghostface'.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject 'Ghostface' not found.");
        }



    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
           
            agent.SetDestination(player.position);
            if (distanceToPlayer <= insideattackrange)
            {
               
                audiosource.Stop();
               

                agent.enabled = false;
                LookAtPlayer();
                anim.SetBool("iswalking", false);
                enemyfire.enabled = true;


            }
           
        }
        else
        {  
            audiosource.Play();
            enemyfire.enabled= false;
            agent.enabled = true;
            anim.SetBool("iswalking", true);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                }
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep only the horizontal rotation
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
