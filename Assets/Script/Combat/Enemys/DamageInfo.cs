using UnityEngine;
using System.Collections;
using System;

public class DamageInfo {
    public float normal = 0;
    public float fire = 0;
    public float water = 0;
    public float nature = 0;

    public void setNeutralResistance()
    {
        normal = 1.0f;
        fire = 1.0f;
        water = 1.0f;
        nature = 1.0f;
    }

    internal float calcAbsoluteDmg(DamageInfo dmgfactor)
    {
        float absDmg = normal * dmgfactor.normal +
            fire * dmgfactor.fire +
            water * dmgfactor.water +
            nature * dmgfactor.nature;
        return absDmg;
    }
}
