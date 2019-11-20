using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charapter : Cube
{

    #region Private Fields    
    private bool isGrounded;    
    private int _startCountLives;
    #endregion

    #region Public Fields
    public float Forse;
    public float JumpForse; 
    public static int lives;
    #endregion

    #region Start and FixedUpdate
    
    void Start()
    {        
        Context context = new Context();        

        context.SetStrategy(new GetLives());        
        lives = context.Random(weight);        
        _startCountLives = lives;        
    }

    
    void FixedUpdate()
    {
        
        RaycastHit2D isGround = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 - 0.01f), Vector2.down, 0.01f);
        float moveHorizontal = Input.GetAxis("Horizontal");
        rigidbody.AddForce(new Vector2(moveHorizontal * Forse, rigidbody.velocity.y));        
        if(isGround.collider != null)
        {
            if (isGround.collider.tag == "defaultCube" && Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(Vector2.up * JumpForse);
            }
        }
           
             
        

        if (GameController.restart)
        {           
            transform.position = startPos;
            lives = _startCountLives;
            rigidbody.velocity = Vector3.zero;
        }
        
    }
    #endregion

    #region Private Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemyCube")
        {
            lives--;            
            
            if (lives == 0)
            {
                GameController.finish = true;
                GameController.restart = true;
            }

        }

        if (collision.collider.tag == "lavaCube")
        {
           GameController.finish = true;
           GameController.restart = true;            
        }                    
    }
    #endregion
}
