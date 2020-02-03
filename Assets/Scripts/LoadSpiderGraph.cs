using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpiderGraph : MonoBehaviour
{
    public Image spider;

    private string folderPath;
    private string[] filePaths;
    private void Start()
    {
        ImageLoader();
    }

    void ImageLoader()
    {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
        filePaths = Directory.GetFiles(folderPath, "*.png"); // Get all files of type .png in this folder

        //Converts desired path into byte array

        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[1]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(200, 200);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));


        //Assigns the UI sprite
        //spider.rectTransform.sizeDelta = fromTex.bounds.size;
        spider.sprite = fromTex;
    }

}
