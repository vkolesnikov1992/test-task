﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    public GameObject defaultCube;
    public GameObject lavaCube;
    public GameObject charapter;
    public GameObject enemyCube;
    public GameObject finishCube;
    [SerializeField]
    private int width;
    [SerializeField] 
    private int height;
    private int[,] cubesArray;
    public GameObject[,] gameObjects;
    private List<Vector3> fullPath = new List<Vector3>();



    PathFinding pathF;

    void Start()
    {
        pathF = new PathFinding(width, height);
        cubesArray = new int[width, height];
        gameObjects = new GameObject[width, height];


        for (int x = 0; x < cubesArray.GetLength(0); x++)
        {
            for (int y = 0; y < cubesArray.GetLength(1); y++)
            {
                if (y == 0)
                {
                    gameObjects[x, y] = Instantiate(lavaCube, new Vector3(x + 0.5f, y + 0.5f, 0), transform.rotation);

                }
                else
                {
                    gameObjects[x, y] = Instantiate(defaultCube, new Vector3(x + 0.5f, y + 0.5f, 0), transform.rotation);
                }


            }
        }

        int steps;
        steps = (width - 2) / 3;        
        if((width - 2) % 3 != 0)
        {
            steps++;
        }
        Context context = new Context();
        context.SetStrategy(new GetHeight());
        int step = 1;
        int previousPos = 0;        
        int random;
        int charCount = 0;


        List<Vector3> fullPath = new List<Vector3>();

        for (int i = 0; i < steps; i++)
        {
           
            List<PathNote> pathNotes;            
            random = context.Random(height);
            
            while (random == previousPos)
            {
                random = context.Random(height);                
            }
            
            
            if (i == 0)
            {
                pathNotes = pathF.FindPath(step, Random.Range(2, height - 3), step + 3, random);
                step += 2;
                
            }

            else if ((width - 2) % 3 != 0 && steps == i + 1)
            {                                
                    pathNotes = pathF.FindPath(step, previousPos, step + (width - 2) % 3, random);
                
            }
            
            else
            {                
                    pathNotes = pathF.FindPath(step, previousPos, step += 3, random);                    
                           

            }




            

            if (pathNotes != null)
            {
                for (int j = 0; j < pathNotes.Count; j++)
                {
                    
                        Destroy(gameObjects[pathNotes[j].x, pathNotes[j].y]);
                        Destroy(gameObjects[pathNotes[j].x, pathNotes[j].y + 1]);
                        Destroy(gameObjects[pathNotes[j].x, pathNotes[j].y + 2]);
                    
                    
                    if (charCount == 0)
                    {
                        Instantiate(charapter, new Vector3(pathNotes[j].x, pathNotes[j].y), transform.rotation);
                        charCount++;
                    }                   
                    
                    
                    previousPos = pathNotes[j].y;

                    if(j != 0)
                    {
                        fullPath.Add(new Vector3(pathNotes[j].x, pathNotes[j].y));
                    }
                   

                    
                    
                    

                }
            }           
            
        }

        int enemyCounter = width / 10;

        if (fullPath != null)
        {

            
            int stepEnemy = (fullPath.Count - 2) / 3;
            int secondStep = stepEnemy;
            
            for (int f = 0; f < fullPath.Count; f++)
            {
                if(enemyCounter != 0)
                {
                    Debug.Log(stepEnemy);

                    Instantiate(enemyCube, fullPath[stepEnemy], transform.rotation);
                    enemyCounter--;
                    stepEnemy += secondStep;
                    
                }
                if(fullPath.Count == f + 1)
                {
                    Instantiate(finishCube, new Vector3(fullPath[f].x + 0.5f, fullPath[f].y + 0.5f), transform.rotation);
                }
            }
        }
        
    }

    
    
}
