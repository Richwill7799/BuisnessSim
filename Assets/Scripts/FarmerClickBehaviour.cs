using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerClickBehaviour : MonoBehaviour
{
    public GameObject field;
    private List<Farmer> farmers = new List<Farmer>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();
    }

    void OnMouseDown()
    {   
        if (field.activeSelf)
        {
            field.SetActive(false);
        }
        else
        {
            field.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
