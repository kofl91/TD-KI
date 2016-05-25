using UnityEngine;
using System.Collections;

public class DamageInfo
{
    public int amount;

}

public class BaseCombat : MonoBehaviour {

    protected int hitpoint = 30;
    protected int maxHitpoint = 30;
    protected int goldBounty = 3;

    public int Hitpoint { set { hitpoint = value; } get { return hitpoint; } }
    public int MaxHitpoint { set { maxHitpoint = value; } get { return maxHitpoint; } }

    public Player player;

    private void Start()
    {
        InitCombat();
    }

    public virtual void InitCombat()
    {
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

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public virtual void OnDeath()
    {
        // Debug.Log(name + "has died!");
        
        player.ChangeBalance(goldBounty);
        player.enemyKilled++;
        Debug.Log(player.enemyKilled);
    }
}
