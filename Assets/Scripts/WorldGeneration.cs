using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private GameObject _defaultCube;
    [SerializeField]
    private GameObject _lavaCube;
    [SerializeField]
    private GameObject _charapter;
    [SerializeField]
    private GameObject _enemyCube;
    [SerializeField]
    private GameObject _finishCube;

    [SerializeField]
    private int _width;
    [SerializeField] 
    private int _height;   

    private GameObject[,] _gameObjects;
    private List<Vector3> _fullPath = new List<Vector3>();
    private PathFinding _pathF;
    #endregion

    #region Start
    void Start()
    {
        _pathF = new PathFinding(_width, _height);        
        _gameObjects = new GameObject[_width, _height];


        InstantiateBlocks();
        LevelGeneration();
        EnemyGeneration();
       
        
    }
    #endregion

    #region Private Methods
    void InstantiateBlocks()
    {
        for (int x = 0; x < _gameObjects.GetLength(0); x++)
        {
            for (int y = 0; y < _gameObjects.GetLength(1); y++)
            {
                if (y == 0)
                {
                    _gameObjects[x, y] = Instantiate(_lavaCube, new Vector3(x + 0.5f, y + 0.5f, 0), transform.rotation);

                }
                else
                {
                    _gameObjects[x, y] = Instantiate(_defaultCube, new Vector3(x + 0.5f, y + 0.5f, 0), transform.rotation);
                }
            }
        }
    }

    void LevelGeneration()
    {
        int steps = (_width - 2) / 3;                
        int step = 1;
        int previousPos = 0;
        int random;
        int charCount = 0;

        Context context = new Context();
        context.SetStrategy(new GetHeight());
         

        if ((_width - 2) % 3 != 0)
        {
            steps++;
        }


        for (int i = 0; i < steps; i++)
        {

            List<PathNote> pathNotes;
            random = context.Random(_height);

            while (random == previousPos)
            {
                random = context.Random(_height);
            }


            if (i == 0)
            {
                pathNotes = _pathF.FindPath(step, Random.Range(2, _height - 3), step + 3, random);
                step += 2;
            }

            else if ((_width - 2) % 3 != 0 && steps == i + 1)
            {
                pathNotes = _pathF.FindPath(step, previousPos, step + (_width - 2) % 3, random);
            }

            else
            {
                pathNotes = _pathF.FindPath(step, previousPos, step += 3, random);
            }



            if (pathNotes != null)
            {
                for (int j = 0; j < pathNotes.Count; j++)
                {

                    Destroy(_gameObjects[pathNotes[j].x, pathNotes[j].y]);
                    Destroy(_gameObjects[pathNotes[j].x, pathNotes[j].y + 1]);
                    Destroy(_gameObjects[pathNotes[j].x, pathNotes[j].y + 2]);


                    if (charCount == 0)
                    {
                        Instantiate(_charapter, new Vector3(pathNotes[j].x, pathNotes[j].y), transform.rotation);
                        charCount++;
                    }


                    previousPos = pathNotes[j].y;

                    if (j != 0)
                    {
                        _fullPath.Add(new Vector3(pathNotes[j].x, pathNotes[j].y));
                    }
                }
            }

        }
    }

    void EnemyGeneration()
    {
        int enemyCounter = _width / 10;

        if (_fullPath != null)
        {

            int stepEnemy = (_fullPath.Count - 2) / 3;
            int secondStep = stepEnemy;

            for (int i = 0; i < _fullPath.Count; i++)
            {
                if (enemyCounter != 0)
                {                 
                    Instantiate(_enemyCube, _fullPath[stepEnemy], transform.rotation);
                    enemyCounter--;
                    stepEnemy += secondStep;

                }

                if (_fullPath.Count == i + 1)
                {
                    Instantiate(_finishCube, new Vector3(_fullPath[i].x + 0.5f, _fullPath[i].y + 0.5f), transform.rotation);
                }
            }
        }
    }
    #endregion
}
