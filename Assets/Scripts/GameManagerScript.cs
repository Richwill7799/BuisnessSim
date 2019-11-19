using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    float[] corn;
    float[] multiplier;
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
        
        multiplier = new float[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            PassYear();
            year++;
            Year.text = "Year " + year;
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            /*Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex+1);*/
            SceneManager.LoadScene("Analysis");
        }
    }
    //TODO de hardcode after demo ?
    private void PassYear() {
        multiplier[0] = Random.Range(0.001f , 3.0f);
        multiplier[1] = Random.Range(0.001f , 3.0f);

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
            while (tempCorn > 11) {
                scaling += 0.5f;
                tempCorn /= 10;
            }
            cornPrefab.transform.localScale *= Mathf.Min(scaling, 2);
            for(int j = 0; j<tempCorn; j++) {
                Instantiate(cornPrefab,feldpos[i].position+new Vector3(Random.Range(-2f,2f), Random.Range(-1f,1f), 1), Quaternion.identity);
            }
            cornPrefab.transform.localScale = new Vector3(1, 1, 1);
            scaling = 0.25f;
        }
    }

    
}
