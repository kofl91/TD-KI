using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoSingleton<UIManager>
{

    public GameObject root;
    public GameObject waveInfo;
    public GameObject resourcesInfo;
    public GameObject messages;
    public GameObject towerPanel;
   

    public Scrollbar speedScroll;

    private Text[] waveInfoText;
    private Text[] resourcesInfoText;
    private Text[] messageText;

    private Button[] tower;

    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
        resourcesInfoText = resourcesInfo.GetComponentsInChildren<Text>();
        messageText = messages.GetComponentsInChildren<Text>();
        speedScroll = speedScroll.GetComponent<Scrollbar>();
        tower = towerPanel.GetComponentsInChildren<Button>();
    }

    #region ResourcesInfo
    public void DrawMessage(string msg)
    {
        messageText[0].text = msg;
    }

    public void DrawResourcesInfo()
    {
        resourcesInfoText[0].text = "Gold : " + GameManager.Instance.GetPlayerGold();
    }
    #endregion


    #region WaveInfo
    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave : " + 1;
        waveInfoText[1].text = "Enemys left : " + 0;
        waveInfoText[2].text = "Lives left : " + GameManager.Instance.GetPlayerLife();
    }
    #endregion


    public void ChooseTower(int towerID)
    {
        GameManager.Instance.SendMessage("ChooseTower", towerID);
    }


    public void ChangeSpeed()
    {
        //Debug.Log(speedScroll.value);
        Time.timeScale = 1 + (speedScroll.value * 5);
    }
}
