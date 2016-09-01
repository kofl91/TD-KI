using UnityEngine;
using System.Collections;

public class ShowOnHover : MonoBehaviour {

	public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
