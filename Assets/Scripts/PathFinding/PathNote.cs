using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNote
{
    public GridSystem<PathNote> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNote cameFromNote;

    public PathNote(GridSystem<PathNote> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

   
}
