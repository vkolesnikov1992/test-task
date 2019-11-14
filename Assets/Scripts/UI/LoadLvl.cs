using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    
   public void LoadLevel(bool loadLevel)
    {
        SceneManager.LoadScene(1);
        GameController.isPaused = loadLevel;
        GameController.finish = false;
    }
}
