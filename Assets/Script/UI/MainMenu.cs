using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public RectTransform currentPanel;
    
    public void SwitchToPanel(RectTransform nextPanel)
    {
        currentPanel.gameObject.SetActive(false);
        nextPanel.gameObject.SetActive(true);
        currentPanel = nextPanel;
    }

}
