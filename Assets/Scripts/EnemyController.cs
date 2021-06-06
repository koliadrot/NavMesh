using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lookRadious = 2f;
    private Vector3 direction;
    private float distance;
    private NavMeshAgent agent;
    private Quaternion lookDirection;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        SuspectToTarget();
    }

    private void SuspectToTarget()
    {
        distance = Vector3.SqrMagnitude(target.position - transform.position);
        if (distance >= lookRadious)
        {
            agent.SetDestination(target.position);
        }
        else if (distance <= lookRadious / 2f)
        {
            agent.SetDestination(-target.position);
        }
        else
        {
            agent.ResetPath();
        }
        FaceOnTarget();
    }

    private void FaceOnTarget()
    {
        direction = (target.position - transform.position).normalized;
        lookDirection = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadious);
    }
}
