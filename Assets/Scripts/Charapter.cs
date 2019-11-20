using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charapter : MonoBehaviour
{

    #region Private Fields
    [SerializeField]
    private int _weight;
    private float _size;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private bool isGrounded;
    private Vector3 _startPos;
    private int _startCountLives;
    #endregion

    #region Public Fields
    public float Forse;
    public float JumpForse;    
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    public static int lives;
    #endregion

    #region Start and FixedUpdate
    void Start()
    {
        Context context = new Context();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        context.SetStrategy(new GetLives());
        _weight = Random.Range(1, 6);
        _rigidbody.mass = _weight;
        lives = context.Random(_weight);

        context.SetStrategy(new GetSize());
        _size = context.FloatRandom(_weight);
        transform.localScale = new Vector3(_size, _size);

        _startPos = transform.position;      
    }

    
    void FixedUpdate()
    {       
        float moveHorizontal = Input.GetAxis("Horizontal");
        _rigidbody.AddForce(new Vector2(moveHorizontal * Forse, _rigidbody.velocity.y));       
        isGrounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, WhatIsGround);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(Vector2.up * JumpForse);            
        }      
             
        

        if (GameController.restart)
        {           
            transform.position = _startPos;
            lives = _startCountLives;
            _rigidbody.velocity = Vector3.zero;
        }
        
    }
    #endregion

    #region Private Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemyCube")
        {
            if(lives != 0)
            {
                lives--;
            }           
            
            else if (lives == 0)
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
