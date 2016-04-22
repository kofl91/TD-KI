using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoSingleton<UIManager> {
    public GameObject generalMessageContainer;
    private List<GeneralMessage> generalMessages;

    public void showGeneralMessage(string msg,Color color,float duration)
    {
       /* GeneralMessage gm = generalMessages.find(m => m.go == generalMessageContainer.transform.GetChild(0).gameObject);
        gm.txt.text = msg;
        gm.txt.color = color;
        gm.duration = duration;
        gm.lastShow = Time.time;
        gm.isActive = true;
        gm.go.SetActive(true);*/

    }

    private class GeneralMessage
    {
        public bool isActive = false;
        public GameObject go;
        //public Text txt;
        public float duration;
        public float lastShow;
    }
}
