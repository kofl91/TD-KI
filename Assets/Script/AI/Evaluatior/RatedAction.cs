using UnityEngine;
using System.Collections;
using System;

public enum Action { Nothing, Build, Send, BuildGoldTower , Destroy, Upgrade };

public class RatedAction: IEquatable<RatedAction>, IComparable<RatedAction>
{
    public Action action;
    public float rating;

    public RatedAction(Action a, float r)
    {
        action = a;
        rating = r;
    }

    public int CompareTo(RatedAction other)
    {
        return (int)(other.rating - rating);
    }

    public bool Equals(RatedAction other)
    {
        return rating == other.rating;
    }
}
