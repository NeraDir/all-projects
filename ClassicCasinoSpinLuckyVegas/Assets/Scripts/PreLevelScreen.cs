using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _wheelScreen;

    [SerializeField]
    private Text _showCurrentLevel;

    private void Start()
    {
        _showCurrentLevel.text = "LVL " + GameController.currentLevel.ToString();
    }

    public void OnEnd() 
    {
        _wheelScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
