using UnityEngine;
using System.Collections;

public class CloseGame : MonoBehaviour {
    public GameObject closePanel;
	public void ChooseClose()
    {
        closePanel.SetActive(true);
    }
    public void Close()
    {
        Application.Quit();
    }
    public void Back()
    {
        closePanel.SetActive(false);
    }
	
}
