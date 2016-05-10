using UnityEngine;
using System.Collections;
using System;

public class FirstMap : Map
{
    public override void createMap()
    {

        mapSizeX = 20;
        mapSizeY = 20;

        grid = new eTileType[mapSizeX, mapSizeY];

        clearMap();
        startWayFromTo(3, 0, 3, 3);
        continueWayTo(17, 3);
        continueWayTo(17, 5);
        continueWayTo(3, 5);
        continueWayTo(3, 8);
        continueWayTo(15, 8);
        continueWayTo(15, 13);
        continueWayTo(8, 13);
        continueWayTo(8, 16);
        continueWayTo(18, 16);
        endWayAt(18, 19);

        GenerateMapVisual();
    }
}
