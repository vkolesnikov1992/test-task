using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charapter : MonoBehaviour
{
    [SerializeField]
    public static int lives;
    [SerializeField]
    private int weight;
    private float size;
    private Rigidbody2D rigidbody;
    private Transform transform;

    public float forse;
    public float jumpForse;
    private bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Vector3 startPos;
    private int startCountLives;

    



    // Start is called before the first frame update
    void Start()
    {
        Context context = new Context();
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        context.SetStrategy(new GetLives());
        weight = Random.Range(1, 6);
        rigidbody.mass = weight;
        lives = context.Random(weight);

        context.SetStrategy(new GetSize());
        size = context.FloatRandom(weight);
        transform.localScale = new Vector3(size, size);

        startPos = transform.position;

        




    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        float moveHorizontal = Input.GetAxis("Horizontal");

        rigidbody.AddForce(new Vector2(moveHorizontal * forse, rigidbody.velocity.y));
        Debug.Log(moveHorizontal * forse + "  " + rigidbody.velocity);
        

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector2.up * jumpForse);            
        }      
        
       
        

        if (GameController.restart)
        {           
            transform.position = startPos;
            lives = startCountLives;
            rigidbody.velocity = Vector3.zero;
        }
        
    }
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
    

    

}
