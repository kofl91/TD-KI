using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public int ownLife;

    public int sendMinions;

    public int remainingGold;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
    }

}
