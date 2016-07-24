using UnityEngine;
using System.Collections.Generic;

public class GridEvaluator {

    // Range of the Tower
    public int range = 3;

    // The grid of the game
    GridStructure grid;

    // Positions rated and sorted by area a tower could hit
    List<RatedPosition> ratedPositions;

    public RatedPosition GetNextPosition()
    {
        RatedPosition returnPosition = ratedPositions[0];
        ratedPositions.Remove(returnPosition);
        return returnPosition;
    }

    public GridEvaluator(GridStructure gridStructure)
    {
        this.grid = gridStructure;
        evaluateGrid();
    }

    // Rates the grid by area a tower could hit
    private void evaluateGrid()
    {
        ratedPositions = new List<RatedPosition>();
        for (int x = 0; x < grid.sizeX; x++)
        {
            for (int y = 0; y < grid.sizeY; y++)
            {
                ratedPositions.Add(new RatedPosition(grid.tiles[x, y], evaluateTile(x, y)));
            }
        }
        ratedPositions.Sort();
    }

    // Rates a tile by area a tower could hit
    private int evaluateTile(int xPos, int yPos)
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
                        }
                    }
                }
            }
        }
        // TODO: REMOVE
        // Change Color of Tiles for Debugging 
        if (temporaryRating > 5)
            ChangeColorOfTile(xPos, yPos, Color.yellow);
        if (temporaryRating > 10)
            ChangeColorOfTile(xPos, yPos, Color.green);

        return temporaryRating;
    }

    // Changes the color of a tile for debug purpose
    public void ChangeColorOfTile(int x, int y, Color newColor)
    {
        GameObject go = grid.tiles[x, y].obj;

        MeshRenderer gameObjectRenderer = go.GetComponent<MeshRenderer>();

        gameObjectRenderer.material.color = newColor;
    }
}
