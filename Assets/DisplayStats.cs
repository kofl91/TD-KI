using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayStats : MonoBehaviour {

    public Text lifeText;
    public Text goldText;
    public Text sendMinionsText;

    // Use this for initialization
    void Start () {
        ScoreManager sm = FindObjectOfType<ScoreManager>();
        lifeText.text = sm.ownLife + "/20";
        goldText.text = sm.remainingGold + "";
        sendMinionsText.text = sm.sendMinions + "";
    }
}
