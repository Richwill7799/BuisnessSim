using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResizeGraph : MonoBehaviour
{
    public Image graph;
    public Button button;
    private bool isBig = false;
    private Vector3 bigSizeImage; //position of image in big
    private Vector3 bigSizeButton; //position of button in big

    private Vector3 normalSizeImage; //position of image in normal
    private Vector3 normalSizeButton; //position of button in normal

    private void Start()
    {
        normalSizeButton = button.GetComponent<RectTransform>().anchoredPosition;
        normalSizeImage = graph.rectTransform.anchoredPosition;
        bigSizeImage = new Vector3(713,-453,0);
        bigSizeButton = new Vector3(276, -211.7f, 0);
    }

    public void Resize()
    {

        if (isBig) //scale to normal size == 1
        {
            graph.rectTransform.anchoredPosition = normalSizeImage;
            graph.transform.localScale = new Vector3(1,1,1);
            button.GetComponent<RectTransform>().anchoredPosition = normalSizeButton;
            button.GetComponentInChildren<Text>().text = "Enlarge";
            isBig = false;
        }
        else //scale to big size == 2
        {
            graph.rectTransform.anchoredPosition = bigSizeImage;
            graph.transform.localScale = new Vector3(2, 2, 1);
            button.GetComponent<RectTransform>().anchoredPosition = bigSizeButton;
            button.GetComponentInChildren<Text>().text = "Reduce";
            isBig = true;
        }
    }

}
