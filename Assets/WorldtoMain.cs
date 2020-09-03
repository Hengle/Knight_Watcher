using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldtoMain : MonoBehaviour
{
   public void ToMain()
    {
        SceneManager.LoadScene(1);
    }
    public void ToWorld()
    {
        SceneManager.LoadScene(0);
    }
}
