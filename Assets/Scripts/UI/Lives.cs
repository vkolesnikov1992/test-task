using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
   
    void Update()
    {       
        if(Charapter.lives == 1)
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            transform.GetChild(2).GetComponent<Image>().enabled = false;
        }

        if (Charapter.lives == 2)
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(2).GetComponent<Image>().enabled = false;
        }

        if (Charapter.lives == 3)
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(2).GetComponent<Image>().enabled = true;
        }        
    }
}
