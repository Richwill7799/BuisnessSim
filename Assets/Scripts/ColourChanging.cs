using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanging : MonoBehaviour
{
    private List<Farmer> farmers = new List<Farmer>();

    private SpriteRenderer renderer;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();

        renderer = GetComponent<SpriteRenderer>();
        //renderer.color = Color.blue;
        renderer.material.SetColor("_Color", Color.red);

        //How do I identify farmer from list?
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
