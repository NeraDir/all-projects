using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int ID;
    [SerializeField] private Image thisImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
    }

    public void SetID (int _ID)
    {
        ID = _ID;
    }

    public void Clicked ()
    {
        Camera.main.GetComponent<GameHandler>().CardClicked(this);
    }

    public void Close ()
    {
        thisImage.sprite = defaultSprite;
    }

    public void Open()
    {
        thisImage.sprite = sprites[ID];
    }
}
