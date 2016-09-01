using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class ChangeToMainMenu : MonoBehaviour {


    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
        LobbyManager.s_Singleton.DisplayMainPanel(true);
    }
}
