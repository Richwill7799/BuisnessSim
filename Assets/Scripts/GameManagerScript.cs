using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    float[] corn;
    int[] multiplier;
    int year;

    public Text[] Scores;
    public Text[] Mults;
    public Text Year;

    public Transform[] feldpos;

    public GameObject cornPrefab;

    // Start is called before the first frame update
    void Start()
    {
        corn = new float[4] {1,1,1,1 };
        
        multiplier = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            PassYear();
            year++;
            Year.text = "Year " + year;
        }
    }

    private void PassYear() {
        multiplier[0] = (int)Random.Range(1, 5);
        multiplier[1] = (int)Random.Range(1, 5);

        corn[0] = corn[0] * multiplier[0];
        
        float temp = corn[1] * multiplier[0]+ corn[2] * multiplier[1];
        corn[1] = temp / 2;
        corn[2] = temp / 2;

        corn[3] = corn[3] * multiplier[1];
        
        Debug.Log("Corn: "+corn[0]+" | "+corn[1]+" | "+corn[2]+" | "+corn[3]+" with multipliers: "+multiplier[0]+" | "+multiplier[1]);
        for(int i = 0; i<4; i++) {
            Scores[i].text = "Score " + (i + 1) + ": " + corn[i];
        }
        Mults[0].text = "Multiplier: " + multiplier[0];
        Mults[1].text = "Multiplier: " + multiplier[1];

        float scaling = 0.25f;
        float tempCorn = 1;
        for(int i = 0; i<4; i++) {
            tempCorn = corn[i];
            while (tempCorn > 21) {
                scaling += 0.5f;
                tempCorn -= 20;
            }
            cornPrefab.transform.localScale *= Mathf.Min(scaling, 2);
            for(int j = 0; j<tempCorn; j++) {
                Instantiate(cornPrefab,feldpos[i].position+new Vector3(Random.Range(-2f,2f), Random.Range(-1f,1f), 1), Quaternion.identity);
            }
            cornPrefab.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    
}
