using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    
   public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        GameController.isPaused = false;
        GameController.finish = false;
    }
}
