using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager> {

    public Map currentMap;

    public Player firstPlayer;

    public void TileClicked(ClickableTile tile)
    {
        if(firstPlayer.Gold > 20)
        {
            Debug.Log("Click");
            int x = tile.tileX;
            int y = tile.tileY;
            Instantiate(PrefabContainer.Instance.turrets[0], new Vector3(x, 1, y), Quaternion.identity);
            firstPlayer.Gold -= 20;
        }
    }


    public float lastTime = 0.0f;
    public float interval = 1.0f;

    public void Update()
    {
        if ((Time.time - lastTime) >= interval)
        {
            UIManager.Instance.DrawResourcesInfo();
            UIManager.Instance.DrawWaveInfo();
        }
    }
}
