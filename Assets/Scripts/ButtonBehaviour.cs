using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public void StartLoad(){
        Destroy(GameObject.Find("GameManagerObj"));
        Destroy(GameObject.Find("SceneInfoObj"));


        //foreach (DontDestroyOnLoad gameobj in GameObject.FindObjectsOfType<DontDestroyOnLoad>()) {
        //    Destroy(gameobj);
        //}
        SceneManager.LoadScene(0);
    }

    public void PrototypeLoad()
    {
        SceneManager.LoadScene(1);
    }

    public void AnalysisLoad()
    {
        SceneManager.LoadScene(2);
    }

    public void EndLoad()
    {
        SceneManager.LoadScene(3);
    }

    public void ComparisonLoad()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
