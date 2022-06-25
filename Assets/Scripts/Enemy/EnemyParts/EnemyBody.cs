using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : EnemyLimb
{
    public override void getDamage(float damage)
    {
        enemyStats.Acctual_healthPoints -= damage * enemyStats.body_multiplier;
        print("BodyShot : " + damage * enemyStats.body_multiplier);
    }

    public override float getMultiplier()
    {
        return enemyStats.body_multiplier;
    }
}
