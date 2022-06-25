using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : EnemyLimb
{
    public override void getDamage(float damage)
    {
        enemyStats.Acctual_healthPoints -= damage * enemyStats.head_multiplier;
        print("HeadShot : " + damage * enemyStats.head_multiplier);
    }

    public override float getMultiplier()
    {
        return enemyStats.head_multiplier;
    }
}
