using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

// A class that draws the UI for a player.
public class PlayerUI : MonoBehaviour {

    private Text[] infoTexts;
    private Text[] infoEnemyTexts;
    private Scrollbar lifebar;
    private Scrollbar lifebarEnemy;

    private Text[] detailtexts;

    //private Button[] towerButton;

    private Scrollbar speedbar;

    public PlayerController player;
    public PlayerController enemy;

    public GameObject InfoPanel;
    public GameObject TowerDetailPanel;
    public GameObject MinionDetailPanel;
    public GameObject InfoEnemyPanel;
    public GameObject TowerPanel;

    public Spawner spawner;

    public BaseTower chosenTower;

    public GameObject chosenTowerParticle;

    // Use this for initialization
    void Start () {
        infoTexts = InfoPanel.GetComponentsInChildren<Text>();
        detailtexts = TowerDetailPanel.GetComponentsInChildren<Text>();
        infoEnemyTexts = InfoEnemyPanel.GetComponentsInChildren<Text>();

        lifebar = InfoPanel.GetComponentInChildren<Scrollbar>();
        lifebarEnemy = InfoEnemyPanel.GetComponentInChildren<Scrollbar>();

        initTowerPanel();

    }

    private void initTowerPanel()
    {
        Button[] buttonList = TowerPanel.GetComponentsInChildren<Button>();
        List<GameObject> towers = PrefabContainer.Instance.turrets;
        for (int i = 0; i < towers.Count; i++)
        {
            Text[] texts = buttonList[i].GetComponentsInChildren<Text>();
            RawImage img = buttonList[i].GetComponentInChildren<RawImage>();
            // Set the name in the panel.
            texts[0].text = towers[i].name;
            // Set the name in the panel.
            texts[1].text = towers[i].GetComponent<BaseTower>().buildCost + "Gold";
            img.texture = towers[i].GetComponent<RawImage>().texture;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Init. Needed in Network Mode.

        if (!player)
        {
            player = GetComponentInParent<PlayerController>();
            return;
        }

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
        if (infoTexts.Length > 0)
        {
            infoTexts[0].text = "" + player.Gold;
        }
        if (infoTexts.Length > 3)
        {
            infoTexts[3].text = "" + spawner.GetWave();
        }

        if (infoEnemyTexts.Length > 0)
        {
            infoEnemyTexts[0].text = "" + enemy.Gold;
        }
        if (infoEnemyTexts.Length > 3)
        {
            infoEnemyTexts[3].text = "" + spawner.GetWave();
        }

        if (lifebar)
        {
            lifebar.size = ((float)player.Life)/20;
        }

        if (lifebarEnemy)
        {
            lifebarEnemy.size = ((float)enemy.Life)/20 ;
        }

        /*if (speedbar)
        {
            Time.timeScale = speedbar.value * 100 + 1.0f;
        }*/
    }

    public void DisplayTowerDetails(BaseTower tower)
    {
        chosenTowerParticle.SetActive(true);
        chosenTowerParticle.transform.position = tower.transform.position;
        TowerDetailPanel.SetActive(true);
        HideMinionDetails();
        detailtexts[1].text = "" + tower.turretDmg.calcDmgVsNeutralResistance();
        detailtexts[3].text = "" + tower.turretDmg.GetDamageType();
        detailtexts[5].text = "" + tower.cooldown;
        detailtexts[7].text = "" + tower.range;
        chosenTower = tower;
    }

    public void DisplayBuildTowerDetails(int id)
    {
        DisplayTowerDetails(PrefabContainer.Instance.turrets[id].GetComponent<BaseTower>());
    }

    public void HideTowerDetails()
    {
        chosenTowerParticle.SetActive(false);
        TowerDetailPanel.SetActive(false);
    }

    public void DisplayMinionDetails(int id)
    {
        HideTowerDetails();
        MinionDetailPanel.SetActive(true);

        Text[] texts = MinionDetailPanel.GetComponentsInChildren<Text>();

        texts[1].text = spawner.GetHPCurrentWave() + "" ;
        DamageInfo resi = PrefabContainer.Instance.enemys[id].GetComponent<BaseEnemy>().resistance;
        texts[4].text = resi.normal + "";
        texts[6].text = resi.fire + "";
        texts[8].text = resi.water + "";
        texts[10].text = resi.nature + "";
    }

    public void HideMinionDetails()
    {
        MinionDetailPanel.SetActive(false);
    }

    public void SellTower()
    {
        player.SellTower(chosenTower);
        TowerDetailPanel.SetActive(false);
    }

    public void UpgradeTower()
    {
        chosenTower.CmdUpgrade();
        DisplayTowerDetails(chosenTower);
    }

    public void HireMinion(int enemyID)
    {
        player.SendEnemys(enemyID);
    }

    public void ChooseTower(int towerID)
    {
        player.ChooseTower(towerID);
    }

    public void DisplayBestMoves()
    {
        player.DisplayBestMoves();
    }

}
