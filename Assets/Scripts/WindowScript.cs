using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WindowScript : MonoBehaviour
{
    public FarmerMovementScript mv;
    public Image field;
    public Image weather;
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
                field.sprite = Resources.Load<Sprite>("field_deer_clean");
                weather.sprite = Resources.Load<Sprite>("skybox_sundown");
                break;
            case 2:
                field.sprite = Resources.Load<Sprite>("field_sowed_clean");
                weather.sprite = Resources.Load<Sprite>("skybox_storm");
                //storm
                break;
            case 3:
                field.sprite = Resources.Load<Sprite>("field_sowed_clean");
                weather.sprite = Resources.Load<Sprite>("skybox_clouds");
                //rain
                break;
            case 4:
                field.sprite = Resources.Load<Sprite>("field_full_clean");
                weather.sprite = Resources.Load<Sprite>("skybox_clouds");
                //cloud
                break;
            case 5:
                field.sprite = Resources.Load<Sprite>("field_full_clean");
                weather.sprite = Resources.Load<Sprite>("skybox_sun");
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
