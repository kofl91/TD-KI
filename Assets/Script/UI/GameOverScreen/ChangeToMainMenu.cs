using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeToMainMenu : MonoBehaviour {


    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
