using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public void RestartLvl()
    {
        GameController.restart = true;
        GameController.finish = false;
    }
}
