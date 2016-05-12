using UnityEngine;
using System.Collections;
using System;

public class SecondMap : Map {

    public override void createMap()
    {
        try
        {
            mapSizeX = 6;
            mapSizeY = 6;

            grid = new eTileType[mapSizeX, mapSizeY];

            clearMap();
            startWayFromTo(0, 2, 3, 2);
            continueWayTo(3, 4);
            endWayAt(5, 4);

            GenerateMapVisual();
        }
        catch (Exception e)
        {

        }
    }
}
