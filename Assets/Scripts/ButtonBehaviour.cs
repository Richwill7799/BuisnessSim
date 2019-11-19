using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public static Button button;

    // Start is called before the first frame update
    public void Start()
    {
        /*Button click = button.GetComponent<Button>();
        click.onClick.AddListener(StartLoad);
        click.onClick.AddListener(PrototypeLoad);
        click.onClick.AddListener(Load);
        click.onClick.AddListener(StartLoad);
        click.onClick.AddListener(StartLoad);*/
    }


    public void StartLoad()
    {
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
}
