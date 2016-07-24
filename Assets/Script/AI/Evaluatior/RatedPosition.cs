using System;

public class RatedPosition : IEquatable<RatedPosition>, IComparable<RatedPosition>
{
    // TODO: Create own file for Structures
    public TileStructure tile;
    public int rating;

    public RatedPosition(TileStructure t, int rating)
    {
        tile = t;
        this.rating = rating;
    }

    public int CompareTo(RatedPosition other)
    {
        return other.rating - rating;
    }

    public bool Equals(RatedPosition other)
    {
        return rating == other.rating;
    }
}