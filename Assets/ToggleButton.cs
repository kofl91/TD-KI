using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleButton : MonoBehaviour {

    protected Button[] buttons;


    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    public void SelectButton(Button selectedButton)
    {
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
        selectedButton.interactable = false;
    }
}
