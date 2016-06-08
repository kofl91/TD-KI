using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Canvas quitMenu;
    public Button playButton;
    public Button exitButton;

    // Use this for initialization
    void Start () {

        quitMenu = quitMenu.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        exitButton.enabled = true;
        playButton.enabled = true;
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        exitButton.enabled = false;
        playButton.enabled = false;
    }
    public void PlayPress()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }



    // Update is called once per frame
    void Update () {
	
	}
}
