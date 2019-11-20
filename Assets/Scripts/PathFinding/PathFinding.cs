using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    #region Private Fields
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14; 

    private GridSystem<PathNote> _grid;
    private List<PathNote> _openList;
    private List<PathNote> _closedList;
    #endregion

    #region Public Methods
    public PathFinding(int width, int height)
    {
        _grid = new GridSystem<PathNote>(width, height, 1f, Vector3.zero, (GridSystem<PathNote> g, int x, int y) => new PathNote(g, x, y)); 
    }

    public GridSystem<PathNote> GetGrid()
    {
        return _grid;
    }

    public List<PathNote> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNote startNote = _grid.GetGridObject(startX, startY);
        PathNote endNote = _grid.GetGridObject(endX, endY);

        _openList = new List<PathNote> { startNote };
        _closedList = new List<PathNote>();

        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                PathNote pathNote = _grid.GetGridObject(x, y);
                pathNote.gCost = int.MaxValue;
                pathNote.CalculateFCost();
                pathNote.cameFromNote = null;
            }
        }

        startNote.gCost = 0;
        startNote.hCost = CalculateDistanceCost(startNote, endNote);
        startNote.CalculateFCost();

        while(_openList.Count > 0)
        {
            PathNote currentNote = GetLowestFCostNote(_openList);
            if(currentNote == endNote)
            {
                return CalculatePath(endNote);
            }
            _openList.Remove(currentNote);
            _closedList.Add(currentNote);

            foreach(PathNote neighbourNote in GetNeighbourList(currentNote))
            {
                if (_closedList.Contains(neighbourNote)) continue;

                int tentativeGCost = currentNote.gCost + CalculateDistanceCost(currentNote, neighbourNote);
                if(tentativeGCost < neighbourNote.gCost)
                {
                    neighbourNote.cameFromNote = currentNote;
                    neighbourNote.gCost = tentativeGCost;
                    neighbourNote.hCost = CalculateDistanceCost(neighbourNote, endNote);
                    neighbourNote.CalculateFCost();

                    if (!_openList.Contains(neighbourNote))
                    {
                        _openList.Add(neighbourNote);
                    }
                }
            }
        }

        return null;
    }

    public PathNote GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
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
            if (currentNote.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x - 1, currentNote.y + 1));
        }
        if (currentNote.x + 1 < _grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y));
            // Right Down
            if (currentNote.y - 1 >= 0) neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y - 1));
            // Right Up
            if (currentNote.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x + 1, currentNote.y + 1));
        }
        // Down
        if (currentNote.y - 1 >= 0) neighbourList.Add(GetNode(currentNote.x, currentNote.y - 1));
        // Up
        if (currentNote.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNote.x, currentNote.y + 1));

        return neighbourList;
    }
    #endregion

    #region Private Methods
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
    #endregion
}
