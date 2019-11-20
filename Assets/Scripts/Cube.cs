using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    protected int weight;
    protected float size;
    protected Rigidbody2D rigidbody;
    protected Transform transform;
    protected Vector3 startPos;
    
    protected void Awake()
    {
        Context context = new Context();
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        weight = Random.Range(1, 6);
        rigidbody.mass = weight;

        context.SetStrategy(new GetSize());
        size = context.FloatRandom(weight);
        transform.localScale = new Vector3(size, size);

        startPos = transform.position;

    }
    
}
