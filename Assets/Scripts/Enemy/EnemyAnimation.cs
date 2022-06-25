using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private EnemyController controller;

    private Vector3 oldPos;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        oldPos = transform.position;
    }

    private void Update()
    {
        CalculateSpeed();

        if (controller.state == EnemyState.Dead)
        {
            DeathAnim();
        }
    }

    private void AttackAnim(int x)
    {
        animator.SetInteger("Attack", x);
    }
    private void DeathAnim()
    {
        animator.SetInteger("Death", Random.Range(1, 4));
    }
    private void CalculateSpeed()
    {
        float speed = Vector3.Distance(oldPos, transform.position) / Time.deltaTime;
        animator.SetFloat("Speed", speed);
        oldPos = transform.position;
    }
}
