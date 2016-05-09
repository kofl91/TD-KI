using UnityEngine;
using System.Collections;

public class DamageInfo
{
    public int amount;

    private eElement element;

    internal eElement Element
    {
        get
        {
            return element;
        }

        set
        {
            element = value;
        }
    }
}

public abstract class BaseEnemy : MonoBehaviour {

    protected int hitpoint;
    protected int maxHitpoint;

    protected int baseMaxHp;
    protected int hpPerLevel;

    protected int bounty;
    protected int baseBounty;
    protected int bountyPerLevel = 1;

    protected BasePlayer enemyOfPlayer;

    public int Hitpoint { set { hitpoint = value; } get { return hitpoint; } }
    public int MaxHitpoint { set { maxHitpoint = value; } get { return maxHitpoint; } }
    public int Bounty { set { bounty = value; } get { return bounty; } }

    private void Start()
    {
        InitCombat(Level.GetCurrentWave());
    }

    public virtual void InitCombat(int wave)
    {
        bounty = baseBounty + wave * bountyPerLevel;
        maxHitpoint = baseMaxHp + wave * hpPerLevel;
        hitpoint = maxHitpoint;
    }

    public virtual void OnDamage(DamageInfo dmg)
    {
        Hitpoint -= dmg.amount;
        //Debug.Log(name + "aua "+ hitpoint);
        if (Hitpoint <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        // Debug.Log(name + "has died!");
        SpawnManager.Instance.Despawn(this.gameObject);
        enemyOfPlayer.EarnBounty(bounty);
    }
}
