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

    private PlayerController player;
    private PlayerController enemy;

    public GameObject InfoPanel;
    public GameObject DetailPanel;

    private Spawner spawner;

    private BaseTower chosenTower;

    // Use this for initialization
    void Start () {
        infotexts = InfoPanel.GetComponentsInChildren<Text>();
        detailtexts = DetailPanel.GetComponentsInChildren<Text>();
        //speedbar = GetComponentInChildren<Scrollbar>();
        //towerButton = GetComponentsInChildren<Button>();
        player = GetComponentInParent<PlayerController>();
        PlayerController[] allplayer = FindObjectsOfType<PlayerController>();
        foreach(PlayerController pl in allplayer)
        {
            if (!pl.Equals(player))
            {
                enemy = pl;
            }
        }
        spawner = FindObjectOfType<Spawner>();
    }
	
	// Update is called once per frame
	void Update () {

        // Refresh the text.
        if (infotexts.Length > 0)
        {
            infotexts[0].text = "Lives :" + player.Life;
        }
        if (infotexts.Length > 1)
        {
            infotexts[1].text = "Gold :" + player.Gold;
        }
        if (infotexts.Length > 2)
        {
            infotexts[2].text = "Wave :" + (spawner.GetWave()+1) ;
        }
        if (infotexts.Length > 3)
        {
            BaseEnemy[] minions = FindObjectsOfType<BaseEnemy>();
            infotexts[3].text = "Minions alive :" + minions.Length;
        }
        if (infotexts.Length > 4)
        {
            infotexts[4].text = "Enemy Lives :" + enemy.Life;
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
        Debug.Log("Click registered");
        spawner.HireMinion(player, enemyID);
    }

}
