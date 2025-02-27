using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRocks : MonoBehaviour
{
    public int rocksCount;

    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private Text _resultTxt;
    [SerializeField] private GameObject _nextButton;


    private void LateUpdate()
    {
        if (rocksCount >= 70 && !_resultScreen.activeInHierarchy)
        {
            _resultScreen.SetActive(true);
            _resultTxt.text = "COMPLETED";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RockComponent rock))
        {
            rocksCount += 1;
        }
        if (other.TryGetComponent(out SnakeComponent snake))
        {
            if (_resultScreen.activeInHierarchy)
                return;
            _resultScreen.SetActive(true);
            _nextButton.SetActive(false);
            _resultTxt.text = "NOT COMPLETED";
        }
    }
}
