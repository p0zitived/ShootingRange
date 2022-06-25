using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Attacking,
    Runing,
    Dying,
    Dead,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [SerializeField] Transform rayOrigin;

    [SerializeField] float time_to_delete;
    public EnemyState state;

    private Vector3 acctual_destination;
    private NavMeshAgent navAgent;
    private float initial_speed;


    private void Start()
    {
        state = EnemyState.Idle;
        navAgent = GetComponent<NavMeshAgent>();

        initial_speed = navAgent.speed;

        RandomDestination(20);
    }

    private void Update()
    {
        CheckIfDead();

        if (Vector3.Distance(transform.position,acctual_destination) <= 2)
        {
            RandomDestination(10);
        }
    }

    private void CheckIfDead()
    {
        if (stats.Acctual_healthPoints <= 0 && state != EnemyState.Dead)
        {
            navAgent.isStopped = true;
            state = EnemyState.Dead;
            GlobalFields.points += stats.points;
            Destroy(transform.gameObject, time_to_delete);
        }
    }
    private void RandomDestination(float x)
    {
        acctual_destination = new Vector3(Random.Range(transform.position.x - x,transform.position.x+x),transform.position.y,Random.Range(transform.position.z - x, transform.position.z + x));
        float speed = Random.Range(2f, initial_speed);
        navAgent.speed = speed;

        NavMeshPath path = new NavMeshPath();
        if (!navAgent.CalculatePath(acctual_destination, path))
        {
            RandomDestination(x);
        }
        else
        {
            navAgent.SetDestination(acctual_destination);
        }
    }
}
