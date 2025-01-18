using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostClickObject : MonoBehaviour
{
    public Color beginColor;
    public Color endColor;

    private SpriteRenderer myImage;

    public Sprite[] crystals;

    public SpriteRenderer myCrystalImage;

    public float value = 0;

    public GameObject good;
    public GameObject bad;
    public GameObject perfect;

    public AudioClip clickSound;

    public AudioSource clickSoundPlayer;

    private bool isDestroyed;

    private IEnumerator Start()
    {
        myImage = GetComponent<SpriteRenderer>();
        myCrystalImage.sprite = crystals[Random.Range(0, crystals.Length)];
        myImage.color = beginColor;
        while (value != 100)
        {
            value = Mathf.MoveTowards(value, 100, LostGameConfig.clickObjectSpeedValueChanger * Time.deltaTime);
            myImage.color = Color.Lerp(myImage.color, endColor,LostGameConfig.clickObjectChangeColorSpeed * Time.deltaTime);
            yield return null;
        }
        if (!isDestroyed)
        {
            LostSpawnClickObjects.totalValue -= 10;
            LostSpawnClickObjects.comboCount = 0;
            bad.SetActive(gameObject);
        }
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        if (isDestroyed)
        {
            return;
        }
        isDestroyed = true;
        if (value < 40)
        {
            bad.SetActive(true);
            LostSpawnClickObjects.comboCount = 0;
        }
        else if(value > 40 && value < 80)
        {
            good.SetActive(gameObject);
            LostSpawnClickObjects.comboCount += 1;
        }
        else if (value > 80)
        {
            perfect.SetActive(gameObject);
            LostSpawnClickObjects.comboCount += 1;
        }
        LostSpawnClickObjects.totalValue += (int)value;
        LostSpawnClickObjects.countDestroyedCircles += 1;
        
        clickSoundPlayer.PlayOneShot(clickSound);
        myImage.enabled = false;
        myCrystalImage.enabled = false;
        Destroy(gameObject,1);
    }
}
