using UnityEngine;
using System.Collections;

public class FastEnemy : Enemy {

    // Use this for initialization
    void Start()
    {
        speed = 10f;
        life = 2f;
        
    }
    
    void ReachedGoal()
    {

        Destroy(gameObject);
    }

    public void OnDamage(DamageInfo damage)
    {
        life -= damage.amount;
        if (life <= 0)
        {
            GameManager.Instance.firstPlayer.enemyKilled++;
            Destroy(this.gameObject);
        }
    }
}
