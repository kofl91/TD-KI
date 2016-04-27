using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoSingleton<UIManager> {

    public GameObject root;
    public GameObject waveInfo;
    public GameObject resourcesInfo;
    public GameObject messages;

    private Text[] waveInfoText;
    private Text[] resourcesInfoText;
    private Text[] messageText;


    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
        resourcesInfoText = resourcesInfo.GetComponentsInChildren<Text>();
        messageText = messages.GetComponentsInChildren<Text>();
    }

    #region ResourcesInfo
    public void DrawMessage(string msg)
    {
        messageText[0].text = msg;
    }
    #endregion



    #region ResourcesInfo
    public void DrawResourcesInfo()
    {
        resourcesInfoText[0].text = "Gold : " + LevelManger.Instance.GetCurrentGold();
    }

    #endregion


    #region WaveInfo
    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave : " + LevelManger.Instance.GetCurrentWave();
        waveInfoText[1].text = "Enemys left : " + LevelManger.Instance.GetEnemysLeft();
        waveInfoText[2].text = "Lives left : " + LevelManger.Instance.GetLivesLeft();
    }
    #endregion

}
