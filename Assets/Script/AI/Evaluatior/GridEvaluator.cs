using UnityEngine;
using System.Collections.Generic;

public class GridEvaluator
{

    // Range of the Tower
    public int range = 3;

    // The grid of the game
    public GridStructure grid;

    // Positions rated and sorted by area a tower could hit
    List<RatedPosition> ratedPositions = new List<RatedPosition>();

    public RatedPosition GetNextPosition()
    {
        int index = Random.Range(0, 10);
        RatedPosition returnPosition = ratedPositions[index];
        ratedPositions.Remove(returnPosition);
        return returnPosition;
    }

    public GridEvaluator(GridStructure gridStructure)
    {
        this.grid = gridStructure;
        evaluateGrid();
    }

    // Rates the grid by area a tower could hit
    public void evaluateGrid()
    {
        ratedPositions.Clear();
        for (int x = 0; x < grid.sizeX; x++)
        {
            for (int y = 0; y < grid.sizeY; y++)
            {
                ratedPositions.Add(new RatedPosition(grid.tiles[x, y], EvaluateTile(x, y)));
            }
        }
        ratedPositions.Sort();
    }


    public void UpdateRating()
    {
        foreach (RatedPosition rp in ratedPositions)
        {
            rp.rating = EvaluateTile(rp.tile.xPos, rp.tile.yPos);
        }
        ratedPositions.Sort();
    }
    // Rates a tile by area a tower could hit
    public int EvaluateTile(int xPos, int yPos)
    {
        int temporaryRating = 0;

        if (grid.GetTypeOfPosition(xPos, yPos) == eTile.Free)
        {
            for (int x = xPos - range; x < xPos + range; x++)
            {
                if ((x >= 0) && (x < grid.sizeX))
                {
                    for (int y = yPos - range; y < yPos + range; y++)
                    {
                        if ((y >= 0) && (y < grid.sizeY) &&  // is in y Bound 
                            (grid.GetTypeOfPosition(x, y) == eTile.Path))
                        {
                            temporaryRating++;
                            temporaryRating += grid.tiles[x, y].minionsPassed;
                        }
                    }
                }
            }
        }
        return temporaryRating;
    }

    // Changes the color of a tile for debug purpose
    public void ChangeColorOfTile(int x, int y, Color newColor)
    {
        grid.tiles[x, y].obj.GetComponent<MeshRenderer>().material.color = newColor;
    }

    public void ResetMinionsPassed()
    {
        for (int x = 0; x < grid.sizeX; x++)
        {
            for (int y = 0; y < grid.sizeY; y++)
            {
                grid.tiles[x, y].minionsPassed = 0;
                grid.tiles[x, y].obj.GetComponent<MeshRenderer>().material.color = Color.red;

            }
        }
    }

    public void DisplayTopTenPositions()
    {
        if (ratedPositions.Count == 0)
        {
            evaluateGrid();
        }
        else
        {
            UpdateRating();
        }
        for (int i = 0; i < 10; i++)
        {
            if (ratedPositions.Count > i)
            {
                ratedPositions[i].tile.obj.GetComponent<MeshRenderer>().enabled = true;
                if (ratedPositions[i].rating > 5)
                    ratedPositions[i].tile.obj.GetComponent<MeshRenderer>().material.color = Color.yellow;
                if (ratedPositions[i].rating > 10)
                    ratedPositions[i].tile.obj.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }

    public int GetLowestTopRating()
    {
        // Return the 4th rated Position. Lower if there are less.
        return ratedPositions[10 % ratedPositions.Count].rating;
    }

    public RatedPosition BadlyPlacedTower()
    {
        foreach (RatedPosition r in ratedPositions)
        {
            if ((r.tile.type == eTile.Tower) && (r.rating < GetLowestTopRating()))
            {
                return r;
            }
        }
        return null;
    }

    public void ShowBestPositions()
    {
        foreach (RatedPosition r in ratedPositions)
        {
            r.tile.obj.GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < 10; i++)
        {
            ChangeColorOfTile(ratedPositions[i].tile.xPos, ratedPositions[i].tile.yPos, Color.green);
            ratedPositions[i].tile.obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
