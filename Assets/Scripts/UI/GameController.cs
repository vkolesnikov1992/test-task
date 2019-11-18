using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Public Fields
    public static bool isPaused;
    public static bool restart;
    public static bool finish;    

    public GameObject pauseMenuCanvas;
    public GameObject finishMenuCanvas;
    #endregion

    #region Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            isPaused = true;       
        }


        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }       
        
                

        if (finish)
        {
            finishMenuCanvas.SetActive(true);
            Time.timeScale = 0.001f;
        }
        else
        {
            finishMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
               
    }
    #endregion

}
