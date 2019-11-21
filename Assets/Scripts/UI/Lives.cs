using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
   

    void Update()
    {    
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i < Сharacter.lives)
            {
                transform.GetChild(i).GetComponent<Image>().enabled = true;
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
        }
       
        
        
    }
}
