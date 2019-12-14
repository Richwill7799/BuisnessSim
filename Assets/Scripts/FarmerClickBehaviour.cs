using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerClickBehaviour : MonoBehaviour
{
    public GameObject field;

    // Start is called before the first frame update
    void Start()
    {
        
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
