using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(0);
    }

    public static void LoadGameOver()
    {
        //SceneManager.LoadScene(2);
    }
}
