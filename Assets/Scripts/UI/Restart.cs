using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public void RestartLvl(bool restart)
    {

        GameController.restart = true;
        GameController.finish = false;
    }
}
