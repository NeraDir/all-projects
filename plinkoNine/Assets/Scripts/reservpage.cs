using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reservpage : MonoBehaviour
{
    [SerializeField]
    private Button[] _reservButtons;

    [SerializeField]
    private Image[] _reservImages;

    private void Start()
    {
        int rndCount = Random.Range(1, _reservButtons.Length);
        for (int i = 0; i < rndCount; i++)
        {
            int rndIndex = Random.Range(0, _reservButtons.Length);
            _reservButtons[rndIndex].interactable = false;
            _reservImages[rndIndex].gameObject.SetActive(true);
        }
    }
}
