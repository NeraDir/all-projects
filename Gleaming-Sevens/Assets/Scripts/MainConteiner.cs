using UnityEngine.UI;
using UnityEngine;

public class MainConteiner : MonoBehaviour
{
    public int index;

    public float price;

    public Sprite[] itemIcons;

    public Image curentImage;

    public void INIT() 
    {
        index = Random.Range(0, itemIcons.Length);
        curentImage.sprite = itemIcons[index];
        price = (LoopIterController.betVar * (index + 1)) / 3;
    }
}
