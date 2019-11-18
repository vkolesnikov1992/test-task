using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Private Fields
    private int weight;
    private float size;
    private Rigidbody2D rigidbody;
    private Transform transform;
    private Ray2D ray;
    private bool isMove;
    public int speed = 1;
    private bool isRightMove = true;
    private Vector3 startPos;
    #endregion

    #region Start and FixedUpdate
    void Start()
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

    
    private void FixedUpdate()
    {

        Movement();
        if (GameController.restart)
        {
            transform.position = startPos;
            GameController.restart = false;
        }

        
    }
    #endregion

    #region Private Methods
    void Movement()
    {
           
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2 + 0.01f, transform.position.y), Vector2.right, 0.01f);
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2 - 0.01f, transform.position.y), -Vector2.right, 0.01f);
        

        if (leftHit.collider != null)
        {
            if(leftHit.collider.tag == "defaultCube" || leftHit.collider.tag == "enemyCube")
            {
                isRightMove = true;
            }
            
        }

        if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "defaultCube" || rightHit.collider.tag == "enemyCube")
            {
                isRightMove = false;
            }
        }

        if (isMove)
        {
            if (isRightMove)
            {


                rigidbody.AddForce(Vector2.right * speed);

            }
            if (!isRightMove)
            {


                rigidbody.AddForce(Vector2.left * speed);
            }

        }

    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {        
        
        isMove = true;            
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMove = false;
       
    }
    #endregion
}
