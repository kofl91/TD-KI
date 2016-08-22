using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinConditionChecker : MonoBehaviour {

    protected PlayerController[] player;

    bool init = false;

	// Update is called once per frame
	void Update () {
        if (!init)
        {
            player = FindObjectsOfType<PlayerController>();
            if (player.Length >= 2)
            {
                init = true;
            }
            else
                return;
        }

        if (player[0].Life <= 0)
        {
            //SceneManager.LoadScene("GamerOver");
        }
        if (player[1].Life <= 0)
        {
           // SceneManager.LoadScene("VictoryScreen");
        }
    }
}
