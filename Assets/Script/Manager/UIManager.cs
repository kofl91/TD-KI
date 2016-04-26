using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoSingleton<UIManager> {

    public GameObject root;
    public GameObject waveInfo;

    private Text[] waveInfoText;

    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
    }

    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave : " + 0;
        waveInfoText[1].text = "Enemys left : " + 0;
        waveInfoText[2].text = "Lives left : " + 0;
    }

}
