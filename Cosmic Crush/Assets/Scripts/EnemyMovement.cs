using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Reference to the NavMeshAgent component for pathfinding.
    private NavMeshAgent navMeshAgent;

    float randX;
    float randZ;

    int randomPos;

    Vector3 PosX;
    Vector3 PosZ;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the NavMeshAgent component attached to this object.
        navMeshAgent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame.
    void Update()
    {
        if (navMeshAgent.destination == PosX || navMeshAgent.destination == PosZ)
        {
            RandomGen();
        }

        if (randomPos == 0)
        {
            if (PosX != null)
            {
                navMeshAgent.SetDestination(PosX);
            }
        }
        else
        {
            if (PosZ != null)
            {
                navMeshAgent.SetDestination(PosZ);
            }
        }
    }

    void RandomGen()
    {
        randX = Random.Range(-29.3f, 29.3f);
        randZ = Random.Range(29.6f, 29.6f);

        randomPos = Random.Range(0, 1);

        PosX = new Vector3(randX, 0, 0);
        PosZ = new Vector3(0, 0, randZ);
    }
}