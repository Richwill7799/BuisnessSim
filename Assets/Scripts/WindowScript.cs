using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WindowScript : MonoBehaviour
{
    public FarmerMovementScript mv;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void editWeather(int type)
    {
        //  int i = this.gameObject.GetComponent<FarmerMovementScript>().id;
        int id = mv.id;
        Farmer farmer = mv.s.farmers[id];
        UnityEngine.Debug.Log("bauer mit id "+id+ "geklickt ");
        switch (type)
        {
            case 1:
                //goblin
                break;
            case 2:
                //storm
                break;
            case 3:
                //rain
                break;
            case 4:
                //cloud
                break;
            case 5:
                //sun
                break;
            default:
                break;
        }
    }
    public void RemoveWindow()
    {
        this.gameObject.SetActive(false);
    }
}
