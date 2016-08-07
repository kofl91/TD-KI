using UnityEngine;
using UnityEngine.Networking;

public class TempPlayer : NetworkBehaviour {

    public GameObject go;

    void Update()
    {
        if (isLocalPlayer)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
            if (Input.GetKeyDown("space"))
            {
                CmdSpawnBox();
            }
                
        }      
    }

    [Command]
    void CmdSpawnBox()
    {
        GameObject tree = (GameObject)Instantiate(go, transform.position, transform.rotation);
        tree.transform.position = transform.position;
        NetworkServer.Spawn(tree);
    }

    [Command]
    public void CmdSpawn(GameObject go)
    {
        NetworkServer.Spawn(go);
    }
}
