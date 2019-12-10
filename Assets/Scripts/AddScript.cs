using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddScript : MonoBehaviour
{

    public Image image;
    public Canvas canvas;

    private int offsetDown = -20;
    private int offsetUp = 20;


    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>(); //todo: this list in Scene Info Object therefore simulations get all Data out of it
    private List<Button> buttons = new List<Button>();

    public void AddClick()
    {
        GameObject go = new GameObject("New Button via Script");
        Image image = go.AddComponent<Image>();
        image = this.image;
        Button button = go.AddComponent<Button>();
        go.transform.SetParent(canvas.transform, false);
        button.transform.position = new Vector3();//todo: set position of button
        image.rectTransform.sizeDelta = new Vector2(130, 25); //todo: size to correct size
        ColorBlock cb = button.colors;
        cb.normalColor = Color.cyan; //todo: change colors
        button.colors = cb;
    }
    //TODO: instaniate button for single famrer and coop famrer(both same color) tranform.position +/- offset
    //TODO: chnage color of button 
    //TODO: If 1 or more button are created activate delete button
    //TODO: create/save farmers values (variant, team, color etc.)
}
