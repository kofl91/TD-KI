using System;

[Serializable]
// Eine Klasse die Schadenswerte und Resistenzen abbildet
public class DamageInfo {
    public float normal = 0;
    public float fire = 0;
    public float water = 0;
    public float nature = 0;

    // Initialisierung für Standard Resistenzen
    public void setNeutralResistance()
    {
        normal = 1.0f;
        fire = 1.0f;
        water = 1.0f;
        nature = 1.0f;
    }

    public DamageInfo Multiply(float dmgFactor)
    {
        normal *= dmgFactor;
        fire *= dmgFactor;
        water *= dmgFactor;
        nature *= dmgFactor;
        return this;
    }

    // Berechnet den Absoluten Schaden gegen eine Resistenz
    internal float calcAbsoluteDmg(DamageInfo resistance)
    {
        float absDmg =  normal * resistance.normal +
                        fire * resistance.fire +
                        water * resistance.water +
                        nature * resistance.nature;
        return absDmg;
    }

    // Schaden gegen neutrale Resistenz
    internal float calcDmgVsNeutralResistance()
    {
        float absDmg = normal  +
            fire  +
            water  +
            nature ;
        return absDmg;
    }

    // Addiert Schaden hinzu
    internal void Add(DamageInfo dmg)
    {
        this.normal += dmg.normal;
        this.fire += dmg.fire;
        this.water += dmg.water;
        this.nature += dmg.nature;
    }

    // Setzt Schadenswerte
    internal void Set(float normal, float fire, float water, float nature)
    {
        this.normal = normal;
        this.fire = fire;
        this.water = water;
        this.nature = nature;
    }

    internal string GetDamageType()
    {
        if ((normal >= fire) && (normal >= water) && (normal >= nature))
        {
            return "Normal";
        }
        else if ((fire >= water) && (fire >= nature))
        {
            return "Fire";
        }
        else if (water >= nature)
        {
            return "Water";
        }
        else
        {
            return "Nature";
        }
    }

    internal void Add(object v)
    {
        throw new NotImplementedException();
    }
}
