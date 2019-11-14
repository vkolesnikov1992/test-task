﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridSystemObject> 
{
    private int width;
    private int height;
    private float cellSize;
    private TGridSystemObject[,] gridArray;
    private Vector3 originPosition;
    

    



    public GridSystem(int width, int heigth, float cellSize, Vector3 originPosition, Func<GridSystem<TGridSystemObject>, int, int, TGridSystemObject> createTGridSystemObject)
    {
        this.width = width;
        this.height = heigth;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridSystemObject[width, heigth];
        

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
               // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 300f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 300f);
                gridArray[x, y] = createTGridSystemObject(this, x, y);
                
                
            }
        }
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public TGridSystemObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridSystemObject);
        }
    }

    public TGridSystemObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
