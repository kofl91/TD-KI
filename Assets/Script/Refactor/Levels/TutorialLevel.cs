using UnityEngine;
using System.Collections;

public class TutorialLevel : Level {

    public override void StartLevel()
    {
        map = new FirstMap();
        map.createMap();
    }

    
}
