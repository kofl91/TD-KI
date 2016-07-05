using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerUI : MonoBehaviour, IBelongsToPlayer {

    private Text[] texts;

    private Button[] towerButton;

    private Scrollbar speedbar;

    private PlayerController player;
    public int playerId = 0;

	// Use this for initialization
	void Start () {
        texts = GetComponentsInChildren<Text>();
        speedbar = GetComponentInChildren<Scrollbar>();
        towerButton = GetComponentsInChildren<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        if (texts.Length > 0)
        {
            texts[0].text = "Lives :" + player.Life;
        }
        if (texts.Length > 1)
        {
            texts[1].text = "Gold :" + player.Gold;
        }
        if (speedbar)
        {
            Time.timeScale = speedbar.value + 1.0f;
        }
    }

    public void SetPlayer(int id)
    {
        playerId = id;

    }

    public PlayerController GetPlayer()
    {
        return GameObject.FindObjectsOfType<PlayerController>()[playerId - 1];
    }
}
