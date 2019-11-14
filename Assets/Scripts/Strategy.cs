
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private IStrategy _strategy;

    public Context(IStrategy strategy)
    {
       
        this._strategy = strategy;
    }

    public Context()
    {

    }
    

    public void SetStrategy(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public int Random(int number)
    {
        int result;        
        return result = this._strategy.GetRandom(number);
    }

    public float FloatRandom(float number)
    {
        float result;
        return result = this._strategy.GetFloatRandom(number);
    }
   



}

public interface IStrategy
{
    int GetRandom(int number);
    float GetFloatRandom(float number);
}

class GetHeight : IStrategy
{
    public int GetRandom(int height)
    {
        int result = Random.Range(1, height - 3);
        return result;


    }

    public float GetFloatRandom(float height)
    {
        float result = Random.Range(1, height - 2);
        return result;


    }
}

class GetSize : IStrategy
{
    public int GetRandom(int weight)
    {        
        return 0;
    }

    public float GetFloatRandom(float weight)
    {
        float result;
        if (weight == 5)
        {
           return result = 1.0f;

        }
        if(weight == 4)
        {
            result = Random.Range(0.8f, 0.9f);
            return result;
        }
        if (weight == 3)
        {
            result = Random.Range(0.7f, 0.8f);
            return result;
        }
        if(weight == 2)
        {
            result = Random.Range(0.6f, 0.7f);
            return result;
        }
        if (weight == 1)
        {
            result = Random.Range(0.5f, 0.6f);
            return result;
        }
        
        return 0;
    }
}


class GetLives : IStrategy
{
    public int GetRandom(int weight)
    {
        int result;
        if (weight == 5)
        {
            return result = 3;

        }
        if (weight == 4)
        {
            result = Random.Range(2, 4);
            return result;
        }
        if (weight == 3)
        {
            result = 2;
            return result;
        }
        if (weight == 2)
        {
            result = Random.Range(1, 3);
            return result;
        }
        if (weight == 1)
        {            
            return result = 1;
        }

        return 0;
    }

    public float GetFloatRandom(float height)
    {
        
        return 0;


    }
}

