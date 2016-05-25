using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager> {

    public GameObject container;

    public Player firstPlayer = null;

    private int chosenTower = 0;

    public bool isLearning = false;

    void Start()
    {
        container = new GameObject();
        firstPlayer.ClearTiles();
    }

    public void TileClicked(ClickableTile tile)
    {
        firstPlayer.CreateTurretUnit(tile.tileX, tile.tileY, PrefabContainer.Instance.turrets[chosenTower]);
    }


    float lastTime = 0.0f;
    public float interval = 1.0f;

    public void Update()
    {
        if ((Time.time - lastTime) >= interval)
        {
            UIManager.Instance.DrawResourcesInfo();
            UIManager.Instance.DrawWaveInfo();
        }
    }

    internal int GetPlayerGold()
    {
        if (firstPlayer != null)
        {
            return firstPlayer.Gold;
        }
        return -1;
    }

    internal int GetPlayerLife()
    {

        if (firstPlayer != null)
        {
            return firstPlayer.Life;
        }
        return -1;
    }

    internal void GameOver(bool hasWon)
    {
        firstPlayer.RemoveTurrets();
        if (!isLearning)
        {
            if (hasWon)
            {
                SceneManager.LoadScene("VictoryScreen");
            }
            else
            {
                SceneManager.LoadScene("GameOverScreen");
            }
        }
    }

    void ChooseTower(int ID)
    {
        chosenTower = ID;
    }
}
