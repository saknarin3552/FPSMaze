using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow2 : MonoBehaviour
{

    public float closeDistance = 5f;
    public float longDistance = 10f;

    Transform target;
    NavMeshAgent agent;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= longDistance)
        {
            
            agent.SetDestination(target.position);

            if (distance <= closeDistance)
            {
                
            }

            if (distance <= agent.stoppingDistance)
            {
                
                FaceTarget();
            }
        }
        else
        {
            
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, longDistance);
    }
}
