using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedGame : MonoBehaviour
{
    
   public void pasedgame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
