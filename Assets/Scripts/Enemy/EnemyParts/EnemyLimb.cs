using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLimb : MonoBehaviour
{
    public EnemyStats enemyStats { get; set; }

    private void Start()
    {
        enemyStats = transform.GetComponentInParent<EnemyStats>();
    }

    public abstract void getDamage(float damage);
    public abstract float getMultiplier();
}
