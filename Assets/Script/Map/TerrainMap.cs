using UnityEngine;
using System.Collections;
using System;

public class TerrainMap : Map
{
    public override void createMap()
    {
        mapSizeX = 19;
        mapSizeY = 21;
        tileSize = 30;
        xOffset = 220;
        yOffset = 193;
        grid = new eTileType[mapSizeX, mapSizeY];
        clearMap();
        
        setWayFromTo(13, 0, 13, 4);
        setWayFromTo(14, 0, 14, 4);

        setWayFromTo(1, 3, 14, 3);
        setWayFromTo(1, 4, 14, 4);

        setWayFromTo(1, 3, 1, 18);
        setWayFromTo(2, 3, 2, 18);

        setWayFromTo(1, 16, 18, 16);
        setWayFromTo(1, 17, 18, 17);
        setWayFromTo(1, 18, 18, 18);

        
        setWayFromTo(18, 19, 18, 20);
        setWayFromTo(17, 19, 17, 20);


        setWayFromTo(10, 7, 10, 14);
        setWayFromTo(11, 7, 11, 14);
        setWayFromTo(12, 7, 12, 14);
        setWayFromTo(13, 7, 13, 14);
        setWayFromTo(14, 7, 14, 14);
        setWayFromTo(15, 7, 15, 14);
        setWayFromTo(16, 7, 16, 14);
        setWayFromTo(17, 7, 17, 14);
        setWayFromTo(18, 7, 18, 14);

        setWayFromTo(0, 18, 3, 18);
        setWayFromTo(0, 19, 3, 19);
        setWayFromTo(0, 20, 3, 20);

        GenerateMapVisual();
    }
}
