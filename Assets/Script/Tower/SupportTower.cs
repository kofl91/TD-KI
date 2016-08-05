using UnityEngine;

// Ein Turm der andere Türme bufft
public class SupportTower : BaseTower {

    // Der Support Turm schießt nicht. Daher muss die Action überschrieben werden.
    protected override void Action(Transform t)
    {
        lastAction = Time.time;
    }
}
