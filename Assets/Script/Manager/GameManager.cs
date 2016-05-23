using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoSingleton<GameManager> {

    public GameObject container;

    public Player firstPlayer = null;

    private int chosenTower = 0;

    void Start()
    {
        container = new GameObject();
    }

    public void TileClicked(ClickableTile tile)
    {
        if(firstPlayer.Gold > 20)
        {
            Debug.Log("Click");
            Debug.Log(tile);
            int x = tile.tileX;
            int y = tile.tileY;
            int offsetX = Map.Instance.xOffset;
            int offsetY = Map.Instance.yOffset;
            int tilesize = Map.Instance.tileSize;
            Debug.Log("Turret at:" + x + "/" + y);
            GameObject go = (GameObject) Instantiate(PrefabContainer.Instance.turrets[chosenTower], new Vector3(x* tilesize+ offsetX, 2.0f, y* tilesize+ offsetY), PrefabContainer.Instance.turrets[chosenTower].transform.rotation);
            go.transform.parent = container.transform;
            firstPlayer.Gold -= 20;
        }
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

    void ChooseTower(int ID)
    {
        chosenTower = ID;
    }
}
