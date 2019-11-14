using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
   
    private Transform player;

    

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }


        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, transform.position.z);
    }
}
