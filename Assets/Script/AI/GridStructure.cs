using UnityEngine;

public enum eTile { Free, Path, Tower, Else };

public class TileStructure
{
    
    public int xPos;
    public int yPos;
    public eTile type;
    public GameObject obj;

    public TileStructure(int x, int y, eTile t, GameObject go)
    {
        xPos = x;
        yPos = y;
        type = t;
        obj = go;
    }
}

public class GridStructure
{
    public int sizeX;
    public int sizeY;
    public TileStructure[,] tiles;

    public GridStructure(int x, int y, TileStructure[,] tsgrid)
    {
        sizeX = x;
        sizeY = y;
        tiles = tsgrid;
    }

    public eTile GetTypeOfPosition(int x, int y)
    {
        return tiles[x, y].type;
    }
}