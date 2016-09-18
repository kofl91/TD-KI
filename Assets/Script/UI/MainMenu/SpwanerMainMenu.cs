using UnityEngine;
using System.Collections;

public class SpwanerMainMenu : MonoBehaviour {

    public GameObject Fire;
    public GameObject Water;
    public GameObject Green;
    public GameObject target;


    void Start()
    {
        StartCoroutine(Wave());
    }


    IEnumerator Wave()
    {
        GameObject go;
        while (true)
        {
            go = Instantiate(Fire, this.transform.position, this.transform.rotation)as GameObject;
            go.transform.SetParent(transform);     
            yield return new WaitForSeconds(3);

            go = Instantiate(Water, this.transform.position, this.transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            yield return new WaitForSeconds(3);

            go = Instantiate(Green, this.transform.position, this.transform.rotation) as GameObject;
            go.transform.SetParent(transform);
            yield return new WaitForSeconds(3);

        }

    }

}


 