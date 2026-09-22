using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // This is how far the enemy picks its next target from its current position
    private float wanderRadius = 15f;

    // The playable area
    private Vector2 mapBounds = new Vector2(29.3f, 29.6f);

    // How close to its current target the enemy needs to be to pick a new one
    private float arriveDistance = 1.0f;

    private int maxSampleAttempts = 30;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        PickNewDestination();
    }

    void Update()
    {
        if (navMeshAgent.pathPending)
        {
            return;
        }

        // Checks that the enemy has actually come to a stop since remainingDistance reads as 0 for a moment after setDestination
        bool arrived = navMeshAgent.remainingDistance <= Mathf.Max(arriveDistance, navMeshAgent.stoppingDistance)
                                                         && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 0.01f);

        if (arrived)
        {
            PickNewDestination();
        }
    }

    void PickNewDestination()
    {
        for (int index = 0; index < maxSampleAttempts; index++)
        {
            // A random point in a radius around the enemy -- originally was forcing movement along single axis
            Vector2 offset = Random.insideUnitCircle * wanderRadius;
            Vector3 candidate = transform.position + new Vector3(offset.x, 0.0f, offset.y);

            candidate.x = Mathf.Clamp(candidate.x, -mapBounds.x, mapBounds.x);
            candidate.z = Mathf.Clamp(candidate.z, -mapBounds.y, mapBounds.y);

            // Snaps the candidate position onto the navmesh sot the enemy can actually reach it
            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position);

                return;
            }


        }
    }
}