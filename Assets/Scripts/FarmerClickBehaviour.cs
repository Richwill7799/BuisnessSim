using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerClickBehaviour : MonoBehaviour
{
    public GameObject field;
    public GameObject panel;
    private List<Farmer> farmers = new List<Farmer>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();
        
    }

    void OnMouseDown()
    {   
        if (panel.activeSelf)
        {
            //field.SetActive(false);
            panel.SetActive(false);

        }
        else
        {
            //field.SetActive(true);
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
