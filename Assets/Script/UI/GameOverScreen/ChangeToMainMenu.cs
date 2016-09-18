using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class ChangeToMainMenu : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
        LobbyManager.s_Singleton.DisplayMainPanel(true);
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
    }


    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
    #else
        Application.Quit();
    #endif
    }
}
