using UnityEngine;
using System.Collections;
using System;

public class RatedTower : IEquatable<RatedTower>, IComparable<RatedTower>
{

    public TowerStructure tower;

    public float rating;

    public RatedTower(TowerStructure tower, float rating)
    {
        this.tower = tower;
        this.rating = rating;
    }

    public bool Equals(RatedTower other)
    {
        return rating == other.rating;
    }

    public int CompareTo(RatedTower other)
    {
        return (int) ((other.rating - rating)*1000);
    }
}
