using UnityEngine;
using System.Collections;

public class InfoScript : MonoBehaviour {

    public GameObject bluePanel;
    public GameObject redPanel;
    public GameObject greenPanel;
    public GameObject buffPanel;
    public GameObject normalPanel;
    public GameObject priestPanel;

  
    public void InfoBlue()
    {

        bluePanel.SetActive(true);
        redPanel.SetActive(false);
        greenPanel.SetActive(false);
        buffPanel.SetActive(false);
        normalPanel.SetActive(false);
        priestPanel.SetActive(false);
    }

    public void InfoRed()
    {

        bluePanel.SetActive(false);
        redPanel.SetActive(true);
        greenPanel.SetActive(false);
        buffPanel.SetActive(false);
        normalPanel.SetActive(false);
        priestPanel.SetActive(false);
    }

    public void InfoGreen()
    {

        bluePanel.SetActive(false);
        redPanel.SetActive(false);
        greenPanel.SetActive(true);
        buffPanel.SetActive(false);
        normalPanel.SetActive(false);
        priestPanel.SetActive(false);
    }
    public void InfoNormal()
    {

        bluePanel.SetActive(false);
        redPanel.SetActive(false);
        greenPanel.SetActive(false);
        buffPanel.SetActive(false);
        normalPanel.SetActive(true);
        priestPanel.SetActive(false);
    }

    public void InfoPriest()
    {

        bluePanel.SetActive(false);
        redPanel.SetActive(false);
        greenPanel.SetActive(false);
        buffPanel.SetActive(false);
        normalPanel.SetActive(false);
        priestPanel.SetActive(true);
    }

    public void InfoBuff()
    {

        bluePanel.SetActive(false);
        redPanel.SetActive(false);
        greenPanel.SetActive(false);
        buffPanel.SetActive(true);
        normalPanel.SetActive(false);
        priestPanel.SetActive(false);
    }




    void Update () {
	
    
	}
}
