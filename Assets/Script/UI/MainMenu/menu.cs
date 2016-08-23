using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Prototype.NetworkLobby;

public class menu : MonoBehaviour {

   
    public GameObject backPlay;
    public GameObject playVsCom;
    public GameObject plyVsply;
    public GameObject exit;
    public GameObject modus;
    public List<Scene> Level;

    bool moCom = false;
    bool moPly = false;
    bool com = false;
    bool net = false;
    bool ex = false;

	
    public void BackPlay()
    {
        if (moPly == true)
        {
            
            modus.SetActive(false);
            moPly = false;
            
        }
        if (net == true)
        {
            plyVsply.SetActive(false);
            modus.SetActive(true);
            moPly = true;
            net = false;
        }

        if (moCom == true)
        {
            modus.SetActive(false);
            moCom = false;

        }
        if (com == true)
        {
            Debug.Log("´back");
            playVsCom.SetActive(false);
            modus.SetActive(true);
            moCom = true;
            com = false;
        }
    }

    public void PopPlayVsComMod()
    {
        moCom = true;
       
        if(net == false && ex == false)
        {
            modus.SetActive(true);
        }
    }

    public void PlayVsPlayMod()
    {
        moPly = true;
        
        
       if(com == false && ex == false)
       {            
            modus.SetActive(true);
       }      
    }

    public void OnClickLevelChoice(string levelname)
    {
        if (net == false && ex == false && moCom == true)
        {
            LobbyManager.s_Singleton.playScene = levelname; //+" - 2P";
            LobbyManager.s_Singleton.StartHost();
            modus.SetActive(false);
            playVsCom.SetActive(true);
            com = true;
        }
    }

    public void NextStep()
    {
        if (com == false && ex == false && moPly == true)
        {            
            Debug.Log("MUSS ERST ERSTELLT WERDEN!!");
            modus.SetActive(false);
            plyVsply.SetActive(true);
            net = true;
            moPly = false;
        }

      if (net == false && ex == false && moCom == true)
        {
            Debug.Log("nextStep");
            modus.SetActive(false);
            playVsCom.SetActive(true);
            com = true;

        }
    }

    public void Close()
    {
        ex = true;
        if (net == false && com == false && moCom == false && moPly == false)
        {
            exit.SetActive(true);
        }
        
    }
    public void CloseYes()
    {
        Application.Quit();
    }
    public void CloseNo()
    {
        exit.SetActive(false);
        ex = false;
        com = false;
        net = false;
    }
	
    public void Easy()
    {

    }

    public void Normal()
    {

        Debug.Log("MUSS ERST ERSTELLT WERDEN!!");
    }
    public void Hard()
    {
        //SceneManager.LoadScene();
        Debug.Log("MUSS ERST ERSTELLT WERDEN!!");
    }

    public void StartGame()
    {
        LobbyManager.s_Singleton.ServerChangeScene(LobbyManager.s_Singleton.playScene);
    }
}
