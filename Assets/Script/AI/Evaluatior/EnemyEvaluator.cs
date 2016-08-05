using System;
using System.Collections.Generic;

internal class EnemyEvaluator
{
    private List<EnemyStructure> enemys;

    public EnemyEvaluator(List<EnemyStructure> enemys)
    {
        this.enemys = enemys;
    }

    internal EnemyStructure GetBestEnemy(DamageInfo enemyDmg)
    {
        foreach (EnemyStructure en in enemys)
        {
            en.SetIncomingDamage(enemyDmg);
        }
        enemys.Sort();
        return enemys[0];
    }
}