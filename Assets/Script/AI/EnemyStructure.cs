using System;

public class EnemyStructure : IEquatable<EnemyStructure>, IComparable<EnemyStructure>
{
    public int Id;

    public BaseEnemy enemy;

    private DamageInfo incomingDamage;

    public void SetIncomingDamage(DamageInfo dmg)
    {
        incomingDamage = dmg;
    }

    public int CompareTo(EnemyStructure other)
    {
        return (int) ((incomingDamage.calcAbsoluteDmg(enemy.GetResistance()) - incomingDamage.calcAbsoluteDmg(other.enemy.GetResistance()))*100);
    }

    public bool Equals(EnemyStructure other)
    {
        return incomingDamage.calcAbsoluteDmg(enemy.GetResistance()) == incomingDamage.calcAbsoluteDmg(other.enemy.GetResistance());
    }
}