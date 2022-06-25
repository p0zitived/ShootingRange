using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeg : EnemyLimb
{
    public override void getDamage(float damage)
    {
        enemyStats.Acctual_healthPoints -= damage * enemyStats.leg_multiplier;
        print("LegShot : " + damage * enemyStats.leg_multiplier);
    }

    public override float getMultiplier()
    {
        return enemyStats.leg_multiplier;
    }
}
