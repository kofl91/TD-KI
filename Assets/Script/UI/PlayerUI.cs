using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// A class that draws the UI for a player.
public class PlayerUI : MonoBehaviour {

    private Text[] infotexts;
    private Text[] detailtexts;

    //private Button[] towerButton;

    private Scrollbar speedbar;

    public PlayerController player;
    public PlayerController enemy;

    public GameObject InfoPanel;
    public GameObject DetailPanel;

    public Spawner spawner;

    public BaseTower chosenTower;

    // Use this for initialization
    void Start () {
        infotexts = InfoPanel.GetComponentsInChildren<Text>();
        detailtexts = DetailPanel.GetComponentsInChildren<Text>();
        player = GetComponentInParent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        // Init. Needed in Network Mode.
        if (!spawner)
        {
            spawner = FindObjectOfType<Spawner>();
            return;
        }
        if (!enemy)
        {
            PlayerController[] allplayer = FindObjectsOfType<PlayerController>();
            foreach (PlayerController pl in allplayer)
            {
                if (!pl.Equals(player))
                {
                    enemy = pl;
                }
            }
            return;
        }
        // Refresh the text.
        if (infotexts.Length > 0)
        {
            infotexts[0].text = "" + player.Life;
        }
        if (infotexts.Length > 1)
        {
            infotexts[1].text = "" + player.Gold;
        }
        if (infotexts.Length > 3)
        {
            infotexts[3].text = "" + (spawner.GetWave()+1) ;
        }
        if (infotexts.Length > 5)
        {
            BaseEnemy[] minions = FindObjectsOfType<BaseEnemy>();
            infotexts[5].text = "" + minions.Length;
        }
        if (infotexts.Length > 6)
        {
            infotexts[6].text = "" + enemy.Life;
        }
        /*if (speedbar)
        {
            Time.timeScale = speedbar.value * 100 + 1.0f;
        }*/
    }

    public void DisplayTowerDetails(BaseTower tower)
    {
        DetailPanel.SetActive(true);
        detailtexts[1].text = "" + tower.turretDmg.calcDmgVsNeutralResistance();
        detailtexts[3].text = "" + tower.turretDmg.GetDamageType();
        detailtexts[5].text = "" + tower.cooldown;
        detailtexts[7].text = "" + tower.projectile.GetComponent<BaseProjectile>().splash;
        detailtexts[9].text = "" + tower.range;
        detailtexts[11].text = "" + tower.level;
        chosenTower = tower;
        /* Disable all options if the one clicking was not the owner. Not working on network mode.
        Button[] buttons = DetailPanel.GetComponentsInChildren<Button>(true);
        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(tower.owner == player);
        }*/
    }

    public void SellTower()
    {
        player.SellTower(chosenTower);
        DetailPanel.SetActive(false);
    }

    public void UpgradeTower()
    {
        chosenTower.Upgrade();
        DisplayTowerDetails(chosenTower);
    }

    public void HireMinion(int enemyID)
    {
        spawner.HireMinion(player, enemyID);
    }

    public void ChooseTower(int towerID)
    {
        player.ChooseTower(towerID);
    }


}
