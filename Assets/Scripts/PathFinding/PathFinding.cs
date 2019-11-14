using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14; 

    private GridSystem<PathNote> grid;
    private List<PathNote> openList;
    private List<PathNote> closedList;

    public PathFinding(int width, int height)
    {
        grid = new GridSystem<PathNote>(width, height, 1f, Vector3.zero, (GridSystem<PathNote> g, int x, int y) => new PathNote(g, x, y)); 
    }

    public GridSystem<PathNote> GetGrid()
    {
        return grid;
    }

    public List<PathNote> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNote startNote = grid.GetGridObject(startX, startY);
        PathNote endNote = grid.GetGridObject(endX, endY);

        openList = new List<PathNote> { startNote };
        closedList = new List<PathNote>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                PathNote pathNote = grid.GetGridObject(x, y);
                pathNote.gCost = int.MaxValue;
                pathNote.CalculateFCost();
                pathNote.cameFromNote = null;
            }
        }

        startNote.gCost = 0;
        startNote.hCost = CalculateDistanceCost(startNote, endNote);
        startNote.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNote currentNote = GetLowestFCostNote(openList);
            if(currentNote == endNote)
            {
                return CalculatePath(endNote);
            }
            openList.Remove(currentNote);
            closedList.Add(currentNote);

            foreach(PathNote neighbourNote in GetNeighbourList(currentNote))
            {
                if (closedList.Contains(neighbourNote)) continue;

                int tentativeGCost = currentNote.gCost + CalculateDistanceCost(currentNote, neighbourNote);
                if(tentativeGCost < neighbourNote.gCost)
                {
                    neighbourNote.cameFromNote = currentNote;
                    neighbourNote.gCost = tentativeGCost;
                    neighbourNote.hCost = CalculateDistanceCost(neighbourNote, endNote);
                    neighbourNote.CalculateFCost();

                    if (!openList.Contains(neighbourNote))
                    {
                        openList.Add(neighbourNote);
                    }
                }
            }
        }

        return null;
    }
    
    private List<PathNote> GetNeighbourList(PathNote currentNote)
    {
        List<PathNote> neighbourList = new List<PathNote>();

        if (currentNote.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNote.x - 1, currentNote.y));
            // Left Down
            if (currentNote.y - 1 >= 0) neighbourList.Add(GetNode(currentNote.x - 1, currentNote.y - 1));
            // Left Up
            if (currentNote.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x - 1, currentNote.y + 1));
        }
        if (currentNote.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y));
            // Right Down
            if (currentNote.y - 1 >= 0) neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y - 1));
            // Right Up
            if (currentNote.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y + 1));
        }
        // Down
        if (currentNote.y - 1 >= 0) neighbourList.Add(GetNode(currentNote.x, currentNote.y - 1));
        // Up
        if (currentNote.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x, currentNote.y + 1));

        return neighbourList;
    }

    public PathNote GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNote> CalculatePath(PathNote endNote)
    {
        List<PathNote> path = new List<PathNote>();
        path.Add(endNote);
        PathNote currentNote = endNote;
        while(currentNote.cameFromNote != null)
        {
            path.Add(currentNote.cameFromNote);
            currentNote = currentNote.cameFromNote;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNote a, PathNote b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNote GetLowestFCostNote(List<PathNote> pathNoteList)
    {
        PathNote lowestFCostNote = pathNoteList[0];
        for(int i = 1; i < pathNoteList.Count; i++)
        {
            if(pathNoteList[i].fCost < lowestFCostNote.fCost)
            {
                lowestFCostNote = pathNoteList[i];
            }
        }
        return lowestFCostNote;
    }
}
