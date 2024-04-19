using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public float detectionRange = 0.05f;
    private Animator anim;
    public Transform player;
    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the distance to the player is smaller than the detection range
        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
            // Disable the NavMeshAgent to stop the random movement
            if (distanceToPlayer <= 2)
            {
                agent.enabled = false;
                anim.SetBool("iswalking", false);
            }

            // Set the destination of the NavMeshAgent to the player's position
          
        }
        else
        {
            // Enable the NavMeshAgent for random movement if the player is out of range
            agent.enabled = true;
            anim.SetBool("iswalking", true);

            // If the agent is done with its path, generate a new random destination
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

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}