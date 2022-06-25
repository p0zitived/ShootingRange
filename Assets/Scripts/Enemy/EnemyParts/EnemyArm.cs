using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArm : EnemyLimb
{
    public override void getDamage(float damage)
    {
        enemyStats.Acctual_healthPoints -= damage * enemyStats.arm_multiplier;
        print("HandShot : " + damage * enemyStats.arm_multiplier);
    }

    public override float getMultiplier()
    {
        return enemyStats.arm_multiplier;
    }
}
