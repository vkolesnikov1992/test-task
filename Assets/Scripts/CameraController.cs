using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    private Transform _player;  
        
    void Update()
    {
        if(_player == null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.5f, transform.position.z);
    }
}
