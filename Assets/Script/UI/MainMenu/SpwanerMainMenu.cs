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

        while (true)
        {
            yield return new WaitForSeconds(3);
            Instantiate(Fire, this.transform.position, this.transform.rotation);
                      
            yield return new WaitForSeconds(3);
            Instantiate(Water, this.transform.position, this.transform.rotation);
            
            yield return new WaitForSeconds(3);
            Instantiate(Green, this.transform.position, this.transform.rotation);      
        }

    }

}


 